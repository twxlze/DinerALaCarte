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

namespace IHM_Footies.GroupeInvite
{
    /// <summary>
    /// Logique d'interaction pour VuePageDetailInviteDansGroupe.xaml
    /// </summary>
    public partial class VuePageDetailInviteDansGroupe : Window
    {
        #region Attributs
        private VMPageGroupeInvite vmPageGroupeInvite;
        private List<VueInvite> vueInvite;
        private VMGroupeInvite groupeInviteSelectionne;
        #endregion
        /// <summary>
        /// Constructeur par défaut d'une page de détail des invités dans un groupe
        /// </summary>
        public VuePageDetailInviteDansGroupe(VMGroupeInvite groupeInvite)
        {
            InitializeComponent();
            this.groupeInviteSelectionne = groupeInvite;
            this.vueInvite = new List<VueInvite>();

            this.vmPageGroupeInvite = new VMPageGroupeInvite();
            this.vmPageGroupeInvite.GroupeSelectionne = groupeInvite;
            this.DataContext = this.vmPageGroupeInvite;

            this.vmPageGroupeInvite.PropertyChanged += VMPageGroupeInvite_PropertyChanged;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            this.RafraichirListe();

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
        /// Rafraîchit la liste des invités affichés du groupe sélectionné
        /// </summary>
        private async void RafraichirListe()
        {
            this.PanelInvitesDansGroupe.Children.Clear();

            await this.vmPageGroupeInvite.ChargerInvitesDansGroupe(groupeInviteSelectionne);

            foreach (VMInvite invite in this.vmPageGroupeInvite.ListeVMInviteGroupe)
            {
                VueInvite vue = new VueInvite(invite);
                this.vueInvite.Add(vue);
                this.PanelInvitesDansGroupe.Children.Add(vue);
            }
        }

        /// <summary>
        /// Bouton pour retourner à la page des groupes invités
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RetourAuGroupeInvite_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerGroupesInvites(this);
        }
        

        #endregion
    }
}
