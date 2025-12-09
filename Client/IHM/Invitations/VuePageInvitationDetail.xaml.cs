using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VM_Footies.VM;

namespace IHM_Footies.Invitations
{
    /// <summary>
    /// Logique d'interaction pour VuePageInvitationDetail.xaml
    /// </summary>
    public partial class VuePageInvitationDetail : Window
    {
        private VMInvitation vmInvitation;
        private string provenance;

        public VuePageInvitationDetail(VMInvitation invitation, string provenance = "Invitation")
        {
            InitializeComponent();
            this.vmInvitation = invitation;
            this.DataContext = this.vmInvitation;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.provenance = provenance;
        }

        private void RetourAuxInvitations_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerInvitations(this);
        }
    }
}