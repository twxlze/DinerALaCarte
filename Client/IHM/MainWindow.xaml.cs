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
using VM_Footies.VM;

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
    private List<VueGroupeInvite> vueGroupeInvites;
    private VMPageGroupeInvite vmPageGroupeInvite;
    #endregion

    /// <summary>
    /// Constructeur par défaut de la fenêtre principale
    /// </summary>
    public MainWindow()
    {
        InitialiserViewModels();
        InitialiserEvenements();
        InitializeComponent();
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
        ChargerToutesLesDonnees();
    }

    /// <summary>
    /// Initialise les ViewModels utilisés dans la fenêtre principale
    /// </summary>
    private void InitialiserViewModels()
    {
        this.vueInvite = new List<VueInvite>();
        this.vmPageInvite = new VMPageInvite();

        this.vuePlat = new List<VuePlat>();
        this.vmPagePlat = new VMPagePlat();

        this.vueGroupeInvites = new List<VueGroupeInvite>();
        this.vmPageGroupeInvite = new VMPageGroupeInvite();
    }

    /// <summary>
    /// Initialise les événements pour les ViewModels utilisés dans la fenêtre principale
    /// </summary>
    private void InitialiserEvenements()
    {
        this.vmPageInvite.PropertyChanged += VMPage_PropertyChanged;
        this.vmPagePlat.PropertyChanged += VMPage_PropertyChanged;
        this.vmPageGroupeInvite.PropertyChanged += VMPage_PropertyChanged;
    }

    /// <summary>
    /// Charge toutes les données nécessaires pour la fenêtre principale
    /// </summary>
    private async void ChargerToutesLesDonnees()
    {
        await Task.WhenAll(
            ChargerInvites(),
            ChargerPlats(),
            ChargerGroupes()
        );
    }

    /// <summary>
    /// Gestion du changement de propriété dans le VMPageInvite
    /// </summary>
    private void VMPage_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case "VMInvites":
                this.ChargerInvites();
                break;
            case "VMPlat":
                this.ChargerPlats();
                break;
            case "ListeVMGroupeInvite":
                this.ChargerGroupes();
                break;
        }
    }


    /// <summary>
    /// Sélectionne une personne dans la liste des invités
    /// </summary>
    private void SelectionnerInvite(VueInvite vue)
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

    private void SelectionnerGroupeInvite(VueGroupeInvite vue)
    {
        this.vmPageGroupeInvite.GroupeSelectionne = vue.Groupe;
        foreach (VueGroupeInvite vueGI in this.vueGroupeInvites)
        {
            vueGI.Deselectionner();
        }
        vue.Selectionner();
    }
    /// <summary>
    /// Rafraîchit la liste des invités affichés
    /// </summary>
    private async Task ChargerInvites()
    {
        this.PanelListeInvites.Children.Clear();
        this.vueInvite.Clear();

        await this.vmPageInvite.ChargerInvites();

        foreach (VMInvite invite in this.vmPageInvite.VMInvites)
        {
            VueInvite vue = new VueInvite(invite);

            vue.MouseDown += (s, e) => this.SelectionnerInvite(vue);

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
    private async Task ChargerPlats()
    {
        this.PanelListePlat.Children.Clear();
        this.vuePlat.Clear();

        await this.vmPagePlat.ChargerPlats();

        foreach (VMPlat plat in this.vmPagePlat.VMPlat)
        {
            VuePlat vue = new VuePlat(plat);

            vue.MouseDown += (s, e) => this.SelectionnerPlat(vue);
            vue.Height = 20;
            vue.HorizontalAlignment = HorizontalAlignment.Center;
            vue.Margin = new Thickness(0, 0, 20, 0);
            vue.VerticalAlignment = VerticalAlignment.Center;

            this.vuePlat.Add(vue);
            this.PanelListePlat.Children.Add(vue);
        }
    }

    /// <summary>
    /// Rafraîchit la liste des groupes d'invités affichés
    /// </summary>
    private async Task ChargerGroupes()
    {
        this.PanelListeGroupeInvite.Children.Clear();
        this.vueGroupeInvites.Clear();
        await this.vmPageGroupeInvite.ChargerGroupeInvites();
        foreach (VMGroupeInvite groupe in this.vmPageGroupeInvite.VMGroupeInvite)
        {
            VueGroupeInvite vue = new VueGroupeInvite(groupe);
            vue.MouseDown += (s, e) => this.SelectionnerGroupeInvite(vue);
            vue.Height = 20;
            vue.Width = 600;
            vue.HorizontalAlignment = HorizontalAlignment.Center;
            vue.VerticalAlignment = VerticalAlignment.Center;
            this.vueGroupeInvites.Add(vue);
            this.PanelListeGroupeInvite.Children.Add(vue);
        }
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