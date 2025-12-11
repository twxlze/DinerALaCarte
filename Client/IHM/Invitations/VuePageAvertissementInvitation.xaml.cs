using System;
using System.Windows;
using VM_Footies.VM;
using VM_Footies.VM_Page;

namespace IHM_Footies.Invitations
{
    public partial class VuePageAvertissementInvitation : Window
    {
        #region Attributs
        private VMPageAvertissementInvitation vm;
        private VMInvitation invitation;
        private bool invitationConfirmee = false;
        private bool retournerAuxPlats = false;
        #endregion

        #region Propriétés
        public bool InvitationConfirmee => invitationConfirmee;
        public bool RetournerAuxPlats => retournerAuxPlats;
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur de la page
        /// </summary>
        /// <param name="invitation">Invitation dont l'analyse sera issue</param>
        public VuePageAvertissementInvitation(VMInvitation invitation)
        {
            InitializeComponent();

            this.invitation = invitation;
            this.vm = new VMPageAvertissementInvitation(invitation);
            this.DataContext = this.vm;
        }
        #endregion

        #region Boutons de navigation
        private void BoutonConfirmer_Click(object sender, RoutedEventArgs e)
        {
            invitationConfirmee = true;
            this.DialogResult = true;
            this.Close();
        }

        private void BoutonRetourPlats_Click(object sender, RoutedEventArgs e)
        {
            retournerAuxPlats = true;
            this.DialogResult = false;
            this.Close();
        }

        private void BoutonAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void BoutonFermer_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
        #endregion
    }
}
