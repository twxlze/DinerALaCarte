using METIER_Footies.Metier;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM_Footies.VM;

namespace VM_Footies.VM_Page
{
    public class VMPageTableauDeBord : INotifyPropertyChanged
    {
        #region Attributs
        private VMPageInvitation _invitation;
        private VMPageInvite _invite;
        private ObservableCollection<VMInvite> listeInvite;
        #endregion

        #region Propriétés

        /// <summary>
        /// La liste de tous les invites
        /// </summary>
        public ObservableCollection<VMInvite> ListeInvites
        {
            get { return listeInvite; }
            set 
            {
                listeInvite = value;
                Notify("ListeInvites");
            }
        }

        /// <summary>
        /// Implémentation de l'interface INotifyPropertyChanged
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;
        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur du ViewModel de la page tableau de bord
        /// </summary>
        public VMPageTableauDeBord()
        {
            _invitation = new VMPageInvitation();
            _invite = new VMPageInvite();
            listeInvite = new ObservableCollection<VMInvite>();
        }
        #endregion

        #region Méthodes publiques
        /// <summary>
        /// Charge les données des invités et invitation pour initialiser les statistiques
        /// </summary>
        /// <returns>une tache</returns>
        public async Task ChargerDonneesInvite()
        {
            await _invite.ChargerInvites();
            await _invitation.ChargerInvitations();

            foreach (VMInvite vMInvite in _invite.VMInvites)
            {
                listeInvite.Add(vMInvite);
            }
        }

        public VMStats ChargerInvitationsParticipe(VMInvite inviteParticipe)
        {
            VMStats vMStats = new VMStats(inviteParticipe);

            foreach (VMInvitation invitation in this._invitation.VMInvitations)
            {
                foreach (Invite invite in invitation.Invites)
                {
                    if (inviteParticipe.Id == invite.Id)
                    {
                        vMStats.InvitationsParticipe.Add(invitation);
                    }
                }

                foreach (GroupeInvites groupe in invitation.GroupeInvites)
                {
                    foreach (Invite invite in groupe.Invites)
                    {
                        if (inviteParticipe.Id == invite.Id)
                        {
                            vMStats.InvitationsParticipe.Add(invitation);
                        }
                    }
                }
            }

            return vMStats;
        }

        /// <summary>
        /// Recherche un invité dans le tableau de bord en fonction du texte recherché
        /// ici les invites correspondant au texte de recherche sont placés en haut de la liste
        /// </summary>
        /// <param name="textrechercher">le text de recherche</param>
        public void RechercherInviteTableauDeBord(string textrechercher)
        {
            List<VMInvite> invitesFiltres = new List<VMInvite>();
            List<VMInvite> invitesNonFiltres = new List<VMInvite>();

            foreach (VMInvite inviteStat in listeInvite)
            {
                if (inviteStat.Invite.Identite.Contains(textrechercher, StringComparison.OrdinalIgnoreCase))
                {
                    invitesFiltres.Add(inviteStat);
                }
                else
                {
                    invitesNonFiltres.Add(inviteStat);
                }
            }

            invitesFiltres = invitesFiltres.OrderBy(i => i.Invite.Identite).ToList();
            invitesNonFiltres = invitesNonFiltres.OrderBy(i => i.Invite.Identite).ToList();
            invitesFiltres.AddRange(invitesNonFiltres);

            listeInvite.Clear();
            foreach (VMInvite inviteStat in invitesFiltres)
            {
                listeInvite.Add(inviteStat);
            }
        }
        #endregion

        #region Méthodes protegées
        protected void Notify(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
