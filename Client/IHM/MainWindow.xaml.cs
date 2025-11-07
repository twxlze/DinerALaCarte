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
    /// <summary>
    /// Constructeur par défaut de la fenêtre principale
    /// </summary>
    public MainWindow()
    {
        InitializeComponent();
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
    }

    /// <summary>
    /// Bouton pour aller à la vue des invités
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ClickBoutonAjouterInvite(object sender, RoutedEventArgs e)
    {
        Navigation.AllerInvites(this);
    }

    /// <summary>
    /// Bouton pour fermer la fenêtre
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ButonFermerFenetre_Click(object sender, RoutedEventArgs e)
    {
        Navigation.FermerFenetre(this);
    }
}