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

namespace IHM_Footies.Connexion
{
    /// <summary>
    /// Logique d'interaction pour VueConnexion.xaml
    /// </summary>
    public partial class VueConnexion : Window
    {
        #region Attributs
        private VMPageConnexion vmPageConnexion;
        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur de la vue de connexion
        /// </summary>
        public VueConnexion()
        {
            InitializeComponent();
            this.vmPageConnexion = new VMPageConnexion();
            this.DataContext = this.vmPageConnexion;
        }
        #endregion

        #region Méthodes
        /// <summary>
        /// Gère le clic sur le bouton de connexion
        /// </summary>
        /// <param name="sender"> L'expéditeur du clic </param>
        /// <param name="e"> Les arguments de l'événement </param>
        private async void BoutonConnexion_Click(object sender, RoutedEventArgs e)
        {
            this.vmPageConnexion.MotDePasse = ChampMotDePasse.Password;
            bool connexionReussie = await this.vmPageConnexion.Connexion();

            if (connexionReussie)
            {
                Navigation.AllerAccueil(this);
            }
            else
            {
                MessageBox.Show(this.vmPageConnexion.MessageErreur, "Erreur de connexion", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

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