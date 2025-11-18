using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace METIER_Footies.Metier
{
    /// <summary>
    /// Représente un menu contenant une sélection de plats, identifiés par un identifiant unique et un nom.
    /// </summary>
    public class Menu
    {
        #region Attributs
        private long idMenu;
        private string nom;
        private List<Plat> plat;
        #endregion

        #region Propriétés
        /// <summary>
        /// Id du menu
        /// </summary>
        public long IdMenu
        {
            get => idMenu;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("L'ID doit être un entier positif");
                }
                idMenu = value;
            }
        }

        /// <summary>
        /// Nom du menu
        /// </summary>
        public string Nom
        {
            get => nom;
            set
            {
                nom = value;
            }
        }

        /// <summary>
        /// Liste des plats du menu
        /// </summary>
        public List<Plat> Plat
        {
            get => plat;
            set => plat = value;
        }


        #endregion

        #region Constructeurs
        /// <summary>
        /// COnstructeur pas copy d'un Menu en copiant les valeurs
        /// </summary>
        /// <param name="menu">L'instance Menu à copier. Ne doit pas être nulle.</param>
        public Menu(Menu menu)
        {
            this.nom = menu.nom;
            this.plat = menu.plat;
            this.idMenu = menu.idMenu;
        }

        /// <summary>
        /// Initialise un nouveau Menu
        /// </summary>
        public Menu()
        {
            this.nom = "";
            this.plat = new List<Plat>();
        }
        #endregion
    }
}
