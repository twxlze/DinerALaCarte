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
using METIER_Footies.Metier;
using VM_Footies.VM;

namespace IHM_Footies
{
    /// <summary>
    /// Logique d'interaction pour VueInvite.xaml
    /// </summary>
    public partial class VuePlat : UserControl
    {
        #region Attributs
        private VMPlat plat;
        #endregion

        #region Propriétés
        /// <summary>
        /// Le VMPlat associé à la vue
        /// </summary>
        public VMPlat Plat => this.plat;
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur d'une vue d'invité
        /// </summary>
        /// <param name="invite"> Le VMInvite à afficher </param>
        public VuePlat(VMPlat plat)
        {
            this.plat = plat;
            this.DataContext = this.plat;
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