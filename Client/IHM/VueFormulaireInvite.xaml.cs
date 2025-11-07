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

namespace IHM_Footies
{
    /// <summary>
    /// Logique d'interaction pour VueFormulaireInvite.xaml
    /// </summary>
    public partial class VueFormulaireInvite : Window
    {
        #region Attributs
        private VMInvite invite;
        public VMInvite Invite => this.invite;
        #endregion


        #region Constructeurs
        /// <summary>
        /// Constructeur d'une vue de formulaire d'invité
        /// </summary>
        /// <param name="invite"> Le VMInvite à afficher </param>
        public VueFormulaireInvite(VMInvite invite)
        {
            this.invite = invite;
            this.DataContext = this.invite;

            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        /// <summary>
        /// Constructeur par défaut d'une vue de formulaire d'invité
        /// </summary>
        public VueFormulaireInvite() : this(new VMInvite())
        {
        }
        #endregion

        #region Boutons d'action
        /// <summary>
        /// Gestion du clic sur le bouton Enregistrer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Enregistrer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<string> erreurs = new List<string>();

                // nom
                if (string.IsNullOrWhiteSpace(this.invite.Nom))
                {
                    erreurs.Add("Entrez le nom de l'invité");
                }

                // prénom
                if (string.IsNullOrWhiteSpace(this.invite.Prenom))
                {
                    erreurs.Add("Entrez le prénom de l'invité");
                }

                // téléphone
                if (!string.IsNullOrWhiteSpace(this.invite.Telephone))
                {
                    if (!long.TryParse(this.invite.Telephone, out _))
                    {
                        erreurs.Add("Le numéro de téléphone doit contenir uniquement des chiffres");
                    }
                    else if (this.invite.Telephone.Length != 10)
                    {
                        erreurs.Add("Le numéro de téléphone doit avoir 10 chiffres");
                    }
                }

                // email
                if (!string.IsNullOrWhiteSpace(this.invite.Email))
                {
                    if (!Regex.IsMatch(this.invite.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                    {
                        erreurs.Add("L'adresse email n'est pas valide");
                    }
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
        #endregion

        #region Boutons de navigation
        /// <summary>
        /// Bouton pour aller à la vue d'accueil
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonAccueil_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerAccueil(this);
        }

        /// <summary>
        /// Bouton pour aller à la vue des invités
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButonFermerFenetre_Click(object sender, RoutedEventArgs e)
        {
            Navigation.FermerFenetre(this);
        }

        
        #endregion
    }
}
