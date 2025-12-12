using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using IHM_Footies.Menu;
using METIER_Footies.Data;
using METIER_Footies.Metier;
using VM_Footies;
using VM_Footies.VM;
using VM_Footies.VM_Element_Selectionne;
using VM_Footies.VM_Page;

namespace IHM_Footies.Invitations
{
    public partial class VuePageInvitationDetail : Window
    {
        #region Attributs
        private VMInvitation vmInvitation;
        private string provenance;
        private VMPageInvite vmPageInvite = new VMPageInvite();
        private List<VueInvite> vueInvites = new List<VueInvite>();
        private VMPageGroupeInvite vMPageGroupeInvite = new VMPageGroupeInvite();
        private List<VueGroupeInvite> vueGroupes = new List<VueGroupeInvite>();
        private VMPageMenu vmPageMenu = new VMPageMenu();
        private List<VueMenu> vueMenus = new List<VueMenu>();
        private VMPagePlat VMPagePlat = new VMPagePlat();
        private List<VuePlat> vuePlats = new List<VuePlat>();
        private ObservableCollection<AvisDetail> listeAvis;
        #endregion

        #region propriete 

        /// <summary>
        /// Liste des avis des plats donnée par les invités
        /// </summary>
        public ObservableCollection<AvisDetail> ListeAvis
        {
            get => listeAvis;
            set 
            { 
                listeAvis = value; 
            }
        }

        #endregion

        #region Constructeur
        public VuePageInvitationDetail(VMInvitation invitation, string provenance = "Invitation")
        {
            InitializeComponent();
            this.listeAvis = new ObservableCollection<AvisDetail>();
            this.PanelAvis.ItemsSource = this.listeAvis; 
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
            this.AfficherAvis();
        }

        private async void AfficherAvis()
        {
            try
            {
                InvitationDAO dao = new InvitationDAO();
                List<AvisDetail> avis = await dao.ObtenirAvisPourInvitation(this.vmInvitation.Invitation.IdInvitation);

                this.listeAvis.Clear();
                foreach (AvisDetail a in avis)
                {
                    this.listeAvis.Add(a);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AfficherInvites()
        {
            this.PanelInvites.Children.Clear();
            List<VMInvite> listeVM = this.vmInvitation.ObtenirVMInvites();

            foreach (VMInvite vmInvite in listeVM)
            {
                VueInvite vueInvite = new VueInvite(vmInvite);
                vueInvite.MouseDown += (s, e) => this.SelectionnerInvite(vueInvite);
                vueInvite.MouseDoubleClick += (s, e) => this.OuvrirDetailInvite(vueInvite);
                this.vueInvites.Add(vueInvite);
                this.PanelInvites.Children.Add(vueInvite);
            }
        }

        private void AfficherGroupes()
        {
            this.PanelGroupes.Children.Clear();
            List<VMGroupeInvite> listeVM = this.vmInvitation.ObtenirVMGroupes();

            foreach (VMGroupeInvite vmGroupe in listeVM)
            {
                VueGroupeInvite vue = new VueGroupeInvite(vmGroupe);
                vue.MouseDown += (s, e) => this.SelectionnerGroupe(vue);
                vue.MouseDoubleClick += (s, e) => this.OuvrirDetailGroupe(vue);
                this.vueGroupes.Add(vue);
                this.PanelGroupes.Children.Add(vue);
            }
        }

        private void AfficherMenus()
        {
            this.PanelMenus.Children.Clear();
            List<VMMenu> listeVM = this.vmInvitation.ObtenirVMMenus();

            foreach (VMMenu vmMenu in listeVM)
            {
                VueMenu vue = new VueMenu(vmMenu);
                vue.MouseDown += (s, e) => this.SelectionnerMenu(vue);
                vue.MouseDoubleClick += (s, e) => this.OuvrirDetailMenu(vue);
                this.vueMenus.Add(vue);
                this.PanelMenus.Children.Add(vue);
            }
        }

        private void AfficherPlats()
        {
            this.PanelPlats.Children.Clear();
            List<VMPlat> listeVM = this.vmInvitation.ObtenirVMPlats();

            foreach (VMPlat vmPlat in listeVM)
            {
                VuePlat vue = new VuePlat(vmPlat);
                vue.MouseDown += (s, e) => this.SelectionnerPlat(vue);
                vue.MouseDoubleClick += (s, e) => this.OuvrirDetailPlat(vue);
                this.vuePlats.Add(vue);
                this.PanelPlats.Children.Add(vue);
            }
        }

        /// <summary>
        /// Sélectionne un invité dans la liste d'invités
        /// </summary>
        /// <param name="vue"> VueInvite sélectionnée </param>
        private void SelectionnerInvite(VueInvite vue)
        {
            this.vmPageInvite.InviteSelectionne = vue.Invite;
            foreach (VueInvite vueI in this.vueInvites)
            {
                vueI.Deselectionner();
            }
            vue.Selectionner();
        }

        private async Task OuvrirDetailInvite(VueInvite vue)
        {
            if (this.vmPageInvite.InviteSelectionne != null)
            {
                Navigation.AllerDetailInvite(this, this.vmPageInvite.InviteSelectionne, "Invitation", this.vmInvitation);
            }
        }

        /// <summary>
        /// Sélectionne un groupe dans la liste de groupe d'invités
        /// </summary>
        /// <param name="vue"> VueGroupe sélectionnée </param>
        private void SelectionnerGroupe(VueGroupeInvite vue)
        {
            this.vMPageGroupeInvite.GroupeSelectionne = vue.Groupe;
            foreach (VueGroupeInvite vueG in this.vueGroupes)
            {
                vueG.Deselectionner();
            }
            vue.Selectionner();
        }

        private async Task OuvrirDetailGroupe(VueGroupeInvite vue)
        {
            if (this.vMPageGroupeInvite.GroupeSelectionne != null)
            {
                Navigation.AllerDetailGroupeInvite(this, this.vMPageGroupeInvite.GroupeSelectionne, "Invitation", this.vmInvitation);
            }
        }

        /// <summary>
        /// Sélectionne un menu dans la liste des menus
        /// </summary>
        /// <param name="vue"> VueMenu sélectionné </param>
        private void SelectionnerMenu(VueMenu vue)
        {
            this.vmPageMenu.MenuSelectionne = vue.Menu;
            foreach (VueMenu vueM in this.vueMenus)
            {
                vueM.Deselectionner();
            }
            vue.Selectionner();
        }

        private async Task OuvrirDetailMenu(VueMenu vue)
        {
            if (this.vmPageMenu.MenuSelectionne != null)
            {
                Navigation.AllerDetailMenu(this, this.vmPageMenu.MenuSelectionne, "Invitation", this.vmInvitation);
            }
        }

        /// <summary>
        /// Sélectionne un plat dans la liste des plats
        /// </summary>
        /// <param name="vue"> VuePlat sélectionnée </param>
        private void SelectionnerPlat(VuePlat vue)
        {
            this.VMPagePlat.PlatSelectionne = vue.Plat;
            foreach (VuePlat vueP in this.vuePlats)
            {
                vueP.Deselectionner();
            }
            vue.Selectionner();
        }

        private async Task OuvrirDetailPlat(VuePlat vue)
        {
            if (this.VMPagePlat.PlatSelectionne != null)
            {
                Navigation.AllerDetailPlat(this, this.VMPagePlat.PlatSelectionne, "Invitation", this.vmInvitation);
            }
        }

        private void RetourAuxInvitations_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerInvitations(this);
        }
        #endregion
    }
}