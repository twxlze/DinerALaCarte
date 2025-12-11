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
    /// Logique d'interaction pour VueCreationCompte.xaml
    /// </summary>
    public partial class VueCreationCompte : Window
    {
        #region Attributs
        private VMPageInscription vmInscription;
        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur de la vue de création de compte
        /// </summary>
        public VueCreationCompte()
        {
            InitializeComponent();
            this.vmInscription = new VMPageInscription();
            this.DataContext = this.vmInscription;
        }
        #endregion

        #region Méthodes (Gestionnaires d'événements)

        /// <summary>
        /// Gère le clic sur le bouton "S'enregistrer"
        /// </summary>
        /// <param name="sender">L'expéditeur du clic</param>
        /// <param name="e">Les arguments de l'événement</param>
        private async void BoutonEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string motDePasseSaisi = ChampMotDePasse.Password;
                bool inscriptionReussie = await this.vmInscription.Inscription(motDePasseSaisi);
                if (inscriptionReussie)
                {
                    MessageBox.Show( "Compte créé avec succès ! Vous pouvez maintenant vous connecter.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                    Navigation.AllerConnexion(this);
                }
                else
                {
                    MessageBox.Show( this.vmInscription.MessageErreur, "Erreur d'inscription", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show( "Une erreur inattendue est survenue : " + ex.Message, "Erreur Critique", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Gère le clic sur le bouton de retour à la connexion
        /// </summary>
        private void BoutonRetourConnexion_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerConnexion(this);
        }

        /// <summary>
        /// Gère le clic sur le bouton pour fermer la fenêtre
        /// </summary>
        private void BoutonFermerFenetre_Click(object sender, RoutedEventArgs e)
        {
            Navigation.FermerFenetre(this);
        }

        #endregion
    }
}