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

namespace IHM_Footies.Menu
{
    /// <summary>
    /// Logique d'interaction pour VueMenu.xaml
    /// </summary>
    public partial class VueMenu : UserControl
    {
        #region Attributs
        private VMMenu menu;
        public VMMenu Menu => this.menu;
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur d'une vue de menu
        /// </summary>
        /// <param name="menu"> Le VMMenu à afficher </param>
        public VueMenu(VMMenu menu)
        {
            this.menu = menu;
            this.DataContext = this.menu;

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
        /// <summary>
        /// Désélectionne la vue de menu (remet les couleurs par défaut)
        /// </summary>
        public void Deselectionner()
        {
            this.Background = new SolidColorBrush(Colors.Transparent);
            this.Foreground = new SolidColorBrush(Colors.Black);
        }

        /// <summary>
        /// Sélectionne la vue de menu (change les couleurs pour indiquer la sélection)
        /// </summary>
        public void Selectionner()
        {
            this.Background = new SolidColorBrush(Colors.Maroon);
            this.Foreground = new SolidColorBrush(Colors.White);
        }
        #endregion

    }
}
