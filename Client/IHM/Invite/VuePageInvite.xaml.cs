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
        /// <summary>
        /// Constructeur par défaut d'une page d'invité
        /// </summary>
        public VuePageInvite()
        {
            InitializeComponent();

            this.vueInvite = new List<VueInvite>();
            this.vmPageInvite = new VMPageInvite();
            this.vmPageInvite.PropertyChanged += VMPageInvite_PropertyChanged;

            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.RafraichirListe();
        }
        #endregion

        #region Méthodes
        /// <summary>
        /// Gestion du changement de propriété dans le VMPageInvite
        /// </summary>
        /// <param name="sender"> </param>
        /// <param name="e"></param>
        private void VMPageInvite_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "VMInvites") this.RafraichirListe();
        }

        /// <summary>
        /// Rafraîchit la liste des invités affichés
        /// </summary>
        private async void RafraichirListe()
        {
            this.PanelListeInvites.Children.Clear();
            this.vueInvite.Clear();

            await this.vmPageInvite.ChargerInvitesAsync();

            foreach (VMInvite invite in this.vmPageInvite.VMInvites)
            {
                VueInvite vue = new VueInvite(invite);
                vue.MouseDown += (s, e) => this.SelectionnerPersonne(vue);
                vue.MouseDoubleClick += (s, e) => this.OuvrirModification(vue);
                this.vueInvite.Add(vue);
                this.PanelListeInvites.Children.Add(vue);
            }
        }

        /// <summary>
        /// Ouvre la fenêtre de modification d'un invité
        /// </summary>
        /// <param name="vue"></param>
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

        /// <summary>
        /// Sélectionne une personne dans la liste des invités
        /// </summary>
        /// <param name="vue"> VueInvite sélectionnée </param>
        public void SelectionnerPersonne(VueInvite vue)
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
        /// <summary>
        /// Ouvre la fenêtre d'ajout d'un invité
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BoutonAjouterInvite_Click(object sender, RoutedEventArgs e)
        {
            VueFormulaireInvite fenetre = new VueFormulaireInvite();
            bool? result = fenetre.ShowDialog();
            if (result == true)
            {
                /*
                await this.vmPageInvite.AjouterInvite(fenetre.Invite);
                this.RafraichirListe();
                */
                await this.vmPageInvite.AjouterInvite(fenetre.Invite);
                VueInvite vue = new VueInvite(fenetre.Invite);
                vue.MouseDown += (s, ev) => this.SelectionnerPersonne(vue);
                vue.MouseDoubleClick += (s, ev) => this.OuvrirModification(vue);
                this.vueInvite.Add(vue);
                this.PanelListeInvites.Children.Add(vue);
            }
            /*
               // this.RafraichirListe();
            }
            */
        }

        /// <summary>
        /// Ouvre la fenêtre de modification d'un invité sélectionné
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BoutonModifierInvite_Click(object sender, RoutedEventArgs e)
        {
            if (this.vmPageInvite.InviteSelectionne != null)
            {
                VueFormulaireInvite fenetre = new VueFormulaireInvite(this.vmPageInvite.InviteSelectionne);
                bool? result = fenetre.ShowDialog();
                if (result == true)
                {
                    await this.vmPageInvite.ModifierInvite(fenetre.Invite);
                    this.RafraichirListe();
                }
            }
        }


        /// <summary>
        /// Supprime l'invité sélectionné
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BoutonSupprimerInvite_Click(object sender, RoutedEventArgs e)
        {
            if (this.vmPageInvite.InviteSelectionne != null)
            {
                MessageBoxResult resultat = MessageBox.Show(
                    "Êtes-vous sûr de vouloir supprimer cet invité ?",
                    "Confirmation de suppression",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (resultat == MessageBoxResult.Yes)
                {
                    bool suppressionReussie = await this.vmPageInvite.SupprimerInvite();

                    if (!suppressionReussie)
                    {
                        MessageBox.Show(
                            "Suppression impossible, l'invité fait partie d'un ou plusieurs groupes.",
                            "Suppression impossible",
                            MessageBoxButton.OK,
                            MessageBoxImage.Warning);
                    }
                    else
                    {
                        this.RafraichirListe();
                    }
                }
            }
            else
            {
                MessageBox.Show(
                    "Veuillez sélectionner un invité à supprimer.",
                    "Aucun invité sélectionné",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
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
        /// Bouton pour aller à la vue du formulaire d'invité
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonAccueil_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerAccueil(this);
        }

        /// <summary>
        /// Bouton pour aller à la page plat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonPlat_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerPlat(this);
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
        #endregion

        private void BoutonGroupeInvite_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerGroupesInvites(this);
        }
    }
}
