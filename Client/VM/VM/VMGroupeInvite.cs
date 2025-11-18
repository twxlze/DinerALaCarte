using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using METIER_Footies.Data;
using METIER_Footies.Data.Interfaces;
using METIER_Footies.Metier;
using VM_Footies.VM_Element_Selectionne;

namespace VM_Footies.VM
{
    /// <summary>
    /// ViewModel pour gérer un groupe d'invités et ses invités
    /// </summary>
    public class VMGroupeInvite : INotifyPropertyChanged
    {
        #region Attributs
        private GroupeInvites groupe; 
        private ObservableCollection<VMInviteSelectionne> invitesListe;
        #endregion

        #region Evénement
        public event PropertyChangedEventHandler? PropertyChanged;
        #endregion

        #region Propriétés
        /// <summary>
        /// Invité encapsulé
        /// </summary>
        public GroupeInvites Groupe => this.groupe;

        /// <summary>
        /// Le nom du groupe (modifiable)
        /// </summary>
        public string Nom
        {
            get
            {
                return this.groupe.Nom;
            }
            set
            {
                if (groupe.Nom != value)
                {
                    groupe.Nom = value;
                    Notify("Nom");
                }
            }
        }

        public List<Invite> Invites
        {
            get => this.groupe.Invites;
        }

        #endregion

        #region Propriétés pour les invités séléctionnables
        public ObservableCollection<VMInviteSelectionne> InvitesListe
        {
            get => invitesListe;
            set
            {
                invitesListe = value;
                Notify("InvitesListe");
            }
        }
        #endregion


        #region Constructeurs
        /// <summary>
        /// Constructeur d'un VMGroupeInvite à partir d'un modèle GroupeInvites
        /// </summary>
        /// <param name="groupe">Le groupe à gérer</param>
        public VMGroupeInvite(GroupeInvites groupeInvite)
        {
            this.groupe = groupeInvite;
            this.invitesListe = new ObservableCollection<VMInviteSelectionne>();
        }

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public VMGroupeInvite(VMGroupeInvite modele)
        {
            this.groupe = new GroupeInvites(modele.Groupe);
            this.invitesListe = new ObservableCollection<VMInviteSelectionne>();
        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe VMGroupeInvite
        /// </summary>
        public VMGroupeInvite()
        {
            this.groupe = new GroupeInvites();
            this.invitesListe = new ObservableCollection<VMInviteSelectionne>();
        }
        #endregion


        #region Méthodes privées
        /// <summary>
        /// Notifie l'UI d'un changement de propriété
        /// </summary>
        /// <param name="propriete">Nom de la propriété modifiée</param>
        private void Notify(string message)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(message));
        }
        #endregion

        #region Méthodes de gestion des invités
        /// <summary>
        /// Synchronise la liste des invités sélectionnés avec le modèle
        /// </summary>
        /// <param name="sender"> L'expéditeur </param>
        /// <param name="e"> Les arguments de l'événement </param>
        private void VmInvite_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "InviteSelectionne")
            {
                SynchroniserInvitesSelectionnes();
            }
        }

        /// <summary>
        /// Met à jour la liste des invités sélectionnés dans le modèle
        /// </summary>
        public void SynchroniserInvitesSelectionnes()
        {
            List<Invite> inviteSelectionne = new List<Invite>();
            foreach (VMInviteSelectionne vmInvite in this.invitesListe)
            {
                if (vmInvite.EstSelectionne)
                {
                    inviteSelectionne.Add(vmInvite.Invite);
                }
            }
            this.groupe.Invites = inviteSelectionne;
            Notify("Invites");
        }

        /// <summary>
        /// Ajoute un gestionnaire d'événement pour un VMInviteSelectionne
        /// </summary>
        /// <param name="vmInvite">L'invité sélectionnable</param>
        public void GestionnaireEvenement(VMInviteSelectionne vmInvite)
        {
            vmInvite.PropertyChanged += VmInvite_PropertyChanged;
        }
        #endregion
    }
}
