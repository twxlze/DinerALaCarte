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
using VM_Footies;

namespace IHM_Footies
{
    /// <summary>
    /// Logique d'interaction pour VuePageInvite.xaml
    /// </summary>
    public partial class VuePageInvite : Window
    {
        #region Attributs
        private VMPageInvite vmPageInvite;
        private List<VueInvite> vueInvite;
        #endregion

        #region Constructeur
        public VuePageInvite()
        {
            this.vueInvite = new List<VueInvite>();
            this.vmPageInvite = new VMPageInvite();
            this.vmPageInvite.PropertyChanged += VMPageInvite_PropertyChanged;

            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.RafraichirListe();
        }
        #endregion

        #region Méthodes
        private void VMPageInvite_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "VMInvite") this.RafraichirListe();
        }

        private void RafraichirListe()
        {
            this.PanelListeInvites.Children.Clear();
            this.vueInvite.Clear();

            foreach (VMInvite invite in this.vmPageInvite.VMInvites)
            {
                VueInvite vue = new VueInvite(invite);
                vue.MouseDown += (s, e) => this.SelectionnerPersonne(vue);
                vue.MouseDoubleClick += (s, e) => this.OuvrirModification(vue);
                this.vueInvite.Add(vue);
                this.PanelListeInvites.Children.Add(vue);
            }
        }

        private void OuvrirModification(VueInvite vue)
        {
            VMInvite memoire = new VMInvite(vue.Invite);
            VueFormulaireInvite fenetre = new VueFormulaireInvite(vue.Invite);
            bool? result = fenetre.ShowDialog();
            if (result == false)
            {
                vue.Invite.ModifierInvite(memoire);
            }
        }
        private void SelectionnerPersonne(VueInvite vue)
        {
            this.vmPageInvite.InviteSelectionne = vue.Invite;
            foreach (VueInvite vueI in this.vueInvite)
            {
                vueI.Deselectionner();
            }
            vue.Selectionner();
        }
        #endregion

        #region Boutons 
        private void BoutonAjouterInvite_Click(object sender, RoutedEventArgs e)
        {
            VueFormulaireInvite fenetre = new VueFormulaireInvite();
            bool? result = fenetre.ShowDialog();
            if (result == true)
            {
                this.vmPageInvite.AjouterInvite(fenetre.Invite);
                this.RafraichirListe();
            }
        }

        
        private void BoutonSupprimerInvite_Click(object sender, RoutedEventArgs e)
        {
            //this.vmPageInvite.SupprimerInvite();
        }
        

        #endregion

        #region Boutons de navigation
        private void BoutonVueAccueil(object sender, RoutedEventArgs e)
        {
            Navigation.AllerAccueil(this);
        }

        private void BoutonInvite_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerInvites(this);
        }

        private void BoutonAccueil_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerAccueil(this);
        }

        private void ButonFermerFenetre_Click(object sender, RoutedEventArgs e)
        {
            Navigation.FermerFenetre(this);
        }
        #endregion
    }
}
