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
        private List<Enum.NomAllergene>? allergenes;
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

        /// <summary>
        /// Retourne ou modifie la liste des allergènes du plat
        /// </summary>
        public List<Enum.NomAllergene>? Allergenes
        {
            get { return allergenes; }
            set { allergenes = value; }
        }
        #endregion

        /// <summary>
        /// Constructeur du plat
        /// </summary>
        /// <param name="nom">nom du plat</param>
        /// <param name="description">description du plat</param>
        /// <param name="categorie">categorie du plat</param>
        /// <param name="allergenes">liste des allergènes du plat</param>
        public Plat(long id, string nom, string? description, Enum.CategoriePlat categorie, string? ingredients, List<Enum.NomAllergene>? allergenes)
        {
            this.id = id;
            this.nom = nom;
            this.description = description;
            this.categorie = categorie;
            this.ingredients = ingredients;
            this.allergenes = allergenes;
        }

        /// <summary>
        /// Constructeur par défaut du plat
        /// </summary>
        public Plat() { }
    }
}
