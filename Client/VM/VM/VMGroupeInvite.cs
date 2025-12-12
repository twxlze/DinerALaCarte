using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using METIER_Footies.Data;
using METIER_Footies.Data.Interfaces;
using METIER_Footies.Metier;

namespace VM_Footies.VM
{
    /// <summary>
    /// ViewModel pour gérer un groupe d'invités et ses invités
    /// </summary>
    public class VMGroupeInvite : INotifyPropertyChanged
    {
        #region Attributs
        private GroupeInvites groupe; 
        private bool estSelectionne;
        private ObservableCollection<VMInvite> invitesListe;
        private List<VMInvite> invitesSauvegardes;
        private string texteRechercheInvite;
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

        /// <summary>
        /// État de sélection du groupe d'invités
        /// </summary>
        public bool EstSelectionne
        {
            get => estSelectionne;
            set
            {
                if (estSelectionne != value)
                {
                    estSelectionne = value;
                    Notify("GroupeSelectionne");
                }
            }
        }

        /// <summary>
        /// Texte de recherche pour les invités dans le groupe
        /// </summary>
        public string TexteRechercheInvite
        {
            get => texteRechercheInvite;
            set
            {
                texteRechercheInvite = value;
                Notify("TexteRechercheInvite");
            }
        }
        #endregion

        #region Propriétés pour les invités séléctionnables
        public ObservableCollection<VMInvite> InvitesListe
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
        public VMGroupeInvite(GroupeInvites groupeInvite, bool estSelectionne = false)
        {
            this.groupe = groupeInvite;
            this.estSelectionne = estSelectionne;
            this.invitesListe = new ObservableCollection<VMInvite>();
            this.invitesSauvegardes = new List<VMInvite>();
        }

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public VMGroupeInvite(VMGroupeInvite modele)
        {
            this.groupe = new GroupeInvites(modele.Groupe);
            this.invitesListe = new ObservableCollection<VMInvite>();
            this.estSelectionne = modele.EstSelectionne;
            this.invitesSauvegardes = new List<VMInvite>();
        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe VMGroupeInvite
        /// </summary>
        public VMGroupeInvite() : this(new GroupeInvites())
        {
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
            List<VMInvite> listeSource;

            if (invitesSauvegardes != null && invitesSauvegardes.Count > 0)
                listeSource = invitesSauvegardes;
            else
                listeSource = invitesListe.ToList();

            foreach (VMInvite vmInvite in listeSource)
            {
                if (vmInvite.InviteSelectionne)
                    inviteSelectionne.Add(vmInvite.Invite);
            }
            this.groupe.Invites = inviteSelectionne;
            Notify("Invites");
        }

        /// <summary>
        /// Sauvegarde la liste complète des invités pour permettre le filtrage
        /// </summary>
        public void InitialiserSauvegardePourRecherche()
        {
            this.invitesSauvegardes = new List<VMInvite>(this.InvitesListe);
        }

        /// <summary>
        /// Filtre la liste des invités affichés selon le texte
        /// </summary>
        public void RechercherInviteDansGroupe(string texteRecherche)
        {
            if (string.IsNullOrWhiteSpace(texteRecherche))
                this.InvitesListe = new ObservableCollection<VMInvite>(invitesSauvegardes);
            else
            {
                List<VMInvite> resultats = invitesSauvegardes.Where(i => i.Identite.Contains(texteRecherche, StringComparison.OrdinalIgnoreCase)).OrderBy(i => i.Identite).ToList();

                this.InvitesListe = new ObservableCollection<VMInvite>(resultats);
            }
        }

        /// <summary>
        /// Ajoute un gestionnaire d'événement pour un VMInviteSelectionne
        /// </summary>
        /// <param name="vmInvite">L'invité sélectionnable</param>
        public void GestionnaireEvenement(VMInvite vmInvite)
        {
            vmInvite.PropertyChanged += VmInvite_PropertyChanged;
        }
        #endregion
    }
}
