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
        private string provenance;
        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur de la vue de détail d'un plat
        /// </summary>
        /// <param name="vmPlat">Le plat à afficher</param>
        /// <param name="provenance">La page de provenance ("Plat" ou "Accueil")</param>
        public VuePagePlatDetail(VMPlat vmPlat, string provenance = "Plat")
        {
            InitializeComponent();
            this.vmPagePlat = new VMPagePlat();
            this.vmPagePlat.PlatSelectionne = vmPlat;
            this.provenance = provenance;
            this.DataContext = this.vmPagePlat;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        #endregion

        #region Méthodes
        /// <summary>
        /// Gère le clic sur le bouton Retour en fonction de la page de provenance
        /// </summary>
        private void RetourAPlat_Click(object sender, RoutedEventArgs e)
        {
            if (this.provenance == "Accueil")
            {
                Navigation.AllerAccueil(this);
            }
            else
            {
                Navigation.AllerPlat(this);
            }
        }
        #endregion
    }
}
