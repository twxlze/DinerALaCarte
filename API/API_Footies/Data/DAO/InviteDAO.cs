using System.Data;
using API_Footies.Data.Interfaces;
using API_Footies.Metier;
using API_Footies.Metier.Enum;

namespace API_Footies.Data.DAO
{
    /// <summary>
    /// Classe en charge de tout ce qui touche les invités dans la base de données
    /// </summary>
    public class InviteDAO : IInviteDAO
    {
        #region Méthodes publiques
        public bool AjouterInvite(Invite invite, long idUtilisateur)
        {
            using (SQLiteConnector connection = new SQLiteConnector())
            {
                if (connection == null)
                {
                    throw new Exception("Erreur de connexion à la base de données");
                }

                InsererInvite(connection, invite, idUtilisateur);
                AjouterAllergenesInvite(connection, invite.Id, invite.Allergenes);
                AjouterPlatsDetestesInvite(connection, invite.Id, invite.PlatsDetestes);
                AjouterPlatsPreferesInvite(connection, invite.Id, invite.PlatsPreferes);
                return true;
            }
        }

        public bool ModifierInvite(Invite invite, long idUtilisateur)
        {
            using (SQLiteConnector connection = new SQLiteConnector())
            {
                if (connection == null)
                {
                    throw new Exception("Erreur de connexion à la base de données");
                }

                MettreAJourInformationsInvite(connection, invite, idUtilisateur);
                SupprimerAllergenesInvite(connection, invite.Id);
                AjouterAllergenesInvite(connection, invite.Id, invite.Allergenes);
                SupprimerPlatsDetestesInvite(connection, invite.Id);
                AjouterPlatsDetestesInvite(connection, invite.Id, invite.PlatsDetestes);
                SupprimerPlatsPreferesInvite(connection, invite.Id);
                AjouterPlatsPreferesInvite(connection, invite.Id, invite.PlatsPreferes);
                return true;
            }
        }

        public List<Invite> ListInvite(long idUtilisateur)
        {
            List<Invite> listeInvite = new List<Invite>();
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
                DataTable dataTable = connection.ExecuteQuery("SELECT * FROM Invite WHERE IdUtilisateur = @IdUtilisateur", parameters);

                foreach (DataRow? row in dataTable.Rows)
                {
                    Invite invite = CreerInviteDepuisDataRow(connection, row);
                    listeInvite.Add(invite);
                }
            }
            return listeInvite;
        }

        public void SupprimerInvite(long id, long idUtilisateur)
        {
            using (SQLiteConnector connection = new SQLiteConnector())
            {
                if (connection == null)
                {
                    throw new Exception("Erreur de connexion à la base de données");
                }
                SupprimerAllergenesInvite(connection, id);
                SupprimerPlatsDetestesInvite(connection, id);
                SupprimerPlatsPreferesInvite(connection, id);
                SupprimerInviteParId(connection, id, idUtilisateur);
            }
        }

        public bool EstDansUnGroupe(long idInvite)
        {
            using (SQLiteConnector connection = new SQLiteConnector())
            {
                if (connection == null)
                {
                    throw new Exception("Erreur de connexion à la base de données");
                }
                return VerifieAppartientGroupe(connection, idInvite);
            }
        }

        public List<Invite> ChercherInvite(string texterecherche, long idUtilisateur)
        {
            List<Invite> listeInvite = new List<Invite>();

            using (SQLiteConnector connection = new SQLiteConnector())
            {
                if (connection == null)
                {
                    throw new Exception("Erreur de connexion à la base de données");
                }

                DataTable dataTable = RechercherInvitesParTexte(connection, texterecherche, idUtilisateur);

                foreach (DataRow? row in dataTable.Rows)
                {
                    Invite invite = CreerInviteDepuisDataRow(connection, row);
                    listeInvite.Add(invite);
                }
            }
            return listeInvite;
        }
        #endregion

        #region Méthodes privées / Gestion des invités

        /// <summary>
        /// Insère les informations de base d'un invité et met à jour son ID (Avec IdUtilisateur)
        /// </summary>
        private void InsererInvite(SQLiteConnector connection, Invite invite, long idUtilisateur)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                {"@Nom", invite.Nom },
                {"@Prenom", invite.Prenom },
                {"@Telephone", invite.Telephone },
                {"@Email", invite.Email },
                {"@IdUtilisateur", idUtilisateur }
            };
            invite.Id = connection.ExecuteInsert("INSERT INTO Invite (Nom,Prenom,NumTel,Mail,IdUtilisateur) VALUES (@Nom,@Prenom,@Telephone,@Email,@IdUtilisateur)", parameters);
        }

        /// <summary>
        /// Met à jour les informations de base d'un invité (Sécurisé par IdUtilisateur)
        /// </summary>
        private void MettreAJourInformationsInvite(SQLiteConnector connection, Invite invite, long idUtilisateur)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                {"@Id", invite.Id },
                {"@Nom", invite.Nom },
                {"@Prenom", invite.Prenom },
                {"@Telephone", invite.Telephone },
                {"@Email", invite.Email },
                {"@IdUtilisateur", idUtilisateur }
            };
            connection.ExecuteQuery("UPDATE Invite SET Nom = @Nom, Prenom = @Prenom, NumTel = @Telephone, Mail = @Email WHERE IDInvite = @Id AND IdUtilisateur = @IdUtilisateur", parameters);
        }

        /// <summary>
        /// Supprime un invité par son ID (Sécurisé par IdUtilisateur)
        /// </summary>
        private void SupprimerInviteParId(SQLiteConnector connection, long id, long idUtilisateur)
        {
            var parameters = new Dictionary<string, object>()
            {
                {"@Id", id },
                {"@IdUtilisateur", idUtilisateur }
            };
            connection.ExecuteQuery("DELETE FROM Invite WHERE IDInvite = @Id AND IdUtilisateur = @IdUtilisateur", parameters);
        }

        /// <summary>
        /// Recherche des invités par nom ou prénom (Filtré par IdUtilisateur)
        /// </summary>
        private DataTable RechercherInvitesParTexte(SQLiteConnector connection, string texteRecherche, long idUtilisateur)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                {"@TexteRecherche", $"%{texteRecherche}%" },
                {"@IdUtilisateur", idUtilisateur }
            };
            return connection.ExecuteQuery("SELECT * FROM Invite WHERE (Nom LIKE @TexteRecherche OR Prenom LIKE @TexteRecherche) AND IdUtilisateur = @IdUtilisateur", parameters);
        }
        #endregion

        #region Méthodes privées / Gestion des allergènes
        private void AjouterAllergenesInvite(SQLiteConnector connection, long idInvite, List<NomAllergene>? allergenes)
        {
            if (allergenes != null && allergenes.Count > 0)
            {
                foreach (NomAllergene allergene in allergenes)
                {
                    Dictionary<string, object> parametersAllergene = new Dictionary<string, object>()
                    {
                        {"@Nom", allergene.ToString() }
                    };
                    DataTable dataTableAllergene = connection.ExecuteQuery("SELECT IDAllergene FROM Allergene WHERE Nom = @Nom", parametersAllergene);

                    if (dataTableAllergene.Rows.Count > 0)
                    {
                        long idAllergene = (long)dataTableAllergene.Rows[0]["IDAllergene"];
                        InsererLienInviteAllergene(connection, idInvite, idAllergene);
                    }
                }
            }
        }

        private void InsererLienInviteAllergene(SQLiteConnector connection, long idInvite, long idAllergene)
        {
            var parameters = new Dictionary<string, object>()
            {
                {"@IdInvite", idInvite },
                {"@IdAllergene", idAllergene }
            };
            connection.ExecuteQuery("INSERT INTO Invite_Allergene (IdInvite, IdAllergene) VALUES (@IdInvite, @IdAllergene)", parameters);
        }

        private void SupprimerAllergenesInvite(SQLiteConnector connection, long idInvite)
        {
            var parameters = new Dictionary<string, object>()
            {
                {"@IdInvite", idInvite }
            };
            connection.ExecuteQuery("DELETE FROM Invite_Allergene WHERE IdInvite = @IdInvite", parameters);
        }

        private List<NomAllergene> ObtenirAllergenesInvite(SQLiteConnector connection, long idInvite)
        {
            List<NomAllergene> allergenes = new List<NomAllergene>();
            var parameters = new Dictionary<string, object>() { { "@IdInvite", idInvite } };
            var dataTable = connection.ExecuteQuery("SELECT A.Nom FROM Allergene A JOIN Invite_Allergene IA ON A.IdAllergene = IA.IdAllergene WHERE IA.IdInvite = @IdInvite", parameters);

            foreach (DataRow? row in dataTable.Rows)
            {
                if (Enum.TryParse(row["Nom"].ToString(), true, out NomAllergene allergene))
                {
                    allergenes.Add(allergene);
                }
            }
            return allergenes;
        }
        #endregion

        #region Méthodes privées / Gestion des plats détestés et préférés
        private void AjouterPlatsDetestesInvite(SQLiteConnector connection, long idInvite, List<Plat>? platsDetestes)
        {
            if (platsDetestes != null && platsDetestes.Count > 0)
            {
                foreach (Plat plat in platsDetestes)
                {
                    if (plat.Id > 0)
                    {
                        var parameters = new Dictionary<string, object>() { { "@IdInvite", idInvite }, { "@IdPlat", plat.Id } };
                        connection.ExecuteQuery("INSERT INTO Invite_PlatDeteste (IdInvite, IdPlat) VALUES (@IdInvite, @IdPlat)", parameters);
                    }
                }
            }
        }

        private void SupprimerPlatsDetestesInvite(SQLiteConnector connection, long idInvite)
        {
            var parameters = new Dictionary<string, object>() { { "@IdInvite", idInvite } };
            connection.ExecuteQuery("DELETE FROM Invite_PlatDeteste WHERE IdInvite = @IdInvite", parameters);
        }

        private List<Plat> ObtenirPlatsDetestesInvite(SQLiteConnector connection, long idInvite)
        {
            List<Plat> plats = new List<Plat>();
            var parameters = new Dictionary<string, object>() { { "@IdInvite", idInvite } };
            var dataTable = connection.ExecuteQuery("SELECT P.IDPlat, P.Nom, P.Description, P.Categorie, P.Ingredients FROM Plat P JOIN Invite_PlatDeteste IPD ON P.IDPlat = IPD.IdPlat WHERE IPD.IdInvite = @IdInvite", parameters);

            foreach (DataRow? row in dataTable.Rows)
            {
                plats.Add(CreerPlatDepuisDataRow(row));
            }
            return plats;
        }

        private void AjouterPlatsPreferesInvite(SQLiteConnector connection, long idInvite, List<Plat>? platsPreferes)
        {
            if (platsPreferes != null && platsPreferes.Count > 0)
            {
                foreach (Plat plat in platsPreferes)
                {
                    if (plat.Id > 0)
                    {
                        var parameters = new Dictionary<string, object>() { { "@IdInvite", idInvite }, { "@IdPlat", plat.Id } };
                        connection.ExecuteQuery("INSERT INTO Invite_PlatPrefere (IdInvite, IdPlat) VALUES (@IdInvite, @IdPlat)", parameters);
                    }
                }
            }
        }

        private void SupprimerPlatsPreferesInvite(SQLiteConnector connection, long idInvite)
        {
            var parameters = new Dictionary<string, object>() { { "@IdInvite", idInvite } };
            connection.ExecuteQuery("DELETE FROM Invite_PlatPrefere WHERE IdInvite = @IdInvite", parameters);
        }

        private List<Plat> ObtenirPlatsPreferesInvite(SQLiteConnector connection, long idInvite)
        {
            List<Plat> plats = new List<Plat>();
            var parameters = new Dictionary<string, object>() { { "@IdInvite", idInvite } };
            var dataTable = connection.ExecuteQuery("SELECT P.IDPlat, P.Nom, P.Description, P.Categorie, P.Ingredients FROM Plat P JOIN Invite_PlatPrefere IPP ON P.IDPlat = IPP.IdPlat WHERE IPP.IdInvite = @IdInvite", parameters);

            foreach (DataRow? row in dataTable.Rows)
            {
                plats.Add(CreerPlatDepuisDataRow(row));
            }
            return plats;
        }
        #endregion

        #region Méthodes privées / Création d'objets & Utilitaires
        private Invite CreerInviteDepuisDataRow(SQLiteConnector connection, DataRow row)
        {
            long idInvite = (long)row["IDInvite"];
            List<NomAllergene> allergenes = ObtenirAllergenesInvite(connection, idInvite);
            List<Plat> platsDetestes = ObtenirPlatsDetestesInvite(connection, idInvite);
            List<Plat> platsPreferes = ObtenirPlatsPreferesInvite(connection, idInvite);

            return new Invite(
                idInvite,
                row["Nom"].ToString(),
                row["Prenom"].ToString(),
                row["NumTel"].ToString(),
                row["Mail"].ToString(),
                allergenes.Count > 0 ? allergenes : null,
                platsDetestes.Count > 0 ? platsDetestes : null,
                platsPreferes.Count > 0 ? platsPreferes : null
            );
        }

        private Plat CreerPlatDepuisDataRow(DataRow row)
        {
            CategoriePlat categorie = CategoriePlat.plat;
            Enum.TryParse(row["Categorie"].ToString(), true, out categorie);
            string? ingredients = row.Table.Columns.Contains("Ingredients") && row["Ingredients"] != DBNull.Value ? row["Ingredients"].ToString() : null;

            return new Plat((long)row["IDPlat"], row["Nom"].ToString(), row["Description"]?.ToString(), categorie, ingredients, null);
        }

        private bool VerifieAppartientGroupe(SQLiteConnector connection, long idInvite)
        {
            var parameters = new Dictionary<string, object>() { { "@IdInvite", idInvite } };
            var dataTable = connection.ExecuteQuery("SELECT COUNT(*) as NombreGroupes FROM Invite_Groupe WHERE IdInvite = @IdInvite", parameters);
            return dataTable.Rows.Count > 0 && Convert.ToInt32(dataTable.Rows[0]["NombreGroupes"]) > 0;
        }
        #endregion
    }
}