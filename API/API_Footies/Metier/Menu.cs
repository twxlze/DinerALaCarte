namespace API_Footies.Metier
{
    /// <summary>
    /// Classe représentant un menu
    /// </summary>
    public class Menu
    {
        #region --- Attributs ---
        private List<Plat> plat = new List<Plat>();
        private long idMenu;
        private string nom;
        #endregion

        #region --- Propriétés ---
        /// <summary>
        /// Retourne ou modifie la liste des plats du menu
        /// </summary>
        public List<Plat> Plat
        {
            get { return plat; }
            set { plat = value; }
        }
        /// <summary>
        /// Retourne ou modifie l'id du menu
        /// </summary>
        public long IdMenu
        {
            get { return idMenu; }
            set { idMenu = value; }
        }
        /// <summary>
        /// Retourne ou modifie le nom du groupe d'invités
        /// </summary>
        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }

        /// <summary>
        /// Constructeur du menu
        /// </summary>
        public Menu(List<Plat> plat, long idMenu, string nom)
        {
            this.idMenu = idMenu;
            this.nom = nom;
            this.plat = plat;
        }
        #endregion
    }
}
