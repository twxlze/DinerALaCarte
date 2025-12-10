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
using IHM_Footies.GroupeInvite;
using IHM_Footies.Invite;
using VM_Footies;
using VM_Footies.VM;

namespace IHM_Footies
{
    /// <summary>
    /// Logique d'interaction pour VuePageInvite.xaml
    /// </summary>
    public partial class VuePageInvite : Window
    {
        #region Attributs
        private VMPageInvite vmPageInvite;
        private List<VueInvite> vueInvite;
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur par défaut d'une page d'invité
        /// </summary>
        public VuePageInvite()
        {
            InitializeComponent();

            this.vueInvite = new List<VueInvite>();
            this.vmPageInvite = new VMPageInvite();
            this.vmPageInvite.PropertyChanged += VMPageInvite_PropertyChanged;
            this.DataContext = this.vmPageInvite;

            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.RafraichirListe();
        }
        #endregion

        #region Méthodes
        /// <summary>
        /// Gestion du changement de propriété dans le VMPageInvite
        /// </summary>
        /// <param name="sender"> L'expéditeur </param>
        /// <param name="e"> Les arguments de l'événement </param>
        private void VMPageInvite_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "VMInvites") this.RafraichirListe();
        }

        /// <summary>
        /// Rafraîchit la liste des invités affichés
        /// </summary>
        private async void RafraichirListe()
        {
            this.PanelListeInvites.Children.Clear();
            this.vueInvite.Clear();

            await this.vmPageInvite.ChargerInvites();

            foreach (VMInvite invite in this.vmPageInvite.VMInvites)
            {
                VueInvite vue = new VueInvite(invite);
                vue.MouseDown += (s, e) => this.SelectionnerPersonne(vue);
                vue.MouseDoubleClick += (s, e) => this.OuvrirDetailGroupe(vue);
                this.vueInvite.Add(vue);
                this.PanelListeInvites.Children.Add(vue);
            }
        }

        /// <summary>
        /// Ouvre la fenêtre des details d'un groupe invité
        /// </summary>
        /// <param name="vue"> La vue du groupe invité pour lequelle on veut ces details </param>
        private void OuvrirDetailGroupe(VueInvite vue)
        {
            if (this.vmPageInvite.InviteSelectionne != null)
            {
                Navigation.AllerDetailInvite(this, this.vmPageInvite.InviteSelectionne);
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un invité pour voir ses détails.", "Aucun invité sélectionné", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Sélectionne une personne dans la liste des invités
        /// </summary>
        /// <param name="vue"> VueInvite sélectionnée </param>
        public void SelectionnerPersonne(VueInvite vue)
        {
            this.vmPageInvite.InviteSelectionne = vue.Invite;
            foreach (VueInvite vueI in this.vueInvite)
            {
                vueI.Deselectionner();
            }
            vue.Selectionner();
        }
        #endregion

        #region Boutons 
        /// <summary>
        /// Ouvre la fenêtre d'ajout d'un invité
        /// </summary>
        /// <param name="sender"> L'expéditeur </param>
        /// <param name="e"> Les arguments de l'événement </param>
        private async void BoutonAjouterInvite_Click(object sender, RoutedEventArgs e)
        {
            VueFormulaireInvite fenetre = new VueFormulaireInvite();
            bool? result = fenetre.ShowDialog();
            if (result == true)
            {
                try
                {
                    await this.vmPageInvite.AjouterInvite(fenetre.Invite);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Une erreur est survenue lors de l'ajout de l'invité : {ex.Message}", "Erreur d'ajout", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// Ouvre la fenêtre de modification d'un invité sélectionné
        /// </summary>
        /// <param name="sender"> L'expéditeur </param>
        /// <param name="e"> Les arguments de l'événement </param>
        private async void BoutonModifierInvite_Click(object sender, RoutedEventArgs e)
        {
            if (this.vmPageInvite.InviteSelectionne != null)
            {
                VueFormulaireInvite fenetre = new VueFormulaireInvite(this.vmPageInvite.InviteSelectionne);
                bool? result = fenetre.ShowDialog();
                if (result == true)
                {
                    await this.vmPageInvite.ModifierInvite(fenetre.Invite);
                }
            }
        }


        /// <summary>
        /// Supprime l'invité sélectionné
        /// </summary>
        /// <param name="sender"> L'expéditeur </param>
        /// <param name="e"> Les arguments de l'événement </param>
        private async void BoutonSupprimerInvite_Click(object sender, RoutedEventArgs e)
        {
            if (this.vmPageInvite.InviteSelectionne != null)
            {
                MessageBoxResult resultat = MessageBox.Show(
                    "Êtes-vous sûr de vouloir supprimer cet invité ?",
                    "Confirmation de suppression",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (resultat == MessageBoxResult.Yes)
                {
                    bool suppressionReussie = await this.vmPageInvite.SupprimerInvite();

                    if (!suppressionReussie)
                    {
                        MessageBox.Show(
                            "Suppression impossible, l'invité fait partie d'un ou plusieurs groupes.",
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
                    "Veuillez sélectionner un invité à supprimer.",
                    "Aucun invité sélectionné",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
        }

        private void BoutonRetour_Click(object sender, RoutedEventArgs e)
        {
            this.RafraichirListe();
            this.vmPageInvite.TexteRecherche = string.Empty;
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
        /// Bouton pour fermer la fenêtre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButonFermerFenetre_Click(object sender, RoutedEventArgs e)
        {
            Navigation.FermerFenetre(this);
        }

        /// <summary>
        /// Bouton pour aller à la page d'invitations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonAllerInvitation_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerInvitations(this);
        }

        /// <summary>
        /// Bouton pour aller à la page des Statistiques
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonAllerStatistique_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerSelectionInvite(this);
        }
        #endregion


        #region Méthodes

        /// <summary>
        /// Sélectionne un groupe dans la liste des groupes invités
        /// </summary>
        /// <param name="vue"> La vue du groupe invité </param>
        private void SelectionnerInvite(VueInvite vue)
        {
            this.vmPageInvite.InviteSelectionne = vue.Invite;
            foreach (VueInvite v in this.vueInvite)
            {
                v.Deselectionner();
            }
            vue.Selectionner();
        }

        /// <summary>
        /// Recherche les invités selon le texte saisi
        /// </summary>
        /// <param name="sender"> L'expéditeur </param>
        /// <param name="e"> Les arguments de l'événement </param>
        private async void RechercheInvite_Click(object sender, RoutedEventArgs e)
        {
            this.PanelListeInvites.Children.Clear();
            this.vueInvite.Clear();

            await this.vmPageInvite.ChercherInvite(this.vmPageInvite.TexteRecherche);

            if (this.vmPageInvite.VMInvites.Count != 0)
            {
                foreach (VMInvite invite in this.vmPageInvite.VMInvites)
                {
                    VueInvite vue = new VueInvite(invite);
                    vue.MouseDown += (s, e) => this.SelectionnerInvite(vue);
                    vue.MouseDoubleClick += (s, e) => this.OuvrirDetailGroupe(vue);
                    this.vueInvite.Add(vue);
                    this.PanelListeInvites.Children.Add(vue);
                }
            }
            else
            {
                TextBlock aucunResultat = new TextBlock
                {
                    Text = "Aucun résultat trouvé",
                    Foreground = Brushes.Gray,
                    FontSize = 16,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(0, 20, 0, 0)
                };

                this.PanelListeInvites.Children.Add(aucunResultat);
            }
        }

        

        #endregion


    }
}
