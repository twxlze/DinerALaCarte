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
using IHM;

namespace IHM_Footies
{
    /// <summary>
    /// Logique d'interaction pour VueInvites.xaml
    /// </summary>
    public partial class VueInvites : Window
    {
        public VueInvites(MainWindow mainWindows)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private async void BoutonAccueil_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            await Task.Delay(500);
            this.Close();
        }

        private void BoutonInvite_Click(object sender, RoutedEventArgs e)
        {
            // Cette page est déjà la page des invités, donc pas besoin de faire quoi que ce soit
            // Ou vous pouvez rafraîchir la page si nécessaire
        }
    }
}