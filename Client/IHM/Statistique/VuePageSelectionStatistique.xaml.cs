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
        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur de la vue de sélection des statistiques
        /// </summary>
        /// <param name="vMPageInvitation">prend en parametre le model des invitations</param>
        public VuePageSelectionStatistique()
        {
            InitializeComponent();
            this.vmPageStatistique = new VmPageStatistique();
            this.DataContext = this.vmPageStatistique;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        #endregion

        #region Boutons enregistrer l'affichage 
        /// <summary>
        /// Gestion du clic sur le bouton Enregistrer
        /// </summary>
        /// <param name="sender"> L'expéditeur </param>
        /// <param name="e"> Les arguments de l'événement </param>

        private async void Afficher_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool AumoinUnSelection = this.vmPageStatistique.InvitesSelectionnes != null;

                if (!AumoinUnSelection)
                {
                    MessageBox.Show(
                        "Sélectionnez au moins un invité pour voir les stats",
                        "Erreur de validation",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error
                    );
                }
                else
                {
                    this.vmPageStatistique.CreerStatistique();
                    Navigation.AllerStatistique(this, this.vmPageStatistique);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region Boutons de navigation
        /// <summary>
        /// Bouton pour aller à la page plat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonAllerPlat_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerPlat(this);
        }

        /// <summary>
        /// Bouton pour aller à l'accueil
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonAllerAccueil_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerAccueil(this);
        }

        /// <summary>
        /// Bouton pour aller au menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonAllerMenu_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerMenu(this);
        }
        /// <summary>
        /// Bouton pour aller à la page invité
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonAllerInvite_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerInvites(this);
        }

        /// <summary>
        /// Bouton pour aller à la page des réglages
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonAllerReglages_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerReglages(this);
        }

        /// <summary>
        /// Bouton pour aller à la page des groupes d'invités
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonAllerGroupeInvite_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerGroupesInvites(this);
        }

        /// <summary>
        /// Bouton pour aller à la page des invitations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonAllerInvitation_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerInvitations(this);
        }

        /// <summary>
        /// Bouton pour fermer la fenêtre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButonFermerFenetre_Click(object sender, RoutedEventArgs e)
        {
            Navigation.FermerFenetre(this);
        }

        /// <summary>
        /// Bouton pour aller à la page des statistiques d'invités
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonAllerStatistique_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerSelectionInvite(this);
        }

        /// <summary>
        /// Bouton pour reinitialiser le contenu de la barre de recherche
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonRetour_Click(object sender, RoutedEventArgs e)
        {
          
        }

        private void RechercheInvite_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

    }
}
