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
        public bool AjouterInvitation(Invitation invitation)
        {
            bool ajoute = false;
            using (SQLiteConnector connection = new SQLiteConnector())
            {
                if (connection == null)
                {
                    throw new Exception("Erreur de connexion à la base de données");
                }

                invitation.IdInvitation = InsererInvitation(connection, invitation);

                AjouterGroupesInvitesDansInvitation(connection, invitation);
                AjouterMenusDansInvitation(connection, invitation);
                AjouterInvitesDansInvitation(connection, invitation);
                AjouterPlatsDansInvitation(connection, invitation);

                ajoute = true;
            }
            return ajoute;
        }

        public List<Invitation> ObtenirToutInvitations()
        {
            List<Invitation> invitations = new List<Invitation>();
            using (SQLiteConnector connection = new SQLiteConnector())
            {
                if (connection == null)
                {
                    throw new Exception("Erreur de connexion à la base de données");
                }

                var dataTable = connection.ExecuteQuery("SELECT * FROM Invitation");

                foreach (DataRow row in dataTable.Rows)
                {
                    long idInvitation = (long)row["IdInvitation"];
                    string nom = row["Nom"].ToString();
                    DateTime date = DateTime.Parse(row["Date"].ToString());

                    List<Invite> invites = ObtenirInvitesDansInvitation(connection, idInvitation);
                    List<Plat> plats = ObtenirPlatsDansInvitation(connection, idInvitation);
                    List<Menu> menus = ObtenirMenusDansInvitation(connection, idInvitation, plats);
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

        public bool ModifierInvitation(Invitation invitation)
        {
            bool modifie = false;
            using (SQLiteConnector connection = new SQLiteConnector())
            {
                if (connection == null)
                {
                    throw new Exception("Erreur de connexion à la base de données");
                }

                invitation.IdInvitation = ModifierInvitation(connection, invitation);

                ModifierGroupesInvitesDansInvitation(connection, invitation);
                ModifierMenusDansInvitation(connection, invitation);
                ModifierInvitesDansInvitation(connection, invitation);
                ModifierPlatsDansInvitation(connection, invitation);
                modifie = true;
            }
            return modifie;
        }

        public void SupprimerInvitation(long idInvitation)
        {
            using (SQLiteConnector connection = new SQLiteConnector())
            {
                if (connection == null)
                {
                    throw new Exception("Erreur de connexion à la base de données");
                }
                var parameters = new Dictionary<string, object>()
                {
                    {"@IdInvitation", idInvitation }
                };
                connection.ExecuteQuery("DELETE FROM Invitation_GroupeInvite WHERE IdInvitation = @IdInvitation", parameters);
                connection.ExecuteQuery("DELETE FROM Invitation_Menu WHERE IdInvitation = @IdInvitation", parameters);
                connection.ExecuteQuery("DELETE FROM Invitation_Invite WHERE IdInvitation = @IdInvitation", parameters);
                connection.ExecuteQuery("DELETE FROM Invitation_Plat WHERE IdInvitation = @IdInvitation", parameters);
                connection.ExecuteQuery("DELETE FROM Invitation WHERE IdInvitation = @IdInvitation", parameters);
            }
        }
        #endregion

        #region Méthodes Inserer / Ajouter

        /// <summary>
        /// Insère l'invitation principale et retourne son ID
        /// </summary>
        private long InsererInvitation(SQLiteConnector connection, Invitation invitation)
        {
            var parameters = new Dictionary<string, object>()
            {
                {"@IdInvitation", invitation.IdInvitation },
                {"@Nom", invitation.Nom },
                {"@Date", invitation.Date }
            };
            return connection.ExecuteInsert("INSERT INTO Invitation (Nom, Date) VALUES (@Nom, @Date)", parameters);
        }

        /// <summary>
        /// Ajoute les groupes d'invités dans une invitation
        /// </summary>
        private void AjouterGroupesInvitesDansInvitation(SQLiteConnector connection, Invitation invitation)
        {
            foreach (GroupeInvites groupeInvites in invitation.GroupeInvites)
            {
                var parameters = new Dictionary<string, object>()
                {
                    {"@IdInvitation", invitation.IdInvitation },
                    {"@IDGroupeInvite", groupeInvites.IdGroupeInvites }
                };
                connection.ExecuteQuery("INSERT INTO Invitation_GroupeInvite (IdInvitation, IDGroupeInvite) VALUES (@IdInvitation, @IDGroupeInvite)",parameters);
            }
        }

        /// <summary>
        /// Ajoute les menus dans une invitation
        /// </summary>
        private void AjouterMenusDansInvitation(SQLiteConnector connection, Invitation invitation)
        {
            foreach (Menu menu in invitation.Menus)
            {
                var parameters = new Dictionary<string, object>()
                {
                    {"@IdInvitation", invitation.IdInvitation },
                    {"@IdMenu", menu.IdMenu }
                };
                connection.ExecuteQuery("INSERT INTO Invitation_Menu (IdInvitation, IdMenu) VALUES (@IdInvitation, @IdMenu)",parameters);
            }
        }

        /// <summary>
        /// Ajoute les invités dans une invitation
        /// </summary>
        private void AjouterInvitesDansInvitation(SQLiteConnector connection, Invitation invitation)
        {
            foreach (Invite invite in invitation.Invites)
            {
                var parameters = new Dictionary<string, object>()
                {
                    {"@IdInvitation", invitation.IdInvitation },
                    {"@IdInvite", invite.Id }
                };
                connection.ExecuteQuery("INSERT INTO Invitation_Invite (IdInvitation, IdInvite) VALUES (@IdInvitation, @IdInvite)",parameters);
            }
        }

        /// <summary>
        /// Ajoute les plats dans une invitation
        /// </summary>
        private void AjouterPlatsDansInvitation(SQLiteConnector connection, Invitation invitation)
        {
            foreach (Plat plat in invitation.Plats)
            {
                var parameters = new Dictionary<string, object>()
                {
                    {"@IdInvitation", invitation.IdInvitation },
                    {"@IdPlat", plat.Id }
                };
                connection.ExecuteQuery("INSERT INTO Invitation_Plat (IdInvitation, IdPlat) VALUES (@IdInvitation, @IdPlat)",parameters);
            }
        }

        #endregion

        #region Méthodes Obtenir

        /// <summary>
        /// Obtient tous les invités d'une invitation
        /// </summary>
        private List<Invite> ObtenirInvitesDansInvitation(SQLiteConnector connection, long idInvitation)
        {
            List<Invite> invites = new List<Invite>();
            var parameters = new Dictionary<string, object>()
            {
                {"@IdInvitation", idInvitation }
            };

            var dataTable = connection.ExecuteQuery(
                @"SELECT I.IdInvite, I.Nom, I.Prenom, I.NumTel, I.Mail 
                  FROM Invite I 
                  INNER JOIN Invitation_Invite II ON I.IdInvite = II.IdInvite 
                  WHERE II.IdInvitation = @IdInvitation",
                parameters);

            foreach (DataRow row in dataTable.Rows)
            {
                Invite invite = new Invite(
                    (long)row["IdInvite"],
                    row["Nom"].ToString(),
                    row["Prenom"].ToString(),
                    row["NumTel"].ToString(),
                    row["Mail"].ToString()
                );
                invites.Add(invite);
            }
            return invites;
        }

        /// <summary>
        /// Obtient tous les plats d'une invitation
        /// </summary>
        private List<Plat> ObtenirPlatsDansInvitation(SQLiteConnector connection, long idInvitation)
        {
            List<Plat> plats = new List<Plat>();
            var parameters = new Dictionary<string, object>()
            {
                {"@IdInvitation", idInvitation }
            };

            var dataTable = connection.ExecuteQuery(
                @"SELECT P.IdPlat, P.Nom, P.Description, P.Categorie, P.Ingredients 
                  FROM Plat P 
                  INNER JOIN Invitation_Plat IP ON P.IdPlat = IP.IdPlat 
                  WHERE IP.IdInvitation = @IdInvitation",
                parameters);

            foreach (DataRow row in dataTable.Rows)
            {
                long idPlat = (long)row["IdPlat"];
                List<NomAllergene> allergenes = ObtenirAllergenesDuPlat(connection, idPlat);
                Plat plat = CreerPlat(row, allergenes);
                plats.Add(plat);
            }

            return plats;
        }

        /// <summary>
        /// Obtient tous les menus d'une invitation
        /// </summary>
        private List<Menu> ObtenirMenusDansInvitation(SQLiteConnector connection, long idInvitation, List<Plat> platsDisponibles)
        {
            List<Menu> menus = new List<Menu>();
            var parameters = new Dictionary<string, object>()
            {
                {"@IdInvitation", idInvitation }
            };

            var dataTable = connection.ExecuteQuery(
                @"SELECT M.IdMenu, M.Nom 
                  FROM Menu M 
                  INNER JOIN Invitation_Menu IM ON M.IdMenu = IM.IdMenu 
                  WHERE IM.IdInvitation = @IdInvitation",
                parameters);

            foreach (DataRow row in dataTable.Rows)
            {
                long idMenu = (long)row["IdMenu"];
                string nomMenu = row["Nom"].ToString();

                List<Plat> platsMenu = ObtenirPlatsDansMenu(connection, idMenu);
                Menu menu = new Menu(platsMenu, idMenu, nomMenu);
                menus.Add(menu);
            }

            return menus;
        }

        /// <summary>
        /// Obtient tous les plats d'un menu
        /// </summary>
        private List<Plat> ObtenirPlatsDansMenu(SQLiteConnector connection, long idMenu)
        {
            List<Plat> plats = new List<Plat>();
            var parameters = new Dictionary<string, object>()
            {
                {"@IdMenu", idMenu }
            };

            var dataTable = connection.ExecuteQuery(
                @"SELECT P.IdPlat, P.Nom, P.Description, P.Categorie, P.Ingredients 
                  FROM Plat P 
                  INNER JOIN Menu_Plat MP ON P.IdPlat = MP.IdPlat 
                  WHERE MP.IdMenu = @IdMenu",
                parameters);

            foreach (DataRow row in dataTable.Rows)
            {
                long idPlat = (long)row["IdPlat"];
                List<NomAllergene> allergenes = ObtenirAllergenesDuPlat(connection, idPlat);
                Plat plat = CreerPlat(row, allergenes);
                plats.Add(plat);
            }

            return plats;
        }

        /// <summary>
        /// Obtient tous les groupes d'invités d'une invitation
        /// </summary>
        private List<GroupeInvites> ObtenirGroupesInvitesDansInvitation(SQLiteConnector connection, long idInvitation)
        {
            List<GroupeInvites> groupesInvites = new List<GroupeInvites>();
            var parameters = new Dictionary<string, object>()
            {
                {"@IdInvitation", idInvitation }
            };

            var dataTable = connection.ExecuteQuery(
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
                GroupeInvites groupeInvites = new GroupeInvites(
                    idGroupeInvite,
                    nomGroupe,
                    invitesGroupe
                );
                groupesInvites.Add(groupeInvites);
            }

            return groupesInvites;
        }

        /// <summary>
        /// Obtient tous les invités d'un groupe d'invités
        /// </summary>
        private List<Invite> ObtenirInvitesDansGroupeInvites(SQLiteConnector connection, long idGroupeInvite)
        {
            List<Invite> invites = new List<Invite>();
            var parameters = new Dictionary<string, object>()
            {
                {"@IDGroupeInvite", idGroupeInvite }
            };

            var dataTable = connection.ExecuteQuery(
                @"SELECT I.IdInvite, I.Nom, I.Prenom, I.NumTel, I.Mail 
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
                    row["NumTel"].ToString(),
                    row["Mail"].ToString()
                );
                invites.Add(invite);
            }

            return invites;
        }

        /// <summary>
        /// Obtient tous les allergènes d'un plat
        /// </summary>
        private List<NomAllergene> ObtenirAllergenesDuPlat(SQLiteConnector connection, long idPlat)
        {
            List<NomAllergene> allergenes = new List<NomAllergene>();
            var parameters = new Dictionary<string, object>()
            {
                {"@IdPlat", idPlat }
            };

            var dataTable = connection.ExecuteQuery(
                @"SELECT a.Nom 
                  FROM Allergene a
                  INNER JOIN Plat_Allergene pa ON a.IDAllergene = pa.IDAllergene 
                  WHERE pa.IDPlat = @IdPlat",
                parameters);

            foreach (DataRow row in dataTable.Rows)
            {
                NomAllergene allergene;
                if (Enum.TryParse(row["Nom"].ToString(), true, out allergene))
                {
                    allergenes.Add(allergene);
                }
            }

            return allergenes;
        }

        #endregion

        #region Méthodes Modifier
        /// <summary>
        /// Insère l'invitation principale et retourne son ID
        /// </summary>
        private long ModifierInvitation(SQLiteConnector connection, Invitation invitation)
        {
            var parameters = new Dictionary<string, object>()
            {
                {"@IdInvitation", invitation.IdInvitation },
                {"@Nom", invitation.Nom },
                {"@Date", invitation.Date }
            };
            connection.ExecuteQuery("UPDATE Invitation SET Nom = @Nom, Date = @Date WHERE IdInvitation = @IdInvitation", parameters);
            return invitation.IdInvitation;
        }

        /// <summary>
        /// Modifie les groupes d'invités dans une invitation
        /// </summary>
        /// <param name="connection"> la connexion à la base de données </param>
        /// <param name="invitation"> l'invitation à modifier </param>
        private void ModifierGroupesInvitesDansInvitation(SQLiteConnector connection, Invitation invitation)
        {
            var deleteParameters = new Dictionary<string, object>()
            {
                {"@IdInvitation", invitation.IdInvitation }
            };
            connection.ExecuteQuery("DELETE FROM Invitation_GroupeInvite WHERE IdInvitation = @IdInvitation", deleteParameters);

            foreach (GroupeInvites groupeInvites in invitation.GroupeInvites)
            {
                var insertParameters = new Dictionary<string, object>()
                {
                    {"@IdInvitation", invitation.IdInvitation },
                    {"@IDGroupeInvite", groupeInvites.IdGroupeInvites }
                };
                connection.ExecuteQuery("INSERT INTO Invitation_GroupeInvite (IdInvitation, IDGroupeInvite) VALUES (@IdInvitation, @IDGroupeInvite)", insertParameters);
            }
        }

        /// <summary>
        /// Modifie les menus dans une invitation
        /// </summary>
        /// <param name="connection"> la connexion à la base de données </param>
        /// <param name="invitation"> l'invitation à modifier </param>
        private void ModifierMenusDansInvitation(SQLiteConnector connection, Invitation invitation)
        {
            var deleteParameters = new Dictionary<string, object>()
            {
                {"@IdInvitation", invitation.IdInvitation }
            };
            connection.ExecuteQuery("DELETE FROM Invitation_Menu WHERE IdInvitation = @IdInvitation", deleteParameters);
            foreach (Menu menu in invitation.Menus)
            {
                var insertParameters = new Dictionary<string, object>()
                {
                    {"@IdInvitation", invitation.IdInvitation },
                    {"@IdMenu", menu.IdMenu }
                };
                connection.ExecuteQuery("INSERT INTO Invitation_Menu (IdInvitation, IdMenu) VALUES (@IdInvitation, @IdMenu)", insertParameters);
            }
        }

        /// <summary>
        /// Modifie les invités dans une invitation
        /// </summary>
        /// <param name="connection"> la connexion à la base de données </param>
        /// <param name="invitation"> l'invitation à modifier </param>
        private void ModifierInvitesDansInvitation(SQLiteConnector connection, Invitation invitation)
        {
            var deleteParameters = new Dictionary<string, object>()
            {
                {"@IdInvitation", invitation.IdInvitation }
            };
            connection.ExecuteQuery("DELETE FROM Invitation_Invite WHERE IdInvitation = @IdInvitation", deleteParameters);
            foreach (Invite invite in invitation.Invites)
            {
                var insertParameters = new Dictionary<string, object>()
                {
                    {"@IdInvitation", invitation.IdInvitation },
                    {"@IdInvite", invite.Id }
                };
                connection.ExecuteQuery("INSERT INTO Invitation_Invite (IdInvitation, IdInvite) VALUES (@IdInvitation, @IdInvite)", insertParameters);
            }
        }

        /// <summary>
        /// Modifie les plats dans une invitation
        /// </summary>
        /// <param name="connection"> la connexion à la base de données </param>
        /// <param name="invitation"> l'invitation à modifier </param>
        private void ModifierPlatsDansInvitation(SQLiteConnector connection, Invitation invitation)
        {
            var deleteParameters = new Dictionary<string, object>()
            {
                {"@IdInvitation", invitation.IdInvitation }
            };
            connection.ExecuteQuery("DELETE FROM Invitation_Plat WHERE IdInvitation = @IdInvitation", deleteParameters);
            foreach (Plat plat in invitation.Plats)
            {
                var insertParameters = new Dictionary<string, object>()
                {
                    {"@IdInvitation", invitation.IdInvitation },
                    {"@IdPlat", plat.Id }
                };
                connection.ExecuteQuery("INSERT INTO Invitation_Plat (IdInvitation, IdPlat) VALUES (@IdInvitation, @IdPlat)", insertParameters);
            }
        }
        #endregion

        #region Méthodes annexes

        /// <summary>
        /// Crée un objet Plat à partir d'une ligne de données
        /// </summary>
        private Plat CreerPlat(DataRow row, List<NomAllergene> allergenes)
        {
            CategoriePlat categorie;
            if (!Enum.TryParse(row["Categorie"].ToString(), true, out categorie))
            {
                categorie = CategoriePlat.plat;
            }

            return new Plat(
                (long)row["IdPlat"],
                row["Nom"].ToString(),
                row["Description"].ToString(),
                categorie,
                row["Ingredients"]?.ToString(),
                allergenes.Count > 0 ? allergenes : null
            );
        }
        #endregion
    }
}
