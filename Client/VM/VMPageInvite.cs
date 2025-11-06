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
        private InviteDAO inviteDAO;
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
        public void AjouterInvite(VMInvite invite)
        {
            this.inviteDAO.AjouterInvite(invite.Invite);
            this.listeVMInvite.Add(invite);
            this.Notify("VMInvites");
        }

        /// <summary>
        /// Supprime un invité de la liste des invités
        /// </summary>
        /// <param name="invite">l'invité à supprimer</param>
        public void SupprimerInvite(VMInvite invite)
        {
            this.inviteDAO.SupprimerInvite(invite.Id);
            this.listeVMInvite.Remove(invite);
            this.Notify("VMInvites");     
        }

        private void Notify(string message)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(message));
        }

        #endregion
    }
}
