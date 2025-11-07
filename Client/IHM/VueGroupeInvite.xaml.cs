using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using VM_Footies;

namespace IHM_Footies
{
    /// <summary>
    /// Logique d'interaction pour VueGroupeInvite.xaml
    /// </summary>
    public partial class VueGroupeInvite : UserControl
    {
        #region Attributs
        private VMGroupeInvite groupe;
        public VMGroupeInvite Groupe => this.groupe;
        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur pour le designer
        /// </summary>
        public VueGroupeInvite()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructeur avec ViewModel
        /// </summary>
        /// <param name="groupe">Le groupe à afficher</param>
        public VueGroupeInvite(VMGroupeInvite groupe)
        {
            this.groupe = groupe;
            this.DataContext = this.groupe;

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
