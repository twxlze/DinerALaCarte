using System.ComponentModel;
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
        /// <summary>
        /// Constructeur de la vue de formulaire d'invitation
        /// </summary>
        /// <param name="invitation"> prend en parametre le model des invitations</param>
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

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
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

        private void Invitation_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "TexteRechercheMenu":
                    this.pageInvitation.RechercherMenuDansFormulaire(this.invitation, this.invitation.TexteRechercheMenu);
                    break;
                case "TexteRecherchePlat":
                    this.pageInvitation.RechercherPlatDansFormulaire(this.invitation, this.invitation.TexteRecherchePlat);
                    break;
            }
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
        /// Bouton pour aller à la page du tableau de bord
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonAllerTableauDeBord_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerTableaudebord(this);
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
                VuePageAvertissementInvitation fenetreVerif = new VuePageAvertissementInvitation(this.invitation);
                bool? resultat = fenetreVerif.ShowDialog();

                if (fenetreVerif.InvitationConfirmee)
                {
                    if (this.invitation.Invitation.IdInvitation != 0)
                        await this.invitationDAO.ModifierInvitation(this.invitation.Invitation);
                    else
                        await this.invitationDAO.AjouterInvitation(this.invitation.Invitation);

                    Navigation.AllerInvitations(this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'analyse ou l'enregistrement : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RechercheMenu_Click(object sender, RoutedEventArgs e)
        {
            this.pageInvitation.RechercherMenuDansFormulaire(this.invitation, this.invitation.TexteRechercheMenu);
        }

        private void RefreshMenu_Click(object sender, RoutedEventArgs e)
        {
            this.invitation.TexteRechercheMenu = string.Empty;
            this.pageInvitation.RechercherMenuDansFormulaire(this.invitation, string.Empty);
        }

        private void RecherchePlat_Click(object sender, RoutedEventArgs e)
        {
            this.pageInvitation.RechercherPlatDansFormulaire(this.invitation, this.invitation.TexteRecherchePlat);
        }

        private void RefreshPlat_Click(object sender, RoutedEventArgs e)
        {
            this.invitation.TexteRecherchePlat = string.Empty;
            this.pageInvitation.RechercherPlatDansFormulaire(this.invitation, string.Empty);
        }
        #endregion

    }
}
