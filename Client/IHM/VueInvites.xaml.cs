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
using IHM;

namespace IHM_Footies
{
    /// <summary>
    /// Logique d'interaction pour VueInvites.xaml
    /// </summary>
    public partial class VueInvites : Window
    {
        public VueInvites()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void BoutonVueAccueil(object sender, RoutedEventArgs e)
        {
            Navigation.AllerAccueil(this);
        }

        private void BoutonInvite_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerInvites(this);
        }

        private void BoutonAccueil_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerAccueil(this);
        }

        private void BoutonAjouterInvite_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerFormulaireInvite(this);
        }
        private void ButonFermerFenetre_Click(object sender, RoutedEventArgs e)
        {
            Navigation.FermerFenetre(this);
        }
    }
}
