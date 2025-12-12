using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Media.TextFormatting;
using System.Windows.Shapes;
using VM_Footies.VM;
using VM_Footies.VM_Page;

namespace IHM_Footies.Invitations
{
    /// <summary>
    /// Logique d'interaction pour VueFormulaireInvitation.xaml
    /// </summary>
    public partial class VueFormulaireInvitation : Window
    {

        #region Attributs
        private VMInvitation invitation;
        private VMPageInvitation vmPageInvitation;
        #endregion

        #region proprietes 
        public VMInvitation Menu => this.invitation;
        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur de la vue de formulaire d'invitation
        /// </summary>
        /// <param name="invitation"> prend en parametre le model des invitations</param>
        public VueFormulaireInvitation(VMInvitation invitation)
        {
            this.invitation = invitation;
            this.vmPageInvitation = new VMPageInvitation();
            this.DataContext = this.invitation;
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ChargerDonnees();
        }

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public VueFormulaireInvitation() : this(new VMInvitation())
        {
        }
        #endregion

        #region methodes
        /// <summary>
        /// Charge les données depuis l'API
        /// </summary>
        private async Task ChargerDonnees()
        {
            await this.vmPageInvitation.ChargerElementsDansInvitation(invitation);
        }

        private void Invitation_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "TexteRechercheInvite":
                    this.vmPageInvitation.RechercherInviteDansFormulaire(this.invitation, this.invitation.TexteRechercheInvite);
                    break;
                case "TexteRechercheGroupeInvite":
                    this.vmPageInvitation.RechercherGroupeDansFormulaire(this.invitation, this.invitation.TexteRechercheGroupeInvite);
                    break;
            }
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
        /// Bouton pour aller à la page des Statistiques
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonAllerStatistique_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerSelectionInvite(this);
        }

        /// <summary>
        /// Bouton pour aller à la page du tableau de bord
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonAllerTableauDeBord_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerTableaudebord(this);
        }
        /// <summary>
        /// Bouton pour aller à la page des informations utilisateur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonAllerInformationUtilisateur_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerInformationUtilisateur(this);
        }
        #endregion

        #region boutons 
        /// <summary>
        /// Bouton pour aller à la page de formulaire d'invitation plat/menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonFormulaireInvitationMenuPlat_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.invitation.Nom))
            {
                MessageBox.Show("Veuillez saisir un nom pour l'invitation.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            this.vmPageInvitation.PreparerSauvegarde(this.invitation);
            Navigation.AllerFormulaireInvitationPlatMenu(this, this.invitation);
        }

        private void RechercheInvite_Click(object sender, RoutedEventArgs e)
        {
            this.vmPageInvitation.RechercherInviteDansFormulaire(this.invitation, this.invitation.TexteRechercheInvite);
        }

        private void RefreshInvite_Click(object sender, RoutedEventArgs e)
        {
            this.invitation.TexteRechercheInvite = string.Empty;
            this.vmPageInvitation.RechercherInviteDansFormulaire(this.invitation, string.Empty);
        }

        private void RechercheGroupe_Click(object sender, RoutedEventArgs e)
        {
            this.vmPageInvitation.RechercherGroupeDansFormulaire(this.invitation, this.invitation.TexteRechercheGroupeInvite);
        }

        private void RefreshGroupe_Click(object sender, RoutedEventArgs e)
        {
            this.invitation.TexteRechercheGroupeInvite = string.Empty;
            this.vmPageInvitation.RechercherGroupeDansFormulaire(this.invitation, string.Empty);
        }
        #endregion

    }
}
