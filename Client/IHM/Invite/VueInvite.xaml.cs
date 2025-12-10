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
using System.Windows.Navigation;
using System.Windows.Shapes;
using VM_Footies.VM;

namespace IHM_Footies
{
    /// <summary>
    /// Logique d'interaction pour VueInvite.xaml
    /// </summary>
    public partial class VueInvite : UserControl
    {
        #region Attributs
        private VMInvite invite;
        public VMInvite Invite => this.invite;
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur d'une vue d'invité
        /// </summary>
        /// <param name="invite"> Le VMInvite à afficher </param>
        public VueInvite(VMInvite invite)
        {
            this.invite = invite;
            this.DataContext = this.invite;
            this.ConfigurerStyle();

            InitializeComponent();
        }
        #endregion

        #region Méthodes
        /// <summary>
        /// Désélectionne cette vue
        /// </summary>
        public void Deselectionner()
        {
            ExtensionVue.Deselectionner(this);
        }
        /// <summary>
        /// Sélectionne cette vue
        /// </summary>
        public void Selectionner()
        {
            ExtensionVue.Selectionner(this);
        }
        #endregion
    }
}
