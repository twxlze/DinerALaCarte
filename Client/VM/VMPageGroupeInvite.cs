using METIER_Footies.Data;
using METIER_Footies.Metier;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM_Footies
{
    class VMPageGroupeInvite : INotifyPropertyChanged
    {
        #region Attributs
        private GroupeInviteDAO groupeDAO; // DAO pour les opérations sur les groupes
        private VMGroupeInvite groupeSelectionner;    // Le groupe sélectionné
        private List<VMGroupeInvite> listeVMGroupeInvite;       // Liste des invités du groupe
        #endregion

        #region Propriétés
        public event PropertyChangedEventHandler? PropertyChanged;
        public VMGroupeInvite GroupeSelectionner
        {
            get => groupeSelectionner;
            set
            {
                groupeSelectionner = value;
                Notifier("GroupeSelectionner");
            }
        }
        public List<VMGroupeInvite> ListeVMGroupeInvite
        {
            get => listeVMGroupeInvite;
            set
            {
                listeVMGroupeInvite = value;
                Notifier("ListeVMGroupeInvite");
            }
        }
        #endregion

        #region Constructeurs
        public VMPageGroupeInvite()
        {
            this.groupeDAO = new GroupeInviteDAO();
            this.listeVMGroupeInvite = new List<VMGroupeInvite>();
        }
        #endregion


        #region Méthodes publiques
        /// <summary>
        /// Modifie le groupe côté serveur et met à jour le ViewModel local si succès
        /// </summary>
        /// <param name="groupe">Le groupe avec les nouvelles informations</param>
        /// <returns>true si la modification a réussi, false sinon</returns>
        public async Task ModifierGroupeAsync(VMGroupeInvite groupe)
        {
            if (groupe != null)
            {
                bool succes = await this.groupeDAO.ModifierGroupe(groupe.Groupe);

                if (succes)
                {
                    groupe.Groupe.Nom = groupe.Nom;
                    this.Notifier("ModifierNom");
                }
            }
        }

        /*   implementer ici supprimer un groupe invite  */

        /// <summary>
        /// Récupère la liste des VMGroupeInvite pour affichage
        /// </summary>
        public async Task ChargerGroupeInvites()
        {
            this.listeVMGroupeInvite.Clear();
            List<GroupeInvites> groupe = await this.groupeDAO.RecupererTousGroupenvites();

            foreach (GroupeInvites g in groupe)
            {
                VMGroupeInvite vmGroupe = new VMGroupeInvite(g);
                this.listeVMGroupeInvite.Add(vmGroupe);
            }
            this.Notifier("ChargerGpInvites");
        }
        #endregion


        #region méthodes privées
        /// <summary>
        /// Notifie l'UI d'un changement de propriété
        /// </summary>
        /// <param name="propriete">Nom de la propriété modifiée</param>
        private void Notifier(string propriete)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propriete));
            }
        }

        #endregion

    }
}
