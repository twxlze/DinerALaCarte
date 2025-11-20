using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using VM_Footies.VM;

namespace IHM_Footies
{
    /// <summary>
    /// Logique d'interaction pour VueGroupeInvite.xaml
    /// </summary>
    public partial class VueGroupeInvite : UserControl
    {
        #region Attributs
        private VMGroupeInvite groupe;
        #endregion

        #region Propriétés
        /// <summary>
        /// Groupe des invités géré par cette vue
        /// </summary>
        public VMGroupeInvite Groupe => this.groupe;
        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur avec ViewModel
        /// </summary>
        /// <param name="groupe">Le groupe à afficher</param>
        public VueGroupeInvite(VMGroupeInvite groupe)
        {
            this.groupe = groupe;
            this.DataContext = this.groupe;
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
