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

namespace IHM_Footies.Invitations
{
    /// <summary>
    /// Logique d'interaction pour VueFormulaireInvitation.xaml
    /// </summary>
    public partial class VueFormulaireInvitation : Window
    {

        #region Attributs
        private VMInvitation invitation;

        #endregion

        #region proprietes 

        public VMInvitation Menu => this.invitation;

        #endregion

        #region Constructeurs
        public VueFormulaireInvitation(VMInvitation invitation)
        {
            this.invitation = invitation;
            this.DataContext = this.invitation;
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ChargerDonnees();
        }

        /// <summary>
        /// Charge les données depuis l'API
        /// </summary>
        private async Task ChargerDonnees()
        {
            await this.invitation.ChargerInvites();
            await this.invitation.ChargerGroupeInvite();
        }

        public VueFormulaireInvitation() : this(new VMInvitation())
        {
        }

        #endregion

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

        /// <summary>
        /// Bouton pour aller à la page invitations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonAllerInvitation_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerInvitations(this);
        }

        /// <summary>
        /// Bouton pour aller à la page des menus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonAllerMenu_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerMenu(this);
        }

        /// <summary>
        /// Bouton pour aller à la page de formulaire d'invitation plat/menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonFormulaireInvitationMenuPlat_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerFormulaireInvitationPlatMenu(this);
        }


        #endregion


    }
}
