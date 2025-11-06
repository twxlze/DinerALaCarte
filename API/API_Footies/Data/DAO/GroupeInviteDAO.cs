using API_Footies.Data.Interfaces;
using API_Footies.Metier;
using System.Data;

namespace API_Footies.Data.DAO
{
    /// <summary>
    /// DAO en charge de la gestion des groupes d'invités
    /// </summary>
    public class GroupeInviteDAO : IGroupeInviteDAO
    {
        public GroupeInvites AjouterGroupeInvite(GroupeInvites groupeInvites)
        {
            //On commence par créer le groupe dans la table des GroupesInvite en récupérant son ID
            using (SQLiteConnector connection = new SQLiteConnector())
            {
                var parameters = new Dictionary<string, object>()
                {
                    {"@Nom", groupeInvites.Nom }
                };
                groupeInvites.IdGroupeInvites = connection.ExecuteInsert("INSERT INTO GroupeInvite (Nom) VALUES (@Nom)", parameters);
            }

            //On place les Invite
            /*
            using (SQLiteConnector connection = new SQLiteConnector())
            {
                foreach (Invite invite in groupeInvites.Invites)
                {
                    var parameters = new Dictionary<string, object>()
                    {
                        {"@IdGroupeInvites",groupeInvites.IdGroupeInvites},
                        {"@Id",invite.Id }
                    };
                    connection.ExecuteQuery("INSERT INTO Invite_Groupe (IDGroupeInvite,IDInvite) VALUES (@IdGroupeInvites,@Id)", parameters);
                }
            }*/
            return groupeInvites;
        }

        public GroupeInvites AjouterInviteAuGroupe(long idGroupeInvites, Invite invite)
        {
            // Ajouter l'invité au groupe dans la table de liaison
            using (SQLiteConnector connection = new SQLiteConnector())
            {
                var parameters = new Dictionary<string, object>()
                {
                    {"@IdGroupeInvites", idGroupeInvites},
                    {"@IdInvite", invite.Id }
                };
                connection.ExecuteQuery("INSERT INTO Invite_Groupe (IDGroupeInvite,IDInvite) VALUES (@IdGroupeInvites,@IdInvite)", parameters);
            }

            // Récupérer le groupe mis à jour
            GroupeInvites groupe = RecupereGroupeViaId(idGroupeInvites);
            if (groupe != null)
            {
                // Ajouter l'invité à la liste en mémoire si ce n'est pas déjà fait
                if (!groupe.Invites.Any(i => i.Id == invite.Id))
                {
                    groupe.Invites.Add(invite);
                }
            }
            return groupe;
        }

        public GroupeInvites RecupereGroupeViaId(long idGroupeInvite)
        {
            GroupeInvites groupeInvite = new GroupeInvites();
            using (SQLiteConnector connection = new SQLiteConnector())
            {
                var parameters = new Dictionary<string, object>()
                {
                    {"@IdGroupeInvites",idGroupeInvite }
                };
                var data = connection.ExecuteQuery("SELECT GroupeInvite.IDGroupeInvite as Id, GroupeInvite.Nom as Nom FROM GroupeInvite WHERE GroupeInvite.IDGroupeInvite=@IdGroupeInvites", parameters);
                if (data.Rows.Count > 0)
                {
                    groupeInvite.IdGroupeInvites = data.Rows[0].Field<Int64>("Id");
                    groupeInvite.Nom = data.Rows[0].Field<string>("Nom");
                }
            }
            //Récupérer les invités associés au groupe
            if (groupeInvite != null)
            {
                using (SQLiteConnector connection = new SQLiteConnector())
                {
                    var parameters = new Dictionary<string, object>()
                    {
                        {"@IdGroupeInvites",idGroupeInvite }
                    };
                    var data = connection.ExecuteQuery("SELECT Invite.IDInvite as Id, Invite.Prenom as Prenom, Invite.Nom as Nom, Invite.Mail as Email, Invite.NumTel as Telephone FROM Invite INNER JOIN Invite_Groupe ON Invite.IDInvite = Invite_Groupe.IDInvite WHERE Invite_Groupe.IDGroupeInvite=@IdGroupeInvites", parameters);
                    foreach (DataRow row in data.Rows)
                    {
                        Invite invite = new Invite
                        {
                            Id = row.Field<Int64>("Id"),
                            Prenom = row.Field<string>("Prenom"),
                            Nom = row.Field<string>("Nom"),
                            Telephone = row.Field<string>("Telephone"),
                            Email = row.Field<string>("Email")
                        };
                        groupeInvite.Invites.Add(invite);
                    }
                }
            }

            return groupeInvite;
        }

        public List<GroupeInvites> RecupererTousGroupesInvites()
        {
            var groupes = new List<GroupeInvites>();
            using (SQLiteConnector connection = new SQLiteConnector())
            {
                // Récupérer tous les groupes
                var groupesData = connection.ExecuteQuery("SELECT IDGroupeInvite as Id, Nom FROM GroupeInvite");
                foreach (DataRow groupeRow in groupesData.Rows)
                {
                    var groupe = new GroupeInvites
                    {
                        IdGroupeInvites = groupeRow.Field<long>("Id"),
                        Nom = groupeRow.Field<string>("Nom"),
                        Invites = new List<Invite>()
                    };

                    // Récupérer les invités pour ce groupe
                    var parameters = new Dictionary<string, object>
                    {
                        { "@IdGroupeInvites", groupe.IdGroupeInvites }
                    };
                    var invitesData = connection.ExecuteQuery(
                        "SELECT Invite.IDInvite as Id, Invite.Prenom as Prenom, Invite.Nom as Nom, Invite.Mail as Email, Invite.NumTel as Telephone FROM Invite INNER JOIN Invite_Groupe ON Invite.IDInvite = Invite_Groupe.IDInvite WHERE Invite_Groupe.IDGroupeInvite=@IdGroupeInvites",
                        parameters
                    );
                    foreach (DataRow inviteRow in invitesData.Rows)
                    {
                        var invite = new Invite
                        {
                            Id = inviteRow.Field<long>("Id"),
                            Prenom = inviteRow.Field<string>("Prenom"),
                            Nom = inviteRow.Field<string>("Nom"),
                            Telephone = inviteRow.Field<string>("Telephone"),
                            Email = inviteRow.Field<string>("Email")
                        };
                        groupe.Invites.Add(invite);
                    }

                    groupes.Add(groupe);
                }
            }
            return groupes;
        }

        public GroupeInvites ModifierGroupe(GroupeInvites groupeInvite)
        {
            using (SQLiteConnector connection = new SQLiteConnector())
            {
                var parameters = new Dictionary<string, object>()
                {
                    {"@IdGroupeInvites", groupeInvite.IdGroupeInvites },
                    {"@Nom", groupeInvite.Nom }
                };

                connection.ExecuteQuery("UPDATE GroupeInvite SET IDGroupeInvite = @IdGroupeInvites, Nom = @Nom WHERE IDGroupeInvite = @IdGroupeInvites", parameters);
            }
            return groupeInvite;
        }

    }
}
