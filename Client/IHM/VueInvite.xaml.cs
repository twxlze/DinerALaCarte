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
using VM_Footies;

namespace IHM_Footies
{
    /// <summary>
    /// Logique d'interaction pour VueInvite.xaml
    /// </summary>
    public partial class VueInvite : UserControl
    {
        #region Attributs
        private VMInvite invite;
        private VMInvite Invite => this.invite;
        #endregion

        #region Constructeur
        public VueInvite()
        {
            InitializeComponent();
        }
        #endregion
    }
}
