using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using METIER_Footies.Data;
using METIER_Footies.Metier;

namespace VM_Footies
{
    internal class VmPagePlat : INotifyPropertyChanged
    {
        #region Attributs
        private List<VMPlat> listeVMPlat;
        private PlatDAO platDAO;
        #endregion

        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        // Liste des VMInvite 
        /// </summary>
        public List<VMPlat> VMPlat => listeVMPlat;

        #region Constructeurs
        /// <summary>
        // Constructeur par défaut d'une page d'invité
        /// </summary>
        public VMPagePlat()
        {
            this.inviteDAO = new InviteDAO();
            this.listeVMPlat = new List<VMPlat>();

            foreach (Plat plat in PlatDAO.ObtenirTout().Result)
            {
                this.listeVMPlat.Add(new VMPlat(plat));
            }
        }
        #endregion

        #region Méthodes
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

        private void Notify(string message)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(message));
        }
        #endregion
    }
}
