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
using VM_Footies.VM;

namespace IHM_Footies.Invitations
{
    /// <summary>
    /// Logique d'interaction pour VueInvitation.xaml
    /// </summary>
    public partial class VueInvitation : UserControl
    {
        #region Attributs
        private VMInvitation invitation;
        #endregion

        #region Propriétés
        /// <summary>
        /// Invitation gérée par cette vue
        /// </summary>
        public VMInvitation Invitation => this.invitation;
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur avec ViewModel
        /// </summary>
        /// <param name="invitation"> L'invitation à afficher </param>
        public VueInvitation(VMInvitation invitation)
        {
            this.invitation = invitation;
            this.DataContext = this.invitation;
            this.ConfigurerStyle();
            InitializeComponent();
        }
        #endregion

        #region Méthodes
        public void Deselectionner()
        {
            ExtensionVue.Deselectionner(this);
        }
        public void Selectionner()
        {
            ExtensionVue.Selectionner(this);
        }
        #endregion
    }
}
