namespace API_Footies.Metier
{
    public class Plat
    {
        #region --- Attributs ---
        private long id;
        private string nom;
        private string? description;
        private CategoriePlat categorie;
        #endregion

        #region --- Enumérations ---
        public enum CategoriePlat { apéritif, entrée, plat, dessert }
        #endregion

        #region --- Propriétés ---

        /// <summary>
        /// Retourne ou modifie l'id du plat
        /// </summary>
        public long Id
        {
            get { return id; }
            set { id = value; }
        }


        /// <summary>
        /// Retourne ou modifie le nom du plat
        /// </summary>
        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }

        /// <summary>
        /// Retourne ou modifie le prénom du plat
        /// </summary>
        public string? Description
        {
            get { return description; }
            set { description = value; }
        }

        /// <summary>
        /// Retourne ou modifie la catégorie du plat
        /// </summary>
        public CategoriePlat Categorie
        {
            get { return categorie; }
            set { categorie = value; }
        }

        #endregion

        /// <summary>
        /// Constructeur du plat
        /// </summary>
        /// <param name="nom">nom du plat</param>
        /// <param name="description">description du plat</param>
        /// <param name="categorie">categorie du plat</param>
        public Plat(long id, string nom, string? description, CategoriePlat categorie)
        {
            this.id = id;
            this.nom = nom;
            this.description = description;
            this.categorie = categorie;
        }

        public Plat() { }
    }
}
