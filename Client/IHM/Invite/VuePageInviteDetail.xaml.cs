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
using VM_Footies;
using VM_Footies.VM_Element_Selectionne;
using VM_Footies.VM_Page;

namespace IHM_Footies.Invite
{
    /// <summary>
    /// Logique d'interaction pour VuePageInviteDetail.xaml
    /// </summary>
    public partial class VuePageInviteDetail : Window
    {
        #region Attributs
        private VMPageInvite vMPageInvite;
        private VMPageGroupeInvite vMPageGroupeInvite;
        private VMInvitation invitationPrecedente;
        private string provenance;
        #endregion
        public VuePageInviteDetail(VMInvite vMInvite, string provenance = "Invite", VMInvitation invitationParent = null)
        {
            InitializeComponent();

            this.vMPageInvite = new VMPageInvite();
            this.vMPageInvite.InviteSelectionne = vMInvite;

            this.vMPageGroupeInvite = new VMPageGroupeInvite();

            this.invitationPrecedente = invitationParent;

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
                    Navigation.AllerDetailGroupeInvite(this, this.vMPageGroupeInvite.GroupeSelectionne);
                    break;
                case "Invitation":
                    if (this.invitationPrecedente != null)
                    {
                        Navigation.AllerDetailInvitation(this, this.invitationPrecedente);
                    }
                    break;
                default:
                    Navigation.AllerInvites(this);
                    break;
            }
        }
        #endregion
    }
}
