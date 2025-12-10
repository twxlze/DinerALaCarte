using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using VM_Footies.VM;

namespace IHM_Footies.Invitations
{
    public partial class VuePageInvitationDetail : Window
    {
        #region Attributs
        private VMInvitation vmInvitation;
        private string provenance;
        #endregion

        #region Constructeur
        public VuePageInvitationDetail(VMInvitation invitation, string provenance = "Invitation")
        {
            InitializeComponent();
            this.vmInvitation = invitation;
            this.DataContext = this.vmInvitation;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.provenance = provenance;
            this.RafraichirToutesLesListes();
        }
        #endregion

        #region Méthodes
        private void RafraichirToutesLesListes()
        {
            this.AfficherInvites();
            this.AfficherGroupes();
            this.AfficherMenus();
            this.AfficherPlats();
        }

        private void AfficherInvites()
        {
            this.PanelInvites.Children.Clear();
            List<VMInvite> listeVM = this.vmInvitation.ObtenirVMInvites();

            foreach (VMInvite vmInvite in listeVM)
            {
                VueInvite vue = new VueInvite(vmInvite);
                this.PanelInvites.Children.Add(vue);
            }
        }

        private void AfficherGroupes()
        {
            this.PanelGroupes.Children.Clear();
            List<VMGroupeInvite> listeVM = this.vmInvitation.ObtenirVMGroupes();

            foreach (VMGroupeInvite vmGroupe in listeVM)
            {
                VueGroupeInvite vue = new VueGroupeInvite(vmGroupe);
                this.PanelGroupes.Children.Add(vue);
            }
        }

        private void AfficherMenus()
        {
            this.PanelMenus.Children.Clear();
            List<VMMenu> listeVM = this.vmInvitation.ObtenirVMMenus();

            foreach (VMMenu vmMenu in listeVM)
            {
                Border bordure = this.EsthetiqueVisuel(vmMenu.Nom);
                this.PanelMenus.Children.Add(bordure);
            }
        }

        private void AfficherPlats()
        {
            this.PanelPlats.Children.Clear();
            List<VMPlat> listeVM = this.vmInvitation.ObtenirVMPlats();

            foreach (VMPlat vmPlat in listeVM)
            {
                Border bordure = this.EsthetiqueVisuel(vmPlat.Nom);
                this.PanelPlats.Children.Add(bordure);
            }
        }

        private Border EsthetiqueVisuel(string texte)
        {
            Border bordure = new Border();
            bordure.Background = Brushes.White;
            bordure.BorderBrush = (Brush)new BrushConverter().ConvertFrom("#E0E0E0");
            bordure.BorderThickness = new Thickness(0, 0, 0, 1);
            bordure.Padding = new Thickness(10);
            bordure.Margin = new Thickness(0, 2, 0, 2);

            TextBlock textBlock = new TextBlock();
            textBlock.Text = texte;
            textBlock.FontSize = 14;
            textBlock.Foreground = (Brush)new BrushConverter().ConvertFrom("#333333");

            bordure.Child = textBlock;

            return bordure;
        }

        private void RetourAuxInvitations_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerInvitations(this);
        }
        #endregion
    }
}