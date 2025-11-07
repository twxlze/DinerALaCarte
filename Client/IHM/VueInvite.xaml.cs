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

        /// <summary>
        /// constructeur par défaut pour le designer
        /// </summary>
        public VueInvite()
        {
            InitializeComponent();
        }
        public VueInvite(VMInvite invite)
        {
            this.invite = invite;
            this.DataContext = this.invite;

            this.Height = 30;
            this.Width = 425;
            this.Background = Brushes.AliceBlue;
            this.FontSize = 14;
            this.HorizontalContentAlignment = HorizontalAlignment.Center;
            this.VerticalContentAlignment = VerticalAlignment.Center;
            this.BorderBrush = new SolidColorBrush(Colors.Gray);
            this.BorderThickness = new Thickness(0.4);

            InitializeComponent();
        }

        

        #endregion

        #region Méthodes
        public void Deselectionner()
        {
            this.Background = new SolidColorBrush(Colors.Transparent);
            this.Foreground = new SolidColorBrush(Colors.Black);
        }

        public void Selectionner()
        {
            this.Background = new SolidColorBrush(Colors.Maroon);
            this.Foreground = new SolidColorBrush(Colors.White);
        }
        #endregion
    }
}
