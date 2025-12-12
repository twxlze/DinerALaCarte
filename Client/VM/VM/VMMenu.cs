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

        // pour l'affichage  
        private ObservableCollection<VMPlat> platsAperitif;
        private ObservableCollection<VMPlat> platsEntree;
        private ObservableCollection<VMPlat> platsPlat;
        private ObservableCollection<VMPlat> platsDessert;

        // pour la recherche
        private List<VMPlat> sauvegardeAperitif;
        private List<VMPlat> sauvegardeEntree;
        private List<VMPlat> sauvegardePlat;
        private List<VMPlat> sauvegardeDessert;

        private string texteRechercheAperitif;
        private string texteRechercheEntree;
        private string texteRecherchePlat;
        private string texteRechercheDessert;

        private bool estSelectionne;
        private string texteRecherche;
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

        /// <summary>
        /// Texte de recherche pour filtrer les plats
        /// </summary>
        public string TexteRecherche
        {
            get => texteRecherche;
            set
            {
                texteRecherche = value;
                Notify("TexteRecherche");
            }
        }
        #endregion

        #region Propriétés - Textes de recherche
        /// <summary>
        /// Texte de recherche pour les plats apéritifs
        /// </summary>
        public string TexteRechercheAperitif
        {
            get => texteRechercheAperitif;
            set 
            {
                texteRechercheAperitif = value;
                Notify("TexteRechercheAperitif");
            }
        }

        /// <summary>
        /// Texte de recherche pour les plats entrées
        /// </summary>
        public string TexteRechercheEntree
        {
            get => texteRechercheEntree;
            set 
            { 
                texteRechercheEntree = value; 
                Notify("TexteRechercheEntree");
            }
        }

        /// <summary>
        /// Texte de recherche pour les plats principaux
        /// </summary>
        public string TexteRecherchePlat
        {
            get => texteRecherchePlat;
            set 
            { 
                texteRecherchePlat = value; 
                Notify("TexteRecherchePlat");
            }
        }

        /// <summary>
        /// Texte de recherche pour les plats desserts
        /// </summary>
        public string TexteRechercheDessert
        {
            get => texteRechercheDessert;
            set {
                texteRechercheDessert = value;
                Notify("TexteRechercheDessert");
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
            Initialiser();
        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe VMMenu en copiant le menu à partir du modèle spécifié.
        /// </summary>
        /// <param name="modele">Le modèle contenant le menu à copier. Ne peut être nul.</param>
        public VMMenu(VMMenu modele)
        {
            this.menu = new Menu(modele.Menu);
            this.estSelectionne = modele.estSelectionne;
            Initialiser();
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
        private void Initialiser()
        {
            this.platsAperitif = new ObservableCollection<VMPlat>();
            this.platsEntree = new ObservableCollection<VMPlat>();
            this.platsPlat = new ObservableCollection<VMPlat>();
            this.platsDessert = new ObservableCollection<VMPlat>();

            this.sauvegardeAperitif = new List<VMPlat>();
            this.sauvegardeEntree = new List<VMPlat>();
            this.sauvegardePlat = new List<VMPlat>();
            this.sauvegardeDessert = new List<VMPlat>();
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
            List<Plat> tousLesPlatsSelectionnes = new List<Plat>();
            AjouterPlatsSelectionnes(tousLesPlatsSelectionnes, sauvegardeAperitif, platsAperitif);
            AjouterPlatsSelectionnes(tousLesPlatsSelectionnes, sauvegardeEntree, platsEntree);
            AjouterPlatsSelectionnes(tousLesPlatsSelectionnes, sauvegardePlat, platsPlat);
            AjouterPlatsSelectionnes(tousLesPlatsSelectionnes, sauvegardeDessert, platsDessert);

            this.menu.Plat = tousLesPlatsSelectionnes;
            Notify("Plats");
        }

        private void AjouterPlatsSelectionnes(List<Plat> listeGlobale, List<VMPlat> sauvegarde, ObservableCollection<VMPlat> listeCourante)
        {
            IEnumerable<VMPlat> source;
            if (sauvegarde != null && sauvegarde.Count > 0)
                source = sauvegarde;
            else
                source = listeCourante;

            foreach (VMPlat vmPlat in source)
            {
                if (vmPlat.EstSelectionne)
                {
                    listeGlobale.Add(vmPlat.Plat);
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

        #region Méthodes - recherche (sauvegarde)

        /// <summary>
        /// Appelé une fois que les données sont chargées pour initialiser les listes de référence
        /// </summary>
        public void InitialiserSauvegardes()
        {
            this.sauvegardeAperitif = new List<VMPlat>(PlatsAperitif);
            this.sauvegardeEntree = new List<VMPlat>(PlatsEntree);
            this.sauvegardePlat = new List<VMPlat>(PlatsPlat);
            this.sauvegardeDessert = new List<VMPlat>(PlatsDessert);
        }

        /// <summary>
        /// Recherche des plats apéritifs correspondant au texte donné
        /// </summary>
        /// <param name="texte"> Texte de recherche </param>
        public void RechercherAperitif(string texte)
        {
            if (string.IsNullOrWhiteSpace(texte))
                PlatsAperitif = new ObservableCollection<VMPlat>(sauvegardeAperitif);
            else
            {
                var resultats = sauvegardeAperitif.Where(p => p.Nom.Contains(texte, StringComparison.OrdinalIgnoreCase)).OrderBy(p => p.Nom).ToList();
                PlatsAperitif = new ObservableCollection<VMPlat>(resultats);
            }
        }

        /// <summary>
        /// Recherche des plats entrées correspondant au texte donné
        /// </summary>
        /// <param name="texte"> Texte de recherche </param>
        public void RechercherEntree(string texte)
        {
            if (string.IsNullOrWhiteSpace(texte))
                PlatsEntree = new ObservableCollection<VMPlat>(sauvegardeEntree);
            else
            {
                var resultats = sauvegardeEntree.Where(p => p.Nom.Contains(texte, StringComparison.OrdinalIgnoreCase)).OrderBy(p => p.Nom).ToList();
                PlatsEntree = new ObservableCollection<VMPlat>(resultats);
            }
        }

        /// <summary>
        /// Recherche des plats principaux correspondant au texte donné
        /// </summary>
        /// <param name="texte"> Texte de recherche </param>
        public void RechercherPlatPrincipal(string texte)
        {
            if (string.IsNullOrWhiteSpace(texte))
                PlatsPlat = new ObservableCollection<VMPlat>(sauvegardePlat);
            else
            {
                var resultats = sauvegardePlat.Where(p => p.Nom.Contains(texte, StringComparison.OrdinalIgnoreCase)).OrderBy(p => p.Nom).ToList();
                PlatsPlat = new ObservableCollection<VMPlat>(resultats);
            }
        }

        /// <summary>
        /// Recherche des plats desserts correspondant au texte donné
        /// </summary>
        /// <param name="texte"> Texte de recherche </param>
        public void RechercherDessert(string texte)
        {
            if (string.IsNullOrWhiteSpace(texte))
                PlatsDessert = new ObservableCollection<VMPlat>(sauvegardeDessert);
            else
            {
                var resultats = sauvegardeDessert.Where(p => p.Nom.Contains(texte, StringComparison.OrdinalIgnoreCase)).OrderBy(p => p.Nom).ToList();
                PlatsDessert = new ObservableCollection<VMPlat>(resultats);
            }
        }
        #endregion
    }
}
