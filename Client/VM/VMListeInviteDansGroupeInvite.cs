using METIER_Footies.Data;
using METIER_Footies.Metier;
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

        private VMGroupeInvite groupeVM;                    // Le ViewModel du groupe
        private GroupeInvites groupe;                       // Le modèle du groupe
        private List<VMInvite> listeVMInviteDuGroupe;       // Liste des invités du groupe
        private GroupeInviteDAO groupeDAO;                 // DAO pour les opérations sur les groupes
        private VMInvite inviteSelectionne;              // Invité sélectionné dans l'UI

        #endregion

        #region Propriétés

        /// <summary>
        /// La liste des invités du groupe pour le binding
        /// </summary>
        public List<VMInvite> VMInvites
        {
            get
            {
                List<VMInvite> invite = new List<VMInvite>();
                if (groupeVM != null)
                {
                    invite = groupeVM.ListeVMInviteDuGroupe;
                }
                return invite ;
            }
        }

        /// <summary>
        /// Invité sélectionné dans la liste
        /// </summary>
        public VMInvite InviteSelectionne
        {
            get { return inviteSelectionne; }
            set { this.inviteSelectionne = value; }
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
        public async Task<bool> AjouterInvite()
        {
            bool resultat = false;
            if (this.inviteSelectionne != null)
            {
                resultat = await groupeVM.AjouterInviteAuGroupe(inviteSelectionne);
                if (resultat)
                {
                    this.Notifier("VMInvites");
                }
            }
            return resultat;
        }

        /*   implementer ici supprimer un invite du groupe  */

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
