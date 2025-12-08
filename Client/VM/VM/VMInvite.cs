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
        public string Identite { get => $"{Prenom} {Nom}"; }

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

        public bool EstSelectionne
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
            InitialiserListeAllergies();
        }

        /// <summary>
        /// Constructeur d'un VMInvite à partir d'un autre VMInvite
        /// </summary>
        /// <param name="modele"> Le VMInvite à copier </param>
        public VMInvite(VMInvite modele) : this(new Invite(modele.Invite))
        {
            this.estSelectionne = modele.EstSelectionne;
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
        #endregion
    }
}
