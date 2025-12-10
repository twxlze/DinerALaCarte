using System.Windows;
using VM_Footies;
using VM_Footies.VM;
using VM_Footies.VM_Page;

namespace IHM_Footies.Plat
{
    /// <summary>
    /// Logique d'interaction pour VuePagePlatDetail.xaml
    /// </summary>
    public partial class VuePagePlatDetail : Window
    {
        #region Attributs
        private VMPagePlat vmPagePlat;
        private string provenance;
        private VMInvitation invitation;
        private VMPageMenu VMPageMenu;
        private VMMenu vMMenu;
        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur de la vue de détail d'un plat
        /// </summary>
        /// <param name="vmPlat">Le plat à afficher</param>
        /// <param name="provenance">La page de provenance ("Plat" ou "Accueil")</param>
        public VuePagePlatDetail(VMPlat vmPlat, string provenance = "Plat", VMInvitation invitationPrecedente = null, VMMenu menuParent = null)
        {
            InitializeComponent();
            this.Initialiser(vmPlat, provenance, invitationPrecedente, menuParent);
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void Initialiser(VMPlat vmPlat, string provenance, VMInvitation invitationPrecedente, VMMenu menuParent)
        {
            this.vmPagePlat = new VMPagePlat();
            this.vmPagePlat.PlatSelectionne = vmPlat;
            this.provenance = provenance;
            this.invitation = invitationPrecedente;
            this.vMMenu = menuParent;
            this.DataContext = this.vmPagePlat;
        }
        #endregion

        #region Méthodes
        /// <summary>
        /// Gère le clic sur le bouton Retour en fonction de la page de provenance
        /// </summary>
        private void RetourAPlat_Click(object sender, RoutedEventArgs e)
        {
            switch (this.provenance)
            {
                case "Accueil":
                    Navigation.AllerAccueil(this);
                    break;
                case "Invitation":
                    Navigation.AllerDetailInvitation(this, this.invitation, "Plat");
                    break;
                case "Menu":
                    if (this.invitation != null)
                        Navigation.AllerDetailMenu(this, this.vMMenu, "Invitation", this.invitation);
                    else
                        Navigation.AllerDetailMenu(this, this.vMMenu);
                    break;
                default:
                    Navigation.AllerPlat(this);
                    break;
            }
        }
        #endregion
    }
}
