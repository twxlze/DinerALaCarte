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
using VM_Footies.VM_Element_Selectionne;

namespace IHM_Footies.Allergenes
{
    /// <summary>
    /// Logique d'interaction pour VueAllergenes.xaml
    /// </summary>
    public partial class VueAllergenes : UserControl
    {
        #region Attributs
        private VMAllergene allergene;
        #endregion

        #region Propriétés
        /// <summary>
        /// Allergène associé à cette vue
        /// </summary>
        public VMAllergene Allergene => this.allergene;
        #endregion

        #region Constructeur
        public VueAllergenes(VMAllergene allergene)
        {
            this.allergene = allergene;
            this.DataContext = this.allergene;
            this.ConfigurerStyle();
            InitializeComponent();
        }
        #endregion

        #region Méthodes
        /// <summary>
        /// Désélectionne la vue
        /// </summary>
        public void Deselectionner()
        {
            ExtensionVue.Deselectionner(this);
        }
        /// <summary>
        /// Sélectionne la vue
        /// </summary>
        public void Selectionner()
        {
            ExtensionVue.Selectionner(this);
        }
        #endregion
    }
}
