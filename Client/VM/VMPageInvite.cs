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
            
            
            //foreach ( Invite invite in inviteDAO.ObtenirTout().Result)
            //{
            //    this.listeVMInvite.Add(new VMInvite(invite));
            //}
           
                    }
        #endregion

        #region Méthodes

        /// <summary>
        // Charge la liste des invités depuis la base de données
        /// </summary>
        public async Task ChargerInvitesAsync()
        {
            this.Notify("VMInvites");
            this.listeVMInvite.Clear();
            List<Invite> invites = await this.inviteDAO.ObtenirTout();
            foreach (Invite invite in invites)
            {
                VMInvite vmInvite = new VMInvite(invite);
                this.listeVMInvite.Add(vmInvite);
            }
        }

        /// <summary>
        // Charge la liste des invités depuis la base de données (version non-async pour compatibilité)
        /// </summary>
        public async void ChargerInvites()
        {
            await ChargerInvitesAsync();
        }

        /// <summary>
        /// Ajoute un invité à la liste des invités
        /// </summary>
        /// <param name="invite"> L'invité à ajouter </param>
        public async Task AjouterInvite(VMInvite invite)
        {
            this.Notify("VMInvites");
            await this.inviteDAO.AjouterInvite(invite.Invite);
            this.listeVMInvite.Add(invite);
        }


        /// <summary>
        /// Supprime un invité de la liste des invités
        /// </summary>
        /// <returns>True si la suppression a réussi, False sinon</returns>
        public async Task<bool> SupprimerInvite()
        {
            bool suppressionReussie = false;

            if (this.inviteSelectionne != null)
            {
                long id = this.inviteSelectionne.Invite.Id;

                if (id != 0)
                {
                    bool estDansUnGroupe = await this.inviteDAO.EstDansUnGroupe(id);
                    if (!estDansUnGroupe)
                    {
                        await this.inviteDAO.SupprimerInvite(id);
                        this.listeVMInvite.Remove(this.inviteSelectionne);
                        this.inviteSelectionne = null;
                        suppressionReussie = true;
                    }
                }
                else
                {
                    this.listeVMInvite.Remove(this.inviteSelectionne);
                    this.inviteSelectionne = null;
                    suppressionReussie = true;
                }
            }

            return suppressionReussie;
        }

        /// <summary>
        /// Modifie un invité dans la liste des invités
        /// </summary>
        /// <param name="invite"></param>
        public async void ModifierInvite(VMInvite invite)
        {
            if (invite != null)
            {
                this.Notify("VMInvite");
                await this.inviteDAO.ModifierInvite(invite.Invite);
            }
        }

        /// <summary>
        /// Notifie le changement d'une propriété
        /// </summary>
        /// <param name="message"> Nom de la propriété changée </param>
        private void Notify(string message)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(message));
        }

        #endregion
    }
}
