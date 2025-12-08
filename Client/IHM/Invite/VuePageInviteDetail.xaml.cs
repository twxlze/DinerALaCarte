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

namespace IHM_Footies.Invite
{
    /// <summary>
    /// Logique d'interaction pour VuePageInviteDetail.xaml
    /// </summary>
    public partial class VuePageInviteDetail : Window
    {
        #region Attributs
        private VMPageInvite vMPageInvite;
        private string provenance;
        #endregion
        public VuePageInviteDetail(VMInvite vMInvite, string provenance = "Invite")
        {
            InitializeComponent();

            this.vMPageInvite = new VMPageInvite();
            this.vMPageInvite.InviteSelectionne = vMInvite;
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
            if (this.provenance == "Accueil")
            {
                Navigation.AllerAccueil(this);
            }
            else
            {
                Navigation.AllerInvites(this);
            }
        }
        #endregion
    }
}
