using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using METIER_Footies.Data;
using METIER_Footies.Data.Interface;
using METIER_Footies.Metier;
using VM_Footies.VM;
using VM_Footies.VM_Element_Selectionne;

namespace VM_Footies.VM_Page
{
    /// <summary>
    /// ViewModel pour la page de gestion des invitations
    /// </summary>
    public class VMPageInvitation : INotifyPropertyChanged
    {
        #region Attributs
        private List<VMInvitation> listeVMInvitation;
        private VMInvitation invitationSelectionnee;
        private IInvitationDAO invitationDAO;
        private VMPageMenu vmPageMenu;
        private VMPageGroupeInvite vmPageGroupeInvite;
        private VMPageInvite vmPageInvite;
        private VMPagePlat vmPagePlat;
        #endregion

        public event PropertyChangedEventHandler? PropertyChanged;

        #region Propriétés
        /// <summary>
        /// Invitation sélectionnée
        /// </summary>
        public VMInvitation InvitationSelectionnee
        {
            get => invitationSelectionnee;
            set
            {
                invitationSelectionnee = value;
                Notify("InvitationSelectionnee");
            }
        }

        /// <summary>
        /// Liste des invitations
        /// </summary>
        public List<VMInvitation> VMInvitations => listeVMInvitation;
        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public VMPageInvitation()
        {
            this.invitationDAO = new InvitationDAO();
            this.listeVMInvitation = new List<VMInvitation>();
            this.vmPageMenu = new VMPageMenu();
            this.vmPageGroupeInvite = new VMPageGroupeInvite();
            this.vmPageInvite = new VMPageInvite();
            this.vmPagePlat = new VMPagePlat();
        }
        #endregion

        #region Méthodes publiques - CRUD
        /// <summary>
        /// Charge toutes les invitations depuis la base de données
        /// </summary>
        public async Task ChargerInvitations()
        {
            this.listeVMInvitation.Clear();
            List<Invitation> invitations = await this.invitationDAO.ObtenirToutesLesInvitations();

            foreach (Invitation invitation in invitations)
            {
                VMInvitation vmInvitation = new VMInvitation(invitation);
                this.listeVMInvitation.Add(vmInvitation);
            }

            this.listeVMInvitation = this.listeVMInvitation.OrderBy(vm => vm.Date).ThenBy(vm => vm.Nom).ToList();
        }

        /// <summary>
        /// Charge tous les éléments disponibles pour une invitation (menus, groupes, invités, plats)
        /// </summary>
        /// <param name="invitation">L'invitation dans laquelle charger les éléments</param>
        public async Task ChargerElementsDansInvitation(VMInvitation invitation)
        {
            try
            {
                await ChargerMenusDansInvitation(invitation);
                await ChargerGroupesInvitesDansInvitation(invitation);
                await ChargerInvitesDansInvitation(invitation);
                await ChargerPlatsDansInvitation(invitation);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erreur lors du chargement des éléments de l'invitation : {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Ajoute une nouvelle invitation
        /// </summary>
        /// <param name="invitation">L'invitation à ajouter</param>
        public async Task AjouterInvitation(VMInvitation invitation)
        {
            if (InvitationExiste(invitation))
            {
                throw new Exception("Une invitation avec le même nom et la même date existe déjà");
            }

            invitation.SynchroniserTout();

            await this.invitationDAO.AjouterInvitation(invitation.Invitation);
            this.listeVMInvitation.Add(invitation);
            this.Notify("VMInvitations");
        }

        /// <summary>
        /// Modifie une invitation existante
        /// </summary>
        /// <param name="invitation">L'invitation avec les modifications</param>
        public async Task ModifierInvitation(VMInvitation invitation)
        {
            if (invitation != null)
            {
                invitation.SynchroniserTout();
                await this.invitationDAO.ModifierInvitation(invitation.Invitation);
                this.Notify("VMInvitations");
            }
        }

        /// <summary>
        /// Supprime l'invitation sélectionnée
        /// </summary>
        /// <returns>True si la suppression a réussi, False sinon</returns>
        public async Task<bool> SupprimerInvitation()
        {
            bool suppressionReussie = false;

            if (this.invitationSelectionnee != null)
            {
                long idInvitation = this.invitationSelectionnee.Invitation.IdInvitation;

                if (idInvitation != 0)
                {
                    await this.invitationDAO.SupprimerInvitation(idInvitation);
                    this.listeVMInvitation.Remove(this.invitationSelectionnee);
                    this.invitationSelectionnee = null;
                    suppressionReussie = true;
                }
                else
                {
                    this.listeVMInvitation.Remove(this.invitationSelectionnee);
                    this.invitationSelectionnee = null;
                    suppressionReussie = true;
                }
            }

            return suppressionReussie;
        }

        /// <summary>
        /// Vérifie si une invitation avec le même nom et date existe déjà
        /// </summary>
        /// <param name="invitation">L'invitation à vérifier</param>
        /// <returns>True si un doublon existe, False sinon</returns>
        public bool InvitationExiste(VMInvitation invitation)
        {
            return this.listeVMInvitation.Any(vm => vm.Invitation.Nom.Equals(invitation.Invitation.Nom, StringComparison.OrdinalIgnoreCase) &&
                                              vm.Invitation.Date.Date == invitation.Invitation.Date.Date);
        }
        #endregion

        #region Méthodes privées - Chargement des éléments
        /// <summary>
        /// Charge tous les menus disponibles dans l'invitation
        /// </summary>
        private async Task ChargerMenusDansInvitation(VMInvitation invitation)
        {
            await this.vmPageMenu.ChargerMenus();

            HashSet<long> idDesMenus = new HashSet<long>();
            if (invitation.Invitation.Menus != null)
            {
                foreach (Menu menu in invitation.Invitation.Menus)
                {
                    idDesMenus.Add(menu.IdMenu);
                }
            }

            invitation.MenusListe.Clear();

            foreach (VMMenu vmMenu in this.vmPageMenu.VMMenu)
            {
                bool estSelectionne = idDesMenus.Contains(vmMenu.Menu.IdMenu);
                VMMenuSelectionne vmMenuSelectionne = new VMMenuSelectionne(vmMenu.Menu, estSelectionne);
                vmMenuSelectionne.PropertyChanged += invitation.VmElement_PropertyChanged;
                invitation.MenusListe.Add(vmMenuSelectionne);
            }
        }

        /// <summary>
        /// Charge tous les groupes d'invités disponibles dans l'invitation
        /// </summary>
        private async Task ChargerGroupesInvitesDansInvitation(VMInvitation invitation)
        {
            await this.vmPageGroupeInvite.ChargerGroupeInvites();

            HashSet<long> idDesGroupes = new HashSet<long>();
            if (invitation.Invitation.GroupeInvites != null)
            {
                foreach (GroupeInvites groupe in invitation.Invitation.GroupeInvites)
                {
                    idDesGroupes.Add(groupe.IdGroupeInvites);
                }
            }

            invitation.GroupesInvitesListe.Clear();

            foreach (VMGroupeInvite vmGroupe in this.vmPageGroupeInvite.VMGroupeInvite)
            {
                bool estSelectionne = idDesGroupes.Contains(vmGroupe.Groupe.IdGroupeInvites);
                VMGroupeInviteSelectionne vmGroupeSelectionne = new VMGroupeInviteSelectionne(vmGroupe.Groupe, estSelectionne);
                vmGroupeSelectionne.PropertyChanged += invitation.VmElement_PropertyChanged;
                invitation.GroupesInvitesListe.Add(vmGroupeSelectionne);
            }
        }

        /// <summary>
        /// Charge tous les invités disponibles dans l'invitation
        /// </summary>
        private async Task ChargerInvitesDansInvitation(VMInvitation invitation)
        {
            await this.vmPageInvite.ChargerInvites();

            HashSet<long> idDesInvites = new HashSet<long>();
            if (invitation.Invitation.Invites != null)
            {
                foreach (Invite invite in invitation.Invitation.Invites)
                {
                    idDesInvites.Add(invite.Id);
                }
            }

            invitation.InvitesListe.Clear();

            foreach (VMInvite vmInvite in this.vmPageInvite.VMInvites)
            {
                bool estSelectionne = idDesInvites.Contains(vmInvite.Id);
                VMInviteSelectionne vmInviteSelectionne = new VMInviteSelectionne(vmInvite.Invite, estSelectionne);
                vmInviteSelectionne.PropertyChanged += invitation.VmElement_PropertyChanged;
                invitation.InvitesListe.Add(vmInviteSelectionne);
            }
        }

        /// <summary>
        /// Charge tous les plats disponibles dans l'invitation
        /// </summary>
        private async Task ChargerPlatsDansInvitation(VMInvitation invitation)
        {
            await this.vmPagePlat.ChargerPlats();

            HashSet<long> idDesPlats = new HashSet<long>();
            if (invitation.Invitation.Plats != null)
            {
                foreach (Plat plat in invitation.Invitation.Plats)
                {
                    idDesPlats.Add(plat.Id);
                }
            }

            invitation.PlatsListe.Clear();

            foreach (VMPlat vmPlat in this.vmPagePlat.VMPlat)
            {
                bool estSelectionne = idDesPlats.Contains(vmPlat.Plat.Id);
                VMPlatSelectionne vmPlatSelectionne = new VMPlatSelectionne(vmPlat.Plat, estSelectionne);
                vmPlatSelectionne.PropertyChanged += invitation.VmElement_PropertyChanged;
                invitation.PlatsListe.Add(vmPlatSelectionne);
            }
        }
        #endregion

        #region Méthodes privées
        /// <summary>
        /// Notifie l'UI d'un changement de propriété
        /// </summary>
        private void Notify(string message)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(message));
        }
        #endregion
    }
}
