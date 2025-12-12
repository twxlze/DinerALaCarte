using IHM_Footies.GroupeInvite;
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
using VM_Footies.VM;

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

        /// <summary>
        /// Constructeur par défaut d'une page de groupe invité
        /// </summary>
        public VuePageGroupeInvite()
        {
            InitializeComponent();
            this.Initialiser();
            this.RafraichirListe();
        }

        private void Initialiser()
        {
            this.vueGroupeInvite = new List<VueGroupeInvite>();
            this.vmPageGroupeInvite = new VMPageGroupeInvite();
            this.vmPageGroupeInvite.PropertyChanged += VMPageGroupeInvite_PropertyChanged;

            this.DataContext = this.vmPageGroupeInvite;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

        }

        #region Méthodes
        /// <summary>
        /// Gestion du changement de propriété dans le VMPageGroupeInvite
        /// </summary>
        /// <param name="sender"> L'expéditeur </param>
        /// <param name="e"> Les arguments de l'événement </param>
        private void VMPageGroupeInvite_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "VMGroupeInvite") this.RafraichirListe();
        }

        /// <summary>
        /// Rafraîchit la liste des groupes invités affichés
        /// </summary>
        private async void RafraichirListe()
        {
            this.PanelListeGroupeInvites.Children.Clear();
            this.vueGroupeInvite.Clear();

            await this.vmPageGroupeInvite.ChargerGroupeInvites();

            foreach (VMGroupeInvite groupe in this.vmPageGroupeInvite.VMGroupeInvite)
            {
                VueGroupeInvite vue = new VueGroupeInvite(groupe);
                vue.MouseDown += (s, e) => this.SelectionnerGroupe(vue);
                vue.MouseDoubleClick += (s, e) => this.OuvrirDetailGroupe(vue);
                this.vueGroupeInvite.Add(vue);
                this.PanelListeGroupeInvites.Children.Add(vue);
            }
        }


        /// <summary>
        /// Ouvre la fenêtre des details d'un groupe invité
        /// </summary>
        /// <param name="vue"> La vue du groupe invité pour lequelle on veut ces details </param>
        private void OuvrirDetailGroupe(VueGroupeInvite vue)
        {
            if (this.vmPageGroupeInvite.GroupeSelectionne != null)
            {
                Navigation.AllerDetailGroupeInvite(this, this.vmPageGroupeInvite.GroupeSelectionne);
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un groupe pour voir ses détails.", "Aucun groupe sélectionné", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Sélectionne un groupe dans la liste des groupes invités
        /// </summary>
        /// <param name="vue"> La vue du groupe invité </param>
        private void SelectionnerGroupe(VueGroupeInvite vue)
        {
            this.vmPageGroupeInvite.GroupeSelectionne = vue.Groupe;
            foreach (VueGroupeInvite v in this.vueGroupeInvite)
            {
                v.Deselectionner();
            }
            vue.Selectionner();
        }

        #endregion

        #region Boutons 
        /// <summary>
        /// Ouvre la fenêtre d'ajout d'un groupe invité
        /// </summary>
        /// <param name="sender"> l'expéditeur </param>
        /// <param name="e"> Les arguments de l'événement </param>
        private async void BoutonAjouterGroupeInvite_Click(object sender, RoutedEventArgs e)
        {
            VMGroupeInvite nvGroupe = new VMGroupeInvite();
            await this.vmPageGroupeInvite.ChargerInvitesDansGroupe(nvGroupe);

            
            VueFormulaireGroupeInvite fenetre = new VueFormulaireGroupeInvite(nvGroupe);
            bool? result = fenetre.ShowDialog();
            
            if (result == true)
            {
                try
                {
                    await this.vmPageGroupeInvite.AjouterGroupe(nvGroupe);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erreur lors de l'ajout du groupe", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// Ouvre la fenêtre de modification d'un groupe invité
        /// </summary>
        /// <param name="sender"> l'expéditeur </param>
        /// <param name="e"> Les arguments de l'événement </param>
        private async void BoutonModifierGroupeInvite_Click(object sender, RoutedEventArgs e)
        {
            VMGroupeInvite copie = new VMGroupeInvite(this.vmPageGroupeInvite.GroupeSelectionne);
            await this.vmPageGroupeInvite.ChargerInvitesDansGroupe(copie);
            VueFormulaireGroupeInvite fenetre = new VueFormulaireGroupeInvite(copie);
            bool? result = fenetre.ShowDialog();
            if (result == true)
            {
                await this.vmPageGroupeInvite.ModifierGroupe(copie);
            }
        }

        /// <summary>
        /// Supprime le menu sélectionné
        /// </summary>
        /// <param name="sender"> L'expéditeur </param>
        /// <param name="e"> Les arguments de l'événement </param>
        private async void BoutonSupprimerGroupeInvite_Click(object sender, RoutedEventArgs e)
        {
            if (this.vmPageGroupeInvite.GroupeSelectionne != null)
            {
                MessageBoxResult resultat = MessageBox.Show("Êtes-vous sûr de vouloir supprimer ce groupe ?","Confirmation de suppression",MessageBoxButton.YesNo,MessageBoxImage.Question);

                if (resultat == MessageBoxResult.Yes)
                {
                    bool suppressionReussie = await this.vmPageGroupeInvite.SupprimerGroupe();

                    if (!suppressionReussie)
                    {
                        MessageBox.Show("Suppression impossible, le groupe est utilisé dans une ou plusieurs invitations.","Suppression impossible",MessageBoxButton.OK,MessageBoxImage.Warning);
                    }
                    else
                    {
                        this.RafraichirListe();
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un groupe à supprimer.","Aucun groupe sélectionné",MessageBoxButton.OK,MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Recherche les groupe invités selon le texte saisi
        /// </summary>
        /// <param name="sender"> L'expéditeur </param>
        /// <param name="e"> Les arguments de l'événement </param>
        private async void RecherchegroupeInvite_Click(object sender, RoutedEventArgs e)
        {
            this.PanelListeGroupeInvites.Children.Clear();
            this.vueGroupeInvite.Clear();

            await this.vmPageGroupeInvite.ChercherGroupeInvite(this.vmPageGroupeInvite.TexteRechercheGroupe);

            if (this.vmPageGroupeInvite.VMGroupeInvite.Count != 0)
            {
                foreach (VMGroupeInvite groupe in this.vmPageGroupeInvite.VMGroupeInvite)
                {
                    VueGroupeInvite vue = new VueGroupeInvite(groupe);
                    vue.MouseDown += (s, e) => this.SelectionnerGroupe(vue);
                    vue.MouseDoubleClick += (s, e) => this.OuvrirDetailGroupe(vue);
                    this.vueGroupeInvite.Add(vue);
                    this.PanelListeGroupeInvites.Children.Add(vue);
                }
            }
            else 
            {   
                TextBlock aucunResultat = new TextBlock
                {
                    Text = "Aucun résultat trouvé",
                    Foreground = Brushes.Gray,
                    FontSize = 16,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(0, 20, 0, 0)
                };

                this.PanelListeGroupeInvites.Children.Add(aucunResultat);
            }
        }

        private void BoutonRetour_Click(object sender, RoutedEventArgs e)
        {
            this.RafraichirListe();
            this.vmPageGroupeInvite.TexteRechercheGroupe = string.Empty;
        }


        #endregion

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

        /// <summary>
        /// Bouton pour aller à la page des réglages
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonAllerReglages_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerReglages(this);
        }

        /// <summary>
        /// Bouton pour aller à la page des groupes d'invités
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonAllerGroupeInvite_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerGroupesInvites(this);
        }


        /// <summary>
        /// Bouton pour aller à la page des invitations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonAllerInvitation_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerInvitations(this);
        }

        /// Bouton pour fermer la fenêtre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButonFermerFenetre_Click(object sender, RoutedEventArgs e)
        {
            Navigation.FermerFenetre(this);
        }

        /// <summary>
        /// Bouton pour aller à la page des Statistiques
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonAllerStatistique_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerSelectionInvite(this);
        }

        /// <summary>
<<<<<<< HEAD
        /// Bouton pour aller à la page du tableau de bord
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonAllerTableauDeBord_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerTableaudebord(this);
=======
        /// Bouton pour aller à la page des informations utilisateur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonAllerInformationUtilisateur_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerInformationUtilisateur(this);
>>>>>>> Test-Merge-TableauDeBord-Sprint3
        }
        #endregion


    }
}
