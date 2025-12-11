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

namespace IHM_Footies
{
    /// <summary>
    /// Logique d'interaction pour VueFormulaireGroupeInvite.xaml
    /// </summary>
    public partial class VueFormulaireGroupeInvite : Window
    {

        #region Attributs
        private VMGroupeInvite groupeInvite;
        #endregion

        #region Propriétés
        /// <summary>
        /// La viewModel du groupe d'invités
        /// </summary>
        public VMGroupeInvite GroupeInvite => this.groupeInvite;
        #endregion


        #region Constructeurs
        /// <summary>
        /// Constructeur avec ViewModel
        /// </summary>
        /// <param name="groupeInvite">la viewModel</param>
        public VueFormulaireGroupeInvite(VMGroupeInvite groupeInvite)
        {
            this.groupeInvite = groupeInvite;
            this.DataContext = this.groupeInvite;
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public VueFormulaireGroupeInvite() : this(new VMGroupeInvite())
        {
        }
        
        #endregion

        #region Boutons enregistrer modifications 
        /// <summary>
        /// Gestion du clic sur le bouton Enregistrer
        /// </summary>
        /// <param name="sender"> L'expéditeur </param>
        /// <param name="e"> Les arguments de l'événement </param>

        private async void Enregistrer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<string> erreurs = new List<string>();
                // nom
                if (string.IsNullOrWhiteSpace(this.groupeInvite.Nom))
                {
                    erreurs.Add("Entrez le nom du groupe d'invités");
                }
                if (this.groupeInvite.Invites == null || this.groupeInvite.Invites.Count == 0)
                {
                    erreurs.Add("Sélectionnez au moins un invité pour le groupe");
                }
                if (erreurs.Count > 0)
                {
                    string message = string.Join("\n", erreurs);
                    MessageBox.Show(message, "Erreur de validation", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    this.DialogResult = true;

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
        /// Bouton pour aller à la page des Statistiques
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonAllerStatistique_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerSelectionInvite(this);
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


    }
}
