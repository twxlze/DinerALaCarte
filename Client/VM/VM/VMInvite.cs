using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using METIER_Footies.Enum;
using METIER_Footies.Metier;
using VM_Footies.VM_Element_Selectionne;

namespace VM_Footies.VM
{
    /// <summary>
    /// Classe ViewModel pour un invité
    /// </summary>
    /// <summary>
    /// Classe ViewModel pour un invité
    /// </summary>
    public class VMInvite : INotifyPropertyChanged
    {
        #region Attributs
        private Invite invite;
        private List<VMAllergene> allergiesListe;
        private bool estSelectionne;
        private ObservableCollection<VMPlat> platsDetestesListe;
        private ObservableCollection<VMPlat> platsPreferesListe;
        private VMInvite vmInviteStat;
        #endregion

        #region
        public event PropertyChangedEventHandler? PropertyChanged;
        #endregion

        #region Propriétés
        /// <summary>
        /// Invite associée au VMInvite
        /// </summary>
        public Invite Invite => invite;

        /// <summary>
        /// Id de l'invité
        /// </summary>
        public long Id
        {
            get => invite.Id;
        }

        /// <summary>
        // Nom de famille de l'invité
        /// </summary>
        /// <remarks> Le set notifie le changement de la propriété </remarks>
        public string Nom
        {
            get => invite.Nom;
            set
            {
                invite.Nom = value;
                Notify("Nom");
                Notify("Identite");
            }
        }

        /// <summary>
        /// Prénom de l'invité
        /// </summary>
        /// <remarks> Le set notifie le changement de la propriété </remarks>
        public string Prenom
        {
            get => invite.Prenom;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    invite.Prenom = char.ToUpper(value[0]) + value.Substring(1);
                else
                    invite.Prenom = value;
                Notify("Prenom");
                Notify("Identite");
            }
        }

        /// <summary>
        /// Téléphone de l'invité
        /// </summary>
        /// <remarks> Le set notifie le changement de la propriété </remarks>
        public string Telephone
        {
            get => invite.Telephone;
            set
            {
                invite.Telephone = value;
                Notify("Telephone");
            }
        }

        /// <summary>
        /// Email de l'invité
        /// </summary>
        /// <remarks> Le set notifie le changement de la propriété </remarks>
        public string Email
        {
            get => invite.Email;
            set
            {
                invite.Email = value;
                Notify("Email");
            }
        }

        /// <summary>
        /// Nom complet de l'invité (Prénom + Nom)
        /// </summary>
        public string Identite { get => invite.Identite; }

        /// <summary>
        /// Liste des allergènes du plat
        /// </summary>
        public List<VMAllergene> Allergies
        {
            get { return allergiesListe; }
            set
            {
                allergiesListe = value;
                Notify("AllergiesListe");
            }
        }

        /// <summary>
        /// Liste des allergies que possède l'invité (pour l'affichage en lecture seule)
        /// </summary>
        public List<VMAllergene> AllergiesSelectionnes
        {
            get
            {
                return allergiesListe?.Where(a => a.EstSelectionne).ToList() ?? new List<VMAllergene>();
            }
        }

        /// <summary>
        /// Liste des plats détestés par l'invité
        /// </summary>
        public List<Plat>? PlatsDetestes
        {
            get => invite.PlatsDetestes;
        }

        /// <summary>
        /// Liste des plats préférés par l'invité
        /// </summary>
        public List<Plat>? PlatsPreferes
        {
            get => invite.PlatsPreferes;
        }


        /// <summary>
        /// Liste observable des plats aimés
        /// </summary>
        public ObservableCollection<VMPlat> PlatsDetestesListe
        {
            get => platsDetestesListe;
            set
            {
                platsDetestesListe = value;
                Notify("PlatsDetestesListe");
            }
        }

        /// <summary>
        /// Liste observable des plats préférés
        /// </summary>
        public ObservableCollection<VMPlat> PlatsPreferesListe
        {
            get => platsPreferesListe;
            set
            {
                platsPreferesListe = value;
                Notify("PlatsPreferesListe");
            }
        }

        /// <summary>
        /// Indique si l'invité est sélectionné dans l'interface
        /// </summary>
        public bool InviteSelectionne
        {
            get { return estSelectionne; }
            set
            {
                if (estSelectionne != value)
                {
                    estSelectionne = value;
                    Notify("InviteSelectionne");
                }
            }
        }

        /// <summary>
        /// Invite associée (pour les statisiques)
        /// </summary>
        public VMInvite VMInviteStat
        {
            get { return vmInviteStat; }
            set
            {
                this.vmInviteStat = value;
                Notify("Invite");
            }
        }
        #endregion

        #region Constructeurs
        /// <summary>
        // Constructeur d'un VMInvite à partir d'un Invite
        /// </summary>
        /// <param name="invite"> L'invité modèle </param>
        public VMInvite(Invite invite, bool estSelectionne = false)
        {
            this.invite = invite;
            this.estSelectionne = estSelectionne;
            this.platsDetestesListe = new ObservableCollection<VMPlat>();
            this.platsPreferesListe = new ObservableCollection<VMPlat>();
            InitialiserListeAllergies();
        }

        /// <summary>
        /// Constructeur d'un VMInvite à partir d'un autre VMInvite
        /// </summary>
        /// <param name="modele"> Le VMInvite à copier </param>
        public VMInvite(VMInvite modele) : this(new Invite(modele.Invite))
        {
            this.estSelectionne = modele.InviteSelectionne;
            this.vmInviteStat = modele;
        }

        /// <summary>
        /// Constructeur par défaut d'un VMInvite
        /// </summary>
        public VMInvite() : this(new Invite())
        {
        }
        #endregion

        #region Méthodes
        /// <summary>
        /// Prépare la liste de toutes les allergies possibles pour l'interface
        /// </summary>
        private void InitialiserListeAllergies()
        {
            List<VMAllergene> listeTemp = new List<VMAllergene>();
            Array valeursEnum = Enum.GetValues(typeof(NomAllergene));

            foreach (NomAllergene allergie in valeursEnum)
            {
                bool estPresent = false;
                if (this.invite.Allergenes != null)
                {
                    estPresent = this.invite.Allergenes.Contains(allergie);
                }
                VMAllergene vmAllergene = new VMAllergene(allergie, estPresent);
                listeTemp.Add(vmAllergene);
            }

            this.allergiesListe = listeTemp;
        }

        /// <summary>
        /// Transfère les cases cochées de l'interface vers le modèle Plat
        /// À appeler avant d'envoyer le plat à la base de données.
        /// </summary>
        public void SauvegarderAllergies()
        {
            List<NomAllergene> allergenesSelectionnes = new List<NomAllergene>();

            foreach (VMAllergene vm in this.allergiesListe)
            {
                if (vm.EstSelectionne)
                {
                    allergenesSelectionnes.Add(vm.Allergene);
                }
            }

            this.invite.Allergenes = allergenesSelectionnes;
        }

        /// <summary>
        // Notifie le changement d'une propriété
        /// </summary>
        /// <param name="message"> Nom de la propriété changée </param>
        private void Notify(string message)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(message));
        }

        /// <summary>
        /// Synchronise la liste des allergènes sélectionnés avec le modèle
        /// </summary>
        /// <param name="sender">L'expéditeur</param>
        /// <param name="e">Les arguments de l'événement</param>
        private void Vm_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "EstSelectionne")
            {
                if (sender is VMPlat vmPlat)
                {
                    if (this.platsDetestesListe.Contains(vmPlat))
                    {
                        SynchroniserPlatsDetestes();
                    }
                    else if (this.platsPreferesListe.Contains(vmPlat))
                    {
                        SynchroniserPlatsPreferes();
                    }
                }
            }
        }

        /// <summary>
        /// Met à jour la liste des plats detestés dans le modèle
        /// </summary>
        public void SynchroniserPlatsDetestes()
        {
            List<Plat> platsDetestes = new List<Plat>();
            foreach (VMPlat vmPlatSelectionne in this.platsDetestesListe)
            {
                if (vmPlatSelectionne.EstSelectionne)
                {
                    platsDetestes.Add(vmPlatSelectionne.Plat);
                }
            }
            this.invite.PlatsDetestes = platsDetestes;
            Notify("PlatsDetestes");
        }

        /// <summary>
        /// Met à jour la liste des plats préférés dans le modèle
        /// </summary>
        public void SynchroniserPlatsPreferes()
        {
            List<Plat> platsPreferes = new List<Plat>();
            foreach (VMPlat vmPlatSelectionne in this.platsPreferesListe)
            {
                if (vmPlatSelectionne.EstSelectionne)
                {
                    platsPreferes.Add(vmPlatSelectionne.Plat);
                }
            }
            this.invite.PlatsPreferes = platsPreferes;
            Notify("PlatsPreferes");
        }

        /// <summary>
        /// Ajoute un gestionnaire d'événement pour un VMPlatSelectionne
        /// </summary>
        /// <param name="vmPlat">Le plat sélectionnable</param>
        public void GestionnaireEvenement(VMPlat vmPlat)
        {
            if (vmPlat != null)
            {
                vmPlat.PropertyChanged += Vm_PropertyChanged;
            }
        }
        #endregion

        #region Propriétés supplémentaires

        /// <summary>
        /// Liste des plats préférés sélectionnés uniquement 
        /// </summary>
        public List<VMPlat> PlatsPreferesSelectionnes
        {
            get
            {
                return platsPreferesListe?.Where(p => p.EstSelectionne).Select(p => p).ToList() ?? new List<VMPlat>();
            }
        }

        #endregion
    }
}
