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
    /// Logique d'interaction pour VuePageStatistique.xaml
    /// </summary>
    public partial class VuePageStatistique : Window
    {
        #region Attributs
        private VmPageStatistique vmPageStatistique;
        #endregion

        #region constructeurs
        /// <summary>
        /// Constructeur de la vue des statistiques
        /// </summary>
        /// <param name="vmPageStatistique">le model statistique</param>
        public VuePageStatistique(VmPageStatistique vmPageStatistique)
        {
            this.vmPageStatistique = vmPageStatistique;
            this.DataContext = vmPageStatistique;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }
        #endregion

        #region Boutons de navigation
        private void BoutonRetour_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerSelectionInvite(this);
        }
        #endregion
    }
}
