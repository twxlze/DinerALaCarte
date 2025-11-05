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
        private List<VMInvite> listeInvites;
        private InviteDAO inviteDAO;
        #endregion

        public event PropertyChangedEventHandler? PropertyChanged;
        public List<VMInvite> VMInvites => listeInvites;

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
    }
}
