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

namespace IHM_Footies.Invitations
{
    /// <summary>
    /// Logique d'interaction pour VuePageInvitation.xaml
    /// </summary>
    public partial class VuePageInvitation : Window
    {
        #region Attributs
        private VMPageInvitation vmPageInvitation = new VMPageInvitation();
        private List<VueInvitation> vueInvitations = new List<VueInvitation>();
        #endregion

        public VuePageInvitation()
        {
            InitializeComponent();
            this.vmPageInvitation.PropertyChanged += VmPageInvitation_PropertyChanged;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //this.RafraichirListe();
        }

        #region Méthodes
        /// <summary>
        /// Gestion du changement de propriété dans le VMPageGroupeInvite
        /// </summary>
        /// <param name="sender"> L'expéditeur </param>
        /// <param name="e"> Les arguments de l'événement </param>
        private void VmPageInvitation_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //if (e.PropertyName == "VMGroupeInvite") this.RafraichirListe();
        }
        #endregion

        #region Boutons d'action
        private void BoutonAjouterInvitation_Click(object sender, RoutedEventArgs e)
        {
        }

        private void BoutonModifierInvitation_Click(object sender, RoutedEventArgs e)
        {
        }

        private void BoutonSupprimerInvitation_Click(object sender, RoutedEventArgs e)
        {
        }
        #endregion

        #region Boutons de navigation

        /// <summary>
        /// Bouton pour aller à la vue d'accueil
        /// </summary>
        /// <param name="sender"> L'expéditeur </param>
        /// <param name="e"> Les arguments de l'événement </param>
        private void BoutonVueAccueil(object sender, RoutedEventArgs e)
        {
            Navigation.AllerAccueil(this);
        }

        /// <summary>
        /// Bouton pour aller à la vue des invités
        /// </summary>
        /// <param name="sender"> L'expéditeur </param>
        /// <param name="e"> Les arguments de l'événement </param>
        private void BoutonInvite_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerInvites(this);
        }

        /// <summary>
        /// Bouton pour fermer la fenêtre
        /// </summary>
        /// <param name="sender"> L'expéditeur du clic </param>
        /// <param name="e"> Les arguments de l'événement </param>
        private void ButonFermerFenetre_Click(object sender, RoutedEventArgs e)
        {
            Navigation.FermerFenetre(this);
        }

        /// <summary>
        /// Bouton pour aller à la vue des groupes invités
        /// </summary>
        /// <param name="sender"> L'expéditeur </param>
        /// <param name="e"> Les arguments de l'événement </param>
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
        /// Bouton pour aller à la page plat
        /// </summary>
        /// <param name="sender"> L'expéditeur </param>
        /// <param name="e"> Les arguments de l'événement </param>
        private void BoutonAllerPlat_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerPlat(this);
        }

        #endregion

        private void BoutonAllerFormulaireInvitation_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerFormulaireInvitation(this);
        }
    }
}
