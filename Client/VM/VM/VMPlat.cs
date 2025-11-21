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
        private ObservableCollection<VMAllergeneSelectionne> allergenesListe;
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
        /// Liste des allergènes du plat
        /// </summary>
        public List<NomAllergene>? Allergenes
        {
            get => plat.Allergenes;
        }

        /// <summary>
        /// Liste observable des allergènes sélectionnables
        /// </summary>
        public ObservableCollection<VMAllergeneSelectionne> AllergenesListe
        {
            get => allergenesListe;
            set
            {
                allergenesListe = value;
                Notify("AllergenesListe");
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
            this.allergenesListe = new ObservableCollection<VMAllergeneSelectionne>();
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
            //AllergenesListe = plat.AllergenesListe;
        }
        #endregion

        /// <summary>
        /// Synchronise la liste des allergènes sélectionnés avec le modèle
        /// </summary>
        /// <param name="sender">L'expéditeur</param>
        /// <param name="e">Les arguments de l'événement</param>
        private void VmAllergene_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "AllergeneSelectionne")
            {
                SynchroniserAllergenesSelectionnes();
            }
        }

        /// <summary>
        /// Met à jour la liste des allergènes sélectionnés dans le modèle
        /// </summary>
        public void SynchroniserAllergenesSelectionnes()
        {
            List<NomAllergene> allergenesSelectionnes = new List<NomAllergene>();
            foreach (VMAllergeneSelectionne vmAllergene in this.allergenesListe)
            {
                if (vmAllergene.EstSelectionne)
                {
                    allergenesSelectionnes.Add(vmAllergene.Allergene);
                }
            }
            this.plat.Allergenes = allergenesSelectionnes;
            Notify("Allergenes");
        }

        /// <summary>
        /// Ajoute un gestionnaire d'événement pour un VMAllergeneSelectionne
        /// </summary>
        /// <param name="vmAllergene">L'allergène sélectionnable</param>
        public void GestionnaireEvenement(VMAllergeneSelectionne vmAllergene)
        {
            vmAllergene.PropertyChanged += VmAllergene_PropertyChanged;
        }
    }
}