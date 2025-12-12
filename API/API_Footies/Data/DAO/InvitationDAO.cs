using System.Data;
using System.Text.Json;
using API_Footies.Data.Interfaces;
using API_Footies.Metier;
using API_Footies.Metier.Enum;
using Microsoft.Extensions.Options;

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
                invitation.IdInvitation = InsererInvitation(connection, invitation, idUtilisateur);
                AjouterGroupesInvitesDansInvitation(connection, invitation);
                AjouterMenusDansInvitation(connection, invitation);
                AjouterInvitesDansInvitation(connection, invitation);
                AjouterPlatsDansInvitation(connection, invitation);
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
                DataTable dataTable = connection.ExecuteQuery("SELECT * FROM Invitation WHERE IdUtilisateur = @IdUtilisateur", parameters);

                foreach (DataRow row in dataTable.Rows)
                {
                    long idInvitation = (long)row["IdInvitation"];
                    string nom = row["Nom"].ToString();
                    string? remarque = row["Remarque"].ToString();
                    DateTime date = DateTime.Parse(row["Date"].ToString());
                    List<Invite> invites = ObtenirInvitesDansInvitation(connection, idInvitation);
                    List<Plat> plats = ObtenirPlatsDansInvitation(connection, idInvitation);
                    List<Menu> menus = ObtenirMenusDansInvitation(connection, idInvitation);
                    List<GroupeInvites> groupesInvites = ObtenirGroupesInvitesDansInvitation(connection, idInvitation);

                    Invitation invitation = new Invitation(groupesInvites, menus, invites, plats, idInvitation, nom, date, remarque);
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
                invitation.IdInvitation = ModifierInvitationInternal(connection, invitation, idUtilisateur);
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
                Dictionary<string, object> parametersLiaison = new Dictionary<string, object>()
                {
                    {"@IdInvitation", idInvitation }
                };
                connection.ExecuteQuery("DELETE FROM Invitation_GroupeInvite WHERE IdInvitation = @IdInvitation", parametersLiaison);
                connection.ExecuteQuery("DELETE FROM Invitation_Menu WHERE IdInvitation = @IdInvitation", parametersLiaison);
                connection.ExecuteQuery("DELETE FROM Invitation_Invite WHERE IdInvitation = @IdInvitation", parametersLiaison);
                connection.ExecuteQuery("DELETE FROM Invitation_Plat WHERE IdInvitation = @IdInvitation", parametersLiaison);
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
                {"@IdUtilisateur", idUtilisateur },
                {"@Remarque", invitation.Remarque ?? "" }
            };
            return connection.ExecuteInsert("INSERT INTO Invitation (Nom, Date, IdUtilisateur, Remarque) VALUES (@Nom, @Date, @IdUtilisateur, @Remarque)", parameters);
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

        public List<Invitation> ChercherInvitations(string InvitationsRechercher, long idUtilisateur)
        {
            List<Invitation> listeInvitations = new List<Invitation>();
            using (SQLiteConnector connection = new SQLiteConnector())
            {
                if (connection == null)
                {
                    throw new Exception("Erreur de connexion à la base de données");
                }
                DataTable dataTable = RechercherInvitationsParTexte(connection, InvitationsRechercher, idUtilisateur);

                foreach (DataRow row in dataTable.Rows)
                {
                    long idInvitation = (long)row["IDInvitation"];
                    string nom = row["Nom"].ToString();
                    DateTime date = DateTime.Parse(row["Date"].ToString());
                    List<Invite> invites = ObtenirInvitesDansInvitation(connection, idInvitation);
                    List<Plat> plats = ObtenirPlatsDansInvitation(connection, idInvitation);
                    List<Menu> menus = ObtenirMenusDansInvitation(connection, idInvitation);
                    List<GroupeInvites> groupesInvites = ObtenirGroupesInvitesDansInvitation(connection, idInvitation);

                    Invitation invitation = new Invitation(groupesInvites, menus, invites, plats, idInvitation, nom, date);
                    listeInvitations.Add(invitation);
                }
            }
            return listeInvitations;
        }
        #endregion

        #region Méthodes Obtenir

        /// <summary>
        /// Récupère la liste des invités pour une invitation donnée
        /// </summary>
        private List<Invite> ObtenirInvitesDansInvitation(SQLiteConnector connection, long idInvitation)
        {
            List<Invite> listeInvites = new List<Invite>();

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@IdInvitation", idInvitation);

            string query = @"SELECT I.IdInvite, I.Nom, I.Prenom, I.NumTel, I.Mail 
                     FROM Invite I 
                     INNER JOIN Invitation_Invite II ON I.IdInvite = II.IdInvite 
                     WHERE II.IdInvitation = @IdInvitation";

            DataTable dataTable = connection.ExecuteQuery(query, parameters);

            foreach (DataRow row in dataTable.Rows)
            {
                Invite invite = CreerInviteDepuisDataRow(connection, row);
                listeInvites.Add(invite);
            }

            return listeInvites;
        }
        private Invite CreerInviteDepuisDataRow(SQLiteConnector connection, DataRow row)
        {
            long idInvite = (long)row["IdInvite"];

            List<NomAllergene> allergenes = ObtenirAllergenesInvite(connection, idInvite);
            List<Plat> platsDetestes = ObtenirPlatsDetestesInvite(connection, idInvite);
            List<Plat> platsPreferes = ObtenirPlatsPreferesInvite(connection, idInvite);

            List<NomAllergene> listeAllergenesFinale = null;
            if (allergenes.Count > 0)
            {
                listeAllergenesFinale = allergenes;
            }

            List<Plat> listePlatsDetestesFinale = null;
            if (platsDetestes.Count > 0)
            {
                listePlatsDetestesFinale = platsDetestes;
            }

            List<Plat> listePlatsPreferesFinale = null;
            if (platsPreferes.Count > 0)
            {
                listePlatsPreferesFinale = platsPreferes;
            }

            string numTel = row["NumTel"] as string;
            string mail = row["Mail"] as string;

            return new Invite(
                idInvite,
                row["Nom"].ToString(),
                row["Prenom"].ToString(),
                numTel,
                mail,
                listeAllergenesFinale,
                listePlatsDetestesFinale,
                listePlatsPreferesFinale
            );
        }

        private List<NomAllergene> ObtenirAllergenesInvite(SQLiteConnector connection, long idInvite)
        {
            List<NomAllergene> listeAllergenes = new List<NomAllergene>();

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@IdInvite", idInvite);

            string query = @"SELECT a.Nom 
                     FROM Allergene a 
                     INNER JOIN Invite_Allergene ia ON a.IDAllergene = ia.IdAllergene 
                     WHERE ia.IdInvite = @IdInvite";

            DataTable dataTable = connection.ExecuteQuery(query, parameters);

            foreach (DataRow rowAllergene in dataTable.Rows)
            {
                NomAllergene allergeneTemp;
                if (Enum.TryParse(rowAllergene["Nom"].ToString(), true, out allergeneTemp))
                {
                    listeAllergenes.Add(allergeneTemp);
                }
            }

            return listeAllergenes;
        }

        private List<Plat> ObtenirPlatsDetestesInvite(SQLiteConnector connection, long idInvite)
        {
            List<Plat> listePlats = new List<Plat>();

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@IdInvite", idInvite);

            string query = @"SELECT p.IDPlat, p.Nom, p.Description, p.Categorie, p.Ingredients 
                     FROM Plat p 
                     INNER JOIN Invite_PlatDeteste ipd ON p.IDPlat = ipd.IdPlat 
                     WHERE ipd.IdInvite = @IdInvite";

            DataTable dataTable = connection.ExecuteQuery(query, parameters);

            foreach (DataRow rowPlat in dataTable.Rows)
            {
                Plat nouveauPlat = CreerPlatDepuisDataRow(rowPlat);
                listePlats.Add(nouveauPlat);
            }

            return listePlats;
        }

        private List<Plat> ObtenirPlatsPreferesInvite(SQLiteConnector connection, long idInvite)
        {
            List<Plat> listePlats = new List<Plat>();

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@IdInvite", idInvite);

            string query = @"SELECT p.IDPlat, p.Nom, p.Description, p.Categorie, p.Ingredients 
                     FROM Plat p 
                     INNER JOIN Invite_PlatPrefere ipp ON p.IDPlat = ipp.IdPlat 
                     WHERE ipp.IdInvite = @IdInvite";

            DataTable dataTable = connection.ExecuteQuery(query, parameters);

            foreach (DataRow rowPlat in dataTable.Rows)
            {
                Plat nouveauPlat = CreerPlatDepuisDataRow(rowPlat);
                listePlats.Add(nouveauPlat);
            }

            return listePlats;
        }
        private Plat CreerPlatDepuisDataRow(DataRow row)
        {
            CategoriePlat categorie = CategoriePlat.plat;
            if (!Enum.TryParse(row["Categorie"].ToString(), true, out categorie))
            {
                categorie = CategoriePlat.plat;
            }

            string description = row["Description"] as string;
            string ingredients = row["Ingredients"] as string;

            return new Plat(
                (long)row["IDPlat"],
                row["Nom"].ToString(),
                description,
                categorie,
                ingredients,
                null
            );
        }

        public List<AvisDetail> ObtenirAvisParInvitation(long idInvitation)
        {
            List<AvisDetail> avisList = new List<AvisDetail>();
            using (SQLiteConnector connection = new SQLiteConnector())
            {
                string query = @"SELECT i.Nom AS NomInvite, i.Prenom AS PrenomInvite, p.Nom AS NomPlat, ap.Note, ap.Commentaire FROM Avis_Plat ap JOIN Invite i ON ap.IdInvite = i.IDInvite JOIN Plat p ON ap.IdPlat = p.IDPlat WHERE ap.IdPlat IN ( SELECT IdPlat FROM Invitation_Plat WHERE IdInvitation = @IdInv UNION SELECT mp.IdPlat  FROM Invitation_Menu im JOIN Menu_Plat mp ON im.IdMenu = mp.IdMenu WHERE im.IdInvitation = @IdInv )";

                Dictionary<string, object> parameters = new Dictionary<string, object> { { "@IdInv", idInvitation } };
                DataTable table = connection.ExecuteQuery(query, parameters);

                foreach (DataRow row in table.Rows)
                {
                    avisList.Add(new AvisDetail
                    {
                        NomInvite = row["NomInvite"].ToString(),
                        PrenomInvite = row["PrenomInvite"].ToString(),
                        NomPlat = row["NomPlat"].ToString(),
                        Note = Convert.ToInt32(row["Note"]),
                        Commentaire = row["Commentaire"].ToString()
                    });
                }
            }
            return avisList;
        }

        private List<NomAllergene> ObtenirAllergenesDeInvite(SQLiteConnector connection, long idInvite)
        {
            List<NomAllergene> allergenes = new List<NomAllergene>();
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "@IdInvite", idInvite }
            };

            DataTable dataTable = connection.ExecuteQuery(@"SELECT A.Nom FROM Allergene A INNER JOIN Invite_Allergene IA ON A.IdAllergene = IA.IdAllergene WHERE IA.IdInvite = @IdInvite", parameters);

            foreach (DataRow row in dataTable.Rows)
            {
                if (Enum.TryParse(row["Nom"].ToString(), true, out NomAllergene allergene))
                    allergenes.Add(allergene);
            }
            return allergenes;
        }

        private List<Plat> ObtenirPlatsDetestesDeInvite(SQLiteConnector connection, long idInvite)
        {
            List<Plat> plats = new List<Plat>();
            Dictionary<string, object> parameters = new Dictionary<string, object>() { { "@IdInvite", idInvite } };

            DataTable dataTable = connection.ExecuteQuery(@"SELECT P.IDPlat, P.Nom FROM Plat P INNER JOIN Invite_PlatDeteste IPA ON P.IDPlat = IPA.IDPlat  WHERE IPA.IdInvite = @IdInvite", parameters);

            foreach (DataRow row in dataTable.Rows)
            {
                long idPlat = (long)row["IDPlat"];
                List<NomAllergene> allergenesPlat = ObtenirAllergenesDuPlat(connection, idPlat);

                Plat plat = new Plat(idPlat, row["Nom"].ToString(), null, CategoriePlat.plat, null, allergenesPlat.Count > 0 ? allergenesPlat : null);
                plats.Add(plat);
            }
            return plats;
        }

        private List<Plat> ObtenirPlatsPreferesDeInvite(SQLiteConnector connection, long idInvite)
        {
            List<Plat> plats = new List<Plat>();
            Dictionary<string, object> parameters = new Dictionary<string, object>() { { "@IdInvite", idInvite } };
            DataTable dataTable = connection.ExecuteQuery(@"SELECT P.IDPlat, P.Nom  FROM Plat P INNER JOIN Invite_PlatPrefere IPP ON P.IDPlat = IPP.IDPlat WHERE IPP.IdInvite = @IdInvite", parameters);

            foreach (DataRow row in dataTable.Rows)
            {
                long idPlat = (long)row["IDPlat"];
                List<NomAllergene> allergenesPlat = ObtenirAllergenesDuPlat(connection, idPlat);

                Plat plat = new Plat(idPlat, row["Nom"].ToString(), null, CategoriePlat.plat, null, allergenesPlat.Count > 0 ? allergenesPlat : null);
                plats.Add(plat);
            }
            return plats;
        }


        private List<NomAllergene> ObtenirAllergenesDuPlat(SQLiteConnector connection, long idPlat)
        {
            List<NomAllergene> allergenes = new List<NomAllergene>();
            Dictionary<string, object> parameters = new Dictionary<string, object>() { { "@IdPlat", idPlat } };
            DataTable dataTable = connection.ExecuteQuery(@"SELECT A.Nom FROM Allergene A INNER JOIN Plat_Allergene PA ON A.IdAllergene = PA.IdAllergene WHERE PA.IdPlat = @IdPlat", parameters);

            foreach (DataRow row in dataTable.Rows)
            {
                if (Enum.TryParse(row["Nom"].ToString(), true, out NomAllergene allergene))
                    allergenes.Add(allergene);
            }
            return allergenes;
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
                List<NomAllergene> allergenes = ObtenirAllergenesDePlats(connection, idPlat);

                Plat plat = new Plat(idPlat, row["Nom"].ToString(), "", CategoriePlat.plat, "", allergenes);
                plats.Add(plat);
            }
            return plats;
        }

        private List<NomAllergene> ObtenirAllergenesDePlats(SQLiteConnector connection, long idPlat)
        {
            List<NomAllergene> allergenes = new List<NomAllergene>();
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "@IdPlat", idPlat }
            };

            DataTable dataTable = connection.ExecuteQuery(@"SELECT A.Nom FROM Allergene A INNER JOIN Plat_Allergene PA ON A.IdAllergene = PA.IdAllergene WHERE PA.IdPlat = @IdPlat", parameters);

            foreach (DataRow row in dataTable.Rows)
            {
                if (Enum.TryParse(row["Nom"].ToString(), true, out NomAllergene allergene))
                    allergenes.Add(allergene);
            }
            return allergenes;
        }

        private List<Menu> ObtenirMenusDansInvitation(SQLiteConnector connection, long idInvitation)
        {
            List<Menu> menus = new List<Menu>();
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                {"@IdInvitation", idInvitation }
            };

            DataTable dataTable = connection.ExecuteQuery(@"SELECT M.IdMenu, M.Nom  FROM Menu M INNER JOIN Invitation_Menu IM ON M.IdMenu = IM.IdMenu WHERE IM.IdInvitation = @IdInvitation", parameters);

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

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@IdMenu", idMenu);

            string queryPlats = @"SELECT P.IdPlat, P.Nom, P.Categorie, P.Description, P.Ingredients
                          FROM Plat P 
                          INNER JOIN Menu_Plat MP ON P.IdPlat = MP.IdPlat 
                          WHERE MP.IdMenu = @IdMenu";

            DataTable dataTable = connection.ExecuteQuery(queryPlats, parameters);

            foreach (DataRow row in dataTable.Rows)
            {
                long idPlat = (long)row["IdPlat"];

                CategoriePlat categorie = CategoriePlat.plat;
                Enum.TryParse(row["Categorie"].ToString(), true, out categorie);

                List<NomAllergene> allergenesPlat = new List<NomAllergene>();
                Dictionary<string, object> parametersAllergene = new Dictionary<string, object>();
                parametersAllergene.Add("@IdPlat", idPlat);

                string queryAllergene = @"SELECT a.Nom 
                                  FROM Allergene a
                                  INNER JOIN Plat_Allergene pa ON a.IDAllergene = pa.IDAllergene 
                                  WHERE pa.IDPlat = @IdPlat";

                DataTable dataTableAllergenes = connection.ExecuteQuery(queryAllergene, parametersAllergene);

                foreach (DataRow rowAllergene in dataTableAllergenes.Rows)
                {
                    NomAllergene allergene;
                    if (Enum.TryParse(rowAllergene["Nom"].ToString(), true, out allergene))
                    {
                        allergenesPlat.Add(allergene);
                    }
                }

                List<NomAllergene> listeAllergenesFinale = null;
                if (allergenesPlat.Count > 0)
                {
                    listeAllergenesFinale = allergenesPlat;
                }

                Plat plat = new Plat(
                    idPlat,
                    row["Nom"].ToString(),
                    row["Description"]?.ToString(),
                    categorie,
                    row["Ingredients"]?.ToString(),
                    listeAllergenesFinale
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

            DataTable dataTable = connection.ExecuteQuery(@"SELECT GI.IDGroupeInvite, GI.Nom FROM GroupeInvite GI  INNER JOIN Invitation_GroupeInvite IGI ON GI.IDGroupeInvite = IGI.IDGroupeInvite  WHERE IGI.IdInvitation = @IdInvitation", parameters);

            foreach (DataRow row in dataTable.Rows)
            {
                long idGroupeInvite = (long)row["IDGroupeInvite"];
                string nomGroupe = row["Nom"].ToString();

                List<Invite> invitesGroupe = ObtenirInvitesDansGroupeInvites(connection, idGroupeInvite);

                GroupeInvites groupe = new GroupeInvites(idGroupeInvite, nomGroupe, invitesGroupe);
                groupesInvites.Add(groupe);
            }
            return groupesInvites;
        }

        private List<Invite> ObtenirInvitesDansGroupeInvites(SQLiteConnector connection, long idGroupeInvite)
        {
            List<Invite> invites = new List<Invite>();

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@IDGroupeInvite", idGroupeInvite);

            string queryInvites = @"SELECT I.IdInvite, I.Nom, I.Prenom, I.NumTel, I.Mail
                    FROM Invite I 
                    INNER JOIN Invite_Groupe IG ON I.IdInvite = IG.IdInvite 
                    WHERE IG.IDGroupeInvite = @IDGroupeInvite";

            DataTable dataTable = connection.ExecuteQuery(queryInvites, parameters);

            foreach (DataRow row in dataTable.Rows)
            {
                Invite invite = CreerInviteDepuisDataRow(connection, row);
                invites.Add(invite);
            }
            return invites;
        }

        #endregion

        #region Méthodes Modifier
        private long ModifierInvitationInternal(SQLiteConnector connection, Invitation invitation, long idUtilisateur)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                {"@IdInvitation", invitation.IdInvitation },
                {"@Nom", invitation.Nom },
                {"@Date", invitation.Date },
                {"@IdUtilisateur", idUtilisateur },
                {"@Remarque", invitation.Remarque ?? ""   }
            };
            connection.ExecuteQuery("UPDATE Invitation SET Nom = @Nom, Date = @Date, Remarque = @Remarque WHERE IdInvitation = @IdInvitation AND IdUtilisateur = @IdUtilisateur", parameters);
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

        /// <summary>
        /// Recherche des invitations par nom ET par utilisateur
        /// </summary>
        private DataTable RechercherInvitationsParTexte(SQLiteConnector connection, string texteRecherche, long idUtilisateur)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                {"@Texte", $"%{texteRecherche}%" },
                {"@IdUtilisateur", idUtilisateur }
            };
            return connection.ExecuteQuery("SELECT * FROM Invitation WHERE Nom LIKE @Texte AND IdUtilisateur = @IdUtilisateur", parameters);
        }
        #endregion
    }
}