using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VM_Footies;
using VM_Footies.VM;
using VM_Footies.VM_Page;

namespace IHM_Footies.Menu
{
    /// <summary>
    /// Logique d'interaction pour VuePageMenuDetail.xaml
    /// </summary>
    public partial class VuePageMenuDetail : Window
    {
        #region Attributs 
        private VMPageMenu vmPageMenu;
        private VMMenu menuSelectionne;
        private string provenance;
        private VMInvitation invitationPrecedente;
        private VMPagePlat vmPagePlat;
        private List<VuePlat> vuePlat = new List<VuePlat>();
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur de la vue de détail d'un menu
        /// </summary>
        /// <param name="menu">Le menu à afficher</param>
        public VuePageMenuDetail(VMMenu menu, string provenance = "Menu", VMInvitation invitationPrecedente = null)
        {
            this.provenance = provenance;
            this.invitationPrecedente = invitationPrecedente;
            InitializeComponent();
            this.Initialiser(menu);
            this.ChargerPlats();
        }
        #endregion

        #region Méthodes
        /// <summary>
        /// Initialise les propriétés et les abonnements de la vue
        /// </summary>
        /// <param name="menu">Le menu à afficher</param>
        private void Initialiser(VMMenu menu)
        {
            this.menuSelectionne = menu;

            this.vmPageMenu = new VMPageMenu();
            this.vmPageMenu.MenuSelectionne = menu;
            this.DataContext = this.vmPageMenu;
            this.vmPagePlat = new VMPagePlat();

            this.vmPageMenu.PropertyChanged += VMPageMenu_PropertyChanged;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        #endregion

        #region Méthodes
        /// <summary>
        /// Charge les plats du menu et rafraîchit l'affichage
        /// </summary>
        private async void ChargerPlats()
        {
            try
            {
                await this.vmPageMenu.ChargerPlatsDansMenu(this.menuSelectionne);
                this.RafraichirListes();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des plats : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Gère les changements de propriétés du ViewModel
        /// </summary>
        private void VMPageMenu_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "VMMenu")
            {
                this.RafraichirListes();
            }
        }

        /// <summary>
        /// Rafraîchit toutes les listes de plats
        /// </summary>
        private void RafraichirListes()
        {
            this.RafraichirListeAperitifs();
            this.RafraichirListeEntrees();
            this.RafraichirListePlats();
            this.RafraichirListeDesserts();
        }

        /// <summary>
        /// Rafraîchit la liste des apéritifs
        /// </summary>
        private void RafraichirListeAperitifs()
        {
            this.PanelAperitif.Children.Clear();

            if (this.vmPageMenu.ListeVMPlatAperitif != null)
            {
                foreach (VMPlat vmPlat in this.vmPageMenu.ListeVMPlatAperitif)
                {
                    VuePlat vue = new VuePlat(vmPlat);
                    vue.MouseDown += (s, e) => this.SelectionnerPlat(vue);
                    vue.MouseDoubleClick += (s, e) => this.OuvrirDetailPlat(vue);
                    this.vuePlat.Add(vue);
                    this.PanelAperitif.Children.Add(vue);
                }
            }

            if (this.PanelAperitif.Children.Count == 0)
            {
                TextBlock textBlockVide = CreerTextBlockVide("Aucun apéritif");
                this.PanelAperitif.Children.Add(textBlockVide);
            }
        }

        /// <summary>
        /// Rafraîchit la liste des entrées
        /// </summary>
        private void RafraichirListeEntrees()
        {
            this.PanelEntree.Children.Clear();

            if (this.vmPageMenu.ListeVMPlatEntree != null)
            {
                foreach (VMPlat vmPlat in this.vmPageMenu.ListeVMPlatEntree)
                {
                    VuePlat vue = new VuePlat(vmPlat);
                    vue.MouseDown += (s, e) => this.SelectionnerPlat(vue);
                    vue.MouseDoubleClick += (s, e) => this.OuvrirDetailPlat(vue);
                    this.vuePlat.Add(vue);
                    this.PanelEntree.Children.Add(vue);
                }
            }

            if (this.PanelEntree.Children.Count == 0)
            {
                TextBlock textBlockVide = CreerTextBlockVide("Aucune entrée");
                this.PanelEntree.Children.Add(textBlockVide);
            }
        }

        /// <summary>
        /// Rafraîchit la liste des plats
        /// </summary>
        private void RafraichirListePlats()
        {
            this.PanelPlat.Children.Clear();

            if (this.vmPageMenu.ListeVMPlatPlat != null)
            {
                foreach (VMPlat vmPlat in this.vmPageMenu.ListeVMPlatPlat)
                {
                    VuePlat vue = new VuePlat(vmPlat);
                    vue.MouseDown += (s, e) => this.SelectionnerPlat(vue);
                    vue.MouseDoubleClick += (s, e) => this.OuvrirDetailPlat(vue);
                    this.vuePlat.Add(vue);
                    this.PanelPlat.Children.Add(vue);
                }
            }

            if (this.PanelPlat.Children.Count == 0)
            {
                TextBlock textBlockVide = CreerTextBlockVide("Aucun plat");
                this.PanelPlat.Children.Add(textBlockVide);
            }
        }

        /// <summary>
        /// Rafraîchit la liste des desserts
        /// </summary>
        private void RafraichirListeDesserts()
        {
            this.PanelDessert.Children.Clear();

            if (this.vmPageMenu.ListeVMPlatDessert != null)
            {
                foreach (VMPlat vmPlat in this.vmPageMenu.ListeVMPlatDessert)
                {
                    VuePlat vue = new VuePlat(vmPlat);
                    vue.MouseDown += (s, e) => this.SelectionnerPlat(vue);
                    vue.MouseDoubleClick += (s, e) => this.OuvrirDetailPlat(vue);
                    this.vuePlat.Add(vue);
                    this.PanelDessert.Children.Add(vue);
                }
            }

            if (this.PanelDessert.Children.Count == 0)
            {
                TextBlock textBlockVide = CreerTextBlockVide("Aucun dessert");
                this.PanelDessert.Children.Add(textBlockVide);
            }
        }

        /// <summary>
        /// Crée un TextBlock pour afficher un message de liste vide
        /// </summary>
        /// <param name="message">Le message à afficher</param>
        /// <returns>Un TextBlock stylisé en italique</returns>
        private TextBlock CreerTextBlockVide(string message)
        {
            return new TextBlock
            {
                Text = message,
                FontSize = 12,
                FontStyle = FontStyles.Italic,
                Foreground = new SolidColorBrush(Color.FromRgb(150, 150, 150)),
                Margin = new Thickness(5),
                HorizontalAlignment = HorizontalAlignment.Center
            };
        }

        private void SelectionnerPlat(VuePlat vue)
        {
            this.vmPagePlat.PlatSelectionne = vue.Plat;
            foreach (VuePlat vueI in this.vuePlat)
            {
                vueI.Deselectionner();
            }
            vue.Selectionner();
        }

        private async Task OuvrirDetailPlat(VuePlat vue)
        {
            if (this.vmPagePlat.PlatSelectionne != null)
            {
                Navigation.AllerDetailPlat(this, this.vmPagePlat.PlatSelectionne, "Menu", this.invitationPrecedente, this.menuSelectionne);
            }
        }

        /// <summary>
        /// Gère le clic sur le bouton Retour
        /// </summary>
        public void RetourAuMenu_Click(object sender, RoutedEventArgs e)
        {
            switch (this.provenance)
            {
                case "Accueil":
                    Navigation.AllerAccueil(this);
                    break;
                case "Invitation":
                        Navigation.AllerDetailInvitation(this, this.invitationPrecedente, "Menu");
                    break;
                default:
                    Navigation.AllerMenu(this);
                    break;
            }
        }
        #endregion
    }
}
