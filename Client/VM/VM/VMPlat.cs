using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using METIER_Footies.Data;
using METIER_Footies.Enum;
using METIER_Footies.Metier;
using VM_Footies.VM_Element_Selectionne;
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
        private List<VMAllergene> allergenesListe;
        private bool estSelectionne;
        #endregion

        #region Evenement
        public event PropertyChangedEventHandler? PropertyChanged;
        #endregion

        #region Propriétés
        /// <summary>
        /// Plat associé au VMPlat
        /// </summary>
        public Plat Plat => plat;

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

        /// <summary>
        /// Liste de TOUS les allergènes (cochés ou non) pour l'affichage XAML
        /// </summary>
        public List<VMAllergene> AllergenesListe
        {
            get
            {
                return allergenesListe;
            }
            set
            {
                allergenesListe = value;
                Notify("AllergenesListe");
            }
        }

        /// <summary>
        /// Indique si le plat est sélectionné (pour les menus)
        /// </summary>
        public bool EstSelectionne
        {
            get { return estSelectionne; }
            set
            {
                if (estSelectionne != value)
                {
                    estSelectionne = value;
                    Notify("EstSelectionne");
                }
            }
        }
        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur d'un VMPlat à partir d'un Plat
        /// </summary>
        /// <param name="plat"> Le plat à utiliser </param>
        public VMPlat(Plat plat, bool estSelectionne = false)
        {
            this.plat = plat;
            this.estSelectionne = estSelectionne;
            InitialiserListeAllergenes();
        }

        /// <summary>
        /// Construit un VMPlat à partir d'un autre VMPlat (constructeur de copie)
        /// </summary>
        /// <param name="modele"> Le VMPlat à copier </param>
        public VMPlat(VMPlat modele) : this(new Plat(modele.Plat))
        {
            this.estSelectionne = modele.EstSelectionne;
        }

        /// <summary>
        /// Constructeur par défaut d'un VMPlat
        /// </summary>
        public VMPlat() : this(new Plat())
        {
        }

        #endregion

        #region Méthodes
        /// <summary>
        /// Prépare la liste de tous les allergènes possibles pour l'interface
        /// </summary>
        private void InitialiserListeAllergenes()
        {
            List<VMAllergene> listeTemp = new List<VMAllergene>();
            Array valeursEnum = Enum.GetValues(typeof(NomAllergene));

            foreach (NomAllergene allergene in valeursEnum)
            {
                bool estPresent = false;
                if (this.plat.Allergenes != null)
                {
                    estPresent = this.plat.Allergenes.Contains(allergene);
                }

                VMAllergene vmAllergene = new VMAllergene(allergene, estPresent);
                listeTemp.Add(vmAllergene);
            }

            this.allergenesListe = listeTemp;
        }

        /// <summary>
        /// Transfère les cases cochées de l'interface vers le modèle Plat
        /// À appeler avant d'envoyer le plat à la base de données.
        /// </summary>
        public void SauvegarderAllergenes()
        {
            List<NomAllergene> allergenesSelectionnes = new List<NomAllergene>();

            foreach (VMAllergene vm in this.allergenesListe)
            {
                if (vm.EstSelectionne)
                {
                    allergenesSelectionnes.Add(vm.Allergene);
                }
            }

            this.plat.Allergenes = allergenesSelectionnes;
        }

        private void Notify(string message)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(message));
        }
        #endregion
    }
}