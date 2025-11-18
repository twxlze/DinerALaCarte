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
using System.Windows.Shapes;
using VM_Footies.VM;

namespace IHM_Footies
{
    /// <summary>
    /// Logique d'interaction pour VueFormulaireMenu.xaml
    /// </summary>
    public partial class VueFormulaireMenu : Window
    {
        #region Attributs
        private VMMenu menu;
        public VMMenu Menu => this.menu;
        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur d'une vue de formulaire de menu
        /// </summary>
        /// <param name="menu">Le VMMenu à afficher</param>
        public VueFormulaireMenu(VMMenu menu)
        {
            this.menu = menu;
            this.DataContext = this.menu;
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        /// <summary>
        /// Constructeur par défaut d'une vue de formulaire de menu
        /// </summary>
        public VueFormulaireMenu() : this(new VMMenu())
        {
        }
        #endregion

        #region Boutons d'action
        /// <summary>
        /// Gestion du clic sur le bouton Enregistrer
        /// </summary>
        /// <param name="sender">L'expéditeur</param>
        /// <param name="e">Les arguments de l'événement</param>
        private async void Enregistrer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<string> erreurs = new List<string>();

                if (string.IsNullOrWhiteSpace(this.menu.Nom))
                {
                    erreurs.Add("Entrez le nom du menu");
                }

                if (this.menu.Plats == null || this.menu.Plats.Count == 0)
                {
                    erreurs.Add("Sélectionnez au moins un plat pour le menu");
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
                MessageBox.Show(
                    "Erreur lors de la validation : " + ex.Message,
                    "Erreur",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
        #endregion

        #region Boutons de navigation
        /// <summary>
        /// Bouton pour aller à la vue d'accueil
        /// </summary>
        /// <param name="sender">L'expéditeur</param>
        /// <param name="e">Les arguments de l'événement</param>
        private void BoutonAccueil_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerAccueil(this);
        }

        /// <summary>
        /// Bouton pour fermer la fenêtre
        /// </summary>
        /// <param name="sender">L'expéditeur du clic</param>
        /// <param name="e">Les arguments de l'événement</param>
        private void ButonFermerFenetre_Click(object sender, RoutedEventArgs e)
        {
            Navigation.FermerFenetre(this);
        }
        #endregion
    }
}
