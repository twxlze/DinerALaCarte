using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VM_Footies;
using VM_Footies.VM;


namespace IHM_Footies
{
    /// <summary>
    /// Logique d'interaction pour VueFormulairePlat.xaml
    /// </summary>
    public partial class VueFormulairePlat : Window
    {
        #region Attributs
        private VMPlat plat;
        private VMPagePlat vmPagePlat;
        #endregion

        #region Propriétés
        /// <summary>
        /// Plat associée au VMPlat
        /// </summary>
        public VMPlat Plat => this.plat;

        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur d'une vue de formulaire de plat
        /// </summary>
        /// <param name="plat"> Le VMPlat à afficher </param>
        public VueFormulairePlat(VMPlat plat)
        {
            this.plat = plat;
            this.vmPagePlat = new VMPagePlat();
            this.DataContext = this.plat;
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        /// <summary>
        /// Constructeur par défaut d'une vue de formulaire de plat
        /// </summary>
        public VueFormulairePlat() : this(new VMPlat())
        {
        }
        #endregion

        #region Boutons d'action
        /// <summary>
        /// Gestion du clic sur le bouton Enregistrer
        /// </summary>
        /// <param name="sender"> L'expéditeur </param>
        /// <param name="e"> Les arguments de l'événement </param>
        private void Enregistrer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<string> erreurs = new List<string>();

                // nom
                if (string.IsNullOrWhiteSpace(this.plat.Nom))
                {
                    erreurs.Add("Entrez le nom du plat");
                }

                // pleins d'erreurs 
                if (erreurs.Count > 0)
                {
                    string message = string.Join("\n", erreurs);
                    MessageBox.Show(message, "Erreur de validation", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    this.plat.SauvegarderAllergenes();
                    this.DialogResult = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'enregistrement : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
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
    }
}
