using System.Collections.ObjectModel;
using System.ComponentModel;
using METIER_Footies.Data;
using METIER_Footies.Data.Interfaces;
using METIER_Footies.Metier;
using VM_Footies.VM;

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

        // VM des sous-pages
        private VMPageMenu vmPageMenu;
        private VMPageGroupeInvite vmPageGroupeInvite;
        private VMPageInvite vmPageInvite;
        private VMPagePlat vmPagePlat;

        // Attributs pour la recherche des éléments
        private List<VMInvite> inviteSauvegardes;
        private List<VMGroupeInvite> groupesSauvegardes;
        private List<VMMenu> menusSauvegardes;
        private List<VMPlat> platsSauvegardes;

        private string texteRecherche;
        private IGroupeInviteDAO groupeDAO;
        #endregion

        #region Evenement
        public event PropertyChangedEventHandler? PropertyChanged;
        #endregion

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
        /// Texte de recherche pour filtrer les invitations
        /// </summary>
        public string TexteRecherche
        {
            get { return texteRecherche; }
            set
            {
                texteRecherche = value;
                Notify("TexteRecherche");
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
            this.Initialiser();
        }

        private void Initialiser()
        {
            this.invitationDAO = new InvitationDAO();
            this.listeVMInvitation = new List<VMInvitation>();

            this.vmPageMenu = new VMPageMenu();
            this.vmPageGroupeInvite = new VMPageGroupeInvite();
            this.vmPageInvite = new VMPageInvite();
            this.vmPagePlat = new VMPagePlat();

            this.groupeDAO = new GroupeInviteDAO();

            this.texteRecherche = string.Empty;

            this.inviteSauvegardes = new List<VMInvite>();
            this.groupesSauvegardes = new List<VMGroupeInvite>();
            this.menusSauvegardes = new List<VMMenu>();
            this.platsSauvegardes = new List<VMPlat>();
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

            this.listeVMInvitation = this.listeVMInvitation
                .OrderByDescending(vm => vm.Date)
                .ThenByDescending(vm => vm.Nom)
                .ToList();
        }

        /// <summary>
        /// Charge tous les éléments disponibles pour une invitation (menus, groupes, invités, plats)
        /// </summary>
        /// <param name="invitation">L'invitation dans laquelle charger les éléments</param>
        public async Task ChargerElementsDansInvitation(VMInvitation invitation)
        {
            try
            {
                await ChargerInvitesDansInvitation(invitation);
                await ChargerGroupesInvitesDansInvitation(invitation);
                await ChargerMenusDansInvitation(invitation);
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

            PreparerSauvegarde(invitation);

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
                PreparerSauvegarde(invitation);
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

        /// <summary>
        // Charge la liste des groupe d'invités correspondant au paramètre de recherche depuis la base de données
        /// </summary>
        public async Task ChercherInvitation(string InvitationsRechercher)
        {
            this.listeVMInvitation.Clear();

            List<Invitation> invitations = await this.invitationDAO.ChercherInvitation(InvitationsRechercher);
            foreach (Invitation i in invitations)
            {
                VMInvitation vmInvitation = new VMInvitation(i);
                this.listeVMInvitation.Add(vmInvitation);
            }
            this.listeVMInvitation = this.listeVMInvitation.OrderBy(vm => vm.Invitation.Nom).ToList();

            this.Notify("VMInvitation");
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

            this.menusSauvegardes.Clear();

            foreach (VMMenu vmMenu in this.vmPageMenu.VMMenu)
            {
                bool estSelectionne = idDesMenus.Contains(vmMenu.Menu.IdMenu);
                VMMenu vmMenuSelectionne = new VMMenu(vmMenu.Menu, estSelectionne);
                vmMenuSelectionne.PropertyChanged += invitation.VmElement_PropertyChanged;
                this.menusSauvegardes.Add(vmMenuSelectionne);
            }
            invitation.MenusListe = new ObservableCollection<VMMenu>(menusSauvegardes);
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

            this.groupesSauvegardes.Clear();

            foreach (VMGroupeInvite vmGroupe in this.vmPageGroupeInvite.VMGroupeInvite)
            {
                bool estSelectionne = idDesGroupes.Contains(vmGroupe.Groupe.IdGroupeInvites);
                VMGroupeInvite vmGroupeSelectionne = new VMGroupeInvite(vmGroupe.Groupe, estSelectionne);
                vmGroupeSelectionne.PropertyChanged += invitation.VmElement_PropertyChanged;
                this.groupesSauvegardes.Add(vmGroupeSelectionne);
            }
            invitation.GroupesInvitesListe = new ObservableCollection<VMGroupeInvite>(groupesSauvegardes);
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

            this.inviteSauvegardes.Clear();

            foreach (VMInvite vmInvite in this.vmPageInvite.VMInvites)
            {
                bool estSelectionne = idDesInvites.Contains(vmInvite.Id);
                VMInvite vmInviteSelectionne = new VMInvite(vmInvite.Invite, estSelectionne);
                vmInviteSelectionne.PropertyChanged += invitation.VmElement_PropertyChanged;
                this.inviteSauvegardes.Add(vmInviteSelectionne);
            }
            invitation.InvitesListe = new ObservableCollection<VMInvite>(inviteSauvegardes);
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

            this.platsSauvegardes.Clear();

            foreach (VMPlat vmPlat in this.vmPagePlat.VMPlat)
            {
                bool estSelectionne = idDesPlats.Contains(vmPlat.Plat.Id);
                VMPlat vmPlatSelectionne = new VMPlat(vmPlat.Plat, estSelectionne);
                vmPlatSelectionne.PropertyChanged += invitation.VmElement_PropertyChanged;
                this.platsSauvegardes.Add(vmPlatSelectionne);
            }
            invitation.PlatsListe = new ObservableCollection<VMPlat>(platsSauvegardes);
        }
        #endregion

        #region Méthodes - Recherche des éléments
        /// <summary>
        /// Recherche un invité dans le cache et met à jour la liste visible de l'invitation
        /// </summary>
        /// <param name="invitation"> L'invitation courante </param>
        /// <param name="texteRecherche"> Le texte de recherche </param>
        public void RechercherInviteDansFormulaire(VMInvitation invitation, string texteRecherche)
        {
            if (string.IsNullOrWhiteSpace(texteRecherche))
                invitation.InvitesListe = new ObservableCollection<VMInvite>(inviteSauvegardes);
            List<VMInvite> resultats = inviteSauvegardes.Where(i => i.Identite.Contains(texteRecherche, StringComparison.OrdinalIgnoreCase)).OrderBy(i => i.Identite).ToList();
            invitation.InvitesListe = new ObservableCollection<VMInvite>(resultats);
        }

        /// <summary>
        /// Recherche un groupe dans le cache et met à jour la liste visible de l'invitation.
        /// </summary>
        /// <param name="invitation"> L'invitation courante </param>
        /// <param name="texteRecherche"> Le texte de recherche </param>
        public void RechercherGroupeDansFormulaire(VMInvitation invitation, string texteRecherche)
        {
            if (string.IsNullOrWhiteSpace(texteRecherche))
                invitation.GroupesInvitesListe = new ObservableCollection<VMGroupeInvite>(groupesSauvegardes);
            List<VMGroupeInvite> resultats = groupesSauvegardes.Where(g => g.Nom.Contains(texteRecherche, StringComparison.OrdinalIgnoreCase)).OrderBy(g => g.Nom).ToList();
            invitation.GroupesInvitesListe = new ObservableCollection<VMGroupeInvite>(resultats);
        }

        /// <summary>
        /// Recherche un menu dans le cache et met à jour la liste visible de l'invitation
        /// </summary>
        /// <param name="invitation"> L'invitation courante </param>
        /// <param name="texteRecherche"> Le texte de recherche </param>
        public void RechercherMenuDansFormulaire(VMInvitation invitation, string texteRecherche)
        {
            if (string.IsNullOrWhiteSpace(texteRecherche))
                invitation.MenusListe = new ObservableCollection<VMMenu>(menusSauvegardes);
            List<VMMenu> resultats = menusSauvegardes.Where(g => g.Nom.Contains(texteRecherche, StringComparison.OrdinalIgnoreCase)).OrderBy(g => g.Nom).ToList();
            invitation.MenusListe = new ObservableCollection<VMMenu>(menusSauvegardes);
        }

        /// <summary>
        /// Recherche un plat dans le cache et met à jour la liste visible de l'invitation
        /// </summary>
        /// <param name="invitation"> L'invitation courante </param>
        /// <param name="texteRecherche"> Le texte de recherche </param>
        public void RechercherPlatDansFormulaire(VMInvitation invitation, string texteRecherche)
        {
            if (string.IsNullOrWhiteSpace(texteRecherche))
                invitation.PlatsListe = new ObservableCollection<VMPlat>(platsSauvegardes);
            List<VMPlat> resultats = platsSauvegardes.Where(g => g.Nom.Contains(texteRecherche, StringComparison.OrdinalIgnoreCase)).OrderBy(g => g.Nom).ToList();
            invitation.PlatsListe = new ObservableCollection<VMPlat>(platsSauvegardes);
        }


        /// <summary>
        /// Prépare l'invitation pour la sauvegarde en récupérant les sélections 
        /// </summary>
        public void PreparerSauvegarde(VMInvitation invitation)
        {
            invitation.SynchroniserTout();
            invitation.Invitation.Invites = inviteSauvegardes.Where(vm => vm.InviteSelectionne).Select(vm => vm.Invite).ToList();
            invitation.Invitation.GroupeInvites = groupesSauvegardes.Where(vm => vm.EstSelectionne).Select(vm => vm.Groupe).ToList();
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
