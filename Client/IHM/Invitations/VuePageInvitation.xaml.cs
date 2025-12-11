using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using VM_Footies.VM;
using VM_Footies.VM_Page;

namespace IHM_Footies.Invitations
{
    /// <summary>
    /// Logique d'interaction pour VuePageInvitation.xaml
    /// </summary>
    public partial class VuePageInvitation : Window
    {
        #region Attributs
        private VMPageInvitation vmPageInvitation = new VMPageInvitation();
        private List<VueInvitation> vueInvitations = new List<VueInvitation>();
        #endregion

        public VuePageInvitation()
        {
            InitializeComponent();
            this.vmPageInvitation.PropertyChanged += VmPageInvitation_PropertyChanged;
            this.DataContext = this.vmPageInvitation;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.RafraichirListe();
        }

        #region Méthodes
        /// <summary>
        /// Gestion du changement de propriété dans le VMPageGroupeInvite
        /// </summary>
        /// <param name="sender"> L'expéditeur </param>
        /// <param name="e"> Les arguments de l'événement </param>
        private void VmPageInvitation_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "VMGroupeInvite") this.RafraichirListe();
        }

        /// <summary>
        /// Rafraîchit la liste des invitations affichés
        /// </summary>
        private async void RafraichirListe()
        {
            this.PanelListeInvitation.Children.Clear();
            this.vueInvitations.Clear();

            await this.vmPageInvitation.ChargerInvitations();

            foreach (VMInvitation invite in this.vmPageInvitation.VMInvitations)
            {
                VueInvitation vue = new VueInvitation(invite);
                vue.MouseDown += (s, e) => this.SelectionnerInvitation(vue);
                vue.MouseDoubleClick += (s, e) => this.OuvrirDetailInvitation(vue);
                this.vueInvitations.Add(vue);
                this.PanelListeInvitation.Children.Add(vue);
            }
        }

        /// <summary>
        /// Ouvre la fenêtre de modification d'une invitation
        /// </summary>
        /// <param name="vue"> La vue de l'invitation à modifier </param>
        private async Task OuvrirDetailInvitation(VueInvitation vue)
        {
            if (this.vmPageInvitation.InvitationSelectionnee != null)
            {
                Navigation.AllerDetailInvitation(this, this.vmPageInvitation.InvitationSelectionnee);
            }
        }


        /// <summary>
        /// Sélectionne un menu dans la liste d'invitations
        /// </summary>
        /// <param name="vue"> VueInvitation sélectionnée </param>
        public void SelectionnerInvitation(VueInvitation vue)
        {
            this.vmPageInvitation.InvitationSelectionnee = vue.Invitation;
            foreach (VueInvitation vueM in this.vueInvitations)
            {
                vueM.Deselectionner();
            }
            vue.Selectionner();
        }


        #endregion

        #region Boutons de navigation

        /// <summary>
        /// Bouton pour aller à la vue d'accueil
        /// </summary>
        /// <param name="sender"> L'expéditeur </param>
        /// <param name="e"> Les arguments de l'événement </param>
        private void BoutonVueAccueil(object sender, RoutedEventArgs e)
        {
            Navigation.AllerAccueil(this);
        }

        /// <summary>
        /// Bouton pour aller à la vue d'invitation
        /// </summary>
        /// <param name="sender"> L'expéditeur </param>
        /// <param name="e"> Les arguments de l'événement </param>
        private void BoutonVueInvitation(object sender, RoutedEventArgs e)
        {
            Navigation.AllerInvitations(this);
        }

        /// <summary>
        /// Bouton pour aller à la vue des invités
        /// </summary>
        /// <param name="sender"> L'expéditeur </param>
        /// <param name="e"> Les arguments de l'événement </param>
        private void BoutonInvite_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerInvites(this);
        }

        /// <summary>
        /// Bouton pour fermer la fenêtre
        /// </summary>
        /// <param name="sender"> L'expéditeur du clic </param>
        /// <param name="e"> Les arguments de l'événement </param>
        private void ButonFermerFenetre_Click(object sender, RoutedEventArgs e)
        {
            Navigation.FermerFenetre(this);
        }

        /// <summary>
        /// Bouton pour aller à la vue des groupes invités
        /// </summary>
        /// <param name="sender"> L'expéditeur </param>
        /// <param name="e"> Les arguments de l'événement </param>
        private void BoutonGroupeInvite_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerGroupesInvites(this);
        }

        /// <summary>
        /// Aller à la page des réglages
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonAllerReglages_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerReglages(this);
        }

        /// <summary>
        /// Bouton pour aller à la page plat
        /// </summary>
        /// <param name="sender"> L'expéditeur </param>
        /// <param name="e"> Les arguments de l'événement </param>
        private void BoutonAllerPlat_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerPlat(this);
        }

        /// <summary>
        /// Bouton pour aller au formulaire d'invitation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonAllerFormulaireInvitation_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerFormulaireInvitation(this);
        }

        /// <summary>
        /// Bouton pour aller à la page des menus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonAllerMenu_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerMenu(this);
        }

        /// <summary>
        /// Bouton pour aller à la page des informations utilisateur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonAllerInformationUtilisateur_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerInformationUtilisateur(this);
        }
        #endregion

        #region boutons

        private async void BoutonSupprimerInvitation_Click(object sender, RoutedEventArgs e)
        {
            if (this.vmPageInvitation.InvitationSelectionnee != null)
            {
                MessageBoxResult resultat = MessageBox.Show(
                    "Êtes-vous sûr de vouloir supprimer cette invitation ?",
                    "Confirmation de suppression",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (resultat == MessageBoxResult.Yes)
                {
                    bool suppressionReussie = await this.vmPageInvitation.SupprimerInvitation();

                    if (!suppressionReussie)
                    {
                        MessageBox.Show(
                            "Suppression impossible.",
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
                    "Veuillez sélectionner une invitation à supprimer.",
                    "Aucune invitation sélectionné",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
        }

        
        private async void BoutonModifierInvitation_Click(object sender, RoutedEventArgs e)
        {
            if (this.vmPageInvitation.InvitationSelectionnee == null)
            {
                MessageBox.Show(
                    "Veuillez sélectionner une invitation à modifier.",
                    "Aucune invitation sélectionnée",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
            VMInvitation invitationAModifier = new VMInvitation(this.vmPageInvitation.InvitationSelectionnee);
            await this.vmPageInvitation.ChargerElementsDansInvitation(invitationAModifier);
            VueFormulaireInvitation fenetre = new VueFormulaireInvitation(invitationAModifier);
            fenetre.ShowDialog();
            this.RafraichirListe();
        }

        /// <summary>
        /// Recherche les groupe invités selon le texte saisi
        /// </summary>
        /// <param name="sender"> L'expéditeur </param>
        /// <param name="e"> Les arguments de l'événement </param>
        private async void RechercheInvitation_Click(object sender, RoutedEventArgs e)
        {
            this.PanelListeInvitation.Children.Clear();
            this.vueInvitations.Clear();

            await this.vmPageInvitation.ChercherInvitation(this.vmPageInvitation.TexteRecherche);

            if (this.vmPageInvitation.VMInvitations.Count != 0)
            {
                foreach (VMInvitation invitation in this.vmPageInvitation.VMInvitations)
                {
                    VueInvitation vue = new VueInvitation(invitation);
                    vue.MouseDown += (s, e) => this.SelectionnerInvitation(vue);
                    vue.MouseDoubleClick += (s, e) => this.OuvrirDetailInvitation(vue);
                    this.vueInvitations.Add(vue);
                    this.PanelListeInvitation.Children.Add(vue);
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

                this.PanelListeInvitation.Children.Add(aucunResultat);
            }
        }

        private void BoutonRetour_Click(object sender, RoutedEventArgs e)
        {
            this.RafraichirListe();
            this.vmPageInvitation.TexteRecherche = string.Empty;
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


    }
}
