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
using VM_Footies.VM_Page;

namespace IHM_Footies.Statistique
{
    /// <summary>
    /// Logique d'interaction pour VuePageSelectionStatistique.xaml
    /// </summary>
    public partial class VuePageSelectionStatistique : Window
    {
        #region Attributs
        private VmPageStatistique vmPageStatistique;
        private VMPageInvitation vMPageInvitation;
        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur de la vue de sélection des statistiques
        /// </summary>
        /// <param name="vMPageInvitation">prend en parametre le model des invitations</param>
        public VuePageSelectionStatistique(VMPageInvitation vMPageInvitation)
        {
            InitializeComponent();
            this.vMPageInvitation = vMPageInvitation;
            this.vmPageStatistique = new VmPageStatistique(vMPageInvitation);
            this.DataContext = this.vmPageStatistique;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        #endregion
    }
}
