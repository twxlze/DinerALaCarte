using System.Collections.ObjectModel;
using System.ComponentModel;
using METIER_Footies.Metier;
using VM_Footies.VM;

namespace VM_Footies.VM_Page
{
    /// <summary>
    /// ViewModel pour la page de notation des plats d'une invitation.
    /// Gère l'agrégation des invités et des plats, ainsi que la saisie de la note.
    /// </summary>
    public class VMPageNotePlats : INotifyPropertyChanged
    {
        #region Attributs
        private Invitation invitationConcernee;
        private ObservableCollection<VMInvite> listeInvites;
        private ObservableCollection<VMPlat> listePlats;
        private VMInvite? inviteSelectionne;
        private VMPlat? platSelectionne;
        private string noteSaisie;
        private string commentaireSaisi;
        #endregion

        public event PropertyChangedEventHandler? PropertyChanged;

        #region Propriétés

        /// <summary>
        /// Liste de tous les invités 
        /// </summary>
        public ObservableCollection<VMInvite> ListeInvites
        {
            get => listeInvites;
            set
            {
                listeInvites = value;
                Notify("ListeInvites");
            }
        }

        /// <summary>
        /// Liste de tous les plats 
        /// </summary>
        public ObservableCollection<VMPlat> ListePlats
        {
            get => listePlats;
            set
            {
                listePlats = value;
                Notify("ListePlats");
            }
        }

        /// <summary>
        /// L'invité sélectionné pour donner la note.
        /// </summary>
        public VMInvite? InviteSelectionne
        {
            get => inviteSelectionne;
            set
            {
                inviteSelectionne = value;
                Notify("InviteSelectionne");
            }
        }

        /// <summary>
        /// Le plat sélectionné pour être noté.
        /// </summary>
        public VMPlat? PlatSelectionne
        {
            get => platSelectionne;
            set
            {
                platSelectionne = value;
                Notify("PlatSelectionne");
            }
        }

        /// <summary>
        /// La note saisie par l'utilisateur
        /// </summary>
        public string NoteSaisie
        {
            get => noteSaisie;
            set
            {
                noteSaisie = value;
                Notify("NoteSaisie");
            }
        }

        /// <summary>
        /// Le commentaire facultatif.
        /// </summary>
        public string CommentaireSaisi
        {
            get => commentaireSaisi;
            set
            {
                commentaireSaisi = value;
                Notify("CommentaireSaisi");
            }
        }

        #endregion

        #region Constructeurs

        /// <summary>
        /// Constructeur par défaut.
        /// </summary>
        public VMPageNotePlats()
        {
            this.listeInvites = new ObservableCollection<VMInvite>();
            this.listePlats = new ObservableCollection<VMPlat>();
            this.noteSaisie = "";
            this.commentaireSaisi = "";
        }

        #endregion

        #region Méthodes Publiques

        /// <summary>
        /// Charge et traite les données de l'invitation pour l'affichage.
        /// Fusionne les listes pour éviter les doublons.
        /// </summary>
        /// <param name="invitation">L'invitation à traiter</param>
        public void ChargerDonneesInvitation(Invitation invitation)
        {

            this.invitationConcernee = invitation;
            HashSet<long> idsInvitesTraites = new HashSet<long>();
            List<VMInvite> invitesTemp = new List<VMInvite>();

            if (invitation.Invites != null)
            {
                foreach (Invite invite in invitation.Invites)
                {
                    if (idsInvitesTraites.Add(invite.Id))
                    {
                        invitesTemp.Add(new VMInvite(invite));
                    }
                }
            }

            if (invitation.GroupeInvites != null)
            {
                foreach (GroupeInvites groupe in invitation.GroupeInvites)
                {
                    if (groupe.Invites != null)
                    {
                        foreach (Invite invite in groupe.Invites)
                        {
                            if (idsInvitesTraites.Add(invite.Id))
                            {
                                invitesTemp.Add(new VMInvite(invite));
                            }
                        }
                    }
                }
            }
            this.ListeInvites = new ObservableCollection<VMInvite>(invitesTemp.OrderBy(vm => vm.Nom).ThenBy(vm => vm.Prenom));

            HashSet<long> idsPlatsTraites = new HashSet<long>();
            List<VMPlat> platsTemp = new List<VMPlat>();
            if (invitation.Plats != null)
            {
                foreach (Plat plat in invitation.Plats)
                {
                    if (idsPlatsTraites.Add(plat.Id))
                    {
                        platsTemp.Add(new VMPlat(plat));
                    }
                }
            }

            if (invitation.Menus != null)
            {
                foreach (Menu menu in invitation.Menus)
                {
                    if (menu.Plat != null)
                    {
                        foreach (Plat plat in menu.Plat)
                        {
                            if (idsPlatsTraites.Add(plat.Id))
                            {
                                platsTemp.Add(new VMPlat(plat));
                            }
                        }
                    }
                }
            }
            this.ListePlats = new ObservableCollection<VMPlat>(platsTemp.OrderBy(vm => vm.Nom));
        }

        #endregion

        #region Méthodes privées

        private void Notify(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}