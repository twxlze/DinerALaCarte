using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using METIER_Footies.Data;
using METIER_Footies.Metier;

namespace VM_Footies.VM
{
    /// <summary>
    /// ViewModel pour gérer un groupe d'invités et ses invités
    /// </summary>
    public class VMGroupeInvite : INotifyPropertyChanged
    {
        #region Attributs

        private GroupeInvites groupe;               // Le modèle du groupe
        private List<VMInvite> listeVMInviteDuGroupe;       // Liste des invités du groupe
        private GroupeInviteDAO groupeDAO; // DAO pour les opérations sur les groupes

        #endregion

        #region Propriétés

        /// <summary>
        /// Le nom du groupe (modifiable)
        /// </summary>
        public string Nom
        {
            get
            {
                return groupe.Nom;
            }
            set
            {
                if (groupe.Nom != value)
                {
                    groupe.Nom = value;
                    Notifier("Nom");
                }
            }
        }

        /// <summary>
        /// Liste des VMInvite pour affichage dans l'UI
        /// </summary>
        public List<VMInvite> ListeVMInviteDuGroupe
        {
            get
            {
                return listeVMInviteDuGroupe;
            }
        }

        public GroupeInvites Groupe => groupe;


        #endregion

        #region Evénement

        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion

        #region Constructeurs

        /// <summary>
        /// Constructeur d'un VMGroupeInvite à partir d'un modèle GroupeInvites
        /// </summary>
        /// <param name="groupe">Le groupe à gérer</param>
        public VMGroupeInvite(GroupeInvites groupes)
        {
            groupe = groupes;
            listeVMInviteDuGroupe = new List<VMInvite>();
            groupeDAO = new GroupeInviteDAO();
        }

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public VMGroupeInvite()
        {
            groupe = new GroupeInvites();
            listeVMInviteDuGroupe = new List<VMInvite>();
            groupeDAO = new GroupeInviteDAO();
        }

        #endregion


        #region Méthodes publiques

        /// <summary>
        /// Charge les invités du groupe depuis le serveur
        /// </summary>
        /// <returns>Une tâche représentant l'opération asynchrone</returns>
        public async Task ChargerInvitesGroupeAsync(VMGroupeInvite vMGroupe)
        {
            listeVMInviteDuGroupe.Clear();
            GroupeInvites? groupes = await groupeDAO.RecupererGroupeParId(vMGroupe.Groupe.IdGroupeInvites);
            if (groupes != null)
            {
                foreach (Invite invite in groupes.Invites)
                {
                    listeVMInviteDuGroupe.Add(new VMInvite(invite));
                }
            }
        }

        /// <summary>
        /// Ajoute un invité au groupe côté serveur et met à jour le ViewModel local si succès
        /// </summary>
        /// <param name="invite"> l'invite (sa vue)</param>
        /// <returns>succes ou pas</returns>
        public async Task<bool> AjouterInviteAuGroupe(VMInvite invite)
        {
            bool succes = await groupeDAO.AjouterInviteAuGroupe(groupe.IdGroupeInvites, invite.Invite);
            if (succes)
            {
                listeVMInviteDuGroupe.Add(invite);
                Notifier("ListeVMInviteDuGroupe");
            }
            return succes;
        }

        #endregion

        #region Méthodes privées

        /// <summary>
        /// Notifie l'UI d'un changement de propriété
        /// </summary>
        /// <param name="propriete">Nom de la propriété modifiée</param>
        private void Notifier(string propriete)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propriete));
            }
        }

        #endregion
    }
}
