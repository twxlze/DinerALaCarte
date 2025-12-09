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

        #region Méthodes publiques
        public bool AjouterGroupeInvites(GroupeInvites groupeInvites, long IdUtilisateur)
        {
            using (SQLiteConnector connection = new SQLiteConnector())
            {
                if (connection == null)
                {
                    throw new Exception("Erreur de connexion à la base de données");
                }

                InsererGroupeInvites(connection, groupeInvites, IdUtilisateur);
                AjouterInvitesDansGroupe(connection, groupeInvites);
                return true;
            }
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

                Dictionary<string, object> parameters = new Dictionary<string, object>()
                {
                    {"@IdUtilisateur", IdUtilisateur }
                };

                DataTable dataTable = connection.ExecuteQuery("SELECT * FROM GroupeInvite WHERE IdUtilisateur = @IdUtilisateur", parameters);

                foreach (DataRow? row in dataTable.Rows)
                {
                    GroupeInvites groupeInvite = CreerGroupeInvitesDepuisDataRow(connection, row);
                    listeGroupesInvites.Add(groupeInvite);
                }
            }
            return listeGroupesInvites;
        }

        public bool ModifierGroupe(GroupeInvites groupeInvite, long IdUtilisateur)
        {
            using (SQLiteConnector connection = new SQLiteConnector())
            {
                if (connection == null)
                {
                    throw new Exception("Erreur de connexion à la base de données");
                }

                MettreAJourGroupeInvites(connection, groupeInvite, IdUtilisateur);
                SupprimerInvitesDuGroupe(connection, groupeInvite.IdGroupeInvites);
                AjouterInvitesDansGroupe(connection, groupeInvite);
                return true;
            }
        }

        public void SupprimerGroupeInvite(long idGroupeInvite, long IdUtilisateur)
        {
            using (SQLiteConnector connection = new SQLiteConnector())
            {
                if (connection == null)
                {
                    throw new Exception("Erreur de connexion à la base de données");
                }

                SupprimerInvitesDuGroupe(connection, idGroupeInvite);
                SupprimerGroupeInvitesParId(connection, idGroupeInvite, IdUtilisateur);
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

                DataTable dataTable = RechercherGroupesInvitesParTexte(connection, GroupeInvitesRechercher, IdUtilisateur);

                foreach (DataRow? row in dataTable.Rows)
                {
                    GroupeInvites groupeInvite = CreerGroupeInvitesDepuisDataRow(connection, row);
                    listeGroupeInvites.Add(groupeInvite);
                }
            }
            return listeGroupeInvites;
        }
        #endregion

        #region Méthodes privées / Gestion du groupe d'invités

        /// <summary>
        /// Insère un groupe d'invités et met à jour son ID (Avec IdUtilisateur)
        /// </summary>
        private void InsererGroupeInvites(SQLiteConnector connection, GroupeInvites groupeInvites, long idUtilisateur)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                {"@Nom", groupeInvites.Nom },
                {"@IdUtilisateur", idUtilisateur }
            };
            groupeInvites.IdGroupeInvites = connection.ExecuteInsert("INSERT INTO GroupeInvite (Nom, IdUtilisateur) VALUES (@Nom, @IdUtilisateur)", parameters);
        }

        /// <summary>
        /// Met à jour les informations d'un groupe d'invités (Sécurisé par IdUtilisateur)
        /// </summary>
        private void MettreAJourGroupeInvites(SQLiteConnector connection, GroupeInvites groupeInvite, long idUtilisateur)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                {"@IdGroupeInvite", groupeInvite.IdGroupeInvites },
                {"@Nom", groupeInvite.Nom },
                {"@IdUtilisateur", idUtilisateur }
            };
            connection.ExecuteQuery("UPDATE GroupeInvite SET Nom = @Nom WHERE IdGroupeInvite = @IdGroupeInvite AND IdUtilisateur = @IdUtilisateur", parameters);
        }

        /// <summary>
        /// Supprime un groupe d'invités par son ID (Sécurisé par IdUtilisateur)
        /// </summary>
        private void SupprimerGroupeInvitesParId(SQLiteConnector connection, long idGroupeInvite, long idUtilisateur)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                {"@IdGroupeInvite", idGroupeInvite },
                {"@IdUtilisateur", idUtilisateur }
            };
            connection.ExecuteQuery("DELETE FROM GroupeInvite WHERE IdGroupeInvite = @IdGroupeInvite AND IdUtilisateur = @IdUtilisateur", parameters);
        }

        /// <summary>
        /// Recherche des groupes d'invités par nom (Filtré par IdUtilisateur)
        /// </summary>
        private DataTable RechercherGroupesInvitesParTexte(SQLiteConnector connection, string texteRecherche, long idUtilisateur)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                {"@Texte", $"%{texteRecherche}%" },
                {"@IdUtilisateur", idUtilisateur }
            };
            return connection.ExecuteQuery("SELECT * FROM GroupeInvite WHERE Nom LIKE @Texte AND IdUtilisateur = @IdUtilisateur", parameters);
        }
        #endregion

        #region Méthodes privées / Gestion des invités du groupe
        /// <summary>
        /// Ajoute les invités dans un groupe
        /// </summary>
        private void AjouterInvitesDansGroupe(SQLiteConnector connection, GroupeInvites groupeInvites)
        {
            if (groupeInvites.Invites != null && groupeInvites.Invites.Count > 0)
            {
                foreach (Invite invite in groupeInvites.Invites)
                {
                    InsererLienGroupeInvite(connection, groupeInvites.IdGroupeInvites, invite.Id);
                }
            }
        }

        /// <summary>
        /// Insère le lien entre un groupe et un invité
        /// </summary>
        private void InsererLienGroupeInvite(SQLiteConnector connection, long idGroupeInvite, long idInvite)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                {"@IdGroupeInvite", idGroupeInvite },
                {"@IdInvite", idInvite }
            };
            connection.ExecuteQuery("INSERT INTO Invite_Groupe (IdInvite, IdGroupeInvite) VALUES (@IdInvite, @IdGroupeInvite)", parameters);
        }

        /// <summary>
        /// Supprime tous les invités d'un groupe
        /// </summary>
        private void SupprimerInvitesDuGroupe(SQLiteConnector connection, long idGroupeInvite)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                {"@IdGroupeInvite", idGroupeInvite }
            };
            connection.ExecuteQuery("DELETE FROM Invite_Groupe WHERE IdGroupeInvite = @IdGroupeInvite", parameters);
        }

        /// <summary>
        /// Obtient la liste des invités d'un groupe
        /// </summary>
        private List<Invite> ObtenirInvitesDuGroupe(SQLiteConnector connection, long idGroupeInvite)
        {
            List<Invite> invites = new List<Invite>();

            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                {"@IdGroupeInvite", idGroupeInvite }
            };

            DataTable dataTable = connection.ExecuteQuery("SELECT i.IDInvite, i.Prenom, i.Nom, i.NumTel, i.Mail FROM Invite i JOIN Invite_Groupe ig ON i.IDInvite = ig.IdInvite WHERE ig.IdGroupeInvite = @IdGroupeInvite", parameters);

            foreach (DataRow? row in dataTable.Rows)
            {
                Invite invite = CreerInviteDepuisDataRow(connection, row);
                invites.Add(invite);
            }
            return invites;
        }
        #endregion

        #region Méthodes privées / Création d'objets
        /// <summary>
        /// Crée un objet GroupeInvites à partir d'une ligne de données
        /// </summary>
        private GroupeInvites CreerGroupeInvitesDepuisDataRow(SQLiteConnector connection, DataRow row)
        {
            long idGroupeInvite = (long)row["IDGroupeInvite"];
            string nom = row["Nom"].ToString();
            List<Invite> invites = ObtenirInvitesDuGroupe(connection, idGroupeInvite);
            return new GroupeInvites(idGroupeInvite, nom, invites);
        }

        /// <summary>
        /// Crée un objet Invite à partir d'une ligne de données
        /// </summary>
        private Invite CreerInviteDepuisDataRow(SQLiteConnector connection, DataRow row)
        {
            long idInvite = (long)row["IDInvite"];
            return new Invite(idInvite, row["Nom"].ToString(), row["Prenom"].ToString(), row["NumTel"].ToString(), row["Mail"].ToString(), null, null, null);
        }
        #endregion
    }
}