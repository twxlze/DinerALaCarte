using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using METIER_Footies;
using METIER_Footies.Data;
using METIER_Footies.Metier;

namespace VM_Footies
{
    public class VMPageInvite : INotifyPropertyChanged
    {
        #region Attributs
        private List<VMInvite> listeVMInvite;
        private InviteDAO inviteDAO;
        #endregion

        public event PropertyChangedEventHandler? PropertyChanged;
        public List<VMInvite> VMInvites => listeVMInvite;

        #region Constructeurs
        /*
        public VMPageInvite()
        {
            this.inviteDAO = new InviteDAO();
            this.listeInvites = new List<VMInvite>();

            foreach (Invite invite in inviteDAO.ObtenirTout()) // coder la méthode ObtenirTout dans InviteDAO
            {
                this.listeInvites.Add(new VMInvite(invite));
            }
        }
        */
        #endregion

        #region Méthodes
        public void AjouterInvite(VMInvite invite)
        {
            this.inviteDAO.AjouterInvite(invite.Invite);
            this.listeVMInvite.Add(invite);
            this.Notify("VMInvites");
        }

        private void Notify(string message)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(message));
        }
        #endregion
    }
}
