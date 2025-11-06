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
        public VueFormulaireInvite(VMInvite invite)
        {
            this.invite = invite;
            this.DataContext = this.invite;

            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        public VueFormulaireInvite() : this(new VMInvite())
        {
        }
        #endregion

        #region Boutons d'action
        private void Enregistrer_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
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

        
        #endregion
    }
}
