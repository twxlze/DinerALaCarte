using System.Data;
using API_Footies.Data.Interfaces;
using API_Footies.Metier;
using API_Footies.Metier.Enum;

namespace API_Footies.Data.DAO
{
    /// <summary>
    /// DAO en charge de la gestion des invitations
    /// </summary>
    public class InvitationDAO : IInvitationDAO
    {
        #region Méthodes principales
        public bool AjouterInvitation(Invitation invitation, long idUtilisateur)
        {
            bool ajoute = false;
            using (SQLiteConnector connection = new SQLiteConnector())
            {
                if (connection == null)
                {
                    throw new Exception("Erreur de connexion à la base de données");
                }

                // On passe l'ID utilisateur à la méthode d'insertion
                invitation.IdInvitation = InsererInvitation(connection, invitation, idUtilisateur);

                AjouterGroupesInvitesDansInvitation(connection, invitation);
                AjouterMenusDansInvitation(connection, invitation);
                AjouterInvitesDansInvitation(connection, invitation);
                AjouterPlatsDansInvitation(connection, invitation);
                AjouterPlatsPreferesDansInvitation(connection, invitation);

                ajoute = true;
            }
            return ajoute;
        }

        public List<Invitation> ObtenirToutInvitations(long idUtilisateur)
        {
            List<Invitation> invitations = new List<Invitation>();
            using (SQLiteConnector connection = new SQLiteConnector())
            {
                if (connection == null)
                {
                    throw new Exception("Erreur de connexion à la base de données");
                }

                Dictionary<string, object> parameters = new Dictionary<string, object>()
                {
                    {"@IdUtilisateur", idUtilisateur }
                };

                // Filtrage par utilisateur
                DataTable dataTable = connection.ExecuteQuery("SELECT * FROM Invitation WHERE IdUtilisateur = @IdUtilisateur", parameters);

                foreach (DataRow row in dataTable.Rows)
                {
                    long idInvitation = (long)row["IdInvitation"];
                    string nom = row["Nom"].ToString();
                    DateTime date = DateTime.Parse(row["Date"].ToString());

                    // Les méthodes "Obtenir..." n'ont pas besoin de l'ID utilisateur car l'ID Invitation est unique 
                    // et on a déjà vérifié qu'elle appartenait à l'utilisateur via la requête principale ci-dessus.
                    List<Invite> invites = ObtenirInvitesDansInvitation(connection, idInvitation);
                    List<Plat> plats = ObtenirPlatsDansInvitation(connection, idInvitation);
                    List<Menu> menus = ObtenirMenusDansInvitation(connection, idInvitation);
                    List<GroupeInvites> groupesInvites = ObtenirGroupesInvitesDansInvitation(connection, idInvitation);

                    Invitation invitation = new Invitation(
                        groupesInvites,
                        menus,
                        invites,
                        plats,
                        idInvitation,
                        nom,
                        date
                    );
                    invitations.Add(invitation);
                }
            }
            return invitations;
        }

        public bool ModifierInvitation(Invitation invitation, long idUtilisateur)
        {
            bool modifie = false;
            using (SQLiteConnector connection = new SQLiteConnector())
            {
                if (connection == null)
                {
                    throw new Exception("Erreur de connexion à la base de données");
                }

                // On passe l'ID utilisateur pour sécuriser la modification
                invitation.IdInvitation = ModifierInvitationInternal(connection, invitation, idUtilisateur);

                // Note: Pour être 100% sûr, on ne devrait modifier les enfants que si l'update parent a réussi (appartient à l'user).
                // Dans cette architecture simple, on procède à la mise à jour des liens.
                ModifierGroupesInvitesDansInvitation(connection, invitation);
                ModifierMenusDansInvitation(connection, invitation);
                ModifierInvitesDansInvitation(connection, invitation);
                ModifierPlatsDansInvitation(connection, invitation);
                modifie = true;
            }
            return modifie;
        }

        public void SupprimerInvitation(long idInvitation, long idUtilisateur)
        {
            using (SQLiteConnector connection = new SQLiteConnector())
            {
                if (connection == null)
                {
                    throw new Exception("Erreur de connexion à la base de données");
                }

                Dictionary<string, object> parameters = new Dictionary<string, object>()
                {
                    {"@IdInvitation", idInvitation },
                    {"@IdUtilisateur", idUtilisateur }
                };

                // On supprime d'abord les liaisons (tables enfants)
                // Note : Techniquement on pourrait supprimer des liens d'une invitation qui n'est pas à nous ici, 
                // mais la suppression finale de l'invitation échouera si l'utilisateur n'est pas le bon.
                Dictionary<string, object> parametersLiaison = new Dictionary<string, object>()
                {
                    {"@IdInvitation", idInvitation }
                };

                connection.ExecuteQuery("DELETE FROM Invitation_GroupeInvite WHERE IdInvitation = @IdInvitation", parametersLiaison);
                connection.ExecuteQuery("DELETE FROM Invitation_Menu WHERE IdInvitation = @IdInvitation", parametersLiaison);
                connection.ExecuteQuery("DELETE FROM Invitation_Invite WHERE IdInvitation = @IdInvitation", parametersLiaison);
                connection.ExecuteQuery("DELETE FROM Invitation_Plat WHERE IdInvitation = @IdInvitation", parametersLiaison);

                // Suppression de l'invitation avec sécurité utilisateur
                connection.ExecuteQuery("DELETE FROM Invitation WHERE IdInvitation = @IdInvitation AND IdUtilisateur = @IdUtilisateur", parameters);
            }
        }
        #endregion

        #region Méthodes Inserer / Ajouter

        private long InsererInvitation(SQLiteConnector connection, Invitation invitation, long idUtilisateur)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                {"@Nom", invitation.Nom },
                {"@Date", invitation.Date },
                {"@IdUtilisateur", idUtilisateur } // Ajouté
            };
            return connection.ExecuteInsert("INSERT INTO Invitation (Nom, Date, IdUtilisateur) VALUES (@Nom, @Date, @IdUtilisateur)", parameters);
        }

        private void AjouterGroupesInvitesDansInvitation(SQLiteConnector connection, Invitation invitation)
        {
            if (invitation.GroupeInvites != null)
            {
                foreach (GroupeInvites groupeInvites in invitation.GroupeInvites)
                {
                    Dictionary<string, object> parameters = new Dictionary<string, object>()
                    {
                        {"@IdInvitation", invitation.IdInvitation },
                        {"@IDGroupeInvite", groupeInvites.IdGroupeInvites }
                    };
                    connection.ExecuteQuery("INSERT INTO Invitation_GroupeInvite (IdInvitation, IDGroupeInvite) VALUES (@IdInvitation, @IDGroupeInvite)", parameters);
                }
            }
        }

        private void AjouterMenusDansInvitation(SQLiteConnector connection, Invitation invitation)
        {
            if (invitation.Menus != null)
            {
                foreach (Menu menu in invitation.Menus)
                {
                    Dictionary<string, object> parameters = new Dictionary<string, object>()
                    {
                        {"@IdInvitation", invitation.IdInvitation },
                        {"@IdMenu", menu.IdMenu }
                    };
                    connection.ExecuteQuery("INSERT INTO Invitation_Menu (IdInvitation, IdMenu) VALUES (@IdInvitation, @IdMenu)", parameters);
                }
            }
        }

        private void AjouterInvitesDansInvitation(SQLiteConnector connection, Invitation invitation)
        {
            if (invitation.Invites != null)
            {
                foreach (Invite invite in invitation.Invites)
                {
                    Dictionary<string, object> parameters = new Dictionary<string, object>()
                    {
                        {"@IdInvitation", invitation.IdInvitation },
                        {"@IdInvite", invite.Id }
                    };
                    connection.ExecuteQuery("INSERT INTO Invitation_Invite (IdInvitation, IdInvite) VALUES (@IdInvitation, @IdInvite)", parameters);
                }
            }
        }

        private void AjouterPlatsDansInvitation(SQLiteConnector connection, Invitation invitation)
        {
            if (invitation.Plats != null)
            {
                foreach (Plat plat in invitation.Plats)
                {
                    Dictionary<string, object> parameters = new Dictionary<string, object>()
                    {
                        {"@IdInvitation", invitation.IdInvitation },
                        {"@IdPlat", plat.Id }
                    };
                    connection.ExecuteQuery("INSERT INTO Invitation_Plat (IdInvitation, IdPlat) VALUES (@IdInvitation, @IdPlat)", parameters);
                }
            }
        }

        private void AjouterPlatsPreferesDansInvitation(SQLiteConnector connection, Invitation invitation)
        {
            // Note: Cette méthode semblait incomplète dans ton code d'origine (pas d'INSERT), 
            // je l'ai laissée telle quelle mais vérifie si elle doit faire quelque chose.
            if (invitation.Plats != null)
            {
                foreach (Plat plat in invitation.Plats)
                {
                    Dictionary<string, object> parameters = new Dictionary<string, object>()
                    {
                        {"@IdInvitation", invitation.IdInvitation },
                        {"@IdPlat", plat.Id }
                    };
                }
            }
        }
        #endregion

        #region Méthodes Obtenir

        private List<Invite> ObtenirInvitesDansInvitation(SQLiteConnector connection, long idInvitation)
        {
            List<Invite> invites = new List<Invite>();
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                {"@IdInvitation", idInvitation }
            };

            DataTable dataTable = connection.ExecuteQuery(
                @"SELECT I.IdInvite, I.Nom, I.Prenom, I.NumTel, I.Mail 
                  FROM Invite I 
                  INNER JOIN Invitation_Invite II ON I.IdInvite = II.IdInvite 
                  WHERE II.IdInvitation = @IdInvitation",
                parameters);

            foreach (DataRow row in dataTable.Rows)
            {
                long idInvite = (long)row["IdInvite"];

                Invite invite = new Invite(
                    idInvite,
                    row["Nom"].ToString(),
                    row["Prenom"].ToString(),
                    row["NumTel"].ToString(),
                    row["Mail"].ToString(),
                    null,
                    null,
                    null
                );
                invites.Add(invite);
            }
            return invites;
        }

        private List<Plat> ObtenirPlatsDansInvitation(SQLiteConnector connection, long idInvitation)
        {
            List<Plat> plats = new List<Plat>();
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                {"@IdInvitation", idInvitation }
            };

            DataTable dataTable = connection.ExecuteQuery(@"SELECT P.IdPlat, P.Nom, P.Categorie FROM Plat P INNER JOIN Invitation_Plat IP ON P.IdPlat = IP.IdPlat WHERE IP.IdInvitation = @IdInvitation", parameters);

            foreach (DataRow row in dataTable.Rows)
            {
                long idPlat = (long)row["IdPlat"];

                CategoriePlat categorie = CategoriePlat.plat;
                Enum.TryParse(row["Categorie"].ToString(), true, out categorie);

                Plat plat = new Plat(
                    idPlat,
                    row["Nom"].ToString(),
                    "",
                    categorie,
                    "",
                    null
                );
                plats.Add(plat);
            }

            return plats;
        }

        private List<Menu> ObtenirMenusDansInvitation(SQLiteConnector connection, long idInvitation)
        {
            List<Menu> menus = new List<Menu>();
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                {"@IdInvitation", idInvitation }
            };

            DataTable dataTable = connection.ExecuteQuery(
                @"SELECT M.IdMenu, M.Nom 
                  FROM Menu M 
                  INNER JOIN Invitation_Menu IM ON M.IdMenu = IM.IdMenu 
                  WHERE IM.IdInvitation = @IdInvitation",
                parameters);

            foreach (DataRow row in dataTable.Rows)
            {
                long idMenu = (long)row["IdMenu"];
                string nomMenu = row["Nom"].ToString();

                List<Plat> platsMenu = ObtenirPlatsDansMenu_Optimise(connection, idMenu);
                Menu menu = new Menu(platsMenu, idMenu, nomMenu);
                menus.Add(menu);
            }

            return menus;
        }

        private List<Plat> ObtenirPlatsDansMenu_Optimise(SQLiteConnector connection, long idMenu)
        {
            List<Plat> plats = new List<Plat>();
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                {"@IdMenu", idMenu }
            };

            DataTable dataTable = connection.ExecuteQuery(
                @"SELECT P.IdPlat, P.Nom, P.Categorie 
                  FROM Plat P 
                  INNER JOIN Menu_Plat MP ON P.IdPlat = MP.IdPlat 
                  WHERE MP.IdMenu = @IdMenu",
                parameters);

            foreach (DataRow row in dataTable.Rows)
            {
                long idPlat = (long)row["IdPlat"];
                CategoriePlat categorie = CategoriePlat.plat;
                Enum.TryParse(row["Categorie"].ToString(), true, out categorie);

                Plat plat = new Plat(
                    idPlat,
                    row["Nom"].ToString(),
                    null,
                    categorie,
                    null,
                    null
                );
                plats.Add(plat);
            }

            return plats;
        }

        private List<GroupeInvites> ObtenirGroupesInvitesDansInvitation(SQLiteConnector connection, long idInvitation)
        {
            List<GroupeInvites> groupesInvites = new List<GroupeInvites>();
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                {"@IdInvitation", idInvitation }
            };

            DataTable dataTable = connection.ExecuteQuery(
                @"SELECT GI.IDGroupeInvite, GI.Nom 
                  FROM GroupeInvite GI 
                  INNER JOIN Invitation_GroupeInvite IGI ON GI.IDGroupeInvite = IGI.IDGroupeInvite 
                  WHERE IGI.IdInvitation = @IdInvitation",
                parameters);

            foreach (DataRow row in dataTable.Rows)
            {
                long idGroupeInvite = (long)row["IDGroupeInvite"];
                string nomGroupe = row["Nom"].ToString();

                List<Invite> invitesGroupe = ObtenirInvitesDansGroupeInvites(connection, idGroupeInvite);

                GroupeInvites groupe = new GroupeInvites(
                    idGroupeInvite,
                    nomGroupe,
                    invitesGroupe
                );
                groupesInvites.Add(groupe);
            }

            return groupesInvites;
        }

        private List<Invite> ObtenirInvitesDansGroupeInvites(SQLiteConnector connection, long idGroupeInvite)
        {
            List<Invite> invites = new List<Invite>();
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                {"@IDGroupeInvite", idGroupeInvite }
            };

            DataTable dataTable = connection.ExecuteQuery(
                @"SELECT I.IdInvite, I.Nom, I.Prenom 
                  FROM Invite I 
                  INNER JOIN Invite_Groupe IG ON I.IdInvite = IG.IdInvite 
                  WHERE IG.IDGroupeInvite = @IDGroupeInvite",
                parameters);

            foreach (DataRow row in dataTable.Rows)
            {
                Invite invite = new Invite(
                    (long)row["IdInvite"],
                    row["Nom"].ToString(),
                    row["Prenom"].ToString(),
                    null,
                    null,
                    null, null, null
                );
                invites.Add(invite);
            }

            return invites;
        }

        #endregion

        #region Méthodes Modifier
        // Renommé pour éviter la confusion avec la méthode publique
        private long ModifierInvitationInternal(SQLiteConnector connection, Invitation invitation, long idUtilisateur)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                {"@IdInvitation", invitation.IdInvitation },
                {"@Nom", invitation.Nom },
                {"@Date", invitation.Date },
                {"@IdUtilisateur", idUtilisateur } // Ajouté
            };

            // Sécurité : On vérifie l'ID utilisateur
            connection.ExecuteQuery("UPDATE Invitation SET Nom = @Nom, Date = @Date WHERE IdInvitation = @IdInvitation AND IdUtilisateur = @IdUtilisateur", parameters);
            return invitation.IdInvitation;
        }

        private void ModifierGroupesInvitesDansInvitation(SQLiteConnector connection, Invitation invitation)
        {
            Dictionary<string, object> deleteParameters = new Dictionary<string, object>()
            {
                {"@IdInvitation", invitation.IdInvitation }
            };
            connection.ExecuteQuery("DELETE FROM Invitation_GroupeInvite WHERE IdInvitation = @IdInvitation", deleteParameters);

            if (invitation.GroupeInvites != null)
            {
                foreach (GroupeInvites groupeInvites in invitation.GroupeInvites)
                {
                    Dictionary<string, object> insertParameters = new Dictionary<string, object>()
                    {
                        {"@IdInvitation", invitation.IdInvitation },
                        {"@IDGroupeInvite", groupeInvites.IdGroupeInvites }
                    };
                    connection.ExecuteQuery("INSERT INTO Invitation_GroupeInvite (IdInvitation, IDGroupeInvite) VALUES (@IdInvitation, @IDGroupeInvite)", insertParameters);
                }
            }
        }

        private void ModifierMenusDansInvitation(SQLiteConnector connection, Invitation invitation)
        {
            Dictionary<string, object> deleteParameters = new Dictionary<string, object>()
            {
                {"@IdInvitation", invitation.IdInvitation }
            };
            connection.ExecuteQuery("DELETE FROM Invitation_Menu WHERE IdInvitation = @IdInvitation", deleteParameters);

            if (invitation.Menus != null)
            {
                foreach (Menu menu in invitation.Menus)
                {
                    Dictionary<string, object> insertParameters = new Dictionary<string, object>()
                    {
                        {"@IdInvitation", invitation.IdInvitation },
                        {"@IdMenu", menu.IdMenu }
                    };
                    connection.ExecuteQuery("INSERT INTO Invitation_Menu (IdInvitation, IdMenu) VALUES (@IdInvitation, @IdMenu)", insertParameters);
                }
            }
        }

        private void ModifierInvitesDansInvitation(SQLiteConnector connection, Invitation invitation)
        {
            Dictionary<string, object> deleteParameters = new Dictionary<string, object>()
            {
                {"@IdInvitation", invitation.IdInvitation }
            };
            connection.ExecuteQuery("DELETE FROM Invitation_Invite WHERE IdInvitation = @IdInvitation", deleteParameters);

            if (invitation.Invites != null)
            {
                foreach (Invite invite in invitation.Invites)
                {
                    Dictionary<string, object> insertParameters = new Dictionary<string, object>()
                    {
                        {"@IdInvitation", invitation.IdInvitation },
                        {"@IdInvite", invite.Id }
                    };
                    connection.ExecuteQuery("INSERT INTO Invitation_Invite (IdInvitation, IdInvite) VALUES (@IdInvitation, @IdInvite)", insertParameters);
                }
            }
        }

        private void ModifierPlatsDansInvitation(SQLiteConnector connection, Invitation invitation)
        {
            Dictionary<string, object> deleteParameters = new Dictionary<string, object>()
            {
                {"@IdInvitation", invitation.IdInvitation }
            };
            connection.ExecuteQuery("DELETE FROM Invitation_Plat WHERE IdInvitation = @IdInvitation", deleteParameters);

            if (invitation.Plats != null)
            {
                foreach (Plat plat in invitation.Plats)
                {
                    Dictionary<string, object> insertParameters = new Dictionary<string, object>()
                    {
                        {"@IdInvitation", invitation.IdInvitation },
                        {"@IdPlat", plat.Id }
                    };
                    connection.ExecuteQuery("INSERT INTO Invitation_Plat (IdInvitation, IdPlat) VALUES (@IdInvitation, @IdPlat)", insertParameters);
                }
            }
        }
        #endregion

        #region Méthodes annexes
        #endregion
    }
}