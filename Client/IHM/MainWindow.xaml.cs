using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using IHM_Footies;

namespace IHM;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
    }

    private void ClickBoutonAjouterInvite(object sender, RoutedEventArgs e)
    {
        Navigation.AllerInvites(this);
    }

    private void ButonFermerFenetre_Click(object sender, RoutedEventArgs e)
    {
        Navigation.FermerFenetre(this);
    }

    private void CliqueBoutonMenu(object sender, RoutedEventArgs e)
    {
        Navigation.AllerFormulaireMenu(this);
    }
}