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
using VM_Footies;

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
        /// Constructeur par défaut
        /// </summary>
        public VueFormulaireGroupeInvite() : this(new VMGroupeInvite())
        {
            InitializeComponent();
        }
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
        #endregion

        #region Boutons enregistrer modifications 

        private void Enregistrer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<string> erreurs = new List<string>();
                // nom
                if (string.IsNullOrWhiteSpace(this.groupeInvite.Nom))
                {
                    erreurs.Add("Entrez le nom du groupe d'invités");
                }
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion


        #region Boutons de navigation

        private void BoutonAccueil_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerAccueil(this);
        }

        private void ButonFermerFenetre_Click(object sender, RoutedEventArgs e)
        {
            Navigation.FermerFenetre(this);
        }

        private void BoutonInvite_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerInvites(this);
        }

        private void BoutonGroupeInvite_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerGroupesInvites(this);
        }


        #endregion
    }
}
