using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace METIER_Footies.Metier
{
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
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("l'id ne peux pas être négatif");
                }
                id = value;
            }
        }


        /// <summary>
        /// Retourne ou modifie le nom du plat
        /// </summary>
        public string Nom
        {
            get { return nom; }
            set
            {

                nom = value;

            }
        }

        /// <summary>
        /// Retourne ou modifie le prénom du plat
        /// </summary>
        public string Description
        {
            get { return description; }
            set
            {

                description = value;
            }
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

        #region Constructeurs
        /// <summary>
        /// Constructeur du plat
        /// </summary>
        /// <param name="nom">nom du plat</param>
        /// <param name="description">description du plat</param>
        /// <param name="categorie">categorie du plat</param>
        public Plat(long id, string nom, string description, Enum.CategoriePlat categorie, string? ingredients = null, List<Enum.NomAllergene>? allergenes = null)
        {
            this.id = id;
            this.nom = nom;
            this.description = description;
            this.categorie = categorie;
            this.ingredients = ingredients;
            this.allergenes = allergenes;
        }

        /// <summary>
        /// Constructeur de copie d'un invité
        /// </summary>
        /// <param name="invite"> L'invité à copier </param>
        public Plat(Plat plat)
        {
            this.id = plat.id;
            this.nom = plat.nom;
            this.description = plat.description;
            this.categorie = plat.categorie;
            this.ingredients = plat.ingredients;
            this.allergenes = plat.allergenes;
        }

        public Plat()
        {
            this.nom = "";
            this.description = "";
            this.categorie = Enum.CategoriePlat.entree;
            this.ingredients = "";
            this.allergenes = new List<Enum.NomAllergene>();
        }
        #endregion
    }
}