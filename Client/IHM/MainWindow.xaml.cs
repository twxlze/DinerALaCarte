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
using VM_Footies;

namespace IHM;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    #region Attributs
    private List<VueInvite> vueInvite;
    private VMPageInvite vmPageInvite;
    #endregion
    /// <summary>
    /// Constructeur par défaut de la fenêtre principale
    /// </summary>
    public MainWindow()
    {
        this.vueInvite = new List<VueInvite>();
        this.vmPageInvite = new VMPageInvite();

        this.vmPageInvite.PropertyChanged += VMPageInvite_PropertyChanged;

        InitializeComponent();
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
        this.RafraichirListe();
    }

    /// <summary>
    /// Gestion du changement de propriété dans le VMPageInvite
    /// </summary>
    private void VMPageInvite_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "VMInvite")
            this.RafraichirListe();
    }

    /// <summary>
    /// Sélectionne une personne dans la liste des invités
    /// </summary>
    private void SelectionnerPersonne(VueInvite vue)
    {
        this.vmPageInvite.InviteSelectionne = vue.Invite;
        foreach (VueInvite vueI in this.vueInvite)
        {
            vueI.Deselectionner();
        }
        vue.Selectionner();
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

    private async void RafraichirListe()
    {
        this.PanelListeInvitesAccueil.Children.Clear();
        this.vueInvite.Clear();

        await this.vmPageInvite.ChargerInvitesAsync();

        foreach (VMInvite invite in this.vmPageInvite.VMInvites)
        {
            VueInvite vue = new VueInvite(invite);

            vue.MouseDown += (s, e) => this.SelectionnerPersonne(vue);
            // vue.MouseDoubleClick += (s, e) => this.OuvrirModification(vue);

            vue.Height = 20;
            vue.HorizontalAlignment = HorizontalAlignment.Center;
            vue.VerticalAlignment = VerticalAlignment.Center;

            this.vueInvite.Add(vue);
            this.PanelListeInvitesAccueil.Children.Add(vue);
        }
    }


}