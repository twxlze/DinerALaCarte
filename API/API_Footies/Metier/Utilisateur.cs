using System.Text.Json.Serialization;

namespace API_Footies.Metier
{
    /// <summary>
    /// Classe métier représentant l'utilisateur (pour la connexion)
    /// </summary>
    public class Utilisateur
    {
        #region Attribut
        private long id;
        private string pseudo;
        private string mdp;     
        private string mdpHash; 
        private string mdpSal;  
        #endregion
        #region Propriété
        /// <summary>
        /// get ou set l'id de l'utilisateur
        /// </summary>
        public long Id
        {
            get { return id; }
            set { id = value; }
        }
       /// <summary>
       /// get ou set le pseudo de l'utilisateur
       /// </summary>
        public string Pseudo
        {
            get { return pseudo; }
            set { pseudo = value; }
        }
        /// <summary>
        /// get ou set le mot de passe de l'utilisateur
        /// </summary>
        public string MotDePasse
        {
            get { return mdp; }
            set { mdp = value; }
        }
        /// <summary>
        /// Get ou set le Mot de passe hashée
        /// </summary>
        public string MotDePasseHash
        {
            get { return mdpHash; }
            set { mdpHash = value; }
        }
        /// <summary>
        /// Get ou set le sel du mot de passe
        /// </summary>
        public string MotDePasseSel
        {
            get { return mdpSal; }
            set { mdpSal = value; }
        }
        #endregion
        #region Constructeur
        /// <summary>
        /// Constructeur vide de utilisateur
        /// </summary>
        public Utilisateur() { 
        }
        #endregion
    }
}
