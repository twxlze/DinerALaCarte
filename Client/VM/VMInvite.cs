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
                this.invite.Prenom = value;
                this.Notify("Prenom");
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
        #endregion
    }
}
