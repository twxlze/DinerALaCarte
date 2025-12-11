using System.Windows;
using METIER_Footies.Data;
using VM_Footies.VM;
using VM_Footies.VM_Page;

namespace IHM_Footies.Invitations
{
    /// <summary>
    /// Logique d'interaction pour VueFormulaireMenuEtPlat_Invitation.xaml
    /// </summary>
    public partial class VueFormulaireMenuEtPlat_Invitation : Window
    {

        #region attributs

        private VMInvitation invitation;

        private InvitationDAO invitationDAO;

        private VMPageInvitation pageInvitation;


        #endregion

        #region proprietes

        /// <summary>
        /// Récupérer les invitations
        /// </summary>
        public VMInvitation Invitation => this.invitation;

        #endregion


        #region constructeurs

        public VueFormulaireMenuEtPlat_Invitation(VMInvitation invitation)
        {
            this.pageInvitation = new VMPageInvitation();
            this.invitationDAO = new InvitationDAO();
            this.invitation = invitation;
            this.DataContext = this.invitation;
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Loaded += VueFormulaireMenuEtPlat_Invitation_Loaded;
        }

        public VueFormulaireMenuEtPlat_Invitation() : this(new VMInvitation())
        {
        }

        #endregion

        #region methodes

        private async void VueFormulaireMenuEtPlat_Invitation_Loaded(object sender, RoutedEventArgs e)
        {
            await ChargerDonnees();
        }

        private async Task ChargerDonnees()
        {
            await this.pageInvitation.ChargerElementsDansInvitation(invitation);
        }


        #endregion


        #region boutons navigations 

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
        /// Bouton pour aller à la page des Statistiques
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonAllerStatistique_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerSelectionInvite(this);
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

        private async void BoutonEnregistrerInvitation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.invitation.SynchroniserTout();
                if (this.invitation.Invitation.IdInvitation != 0)
                {
                    await this.invitationDAO.ModifierInvitation(this.invitation.Invitation);
                }
                else
                {
                    await this.invitationDAO.AjouterInvitation(this.invitation.Invitation);
                }
                Navigation.AllerInvitations(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'enregistrement de l'invitation : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

    }
}
