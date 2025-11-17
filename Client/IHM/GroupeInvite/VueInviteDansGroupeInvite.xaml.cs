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
    /// Logique d'interaction pour VueInviteDansGroupeInvite.xaml
    /// </summary>
    public partial class VueInviteDansGroupeInvite : UserControl
    {
        #region Attributs
        private VMGroupeInvite groupe;
        public VMGroupeInvite Groupe => this.groupe;
        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur pour le designer
        /// </summary>
        public VueInviteDansGroupeInvite()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructeur avec ViewModel
        /// </summary>
        /// <param name="groupe">Le groupe à afficher</param>
        public VueInviteDansGroupeInvite(VMGroupeInvite groupe)
        {
            this.groupe = groupe;
            this.DataContext = this.groupe;
            InitializeComponent();
        }
        #endregion

        #region Méthodes pour boutons
        
        #endregion
    }
}

