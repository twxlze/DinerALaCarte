using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM_Footies.VM
{
    public class VMStats : INotifyPropertyChanged
    {
        #region Attribut
        private List<VMInvitation> _invitationsParticipe;
        private VMInvite _invite;
        #endregion

        #region
        /// <summary>
        /// Les invitations auxquelles l'invité a participé
        /// </summary>
        public List<VMInvitation> InvitationsParticipe
        {
            get { return _invitationsParticipe; }
            set
            {
                _invitationsParticipe = value;
                Notify("InvitationsParticipe");
            }
        }

        /// <summary>
        /// L'identite de l'invite pour le tableau de bord
        /// </summary>
        public string IdentiteInvite
        {
            get { return _invite.Identite; }
        }

        /// <summary>
        /// Le nombre de participations de l'invité
        /// </summary>
        public int NombreParticipation
        {
            get { return InvitationsParticipe.Count; }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        #endregion


        #region Constructeur
        public VMStats(VMInvite vMInvite) 
        { 
            this._invite = vMInvite;
            _invitationsParticipe = new List<VMInvitation>();
        }
        #endregion

        #region Methode prives
        private void Notify(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

    }
}
