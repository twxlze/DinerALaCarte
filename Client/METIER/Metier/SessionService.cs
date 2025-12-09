using System;

namespace METIER_Footies.Metier
{
    public class SessionService
    {
        #region Attributs
        private static SessionService instance = null;
        #endregion
        #region Propriétés
        /// <summary>
        /// Accède à l'utilisateur connecté
        /// </summary>
        public Utilisateur UtilisateurConnecte { get; set; }
        /// <summary>
        /// Accède à l'instance unique de la SessionService
        /// </summary>
        public static SessionService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SessionService();
                }
                return instance;
            }
        }
        #endregion
        #region Constructeurs
        /// <summary>
        /// Constructeur privé pour le singleton
        /// </summary>
        private SessionService()
        {
        }
        #endregion
        #region Méthodes
        /// <summary>
        /// Méthode pour vérifier si un utilisateur est connecté
        /// </summary>
        public bool EstConnecte
        {
            get { return UtilisateurConnecte != null; }
        }
        #endregion
    }
}