using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using METIER_Footies.Data;
using METIER_Footies.Data.Interfaces;
using METIER_Footies.Metier;
using VM_Footies.VM_Element_Selectionne;

namespace VM_Footies.VM
{
    /// <summary>
    /// Représente un modèle de vue pour un menu, fournissant des fonctionnalités de liaison de données et de notification de modification de propriétés.
    /// </summary>
    public class VMMenu : INotifyPropertyChanged
    {

        #region Attributs
        private Menu menu;
        private ObservableCollection<VMPlatSelectionne> platsAperitif;
        private ObservableCollection<VMPlatSelectionne> platsEntree;
        private ObservableCollection<VMPlatSelectionne> platsPlat;
        private ObservableCollection<VMPlatSelectionne> platsDessert;
        #endregion

        #region Événement
        public event PropertyChangedEventHandler? PropertyChanged;
        #endregion

        #region Propriétés 
        /// <summary>
        /// Menu associée au VMMenu
        /// </summary>
        public Menu Menu => this.menu;

        /// <summary>
        // Nom du menu
        /// </summary>
        public string Nom
        {
            get => this.menu.Nom;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    menu.Nom = char.ToUpper(value[0]) + value.Substring(1);
                else
                    menu.Nom = value;
                this.Notify("Nom");
            }
        }

        public List<Plat> Plats
        {
            get => this.menu.Plat;
        }
        #endregion

        #region Propriétés pour les plats sélectionnables
        /// <summary>
        /// Plats apéritifs sélectionnables
        /// </summary>
        public ObservableCollection<VMPlatSelectionne> PlatsAperitif
        {
            get => platsAperitif;
            set
            {
                platsAperitif = value;
                Notify("PlatsAperitif");
            }
        }

        /// <summary>
        /// Plats entrées sélectionnables
        /// </summary>
        public ObservableCollection<VMPlatSelectionne> PlatsEntree
        {
            get => platsEntree;
            set
            {
                platsEntree = value;
                Notify("PlatsEntree");
            }
        }

        /// <summary>
        /// Plats principaux sélectionnables
        /// </summary>
        public ObservableCollection<VMPlatSelectionne> PlatsPlat
        {
            get => platsPlat;
            set
            {
                platsPlat = value;
                Notify("PlatsPlat");
            }
        }

        /// <summary>
        /// Plats desserts sélectionnables
        /// </summary>
        public ObservableCollection<VMPlatSelectionne> PlatsDessert
        {
            get => platsDessert;
            set
            {
                platsDessert = value;
                Notify("PlatsDessert");
            }
        }
        #endregion

        #region Constructeurs
        /// <summary>
        // Constructeur d'un VMMenu à partir d'un Menu
        /// </summary>
        /// <param name="Menu"> menu à copier </param>
        public VMMenu(Menu menu)
        {
            this.menu = menu;
            InitialiserCollections();
        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe VMMenu en copiant le menu à partir du modèle spécifié.
        /// </summary>
        /// <param name="modele">Le modèle contenant le menu à copier. Ne peut être nul.</param>
        public VMMenu(VMMenu modele)
        {
            this.menu = new Menu(modele.Menu);
            InitialiserCollections();
        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe VMMenu.
        /// </summary>
        public VMMenu()
        {
            this.menu = new Menu();
            InitialiserCollections();
        }

        /// <summary>
        /// Initialise les collections de plats sélectionnables
        /// </summary>
        private void InitialiserCollections()
        {
            this.platsAperitif = new ObservableCollection<VMPlatSelectionne>();
            this.platsEntree = new ObservableCollection<VMPlatSelectionne>();
            this.platsPlat = new ObservableCollection<VMPlatSelectionne>();
            this.platsDessert = new ObservableCollection<VMPlatSelectionne>();
        }
        #endregion*

        #region Méthodes
        /// <summary>
        // Notifie le changement d'une propriété
        /// </summary>
        /// <param name="message"> Nom de la propriété changée </param>
        private void Notify(string message)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(message));
        }

        /// <summary>
        /// Gère le changement de sélection d'un plat
        /// </summary>
        private void VmPlat_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "EstSelectionne")
            {
                SynchroniserPlatsSelectionnes();
            }
        }

        /// <summary>
        /// Met à jour la liste des plats du menu en fonction des sélections
        /// </summary>
        public void SynchroniserPlatsSelectionnes()
        {
            List<Plat> platsSelectionnes = new List<Plat>();
            SynchroniserPlatsAperitifs(platsSelectionnes);
            SynchroniserPlatsEntree(platsSelectionnes);
            SynchroniserPlatsPlats(platsSelectionnes);
            SynchroniserPlatsDesserts(platsSelectionnes);
            this.menu.Plat = platsSelectionnes;
            Notify("Plats");
        }

        private void SynchroniserPlatsAperitifs(List<Plat> platsSelectionnes)
        {
            foreach (VMPlatSelectionne vmPlat in PlatsAperitif)
            {
                if (vmPlat.EstSelectionne)
                {
                    platsSelectionnes.Add(vmPlat.Plat);
                }
            }
        }

        private void SynchroniserPlatsEntree(List<Plat> platsSelectionnes)
        {
            foreach (VMPlatSelectionne vmPlat in PlatsEntree)
            {
                if (vmPlat.EstSelectionne)
                {
                    platsSelectionnes.Add(vmPlat.Plat);
                }
            }
        }

        private void SynchroniserPlatsPlats(List<Plat> platsSelectionnes)
        {
            foreach (VMPlatSelectionne vmPlat in PlatsPlat)
            {
                if (vmPlat.EstSelectionne)
                {
                    platsSelectionnes.Add(vmPlat.Plat);
                }
            }
        }

        private void SynchroniserPlatsDesserts(List<Plat> platsSelectionnes)
        {
            foreach (VMPlatSelectionne vmPlat in PlatsDessert)
            {
                if (vmPlat.EstSelectionne)
                {
                    platsSelectionnes.Add(vmPlat.Plat);
                }
            }
        }

        /// <summary>
        /// Ajoute un gestionnaire d'événement pour un VMPlatSelectionne
        /// </summary>
        /// <param name="vmInvite">Le plat sélectionnable</param>
        public void GestionnaireEvenement(VMPlatSelectionne vmPlat)
        {
            vmPlat.PropertyChanged += VmPlat_PropertyChanged;
        }
        #endregion
    }
}
