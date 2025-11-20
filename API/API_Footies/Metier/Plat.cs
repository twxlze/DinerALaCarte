namespace API_Footies.Metier
{
    /// <summary>
    /// Classe représentant un plat
    /// </summary>
    public class Plat
    {
        #region --- Attributs ---
        private long id;
        private string nom;
        private string? description;
        private Enum.CategoriePlat categorie;
        private string? ingredients;
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
        public Enum.CategoriePlat Categorie
        {
            get { return categorie; }
            set { categorie = value; }
        }

        /// <summary>
        /// Retourne ou modifie les ingrédients du plat
        /// </summary>
        public string? Ingredients
        {
            get { return ingredients; }
            set { ingredients = value; }
        }

        #endregion

        /// <summary>
        /// Constructeur du plat
        /// </summary>
        /// <param name="nom">nom du plat</param>
        /// <param name="description">description du plat</param>
        /// <param name="categorie">categorie du plat</param>
        public Plat(long id, string nom, string? description, Enum.CategoriePlat categorie, string? ingredients = null)
        {
            this.id = id;
            this.nom = nom;
            this.description = description;
            this.categorie = categorie;
            this.ingredients = ingredients;
        }

        /// <summary>
        /// Constructeur par défaut du plat
        /// </summary>
        public Plat() { }
    }
}
