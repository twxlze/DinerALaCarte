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
using IHM;
using VM_Footies;
using VM_Footies.VM;

namespace IHM_Footies
{
    /// <summary>
    /// Logique d'interaction pour VuePagePlat.xaml
    /// </summary>
    public partial class VuePagePlat : Window
    {
        #region Attributs
        private VMPagePlat vmPagePlat;
        private List<VuePlat> vuePlat;
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur par défaut d'une page de plat
        /// </summary>
        public VuePagePlat()
        {
            InitializeComponent();
            this.vuePlat = new List<VuePlat>();
            this.vmPagePlat = new VMPagePlat();
            this.vmPagePlat.PropertyChanged += VMPagePlat_PropertyChanged;
            this.DataContext = this.vmPagePlat;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.RafraichirListe();
        }
        #endregion

        #region Méthodes
        /// <summary>
        /// Gestion du changement de propriété dans le VMPagePlat
        /// </summary>
        /// <param name="sender"> L'expéditeur </param>
        /// <param name="e"> Les arguments de l'événement </param>
        private void VMPagePlat_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "VMPlat") this.RafraichirListe();
        }

        /// <summary>
        /// Rafraîchit la liste des plats affichés
        /// </summary>
        private async void RafraichirListe()
        {
            this.PanelListePlat.Children.Clear();
            this.vuePlat.Clear();

            await this.vmPagePlat.ChargerPlats();

            foreach (VMPlat plat in this.vmPagePlat.VMPlat)
            {
                VuePlat vue = new VuePlat(plat);
                vue.MouseDown += (s, e) => this.SelectionnerPlat(vue);
                vue.MouseDoubleClick += (s, e) => this.OuvrirModification(vue);
                this.vuePlat.Add(vue);
                this.PanelListePlat.Children.Add(vue);
            }
        }

        /// <summary>
        /// Ouvre la fenêtre de modification d'un plat
        /// </summary>
        /// <param name="vue"> La vue du plat à modifier </param>
        private void OuvrirModification(VuePlat vue)
        {
            VMPlat memoire = new VMPlat(vue.Plat);
            VueFormulairePlat fenetre = new VueFormulairePlat(vue.Plat);
            bool? result = fenetre.ShowDialog();
            if (result == false)
            {
                vue.Plat.ModifierPlat(memoire);
            }
        }

        /// <summary>
        /// Sélectionne un plat dans la liste des plats
        /// </summary>
        /// <param name="vue"> VuePlat sélectionnée </param>
        public void SelectionnerPlat(VuePlat vue)
        {
            this.vmPagePlat.PlatSelectionne = vue.Plat;
            foreach (VuePlat vueP in this.vuePlat)
            {
                vueP.Deselectionner();
            }
            vue.Selectionner();
        }

        /// <summary>
        /// Recherche les plats selon le texte saisi
        /// </summary>
        /// <param name="sender"> L'expéditeur </param>
        /// <param name="e"> Les arguments de l'événement </param>
        private async void RecherchePlat_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.vmPagePlat.TexteRecherche))
            {
                this.PanelListePlat.Children.Clear();
                this.vuePlat.Clear();

                await this.vmPagePlat.ChercherPlat(this.vmPagePlat.TexteRecherche);

                foreach (VMPlat plat in this.vmPagePlat.VMPlat)
                {
                    VuePlat vue = new VuePlat(plat);
                    vue.MouseDown += (s, e) => this.SelectionnerPlat(vue);
                    vue.MouseDoubleClick += (s, e) => this.OuvrirModification(vue);
                    this.vuePlat.Add(vue);
                    this.PanelListePlat.Children.Add(vue);
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
        /// Ouvre la fenêtre d'ajout d'un plat
        /// </summary>
        /// <param name="sender"> l'expéditeur </param>
        /// <param name="e"> Les arguments de l'événement </param>
        private async void BoutonAjouterPlat_Click(object sender, RoutedEventArgs e)
        {
            VueFormulairePlat fenetre = new VueFormulairePlat();
            bool? result = fenetre.ShowDialog();
            if (result == true)
            {
                try
                {
                    await this.vmPagePlat.AjouterPlat(fenetre.Plat);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Une erreur est survenue lors de l'ajout du plat : {ex.Message}", "Erreur d'ajout", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// Ouvre la fenêtre de modification d'un plat sélectionné
        /// </summary>
        /// <param name="sender"> L'expéditeur </param>
        /// <param name="e"> Les arguments de l'événement </param>
        private async void BoutonModifierPlat_Click(object sender, RoutedEventArgs e)
        {
            if (this.vmPagePlat.PlatSelectionne != null)
            {
                VueFormulairePlat fenetre = new VueFormulairePlat(this.vmPagePlat.PlatSelectionne);
                bool? result = fenetre.ShowDialog();
                if (result == true)
                {
                    await this.vmPagePlat.ModifierPlat(fenetre.Plat);
                }
            }
        }


        /// <summary>
        /// Supprime le plat sélectionné
        /// </summary>
        /// <param name="sender"> L'expéditeur </param>
        /// <param name="e"> Les arguments de l'événement </param>
        private async void BoutonSupprimerPlat_Click(object sender, RoutedEventArgs e)
        {
            if (this.vmPagePlat.PlatSelectionne != null)
            {
                MessageBoxResult resultat = MessageBox.Show(
                    "Êtes-vous sûr de vouloir supprimer ce plat ?",
                    "Confirmation de suppression",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (resultat == MessageBoxResult.Yes)
                {
                    bool suppressionReussie = await this.vmPagePlat.SupprimerPlat();

                    if (!suppressionReussie)
                    {
                        MessageBox.Show(
                            "Suppression impossible, le plat est utilisé dans un ou plusieurs menus.",
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
                    "Veuillez sélectionner un plat à supprimer.",
                    "Aucun plat sélectionné",
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