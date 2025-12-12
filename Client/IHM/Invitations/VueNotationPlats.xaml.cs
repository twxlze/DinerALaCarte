using System.Windows;
using METIER_Footies.Data;
using METIER_Footies.Metier;
using VM_Footies.VM;
using VM_Footies.VM_Page;

namespace IHM_Footies.Invitations
{
    /// <summary>
    /// Logique d'interaction pour VueNotationPlats.xaml
    /// </summary>
    public partial class VueNotationPlats : Window
    {

        #region attributs

        private VMPageNotePlats vmPageNotePlat;
        private VMPageNotePlats notePlat;
        private VMInvitation invitation;
        private InvitationDAO invitationDAO;

        #endregion

        #region proprietes

        /// <summary>
        /// Récupérer les invitations
        /// </summary>
        public VMPageNotePlats NotePlat => this.notePlat;

        #endregion

        #region constructeurs 
        public VueNotationPlats(VMInvitation invitationSelectionnee)
        {
            InitializeComponent();
            this.vmPageNotePlat = new VMPageNotePlats();
            if (invitationSelectionnee != null)
            {
                this.vmPageNotePlat.ChargerDonneesInvitation(invitationSelectionnee.Invitation);
            }
            this.invitation = invitationSelectionnee;
            this.invitationDAO = new InvitationDAO();
            this.DataContext = this.vmPageNotePlat;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        public VueNotationPlats() : this(new VMInvitation())
        {

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
        /// Bouton pour aller à la vue des groupes invités
        /// </summary>
        /// <param name="sender"> L'expéditeur </param>
        /// <param name="e"> Les arguments de l'événement </param>
        private void BoutonGroupeInvite_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerGroupesInvites(this);
        }

        /// <summary>
        /// Bouton pour aller au formulaire d'invitation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonAllerInvitation_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerInvitations(this);
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
        /// Bouton pour aller à la page plat
        /// </summary>
        /// <param name="sender"> L'expéditeur </param>
        /// <param name="e"> Les arguments de l'événement </param>
        private void BoutonAllerPlat_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerPlat(this);
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



        #endregion


        #region boutons 


        private async void BoutonEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.vmPageNotePlat.InviteSelectionne == null || this.vmPageNotePlat.PlatSelectionne == null)
                {
                    MessageBox.Show("Veuillez sélectionner un invité et un plat.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                int note = Int32.Parse(this.vmPageNotePlat.NoteSaisie);
                if (note < 1 || note > 10)
                {
                    MessageBox.Show("La note doit être un chiffre entre 1 et 10.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                if (this.vmPageNotePlat.NoteSaisie == null)
                {
                    MessageBox.Show("Veuillez saisir une note.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                Avis nouvelAvis = new Avis(this.vmPageNotePlat.PlatSelectionne.Id, this.vmPageNotePlat.InviteSelectionne.Id, note, this.vmPageNotePlat.CommentaireSaisi);
                METIER_Footies.Data.PlatDAO platDAO = new METIER_Footies.Data.PlatDAO();
                System.Net.Http.HttpResponseMessage reponse = await platDAO.AjouterAvis(nouvelAvis);

                if (reponse.IsSuccessStatusCode)
                {
                    MessageBox.Show("Note et commentaire enregistrés !", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                    Navigation.AllerInvitations(this);
                }
                else
                {
                    string erreur = await reponse.Content.ReadAsStringAsync();
                    MessageBox.Show($"L'API a refusé l'ajout : {erreur}", "Erreur API", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur technique : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion
    }
}
