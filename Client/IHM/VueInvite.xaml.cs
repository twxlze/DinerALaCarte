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
    /// Logique d'interaction pour VueInvite.xaml
    /// </summary>
    public partial class VueInvite : UserControl
    {
        #region Attributs
        private VMInvite invite;
        public VMInvite Invite => this.invite;
        #endregion

        #region Constructeur
        public VueInvite(VMInvite invite)
        {
            this.invite = invite;
            this.DataContext = this.invite;
            this.Height = 20; /// Juste pour tester
            this.Background = Brushes.AliceBlue; /// Juste pour tester
            InitializeComponent();
        }
        #endregion

        #region Méthodes
        public void Deselectionner()
        {
            this.Background = new SolidColorBrush(Colors.Transparent);
        }

        public void Selectionner()
        {
            this.Background = new SolidColorBrush(Colors.Red);
        }
        #endregion
    }
}
