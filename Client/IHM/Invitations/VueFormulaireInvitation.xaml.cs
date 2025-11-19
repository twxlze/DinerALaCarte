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

namespace IHM_Footies.Invitations
{
    /// <summary>
    /// Logique d'interaction pour VueFormulaireInvitation.xaml
    /// </summary>
    public partial class VueFormulaireInvitation : Window
    {
        public VueFormulaireInvitation()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        #region boutons de navigations

        /// <summary>
        /// Bouton pour fermer la fenêtre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButonFermerFenetre_Click(object sender, RoutedEventArgs e)
        {
            Navigation.FermerFenetre(this);
        }

        #endregion

        /// <summary>
        /// Aller à la vue d'accueil
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonVueAccueil(object sender, RoutedEventArgs e)
        {
            Navigation.AllerAccueil(this);
        }

        /// <summary>
        /// Aller à la page d'invité
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonInvite_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerInvites(this);
        }

        /// <summary>
        /// Aller à la page groupe invité
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonGroupeInvite_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerGroupesInvites(this);
        }

        /// <summary>
        /// Aller à la page des réglages
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonAllerReglages_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerReglages(this);
        }

        /// <summary>
        /// Aller à la page plats
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonAllerPlat_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerPlat(this);
        }
    }
}
