using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using METIER_Footies.Metier;

namespace VM_Footies
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
        public Invite Invite => this.invite;


        #region Propriétés
        /// <summary>
        /// Id de l'invité
        /// </summary>
        public int Id
        {
            get => this.invite.Id;
        }

        /// <summary>
        // Nom de famille de l'invité
        /// </summary>
        /// <remarks> Le set notifie le changement de la propriété </remarks>
        public string Nom
        {
            get => this.invite.Nom;
            set
            {
                this.invite.Nom = value;
                this.Notify("Nom");
                this.Notify("Identite");
            }
        }

        /// <summary>
        /// Prénom de l'invité
        /// </summary>
        /// <remarks> Le set notifie le changement de la propriété </remarks>
        public string Prenom
        {
            get => this.invite.Prenom;
            set
            {
                this.invite.Prenom = char.ToUpper(value[0]) + value.Substring(1);
                this.Notify("Prenom");
                this.Notify("Identite");
            }
        }

        /// <summary>
        /// Téléphone de l'invité
        /// </summary>
        /// <remarks> Le set notifie le changement de la propriété </remarks>
        public string Telephone
        {
            get => this.invite.Telephone;
            set
            {
                this.invite.Telephone = value;
                this.Notify("Telephone");
            }
        }

        /// <summary>
        /// Email de l'invité
        /// </summary>
        /// <remarks> Le set notifie le changement de la propriété </remarks>
        public string Email
        {
            get => this.invite.Email;
            set
            {
                this.invite.Email = value;
                this.Notify("Email");
            }
        }

        /// <summary>
        /// Nom complet de l'invité (Prénom + Nom)
        /// </summary>
        public string Identite { get => $"{this.Prenom} {this.Nom}"; }
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

        public VMInvite(VMInvite modele)
        {
            this.invite = new Invite(modele.invite);
        }

        public VMInvite()
        {
            this.invite = new Invite();
        }
        #endregion

        #region Méthodes
        /// <summary>
        // Notifie le changement d'une propriété
        /// </summary>
        /// <param name="message"> Nom de la propriété changée </param>
        private void Notify(string message)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(message));
        }

        /// <summary>
        /// Modifie les informations de l'invité
        /// </summary>
        /// <param name="invite"> L'invité avec les nouvelles informations </param>
        public void ModifierInvite(VMInvite invite)
        {
            this.Nom = invite.Nom;
            this.Prenom = invite.Prenom;
            this.Telephone = invite.Telephone;
            this.Email = invite.Email;
        }
        #endregion
    }
}
