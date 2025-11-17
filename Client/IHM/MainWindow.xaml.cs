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
    private List<VuePlat> vuePlat;
    private VMPagePlat vmPagePlat;
    #endregion

    /// <summary>
    /// Constructeur par défaut de la fenêtre principale
    /// </summary>
    public MainWindow()
    {
        this.vueInvite = new List<VueInvite>();
        this.vmPageInvite = new VMPageInvite();
        this.vuePlat = new List<VuePlat>();
        this.vmPagePlat = new VMPagePlat();
        this.vmPagePlat.PropertyChanged += VMPagePlat_PropertyChanged;
        this.vmPageInvite.PropertyChanged += VMPageInvite_PropertyChanged;

        InitializeComponent();
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
        this.RafraichirListe();
        this.RafraichirListePlats();
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
    /// Gestion du changement de propriété dans le VMPagePlat
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void VMPagePlat_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "VMPlat")
            this.RafraichirListePlats();
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
    /// Sélectionne un plat dans la liste des plats
    /// </summary>
    /// <param name="vue"> La vue du plat à sélectionner </param>
    private void SelectionnerPlat(VuePlat vue)
    {
        this.vmPagePlat.PlatSelectionne = vue.Plat;
        foreach (VuePlat vueP in this.vuePlat)
        {
            vueP.Deselectionner();
        }
        vue.Selectionner();
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
        this.PanelListeInvites.Children.Clear();
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
            this.PanelListeInvites.Children.Add(vue);
        }
    }

    /// <summary>
    /// Rafraîchit la liste des plats affichés
    /// </summary>
    private async void RafraichirListePlats()
    {
        this.PanelListePlat.Children.Clear();
        this.vuePlat.Clear();

        await this.vmPagePlat.ChargerPlatsAsync();

        foreach (VMPlat plat in this.vmPagePlat.VMPlat)
        {
            VuePlat vue = new VuePlat(plat);

            vue.MouseDown += (s, e) => this.SelectionnerPlat(vue);
            // vue.MouseDoubleClick += (s, e) => this.OuvrirModification(vue);

            vue.Height = 20;
            vue.Width = 580;
            vue.HorizontalAlignment = HorizontalAlignment.Center;
            vue.VerticalAlignment = VerticalAlignment.Center;

            this.vuePlat.Add(vue);
            this.PanelListePlat.Children.Add(vue);
        }
    }


    #region Boutons de navigation
    /// <summary>
    /// Bouton pour aller à la page plat
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BoutonAllerPlat_Click(object sender, RoutedEventArgs e)
    {
        Navigation.AllerPlat(this);
    }

    /// <summary>
    /// Bouton pour aller à l'accueil
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BoutonAllerAccueil_Click(object sender, RoutedEventArgs e)
    {
        Navigation.AllerAccueil(this);
    }

    /// <summary>
    /// Bouton pour aller au menu
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BoutonAllerMenu_Click(object sender, RoutedEventArgs e)
    {
        Navigation.AllerMenu(this);
    }
    /// <summary>
    /// Bouton pour aller à la page invité
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BoutonAllerInvite_Click(object sender, RoutedEventArgs e)
    {
        Navigation.AllerInvites(this);
    }

    #endregion


}