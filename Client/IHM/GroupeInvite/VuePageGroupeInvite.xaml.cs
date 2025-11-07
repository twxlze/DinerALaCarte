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
using VM_Footies;

namespace IHM_Footies
{
    /// <summary>
    /// Logique d'interaction pour VuePageGroupeInvite.xaml
    /// </summary>
    public partial class VuePageGroupeInvite : Window
    {

        #region Attributs
        private VMPageGroupeInvite vmPageGroupeInvite;
        private List<VueGroupeInvite> vueGroupeInvite;
        #endregion

        public VuePageGroupeInvite()
        {
            this.vueGroupeInvite = new List<VueGroupeInvite>();
            this.vmPageGroupeInvite = new VMPageGroupeInvite();
            this.vmPageGroupeInvite.PropertyChanged += VMPageGroupeInvite_PropertyChanged;

            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.RafraichirListe();
        }

        #region Méthodes
        private void VMPageGroupeInvite_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "AjouterGroupe") this.RafraichirListe();
        }

        private async void RafraichirListe()
        {
            this.PanelListeGroupeInvites.Children.Clear();
            this.vueGroupeInvite.Clear();
            await this.vmPageGroupeInvite.ChargerGroupeInvites();
            foreach (VMGroupeInvite groupe in this.vmPageGroupeInvite.ListeVMGroupeInvite)
            {
                VueGroupeInvite vue = new VueGroupeInvite(groupe);
                vue.MouseDown += (s, e) => this.SelectionnerGroupe(vue);
                vue.MouseDoubleClick += (s, e) => this.OuvrirModificationGroupe(vue);
                this.PanelListeGroupeInvites.Children.Add(vue);
                this.vueGroupeInvite.Add(vue);
            }
        }

        private void SelectionnerGroupe(VueGroupeInvite vue)
        {
            this.vmPageGroupeInvite.GroupeSelectionner = vue.Groupe;
            foreach (VueGroupeInvite v in this.vueGroupeInvite)
            {
                v.Deselectionner();
            }
            vue.Selectionner();
        }

        private void OuvrirModificationGroupe(VueGroupeInvite vue)
        {
            VueFormulaireGroupeInvite vueFormulaire = new VueFormulaireGroupeInvite(vue.Groupe);
            vueFormulaire.ShowDialog();
        }
        #endregion

        #region Boutons 
        private async void BoutonAjouterGroupeInvite_Click(object sender, RoutedEventArgs e)
        {
            VueFormulaireGroupeInvite fenetre = new VueFormulaireGroupeInvite();
            bool? result = fenetre.ShowDialog();
            if (result == true)
            {
                await this.vmPageGroupeInvite.AjouterNouveauGroupe(fenetre.GroupeInvite);
                this.RafraichirListe();
            }
        }

        private async void BoutonModifierGroupeInvite_Click(object sender, RoutedEventArgs e)
        {
            if (this.vmPageGroupeInvite.GroupeSelectionner != null)
            {
                VueFormulaireGroupeInvite fenetre = new VueFormulaireGroupeInvite(this.vmPageGroupeInvite.GroupeSelectionner);
                bool? result = fenetre.ShowDialog();
                if (result == false)
                {
                    await this.vmPageGroupeInvite.ModifierGroupeAsync(fenetre.GroupeInvite);
                    this.RafraichirListe();
                }
            }
        }

        #endregion

        #region Boutons de navigation
        /// <summary>
        /// Bouton pour aller à la vue d'accueil
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonVueAccueil(object sender, RoutedEventArgs e)
        {
            Navigation.AllerAccueil(this);
        }

        /// <summary>
        /// Bouton pour aller à la vue des invités
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonInvite_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerInvites(this);
        }

        /// <summary>
        /// Bouton pour aller à la vue d'accueil
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonAccueil_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerAccueil(this);
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

        /// <summary>
        /// Bouton pour aller à la vue des groupes invités
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonGroupeInvite_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerGroupesInvites(this);
        }

        /// <summary>
        /// Bouton pour aller à la page plat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonAllerPlat_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerPlat(this);
        }
        #endregion
    }
}
