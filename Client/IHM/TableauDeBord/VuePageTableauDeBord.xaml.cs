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
using VM_Footies.VM;
using VM_Footies.VM_Page;

namespace IHM_Footies.TableauDeBord
{
    /// <summary>
    /// Logique d'interaction pour VuePageTableauDeBord.xaml
    /// </summary>
    public partial class VuePageTableauDeBord : Window
    {
        #region Atributs    
        private VMPageTableauDeBord vmPageTableauDeBord;
        #endregion

        #region Constructeur
        /// <summary>
        /// le constructeur de la page tableau de bord
        /// </summary>
        public VuePageTableauDeBord()
        {
            InitializeComponent();
            this.vmPageTableauDeBord = new VMPageTableauDeBord();
            this.DataContext = this.vmPageTableauDeBord;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Initialiser();
        }
        #endregion

        #region Methode public
        /// <summary>
        /// Initialisation de la page tableau de bord
        /// </summary>
        public async void Initialiser()
        {
            await this.vmPageTableauDeBord.ChargerDonneesInvite();
            this.listeInviteStat.Children.Clear();
            foreach (VMInvite invite in this.vmPageTableauDeBord.ListeInvites)
            {
                VMStats vMStats = this.vmPageTableauDeBord.ChargerInvitationsParticipe(invite);
                VueTableauDeBord vueTableauDeBord = new VueTableauDeBord(vMStats);
                this.listeInviteStat.Children.Add(vueTableauDeBord);
            }
        }


        #endregion

        #region bouton d'action
        /// <summary>
        /// Bouton pour rechercher un invité dans la barre de recherche du tableau de bord
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RechercheInvite_Click(object sender, RoutedEventArgs e)
        {
            this.vmPageTableauDeBord.RechercherInviteTableauDeBord(this.vmPageTableauDeBord.TexteRechercheInvite);
        }

        /// <summary>
        /// Bouton pour reinitialiser le contenu de la barre de recherche
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonRetour_Click(object sender, RoutedEventArgs e)
        {
            this.vmPageTableauDeBord.TexteRechercheInvite = string.Empty;
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

        /// Bouton pour fermer la fenêtre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButonFermerFenetre_Click(object sender, RoutedEventArgs e)
        {
            Navigation.FermerFenetre(this);
        }

        /// <summary>
        /// Bouton pour aller à la page des Statistiques
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonAllerStatistique_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerSelectionInvite(this);
        }

        /// <summary>
        /// Bouton pour aller à la page tableau de bord
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonAllerTableauDeBord_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerTableaudebord(this);
        }
        #endregion

    }
}
