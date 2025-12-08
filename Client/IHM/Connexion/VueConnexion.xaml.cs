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

namespace IHM_Footies.Connexion
{
    /// <summary>
    /// Logique d'interaction pour VueConnexion.xaml
    /// </summary>
    public partial class VueConnexion : Window
    {
        public VueConnexion()
        {
            InitializeComponent();
        }

        private void BoutonConnexion_Click(object sender, RoutedEventArgs e)
        {
            Navigation.FermerFenetre(this);
        }
        #region Boutons de navigation
        /// <summary>
        /// Bouton pour fermer la fenêtre
        /// </summary>
        /// <param name="sender"> L'expéditeur du clic </param>
        /// <param name="e"> Les arguments de l'événement </param>
        private void BoutonFermerFenetre_Click(object sender, RoutedEventArgs e)
        {
            Navigation.FermerFenetre(this);
        }
        #endregion
    }
}