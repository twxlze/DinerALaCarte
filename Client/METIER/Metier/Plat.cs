using System;
using System.Collections.Generic;
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
        private string ?description;
        private CategoriePlat categorie;
        #endregion

        #region --- Enumérations ---
        public enum CategoriePlat { aperitif, entree, plat, dessert }
        #endregion

        #region --- Propriétés ---

        /// <summary>
        /// Retourne ou modifie l'id du plat
        /// </summary>
        public long Id
        {
            get { return id; }
            set {
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
            set {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Le nom ne peut pas être vide");
                }
                nom = value;
            }
        }

        /// <summary>
        /// Retourne ou modifie le prénom du plat
        /// </summary>
        public string Description
        {
            get { return description; }
            set {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("La description ne peut pas être vide");
                }
                description = value;
            }
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
        public Plat(long id, string nom, string description, CategoriePlat categorie)
        {
            this.id = id;
            this.nom = nom;
            this.description = description;
            this.categorie = categorie;
        }

    }
}
