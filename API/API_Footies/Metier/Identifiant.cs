namespace API_Footies.Metier
{
    /// <summary>
    /// Classe caractérisant les identifiants d'un utilisateur (mdp , pseudo et id)
    /// </summary>
    public class Identifiant
    {
        #region Attributs
        private long id;
        private string pseudo;
        private string motDePasse;
        private string? motDePasseHash;
        #endregion
        #region Propriétés
        /// <summary>
        /// Retourne ou modifie l'id de l'utilisateur
        /// </summary>
        public long Id
        {
            get { return id; }
            set { id = value; }
        }
        /// <summary>
        /// Retourne ou modifie le pseudo de l'utilisateur
        /// </summary>
        public string Pseudo
        {
            get { return pseudo; }
            set { pseudo = value; }
        }
        /// <summary>
        /// Retourne ou modifie le mot de passe de l'utilisateur
        /// </summary>
        public string MotDePasse
        {
            get { return motDePasse; }
            set { motDePasse = value; }
        }

        /// <summary>
        /// Mot de passe hashé (
        /// </summary>
        public string? MotDePasseHash
        {
            get { return motDePasseHash; }
            set { motDePasseHash = value; }
        }
        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur d'un utilisateur
        /// </summary>
        /// 
        public Identifiant()
        {
        }
        #endregion
    }
}
