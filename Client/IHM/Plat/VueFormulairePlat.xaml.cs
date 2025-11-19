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
                    this.DialogResult = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'enregistrement : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Gère le changement de texte dans le champ ingrédients
        /// </summary>
        private async void TextBoxIngredients_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                string texte = textBox.Text;

                // Si le texte est assez long, rechercher des suggestions
                if (!string.IsNullOrWhiteSpace(texte) && texte.Length >= 2)
                {
                    await this.plat.RechercherSuggestionsIngredients(texte);

                    // Afficher la liste si on a des suggestions
                    if (this.plat.SuggestionsIngredients.Count > 0)
                    {
                        ListBoxSuggestions.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        ListBoxSuggestions.Visibility = Visibility.Collapsed;
                    }
                }
                else
                {
                    ListBoxSuggestions.Visibility = Visibility.Collapsed;
                }
            }
        }

        /// <summary>
        /// Gère la sélection d'une suggestion
        /// </summary>
        private void ListBoxSuggestions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBoxSuggestions.SelectedItem is string suggestion)
            {
                // Remplacer le contenu du TextBox par la suggestion
                this.plat.Ingredients = suggestion;
                
                // Cacher la liste
                ListBoxSuggestions.Visibility = Visibility.Collapsed;
                
                // Remettre le focus sur le TextBox
                TextBoxIngredients.Focus();
                TextBoxIngredients.CaretIndex = TextBoxIngredients.Text.Length;
            }
        }
        #endregion

        #region Boutons de navigation
        /// <summary>
        /// Bouton pour aller à la vue d'accueil
        /// </summary>
        /// <param name="sender"> L'expéditeur </param>
        /// <param name="e"> Les arguments de l'événement </param>
        private void BoutonAccueil_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerAccueil(this);
        }


        /// <summary>
        /// Bouton pour aller à la vue des plats
        /// </summary>
        /// <param name="sender"> L'expéditeur </param>
        /// <param name="e"> Les arguments de l'événement </param>
        private void BoutonAllerPlat_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerPlat(this);
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
        /// Bouton pour aller à la vue des invités
        /// </summary>
        /// <param name="sender"> L'expéditeur </param>
        /// <param name="e"> Les arguments de l'événement </param>
        private void BoutonAllerInvite_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerInvites(this);
        }

        /// <summary>
        /// Bouton pour aller à la page groupe invité
        /// </summary>
        /// <param name="sender"> L'expéditeur </param>
        /// <param name="e"> Les arguments de l'événement </param>
        private void BoutonAllerGroupeInvite_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerGroupesInvites(this);
        }

        /// <summary>
        /// Aller à la page d'invitations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonAllerInvitation_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerInvitations(this);
        }
            #endregion


        
    }
}
