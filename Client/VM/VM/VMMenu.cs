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

namespace VM_Footies.VM
{
    /// <summary>
    /// Représente un modèle de vue pour un menu, fournissant des fonctionnalités de liaison de données et de notification de modification de propriétés.
    /// </summary>
    public class VMMenu : INotifyPropertyChanged
    {

        #region Attributs
        private Menu menu;
        private ObservableCollection<VMPlat> platsAperitif;
        private ObservableCollection<VMPlat> platsEntree;
        private ObservableCollection<VMPlat> platsPlat;
        private ObservableCollection<VMPlat> platsDessert;
        private bool estSelectionne;
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

        /// <summary>
        /// Liste des plats du menu
        /// </summary>
        public List<Plat> Plats
        {
            get => this.menu.Plat;
        }

        /// <summary>
        /// État de sélection du menu
        /// </summary>
        public bool EstSelectionne
        {
            get => estSelectionne;
            set
            {
                if (estSelectionne != value)
                {
                    estSelectionne = value;
                    Notify("MenuSelectionne");
                }
            }
        }
        #endregion

        #region Propriétés pour les plats sélectionnables
        /// <summary>
        /// Plats apéritifs sélectionnables
        /// </summary>
        public ObservableCollection<VMPlat> PlatsAperitif
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
        public ObservableCollection<VMPlat> PlatsEntree
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
        public ObservableCollection<VMPlat> PlatsPlat
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
        public ObservableCollection<VMPlat> PlatsDessert
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
        public VMMenu(Menu menu, bool estSelectionne = false)
        {
            this.menu = menu;
            this.estSelectionne = estSelectionne;
            InitialiserCollections();
        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe VMMenu en copiant le menu à partir du modèle spécifié.
        /// </summary>
        /// <param name="modele">Le modèle contenant le menu à copier. Ne peut être nul.</param>
        public VMMenu(VMMenu modele)
        {
            this.menu = new Menu(modele.Menu);
            this.estSelectionne = modele.estSelectionne;
            InitialiserCollections();
        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe VMMenu.
        /// </summary>
        public VMMenu() : this(new Menu())
        {
        }

        /// <summary>
        /// Initialise les collections de plats sélectionnables
        /// </summary>
        private void InitialiserCollections()
        {
            this.platsAperitif = new ObservableCollection<VMPlat>();
            this.platsEntree = new ObservableCollection<VMPlat>();
            this.platsPlat = new ObservableCollection<VMPlat>();
            this.platsDessert = new ObservableCollection<VMPlat>();
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
            foreach (VMPlat vmPlat in PlatsAperitif)
            {
                if (vmPlat.EstSelectionne)
                {
                    platsSelectionnes.Add(vmPlat.Plat);
                }
            }
        }

        private void SynchroniserPlatsEntree(List<Plat> platsSelectionnes)
        {
            foreach (VMPlat vmPlat in PlatsEntree)
            {
                if (vmPlat.EstSelectionne)
                {
                    platsSelectionnes.Add(vmPlat.Plat);
                }
            }
        }

        private void SynchroniserPlatsPlats(List<Plat> platsSelectionnes)
        {
            foreach (VMPlat vmPlat in PlatsPlat)
            {
                if (vmPlat.EstSelectionne)
                {
                    platsSelectionnes.Add(vmPlat.Plat);
                }
            }
        }

        private void SynchroniserPlatsDesserts(List<Plat> platsSelectionnes)
        {
            foreach (VMPlat vmPlat in PlatsDessert)
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
        public void GestionnaireEvenement(VMPlat vmPlat)
        {
            vmPlat.PropertyChanged += VmPlat_PropertyChanged;
        }
        #endregion
    }
}
