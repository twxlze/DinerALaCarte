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
        private VMPlat plat;
        #endregion

        #region Propriétés
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
        /// Nom du plat qui revient le plus souvent
        /// </summary>
        public string NomPlat
        {
            get { return plat.Nom; }
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
        /// <summary>
        /// Constructeur de VMStat prenant en compte un invité
        /// </summary>
        /// <param name="vMInvite"> L'invité </param>
        public VMStats(VMInvite vMInvite)
        { 
            this._invite = vMInvite;
            _invitationsParticipe = new List<VMInvitation>();
        }

        /// <summary>
        /// Constructeur de VMStat prenant en compte un plat
        /// </summary>
        /// <param name="vMPlat">Le plat </param>
        public VMStats(VMPlat vMPlat)
        {
            this.plat = vMPlat;
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
