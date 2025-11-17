using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using METIER_Footies.Metier;

namespace VM_Footies.VM
{
    /// <summary>
    /// Classe ViewModel pour un invité
    /// </summary>
    public class VMInvite : INotifyPropertyChanged
    {
        #region Attributs
        private Invite invite;
        #endregion


        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Invite associée au VMInvite
        /// </summary>
        public Invite Invite => invite;


        #region Propriétés
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
        #endregion

        #region Constructeurs
        /// <summary>
        // Constructeur d'un VMInvite à partir d'un Invite
        /// </summary>
        /// <param name="invite"></param>
        public VMInvite(Invite invite)
        {
            this.invite = invite;
        }

        /// <summary>
        /// Constructeur d'un VMInvite à partir d'un autre VMInvite
        /// </summary>
        /// <param name="modele"> Le VMInvite à copier </param>
        public VMInvite(VMInvite modele)
        {
            invite = new Invite(modele.invite);
        }

        /// <summary>
        /// Constructeur par défaut d'un VMInvite
        /// </summary>
        public VMInvite()
        {
            invite = new Invite();
        }
        #endregion

        #region Méthodes
        /// <summary>
        // Notifie le changement d'une propriété
        /// </summary>
        /// <param name="message"> Nom de la propriété changée </param>
        private void Notify(string message)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(message));
        }

        /// <summary>
        /// Modifie les informations de l'invité
        /// </summary>
        /// <param name="invite"> L'invité avec les nouvelles informations </param>
        public void ModifierInvite(VMInvite invite)
        {
            Nom = invite.Nom;
            Prenom = invite.Prenom;
            Telephone = invite.Telephone;
            Email = invite.Email;
        }
        #endregion
    }
}
