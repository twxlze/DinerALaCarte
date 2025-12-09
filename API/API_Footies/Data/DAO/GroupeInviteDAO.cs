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
        public bool AjouterGroupeInvites(GroupeInvites groupeInvites, long IdUtilisateur)
        {
            bool ajoute = false;
            using (SQLiteConnector connection = new SQLiteConnector())
            {
                if (connection == null)
                {
                    throw new Exception("Erreur de connexion à la base de données");
                }
                else
                {
                    Dictionary<string, object> parameters = new Dictionary<string, object>()
                    {
                        {"@Nom", groupeInvites.Nom },
                        {"@IdUtilisateur", IdUtilisateur }
                    };


                    groupeInvites.IdGroupeInvites = connection.ExecuteInsert("INSERT INTO GroupeInvite (Nom, IdUtilisateur) VALUES (@Nom, @IdUtilisateur)", parameters);

                    foreach (Invite invite in groupeInvites.Invites)
                    {
                        Dictionary<string, object> parametersInvite = new Dictionary<string, object>()
                        {
                            {"@IdGroupeInvite", groupeInvites.IdGroupeInvites },
                            {"@IdInvite", invite.Id }
                        };
                        connection.ExecuteQuery("INSERT INTO Invite_Groupe (IdInvite, IdGroupeInvite) VALUES (@IdInvite, @IdGroupeInvite)", parametersInvite);
                    }

                    ajoute = true;
                }
            }
            return ajoute;
        }

        public List<GroupeInvites> ListeGroupesInvites(long IdUtilisateur)
        {
            List<GroupeInvites> listeGroupesInvites = new List<GroupeInvites>();

            using (SQLiteConnector connection = new SQLiteConnector())
            {
                if (connection == null)
                {
                    throw new Exception("Erreur de connexion à la base de données");
                }
                else
                {
                    Dictionary<string, object> parameters = new Dictionary<string, object>()
                    {
                        {"@IdUtilisateur", IdUtilisateur }
                    };

                    DataTable dataTable = connection.ExecuteQuery("SELECT * FROM GroupeInvite WHERE IdUtilisateur = @IdUtilisateur", parameters);

                    foreach (DataRow? row in dataTable.Rows)
                    {
                        long idGroupeInvite = (long)row["IdGroupeInvite"];
                        string nom = row["Nom"].ToString();
                        List<Invite> invitesGroupeInvite = new List<Invite>();

                        Dictionary<string, object> parametersGroupeInvite = new Dictionary<string, object>()
                        {
                            {"@IdGroupeInvite", idGroupeInvite }
                        };

                        DataTable dataTableInvites = connection.ExecuteQuery(
                            @"SELECT i.* FROM Invite i
                              INNER JOIN Invite_Groupe ig ON i.IdInvite = ig.IdInvite 
                              WHERE ig.IdGroupeInvite = @IdGroupeInvite",
                            parametersGroupeInvite);

                        foreach (DataRow? rowInvite in dataTableInvites.Rows)
                        {
                            Invite invite = new Invite(
                                (long)rowInvite["IdInvite"],
                                rowInvite["Nom"].ToString(),
                                rowInvite["Prenom"].ToString(),
                                rowInvite["NumTel"].ToString(),
                                rowInvite["Mail"].ToString()
                            );
                            invitesGroupeInvite.Add(invite);
                        }

                        GroupeInvites groupeInvite = new GroupeInvites(idGroupeInvite, nom, invitesGroupeInvite);
                        listeGroupesInvites.Add(groupeInvite);
                    }
                }
            }

            return listeGroupesInvites;
        }

        public bool ModifierGroupe(GroupeInvites groupeInvite, long IdUtilisateur)
        {
            bool modifie = false;
            using (SQLiteConnector connection = new SQLiteConnector())
            {
                if (connection == null)
                {
                    throw new Exception("Erreur de connexion à la base de données");
                }
                else
                {
                    Dictionary<string, object> parameters = new Dictionary<string, object>()
                    {
                        {"@IdGroupeInvite", groupeInvite.IdGroupeInvites },
                        {"@Nom", groupeInvite.Nom },
                        {"@IdUtilisateur", IdUtilisateur }
                    };

                    connection.ExecuteQuery("UPDATE GroupeInvite SET Nom = @Nom WHERE IdGroupeInvite = @IdGroupeInvite AND IdUtilisateur = @IdUtilisateur", parameters);

                    Dictionary<string, object> parametersDelete = new Dictionary<string, object>()
                    {
                        {"@IdGroupeInvite", groupeInvite.IdGroupeInvites }
                    };
                    connection.ExecuteQuery("DELETE FROM Invite_Groupe WHERE IdGroupeInvite = @IdGroupeInvite", parametersDelete);

                    foreach (Invite invite in groupeInvite.Invites)
                    {
                        Dictionary<string, object> parametersInvite = new Dictionary<string, object>()
                        {
                            {"@IdGroupeInvite", groupeInvite.IdGroupeInvites },
                            {"@IdInvite", invite.Id }
                        };
                        connection.ExecuteQuery("INSERT INTO Invite_Groupe (IdInvite, IdGroupeInvite) VALUES (@IdInvite, @IdGroupeInvite)", parametersInvite);
                    }

                    modifie = true;
                }
            }
            return modifie;
        }

        public void SupprimerGroupeInvite(long idGroupeInvite, long IdUtilisateur)
        {
            using (SQLiteConnector connection = new SQLiteConnector())
            {
                if (connection == null)
                {
                    throw new Exception("Erreur de connexion à la base de données");
                }
                else
                {
                    Dictionary<string, object> parametersLiaison = new Dictionary<string, object>()
                    {
                        {"@IdGroupeInvite", idGroupeInvite }
                    };
                    connection.ExecuteQuery("DELETE FROM Invite_Groupe WHERE IdGroupeInvite = @IdGroupeInvite", parametersLiaison);

                    Dictionary<string, object> parameters = new Dictionary<string, object>()
                    {
                        {"@IdGroupeInvite", idGroupeInvite },
                        {"@IdUtilisateur", IdUtilisateur }
                    };

                    connection.ExecuteQuery("DELETE FROM GroupeInvite WHERE IdGroupeInvite = @IdGroupeInvite AND IdUtilisateur = @IdUtilisateur", parameters);
                }
            }
        }

        public List<GroupeInvites> ChercherGroupeInvites(string GroupeInvitesRechercher, long IdUtilisateur)
        {
            List<GroupeInvites> listeGroupeInvites = new List<GroupeInvites>();
            using (SQLiteConnector connection = new SQLiteConnector())
            {
                if (connection == null)
                {
                    throw new Exception("Erreur de connexion à la base de données");
                }
                else
                {
                    Dictionary<string, object> parameters = new Dictionary<string, object>()
                    {
                        {"@Texte", $"%{GroupeInvitesRechercher}%" },
                        {"@IdUtilisateur", IdUtilisateur }
                    };


                    DataTable dataTable = connection.ExecuteQuery("SELECT * FROM GroupeInvite WHERE Nom LIKE @Texte AND IdUtilisateur = @IdUtilisateur", parameters);

                    foreach (DataRow? row in dataTable.Rows)
                    {
                        long idGroupeInvite = (long)row["IdGroupeInvite"];
                        string nom = row["Nom"].ToString();
                        List<Invite> invitesGroupeInvite = new List<Invite>();

                        Dictionary<string, object> parametersGroupeInvite = new Dictionary<string, object>()
                        {
                            {"@IdGroupeInvite", idGroupeInvite }
                        };

                        DataTable dataTableInvites = connection.ExecuteQuery(
                                @"SELECT i.* FROM Invite i
                                  INNER JOIN Invite_Groupe ig ON i.IdInvite = ig.IdInvite 
                                  WHERE ig.IdGroupeInvite = @IdGroupeInvite",
                                parametersGroupeInvite);

                        foreach (DataRow? rowInvite in dataTableInvites.Rows)
                        {
                            Invite invite = new Invite(
                                (long)rowInvite["IdInvite"],
                                rowInvite["Nom"].ToString(),
                                rowInvite["Prenom"].ToString(),
                                rowInvite["NumTel"].ToString(),
                                rowInvite["Mail"].ToString()
                            );
                            invitesGroupeInvite.Add(invite);
                        }
                        GroupeInvites groupe = new GroupeInvites(idGroupeInvite, nom, invitesGroupeInvite);
                        listeGroupeInvites.Add(groupe);
                    }
                }
            }
            return listeGroupeInvites;
        }
    }
}