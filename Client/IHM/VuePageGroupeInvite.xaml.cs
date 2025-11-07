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

namespace IHM_Footies
{
    /// <summary>
    /// Logique d'interaction pour VuePageGroupeInvite.xaml
    /// </summary>
    public partial class VuePageGroupeInvite : Window
    {
        public VuePageGroupeInvite()
        {
            InitializeComponent();
        }


        #region Boutons 
        private void BoutonAjouterGroupeInvite_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void BoutonModifierGroupeInvite_Click(object sender, RoutedEventArgs e)
        {
            
        }
        #endregion

        #region Boutons de navigation
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

        private void ButonFermerFenetre_Click(object sender, RoutedEventArgs e)
        {
            Navigation.FermerFenetre(this);
        }

        private void BoutonGroupes_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion
    }
}
