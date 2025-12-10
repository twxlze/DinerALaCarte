using System.Windows;
using VM_Footies.VM;
using VM_Footies;

namespace IHM_Footies.Invite
{
    /// <summary>
    /// Logique d'interaction pour VuePageInviteDetail.xaml
    /// </summary>
    public partial class VuePageInviteDetail : Window
    {
        #region Attributs
        private VMPageInvite vMPageInvite;
        private VMPageGroupeInvite vMPageGroupeInvite; // pour la navigation
        private VMInvitation pageinvitationPrecedente;
        private string provenance;
        private VMGroupeInvite pageGroupePrecedente;
        #endregion
        public VuePageInviteDetail(VMInvite vMInvite, string provenance = "Invite", VMInvitation invitationParent = null, VMGroupeInvite groupeParent = null)
        {
            InitializeComponent();

            this.vMPageInvite = new VMPageInvite();
            this.vMPageInvite.InviteSelectionne = vMInvite;

            this.vMPageGroupeInvite = new VMPageGroupeInvite();

            this.pageinvitationPrecedente = invitationParent;
            this.pageGroupePrecedente = groupeParent;

            this.provenance = provenance;
            this.DataContext = this.vMPageInvite;

            WindowStartupLocation = WindowStartupLocation.CenterScreen;

        }
        #region Méthodes

        /// <summary>
        /// Bouton pour retourner à la page des groupes invités
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RetourAPage_Click(object sender, RoutedEventArgs e)
        {
            switch (this.provenance)
            {
                case "Accueil":
                    Navigation.AllerAccueil(this);
                    break;
                case "Invite":
                    Navigation.AllerInvites(this);
                    break;
                case "GroupeInvite":
                    if (this.pageinvitationPrecedente != null)
                        Navigation.AllerDetailGroupeInvite(this, this.pageGroupePrecedente, "Invitation", this.pageinvitationPrecedente);
                    else
                        Navigation.AllerDetailGroupeInvite(this, this.pageGroupePrecedente);
                    break;
                case "Invitation":
                        Navigation.AllerDetailInvitation(this, this.pageinvitationPrecedente);
                    break;
                default:
                    Navigation.AllerInvites(this);
                    break;
            }
        }
        #endregion
    }
}
