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
using VM_Footies.VM_Page;

namespace IHM_Footies.TableauDeBord
{
    /// <summary>
    /// Logique d'interaction pour VueTableauDeBord.xaml
    /// </summary>
    public partial class VueTableauDeBord : UserControl
    {
        #region constructeur
        public VueTableauDeBord(VMStats TableauDeBord)
        {
            InitializeComponent();
            this.DataContext = TableauDeBord;
        }
        #endregion
    }
}
