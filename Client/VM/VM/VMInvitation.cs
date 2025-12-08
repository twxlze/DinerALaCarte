using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
        private ObservableCollection<VMInvite> invitesListe;
        private ObservableCollection<VMPlat> platsListe;
        #endregion

        public event PropertyChangedEventHandler? PropertyChanged;

        public List<Invite> ObtenirInvitesSelectionnes()
        {
            return InvitesListe.Where(i => i.EstSelectionne).Select(i => i.Invite).ToList();
        }


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
        public ObservableCollection<VMInvite> InvitesListe
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
        public ObservableCollection<VMPlat> PlatsListe
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
            this.invitesListe = new ObservableCollection<VMInvite>();
            this.platsListe = new ObservableCollection<VMPlat>();
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
            foreach (VMInvite vmInvite in this.invitesListe)
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
            foreach (VMPlat vmPlat in this.platsListe)
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
