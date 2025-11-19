using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using METIER_Footies.Data;
using METIER_Footies.Metier;
using static METIER_Footies.Metier.Plat;

namespace VM_Footies.VM
{
    /// <summary>
    /// Classe ViewModel pour un plat
    /// </summary>
    public class VMPlat : INotifyPropertyChanged
    {
        #region Attributs
        private Plat plat;
        private OpenFoodFactsDAO openFoodFactsDAO;
        private List<string> suggestionsIngredients;
        #endregion

        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Plat associé au VMPlat
        /// </summary>
        public Plat Plat => plat;

        #region Propriétés
        /// <summary>
        /// Id du plat
        /// </summary>
        public long Id
        {
            get { return plat.Id; }
        }

        /// <summary>
        /// Nom du plat
        /// </summary>
        /// <remarks> Le set notifie le changement de la propriété </remarks>
        public string Nom
        {
            get { return plat.Nom; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    plat.Nom = char.ToUpper(value[0]) + value.Substring(1);
                }
                else
                {
                    plat.Nom = value;
                }
                Notify("Nom");
            }
        }

        /// <summary>
        /// Description d'un plat
        /// </summary>
        /// <remarks> Le set notifie le changement de la propriété </remarks>
        public string Description
        {
            get { return plat.Description; }
            set
            {
                plat.Description = value;
                Notify("Description");
            }
        }

        /// <summary>
        /// Catégorie du plat
        /// </summary>
        /// <remarks> Le set notifie le changement de la propriété </remarks>
        public CategoriePlat Categorie
        {
            get { return plat.Categorie; }
            set
            {
                plat.Categorie = value;
                Notify("Categorie");
            }
        }

        /// <summary>
        /// Ingrédients du plat
        /// </summary>
        public string? Ingredients
        {
            get { return plat.Ingredients; }
            set
            {
                plat.Ingredients = value;
                Notify("Ingredients");
            }
        }

        /// <summary>
        /// Liste des suggestions d'ingrédients
        /// </summary>
        public List<string> SuggestionsIngredients
        {
            get { return suggestionsIngredients; }
            set
            {
                suggestionsIngredients = value;
                Notify("SuggestionsIngredients");
            }
        }

        /// <summary>
        /// Index de la catégorie pour le ComboBox
        /// </summary>
        public int CategorieIndex
        {
            get
            {
                return (int)plat.Categorie;
            }
            set
            {
                plat.Categorie = (CategoriePlat)value;
                Notify("CategorieIndex");
                Notify("Categorie");
            }
        }
        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur d'un VMPlat à partir d'un Plat
        /// </summary>
        /// <param name="plat"> Le plat à utiliser </param>
        public VMPlat(Plat plat)
        {
            this.plat = plat;
            InitialiserDAO();
        }

        /// <summary>
        /// Construit un VMPlat à partir d'un autre VMPlat (constructeur de copie)
        /// </summary>
        /// <param name="modele"> Le VMPlat à copier </param>
        public VMPlat(VMPlat modele) : this(new Plat(modele.Plat))
        {
        }

        /// <summary>
        /// Constructeur par défaut d'un VMPlat
        /// </summary>
        public VMPlat() : this(new Plat())
        {
        }

        /// <summary>
        /// Initialise le DAO et les collections
        /// </summary>
        private void InitialiserDAO()
        {
            this.openFoodFactsDAO = new OpenFoodFactsDAO();
            this.suggestionsIngredients = new List<string>();
        }
        #endregion

        #region Méthodes
        /// <summary>
        /// Notifie le changement d'une propriété
        /// </summary>
        /// <param name="message"> Nom de la propriété changée </param>
        private void Notify(string message)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(message));
        }

        /// <summary>
        /// Modifie les informations du plat
        /// </summary>
        /// <param name="plat"> le plat avec les nouvelles informations </param>
        public void ModifierPlat(VMPlat plat)
        {
            Nom = plat.Nom;
            Description = plat.Description;
            Categorie = plat.Categorie;
            Ingredients = plat.Ingredients;
        }

        /// <summary>
        /// Recherche des suggestions d'ingrédients
        /// </summary>
        /// <param name="recherche">Texte de recherche</param>
        public async Task RechercherSuggestionsIngredients(string recherche)
        {
            List<string> suggestions = new List<string>();
            if (!string.IsNullOrWhiteSpace(recherche) && recherche.Length >= 2)
            {
                try
                {
                    suggestions = await openFoodFactsDAO.RechercherIngredients(recherche);
                }
                catch (Exception ex)
                {
                    throw new Exception("Erreur lors de la recherche des suggestions d'ingrédients : " + ex.Message);
                }
            }
            this.suggestionsIngredients = suggestions;
            Notify("SuggestionsIngredients");
        }
        #endregion
    }
}