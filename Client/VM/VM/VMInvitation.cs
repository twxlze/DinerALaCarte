using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using METIER_Footies.Data;
using METIER_Footies.Metier;
using VM_Footies.VM_Element_Selectionne;

namespace VM_Footies.VM
{
    /// <summary>
    /// ViewModel pour gérer une invitation avec son menu et son groupe d'invités
    /// </summary>
    public class VMInvitation : INotifyPropertyChanged
    {
        #region Attributs
        private Invitation invitation;
        private ObservableCollection<VMMenuSelectionne> menusListe;
        private ObservableCollection<VMGroupeInviteSelectionne> groupesInvitesListe;
        private ObservableCollection<VMInviteSelectionne> invitesListe;
        private ObservableCollection<VMPlatSelectionne> platsListe;
        #endregion

        public event PropertyChangedEventHandler? PropertyChanged;

        #region PROPRIETES
        /// <summary>
        /// Invitation encapsulée
        /// </summary>
        public Invitation Invitation => invitation;

        /// <summary>
        /// Nom de l'invitation (modifiable)
        /// </summary>
        public string Nom
        {
            get => this.invitation.Nom;
            set
            {
                if (invitation.Nom != value)
                {
                    if (!string.IsNullOrWhiteSpace(value))
                        invitation.Nom = char.ToUpper(value[0]) + value.Substring(1);
                    else
                        invitation.Nom = value;
                    Notify("Nom");
                }
            }
        }

        /// <summary>
        /// Menu de l'invitation
        /// </summary>
        public List<Menu> Menu
        {
            get => invitation.Menus;
            set
            {
                invitation.Menus = value;
                Notify("Menu");
            }
        }

        /// <summary>
        /// Groupe d'invités de l'invitation
        /// </summary>
        public List<GroupeInvites> GroupeInvites
        {
            get => invitation.GroupeInvites;
            set
            {
                invitation.GroupeInvites = value;
                Notify("GroupeInvites");
            }
        }

        /// <summary>
        /// Liste des invités de l'invitation
        /// </summary>
        public List<Invite> Invites
        {
            get => invitation.Invites;
            set
            {
                invitation.Invites = value;
                Notify("Invites");
            }
        }

        /// <summary>
        /// Liste des plats de l'invitation
        /// </summary>
        public List<Plat> Plats
        {
            get => invitation.Plats;
            set
            {
                invitation.Plats = value;
                Notify("Plats");
            }
        }

        /// <summary>
        /// Date de l'invitation
        /// </summary>
        public DateTime Date
        {
            get => invitation.Date;
            set
            {
                invitation.Date = value;
                Notify("Date");
            }
        }

        /// <summary>
        /// Format d'affichage de l'invitation avec le nom et la date
        /// </summary>
        public string FormatInvitation => $"{Nom} - {Date.ToShortDateString()}";
        #endregion

        #region PROPRIETES / Eléments sélectionnables
        /// <summary>
        /// Liste des menus sélectionnables
        /// </summary>
        public ObservableCollection<VMMenuSelectionne> MenusListe
        {
            get => menusListe;
            set
            {
                menusListe = value;
                Notify("MenusListe");
            }
        }

        /// <summary>
        /// Liste des groupes d'invités sélectionnables
        /// </summary>
        public ObservableCollection<VMGroupeInviteSelectionne> GroupesInvitesListe
        {
            get => groupesInvitesListe;
            set
            {
                groupesInvitesListe = value;
                Notify("GroupesInvitesListe");
            }
        }

        /// <summary>
        /// Liste des invités sélectionnables
        /// </summary>
        public ObservableCollection<VMInviteSelectionne> InvitesListe
        {
            get => invitesListe;
            set
            {
                invitesListe = value;
                Notify("InvitesListe");
            }
        }

        /// <summary>
        /// Liste des plats sélectionnables
        /// </summary>
        public ObservableCollection<VMPlatSelectionne> PlatsListe
        {
            get => platsListe;
            set
            {
                platsListe = value;
                Notify("PlatsListe");
            }
        }
        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur d'un VMInvitation à partir d'une invitation
        /// </summary>
        /// <param name="invitation">L'invitation à gérer</param>
        public VMInvitation(Invitation invitation)
        {
            this.invitation = invitation;
            InitialiserCollections();
        }

        /// <summary>
        /// Constructeur par copie d'un VMInvitation
        /// </summary>
        /// <param name="modele">Le VMInvitation à copier</param>
        public VMInvitation(VMInvitation modele)
        {
            this.invitation = new Invitation(modele.invitation);
            InitialiserCollections();
        }

        /// <summary>
        /// Constructeur par défaut d'un VMInvitation
        /// </summary>
        public VMInvitation()
        {
            this.invitation = new Invitation();
            InitialiserCollections();
        }

        /// <summary>
        /// Initialise les collections observables
        /// </summary>
        private void InitialiserCollections()
        {
            this.menusListe = new ObservableCollection<VMMenuSelectionne>();
            this.groupesInvitesListe = new ObservableCollection<VMGroupeInviteSelectionne>();
            this.invitesListe = new ObservableCollection<VMInviteSelectionne>();
            this.platsListe = new ObservableCollection<VMPlatSelectionne>();
        }
        #endregion

        #region METHODES publiques - Synchronisation
        /// <summary>
        /// Gère le changement de sélection d'un élément
        /// </summary>
        /// <param name="sender">L'expéditeur</param>
        /// <param name="e">Les arguments de l'événement</param>
        public void VmElement_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "MenuSelectionne":
                    SynchroniserMenuSelectionne();
                    break;
                case "GroupeSelectionne":
                    SynchroniserGroupeInviteSelectionne();
                    break;
                case "InviteSelectionne":
                    SynchroniserInvitesSelectionnes();
                    break;
                case "PlatSelectionne":
                    SynchroniserPlatsSelectionnes();
                    break;
            }
        }

        /// <summary>
        /// Synchronise tous les éléments
        /// </summary>
        public void SynchroniserTout()
        {
            SynchroniserMenuSelectionne();
            SynchroniserGroupeInviteSelectionne();
            SynchroniserInvitesSelectionnes();
            SynchroniserPlatsSelectionnes();
        }

        /// <summary>
        /// Met à jour le menu sélectionné dans le modèle
        /// </summary>
        private void SynchroniserMenuSelectionne()
        {
            List<Menu> menusSelectionnes = new List<Menu>();
            foreach (VMMenuSelectionne vmMenu in this.menusListe)
            {
                if (vmMenu.EstSelectionne)
                {
                    menusSelectionnes.Add(vmMenu.Menu);
                }
            }
            this.invitation.Menus = menusSelectionnes;
            Notify("Menu");
        }

        /// <summary>
        /// Met à jour le groupe d'invités sélectionné dans le modèle
        /// </summary>
        private void SynchroniserGroupeInviteSelectionne()
        {
            List<GroupeInvites> groupesInvitesSelectionnes = new List<GroupeInvites>();
            foreach (VMGroupeInviteSelectionne vmGroupe in this.groupesInvitesListe)
            {
                if (vmGroupe.EstSelectionne)
                {
                    groupesInvitesSelectionnes.Add(vmGroupe.GroupeInvite);
                }
            }
            this.invitation.GroupeInvites = groupesInvitesSelectionnes;
            Notify("GroupeInvites");
        }

        /// <summary>
        /// Met à jour les invités sélectionnés dans le modèle
        /// </summary>
        private void SynchroniserInvitesSelectionnes()
        {
            List<Invite> invitesSelectionnes = new List<Invite>();
            foreach (VMInviteSelectionne vmInvite in this.invitesListe)
            {
                if (vmInvite.EstSelectionne)
                {
                    invitesSelectionnes.Add(vmInvite.Invite);
                }
            }
            this.invitation.Invites = invitesSelectionnes;
            Notify("Invites");
        }

        /// <summary>
        /// Met à jour les plats sélectionnés dans le modèle
        /// </summary>
        private void SynchroniserPlatsSelectionnes()
        {
            List<Plat> platsSelectionnes = new List<Plat>();
            foreach (VMPlatSelectionne vmPlat in this.platsListe)
            {
                if (vmPlat.EstSelectionne)
                {
                    platsSelectionnes.Add(vmPlat.Plat);
                }
            }
            this.invitation.Plats = platsSelectionnes;
            Notify("Plats");
        }

        /// <summary>
        /// Modifie les informations d'une invitation
        /// </summary>
        /// <param name="invite"> L'invitation avec les nouvelles informations </param>
        public void ModifierInvitation(VMInvitation invitation)
        {
            Nom = invitation.Nom;
            Date = invitation.Date;
            Menu = invitation.Menu;
            GroupeInvites = invitation.GroupeInvites;
            Invites = invitation.Invites;
            Plats = invitation.Plats;

        }



        /// <summary>
        /// Charge les invités depuis l'API et remplit la liste sélectionnable
        /// </summary>
        public async Task ChargerInvites()
        {
            InviteDAO inviteDAO = new InviteDAO();
            List<Invite> tousLesInvites = await inviteDAO.ObtenirTout();

            InvitesListe.Clear();

            foreach (Invite invite in tousLesInvites)
            {
                VMInviteSelectionne vmInvite = new VMInviteSelectionne(invite, false);

                vmInvite.PropertyChanged += VmElement_PropertyChanged;

                InvitesListe.Add(vmInvite);
            }

            foreach (Invite inviteExistant in this.Invites)
            {
                VMInviteSelectionne? vmInviteExistant = InvitesListe.FirstOrDefault(vm => vm.Invite.Id == inviteExistant.Id);
                if (vmInviteExistant != null)
                {
                    vmInviteExistant.EstSelectionne = true;
                }
            }
        }

        /// <summary>
        /// Charger tous les groupes d'invités
        /// </summary>
        public async Task ChargerGroupeInvite()
        {
            GroupeInviteDAO groupeInviteDAO = new GroupeInviteDAO();
            List<GroupeInvites> tousLesGroupeInvites = await groupeInviteDAO.ListeGroupeInvites();

            groupesInvitesListe.Clear();

            foreach (GroupeInvites groupeInvite in tousLesGroupeInvites)
            {
                VMGroupeInviteSelectionne vmGroupeInvite = new VMGroupeInviteSelectionne(groupeInvite, false);

                vmGroupeInvite.PropertyChanged += VmElement_PropertyChanged;

                groupesInvitesListe.Add(vmGroupeInvite);
            }

            foreach (GroupeInvites groupeInviteExistant in this.GroupeInvites)
            {
                VMGroupeInviteSelectionne? vmGroupeInviteExistant = GroupesInvitesListe.FirstOrDefault(vm => vm.GroupeInvite.IdGroupeInvites == groupeInviteExistant.IdGroupeInvites);
                if (vmGroupeInviteExistant != null)
                {
                    vmGroupeInviteExistant.EstSelectionne = true;
                }
            }
        }

        /// <summary>
        /// Charger tous les menus
        /// </summary>
        public async Task ChargerMenu()
        {
            MenuDAO menuDAO = new MenuDAO();
            List<Menu> tousLesMenus = await menuDAO.ObtenirTousLesMenus();

            MenusListe.Clear();

            foreach (Menu menu in tousLesMenus)
            {
                VMMenuSelectionne vmMenu = new VMMenuSelectionne(menu, false);
                vmMenu.PropertyChanged += VmElement_PropertyChanged;
                MenusListe.Add(vmMenu);
            }

            foreach (Menu menuExistant in this.Menu)
            {
                var vmMenuExistant = MenusListe.FirstOrDefault(vm => vm.Menu.IdMenu == menuExistant.IdMenu);
                if (vmMenuExistant != null)
                {
                    vmMenuExistant.EstSelectionne = true;
                }
            }
        }


        #endregion

        #region METHODES privées
        /// <summary>
        /// Notifie l'UI d'un changement de propriété
        /// </summary>
        /// <param name="message">Nom de la propriété modifiée</param>
        private void Notify(string message)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(message));
        }
        #endregion
    }
}
