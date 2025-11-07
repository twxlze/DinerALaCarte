using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace VM_Footies
{
    /// <summary>
    /// ViewModel pour gérer la liste des invités d'un groupe
    /// </summary>
    public class VMListeInviteDansGroupeInvite : INotifyPropertyChanged
    {
        #region Attributs

        private VMGroupeInvite groupeVM;

        #endregion

        #region Propriétés

        /// <summary>
        /// La liste des invités du groupe pour le binding
        /// </summary>
        public List<VMInvite> VMInvites
        {
            get
            {
                if (groupeVM != null)
                {
                    return groupeVM.VMInvites;
                }
                return new List<VMInvite>();
            }
        }

        /// <summary>
        /// Groupe sélectionné
        /// </summary>
        public VMGroupeInvite Groupe
        {
            get
            {
                return groupeVM;
            }
            set
            {
                if (groupeVM != value)
                {
                    groupeVM = value;
                    this.Notifier("VMInvites");
                    this.Notifier("Groupe");
                }
            }
        }

        #endregion

        #region Evénement

        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur prenant un VMGroupeInvite
        /// </summary>
        /// <param name="groupe">Le groupe dont on veut gérer les invités</param>
        public VMListeInviteDansGroupeInvite(VMGroupeInvite groupe)
        {
            this.groupeVM = groupe;
        }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Ajoute un invité au groupe et met à jour la liste pour l'UI
        /// </summary>
        /// <param name="invite">L'invité à ajouter</param>
        /// <returns>true si l'ajout a réussi, false sinon</returns>
        public async Task<bool> AjouterInvite(VMInvite invite)
        {
            if (groupeVM != null)
            {
                bool resultat = await groupeVM.AjouterInvite(invite);
                if (resultat)
                {
                    this.Notifier("VMInvites");
                }
                return resultat;
            }
            return false;
        }

        /// <summary>
        /// Recharge la liste des invités depuis le groupe
        /// </summary>
        public void RechargerInvites()
        {
            if (groupeVM != null)
            {
                groupeVM.ChargerInvites();
                this.Notifier("VMInvites");
            }
        }

        #endregion

        #region Méthodes privées

        /// <summary>
        /// Notifie l'UI d'un changement de propriété
        /// </summary>
        /// <param name="propriete">Nom de la propriété modifiée</param>
        private void Notifier(string propriete)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propriete));
            }
        }

        #endregion
    }
}
