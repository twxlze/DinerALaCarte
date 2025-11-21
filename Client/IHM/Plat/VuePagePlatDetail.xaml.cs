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
using VM_Footies;
using VM_Footies.VM;

namespace IHM_Footies.Plat
{
    /// <summary>
    /// Logique d'interaction pour VuePagePlatDetail.xaml
    /// </summary>
    public partial class VuePagePlatDetail : Window
    {
        #region Attributs
        private VMPagePlat vmPagePlat;
        #endregion

        #region Constructeurs
        public VuePagePlatDetail(VMPlat vmPlat)
        {
            InitializeComponent();
            this.vmPagePlat = new VMPagePlat();
            this.vmPagePlat.PlatSelectionne = vmPlat;
            this.DataContext = this.vmPagePlat;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        #endregion

        #region Méthodes
        private void RetourAPlat_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerPlat(this);
        }
        #endregion
    }
}
