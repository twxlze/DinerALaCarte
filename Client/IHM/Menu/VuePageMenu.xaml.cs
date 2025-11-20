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
using VM_Footies.VM;
using VM_Footies.VM_Page;

namespace IHM_Footies.Menu
{
    /// <summary>
    /// Logique d'interaction pour VuePageMenu.xaml
    /// </summary>
    public partial class VuePageMenu : Window
    {
        #region Attributs
        private VMPageMenu vmPageMenu;
        private List<VueMenu> vueMenu;
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur par défaut d'une page de menu
        /// </summary>
        public VuePageMenu()
        {
            InitializeComponent();
            this.vueMenu = new List<VueMenu>();
            this.vmPageMenu = new VMPageMenu();
            this.DataContext = this.vmPageMenu;
            this.vmPageMenu.PropertyChanged += VMPageMenu_PropertyChanged;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.RafraichirListe();
        }
        #endregion

        #region Méthodes
        /// <summary>
        /// Gestion du changement de propriété dans le VMPageMenu
        /// </summary>
        /// <param name="sender"> L'expéditeur </param>
        /// <param name="e"> Les arguments de l'événement </param>
        private void VMPageMenu_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "VMMenu") this.RafraichirListe();
        }

        /// <summary>
        /// Rafraîchit la liste des menus affichés
        /// </summary>
        private async void RafraichirListe()
        {
            this.PanelListeMenu.Children.Clear();
            this.vueMenu.Clear();

            await this.vmPageMenu.ChargerMenus();

            foreach (VMMenu menu in this.vmPageMenu.VMMenu)
            {
                VueMenu vue = new VueMenu(menu);
                vue.MouseDown += (s, e) => this.SelectionnerMenu(vue);
                vue.MouseDoubleClick += (s, e) => this.OuvrirModification(vue);
                this.vueMenu.Add(vue);
                this.PanelListeMenu.Children.Add(vue);
            }
        }

        /// <summary>
        /// Ouvre la fenêtre de modification d'un menu
        /// </summary>
        /// <param name="vue"> La vue du menu à modifier </param>
        private async Task OuvrirModification(VueMenu vue)
        {
            VMMenu memoire = new VMMenu(vue.Menu);

            await this.vmPageMenu.ChargerPlatsDansMenu(memoire);

            VueFormulaireMenu fenetre = new VueFormulaireMenu(vue.Menu);
            bool? result = fenetre.ShowDialog();
            if (result == true)
            {
                //vue.Menu.ModifierMenu(memoire);
                await this.vmPageMenu.ModifierMenu(vue.Menu);
            }
        }

        /// <summary>
        /// Sélectionne un menu dans la liste des menus
        /// </summary>
        /// <param name="vue"> VueMenu sélectionnée </param>
        public void SelectionnerMenu(VueMenu vue)
        {
            this.vmPageMenu.MenuSelectionne = vue.Menu;
            foreach (VueMenu vueM in this.vueMenu)
            {
                vueM.Deselectionner();
            }
            vue.Selectionner();
        }

        private async void RechercheMenu_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.vmPageMenu.TexteRecherche))
            {
                this.PanelListeMenu.Children.Clear();
                this.vueMenu.Clear();
                await this.vmPageMenu.ChercherMenus(this.vmPageMenu.TexteRecherche);
                foreach (VMMenu menu in this.vmPageMenu.VMMenu)
                {
                    VueMenu vue = new VueMenu(menu);
                    vue.MouseDown += (s, ev) => this.SelectionnerMenu(vue);
                    vue.MouseDoubleClick += (s, ev) => this.OuvrirModification(vue);
                    this.vueMenu.Add(vue);
                    this.PanelListeMenu.Children.Add(vue);
                }
            }
            else
            {
                this.RafraichirListe();
            }
        }
        #endregion

        #region Boutons 
        /// <summary>
        /// Ouvre la fenêtre d'ajout d'un menu
        /// </summary>
        /// <param name="sender"> l'expéditeur </param>
        /// <param name="e"> Les arguments de l'événement </param>
        private async void BoutonAjouterMenu_Click(object sender, RoutedEventArgs e)
        {
            VMMenu nvMenu = new VMMenu();
            await this.vmPageMenu.ChargerPlatsDansMenu(nvMenu);

            VueFormulaireMenu fenetre = new VueFormulaireMenu(nvMenu);
            bool? result = fenetre.ShowDialog();
            if (result == true)
            {
                try
                {
                    await this.vmPageMenu.AjouterMenu(nvMenu);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors de l'ajout du menu : {ex.Message}", "Erreur d'ajout", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// Ouvre la fenêtre de modification d'un menu sélectionné
        /// </summary>
        /// <param name="sender"> L'expéditeur </param>
        /// <param name="e"> Les arguments de l'événement </param>
        private async void BoutonModifierMenu_Click(object sender, RoutedEventArgs e)
        {
            if (this.vmPageMenu.MenuSelectionne != null)
            {
                VMMenu copie = new VMMenu(this.vmPageMenu.MenuSelectionne);

                await this.vmPageMenu.ChargerPlatsDansMenu(copie);
                VueFormulaireMenu fenetre = new VueFormulaireMenu(copie);
                bool? result = fenetre.ShowDialog();
                if (result == true)
                {
                    await this.vmPageMenu.ModifierMenu(copie);
                }
            }
        }

        /// <summary>
        /// Supprime le menu sélectionné
        /// </summary>
        /// <param name="sender"> L'expéditeur </param>
        /// <param name="e"> Les arguments de l'événement </param>
        private async void BoutonSupprimerMenu_Click(object sender, RoutedEventArgs e)
        {
            if (this.vmPageMenu.MenuSelectionne != null)
            {
                MessageBoxResult resultat = MessageBox.Show(
                    "Êtes-vous sûr de vouloir supprimer ce menu ?",
                    "Confirmation de suppression",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (resultat == MessageBoxResult.Yes)
                {
                    bool suppressionReussie = await this.vmPageMenu.SupprimerMenu();

                    if (!suppressionReussie)
                    {
                        MessageBox.Show(
                            "Suppression impossible, le menu est utilisé dans une ou plusieurs invitations.",
                            "Suppression impossible",
                            MessageBoxButton.OK,
                            MessageBoxImage.Warning);
                    }
                    else
                    {
                        this.RafraichirListe();
                    }
                }
            }
            else
            {
                MessageBox.Show(
                    "Veuillez sélectionner un menu à supprimer.",
                    "Aucun menu sélectionné",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
        }
        #endregion

        #region Boutons de navigation
        /// <summary>
        /// Bouton pour aller à la page plat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonAllerPlat_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerPlat(this);
        }

        /// <summary>
        /// Bouton pour aller à l'accueil
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonAllerAccueil_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerAccueil(this);
        }

        /// <summary>
        /// Bouton pour aller au menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonAllerMenu_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerMenu(this);
        }
        /// <summary>
        /// Bouton pour aller à la page invité
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonAllerInvite_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerInvites(this);
        }

        /// <summary>
        /// Bouton pour aller à la page des réglages
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonAllerReglages_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerReglages(this);
        }

        /// <summary>
        /// Bouton pour aller à la page des groupes d'invités
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonAllerGroupeInvite_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerGroupesInvites(this);
        }

        /// <summary>
        /// Bouton pour aller à la page des invitations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonAllerInvitation_Click(object sender, RoutedEventArgs e)
        {
        }

        /// <summary>
        /// Bouton pour fermer la fenêtre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButonFermerFenetre_Click(object sender, RoutedEventArgs e)
        {
            Navigation.FermerFenetre(this);
        }

        #endregion
    }
}
