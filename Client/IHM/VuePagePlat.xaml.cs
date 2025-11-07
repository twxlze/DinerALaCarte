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
    /// Logique d'interaction pour VuePagePlat.xaml
    /// </summary>
    public partial class VuePagePlat : Window
    {
        #region Attributs
        private VMPagePlat vmPagePlat;
        private List<VuePlat> vuePlat;
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur par défaut d'une page de plat
        /// </summary>
        public VuePagePlat()
        {
            InitializeComponent();
            this.vuePlat = new List<VuePlat>();
            this.vmPagePlat = new VMPagePlat();
            this.vmPagePlat.PropertyChanged += VMPagePlat_PropertyChanged;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.RafraichirListe();
        }
        #endregion

        #region Méthodes
        /// <summary>
        /// Gestion du changement de propriété dans le VMPagePlat
        /// </summary>
        /// <param name="sender"> </param>
        /// <param name="e"></param>
        private void VMPagePlat_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "VMPlat") this.RafraichirListe();
        }

        /// <summary>
        /// Rafraîchit la liste des plats affichés
        /// </summary>
        private async void RafraichirListe()
        {
            this.PanelListePlat.Children.Clear();
            this.vuePlat.Clear();

            await this.vmPagePlat.ChargerPlatsAsync();

            foreach (VMPlat plat in this.vmPagePlat.VMPlat)
            {
                VuePlat vue = new VuePlat(plat);
                vue.MouseDown += (s, e) => this.SelectionnerPlat(vue);
                vue.MouseDoubleClick += (s, e) => this.OuvrirModification(vue);
                this.vuePlat.Add(vue);
                this.PanelListePlat.Children.Add(vue);
            }
        }

        /// <summary>
        /// Ouvre la fenêtre de modification d'un plat
        /// </summary>
        /// <param name="vue"></param>
        private void OuvrirModification(VuePlat vue)
        {
            VMPlat memoire = new VMPlat(vue.Plat);
            VueFormulairePlat fenetre = new VueFormulairePlat(vue.Plat);
            bool? result = fenetre.ShowDialog();
            if (result == false)
            {
                vue.Plat.ModifierPlat(memoire);
            }
        }

        /// <summary>
        /// Sélectionne un plat dans la liste des plats
        /// </summary>
        /// <param name="vue"> VuePlat sélectionnée </param>
        public void SelectionnerPlat(VuePlat vue)
        {
            this.vmPagePlat.PlatSelectionne = vue.Plat;
            foreach (VuePlat vueP in this.vuePlat)
            {
                vueP.Deselectionner();
            }
            vue.Selectionner();
        }
        #endregion

        #region Boutons 
        /// <summary>
        /// Ouvre la fenêtre d'ajout d'un plat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BoutonAjouterPlat_Click(object sender, RoutedEventArgs e)
        {
            VueFormulairePlat fenetre = new VueFormulairePlat();
            bool? result = fenetre.ShowDialog();
            if (result == true)
            {
                await this.vmPagePlat.AjouterPlat(fenetre.Plat);
               // this.RafraichirListe();
            }
        }

        /// <summary>
        /// Ouvre la fenêtre de modification d'un plat sélectionné
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BoutonModifierPlat_Click(object sender, RoutedEventArgs e)
        {
            if (this.vmPagePlat.PlatSelectionne != null)
            {
                VueFormulairePlat fenetre = new VueFormulairePlat(this.vmPagePlat.PlatSelectionne);
                bool? result = fenetre.ShowDialog();
                if (result == true)
                {
                    await this.vmPagePlat.ModifierPlat(fenetre.Plat);
                    this.RafraichirListe();
                }
            }
        }


        /// <summary>
        /// Supprime le plat sélectionné
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BoutonSupprimerPlat_Click(object sender, RoutedEventArgs e)
        {
            if (this.vmPagePlat.PlatSelectionne != null)
            {
                MessageBoxResult resultat = MessageBox.Show(
                    "Êtes-vous sûr de vouloir supprimer ce plat ?",
                    "Confirmation de suppression",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (resultat == MessageBoxResult.Yes)
                {
                    bool suppressionReussie = await this.vmPagePlat.SupprimerPlat();

                    if (!suppressionReussie)
                    {
                        MessageBox.Show(
                            "Suppression impossible, le plat est utilisé dans un ou plusieurs menus.",
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
                    "Veuillez sélectionner un plat à supprimer.",
                    "Aucun plat sélectionné",
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
        private void BoutonAccueil_Click(object sender, RoutedEventArgs e)
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
        /// Bouton pour fermer la fenêtre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButonFermerFenetre_Click(object sender, RoutedEventArgs e)
        {
            Navigation.FermerFenetre(this);
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
        #endregion
    }
}