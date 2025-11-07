using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using METIER_Footies;
using METIER_Footies.Data;
using METIER_Footies.Metier;

namespace VM_Footies
{
    public class VMPageInvite : INotifyPropertyChanged
    {
        #region Attributs
        private List<VMInvite> listeVMInvite;
        private VMInvite inviteSelectionne;
        private InviteDAO inviteDAO;
        #endregion

        #region Propriétés 
        /// <summary>
        /// Invité sélectionné dans la liste
        /// </summary>
        public VMInvite InviteSelectionne
        {
            get { return inviteSelectionne; }
            set { this.inviteSelectionne = value; }
        }
        #endregion

        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        // Liste des VMInvite 
        /// </summary>
        public List<VMInvite> VMInvites => listeVMInvite;

        #region Constructeurs
        /// <summary>
        // Constructeur par défaut d'une page d'invité
        /// </summary>
        public VMPageInvite()
        {
            this.inviteDAO = new InviteDAO();
            this.listeVMInvite = new List<VMInvite>();
            
            /*
            foreach ( Invite invite in inviteDAO.ObtenirTout().Result)
            {
                this.listeVMInvite.Add(new VMInvite(invite));
            }
            */
            
        }
        #endregion

        #region Méthodes
        
        /// <summary>
        // Charge la liste des invités depuis la base de données
        /// </summary>
        public async void ChargerInvites()
        {
            this.listeVMInvite.Clear();
            List<Invite> invites = await this.inviteDAO.ObtenirTout();
            foreach (Invite invite in invites)
            {
                VMInvite vmInvite = new VMInvite(invite);
                this.listeVMInvite.Add(vmInvite);
            }
            this.Notify("VMInvites");
        }
        

        /// <summary>
        /// Ajoute un invité à la liste des invités
        /// </summary>
        /// <param name="invite"> L'invité à ajouter </param>
        public async void AjouterInvite(VMInvite invite)
        {
            this.inviteDAO.AjouterInvite(invite.Invite);
            this.listeVMInvite.Add(invite);
            this.Notify("VMInvites");
        }

        
        /// <summary>
        /// Supprime un invité de la liste des invités
        /// </summary>
        /// <param name="invite">l'invité à supprimer</param>
        public async void SupprimerInvite()
        {
            if (this.inviteSelectionne != null)
            {
                int id = this.inviteSelectionne.Invite.Id;

                if (id != 0)
                    await this.inviteDAO.SupprimerInvite(id);

                this.listeVMInvite.Remove(this.inviteSelectionne);
                this.inviteSelectionne = null;

                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(VMInvites)));
            }
        }
        
        

        private void Notify(string message)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(message));
        }

        #endregion
    }
}
