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

namespace IHM_Footies
{
    /// <summary>
    /// Logique d'interaction pour VuePagePlat.xaml
    /// </summary>
    public partial class VuePagePlat : Window
    {
        public VuePagePlat()
        {
            InitializeComponent();
        }

        #region Boutons 
        /// <summary>
        /// Ouvre la fenêtre d'ajout d'un plat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonAjouterPlat_Click(object sender, RoutedEventArgs e)
        {
            /*
            VueFormulairePlat fenetre = new VueFormulairePlat();
            bool? result = fenetre.ShowDialog();
            if (result == true)
            {
                this.vmPagePlat.AjouterPlat(fenetre.Plat);
                this.RafraichirListe();
            }*/
        }

        /// <summary>
        /// Ouvre la fenêtre de modification d'un plat sélectionné
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonModifierPlat_Click(object sender, RoutedEventArgs e)
        {/*
            if (this.vmPagePlat.PlatSelectionne != null)
            {
                VueFormulairePlat fenetre = new VueFormulairePlat(this.vmPagePlat.PlatSelectionne);
                bool? result = fenetre.ShowDialog();
                if (result == true)
                {
                    this.vmPagePlat.ModifierPlat(fenetre.Plat);
                    this.RafraichirListe();
                }
            }*/
        }


        /// <summary>
        /// Supprime le plat sélectionné
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BoutonSupprimerPlat_Click(object sender, RoutedEventArgs e)
        {/*
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
                            "Suppression impossible, le plat fait partie d'un ou plusieurs menus.",
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
            }*/
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
        /// Bouton pour aller à la vue du formulaire d'invité
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
        #endregion
    }
}
