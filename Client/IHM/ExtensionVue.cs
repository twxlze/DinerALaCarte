using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace IHM_Footies
{
    /// <summary>
    /// Méthodes d'extension pour les vues avec sélection
    /// </summary>
    public static class ExtensionVue
    {
        /// <summary>
        /// Configure le style par défaut d'une vue
        /// </summary>
        public static void ConfigurerStyle(this UserControl control)
        {
            control.Height = 30;
            control.Width = 425;
            control.Background = Brushes.AliceBlue;
            control.FontSize = 14;
            control.HorizontalContentAlignment = HorizontalAlignment.Center;
            control.VerticalContentAlignment = VerticalAlignment.Center;
            control.BorderBrush = new SolidColorBrush(Colors.Gray);
            control.BorderThickness = new Thickness(0.4);
        }

        /// <summary>
        /// Sélectionne une vue
        /// </summary>
        public static void Selectionner(this Control control)
        {
            control.Background = new SolidColorBrush(Colors.Maroon);
            control.Foreground = new SolidColorBrush(Colors.White);
        }

        /// <summary>
        /// Désélectionne une vue
        /// </summary>
        public static void Deselectionner(this Control control)
        {
            control.Background = new SolidColorBrush(Colors.Transparent);
            control.Foreground = new SolidColorBrush(Colors.Black);
        }
    }
}
