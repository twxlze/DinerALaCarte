using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using METIER_Footies.Data;
using METIER_Footies.Metier;

namespace VM_Footies
{
    /// <summary>
    /// ViewModel pour gérer un groupe d'invités et ses invités
    /// </summary>
    public class VMGroupeInvite : INotifyPropertyChanged
    {
        #region Attributs

        private GroupeInvites groupe;               // Le modèle du groupe
        private List<VMInvite> listeVMInvite;       // Liste des invités du groupe
        private GroupeInviteDAO groupeDAO;          // DAO pour interagir avec la base

        #endregion

        #region Propriétés

        /// <summary>
        /// Le nom du groupe (modifiable)
        /// </summary>
        public string Nom
        {
            get
            {
                return this.groupe.Nom;
            }
            set
            {
                if (this.groupe.Nom != value)
                {
                    this.groupe.Nom = value;
                    this.Notifier("Nom");
                }
            }
        }

        /// <summary>
        /// Liste des VMInvite pour affichage dans l'UI
        /// </summary>
        public List<VMInvite> VMInvites
        {
            get
            {
                return this.listeVMInvite;
            }
        }

        #endregion

        #region Evénement

        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion

        #region Constructeurs

        /// <summary>
        /// Constructeur d'un VMGroupeInvite à partir d'un modèle GroupeInvites
        /// </summary>
        /// <param name="groupe">Le groupe à gérer</param>
        public VMGroupeInvite(GroupeInvites groupe)
        {
            this.groupe = groupe;
            this.groupeDAO = new GroupeInviteDAO();
            this.listeVMInvite = new List<VMInvite>();

            // Initialiser la liste des VMInvite à partir du modèle
            foreach (Invite invite in groupe.Invites)
            {
                VMInvite vmInvite = new VMInvite(invite);
                this.listeVMInvite.Add(vmInvite);
            }
        }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Modifie le nom du groupe côté serveur
        /// </summary>
        /// <param name="nouveauNom">Le nouveau nom du groupe</param>
        /// <returns>true si la modification a réussi, false sinon</returns>
        public async Task<bool> ModifierNomGroupe(string nouveauNom)
        {
            bool resultat = false;
            try
            {
                this.groupe.Nom = nouveauNom;
                bool reussi = await this.groupeDAO.ModifierGroupe(this.groupe);
                if (reussi)
                {
                    this.Notifier("Nom");
                    resultat = true;
                }
            }
            catch (Exception)
            {
                resultat = false;
            }
            return resultat;
        }

        /// <summary>
        /// Ajoute un invité au groupe et synchronise avec le serveur
        /// </summary>
        /// <param name="invite">L'invité à ajouter</param>
        /// <returns>true si l'ajout a réussi, false sinon</returns>
        public async Task<bool> AjouterInvite(VMInvite invite)
        {
            bool resultat = false;
            try
            {
                bool ok = await this.groupeDAO.AjouterInviteAuGroupe(this.groupe.IdGroupeInvites, invite.Invite);
                if (ok)
                {
                    this.listeVMInvite.Add(invite);
                    this.Notifier("VMInvites");
                    resultat = true;
                }
            }
            catch (Exception)
            {
                resultat = false;
            }
            return resultat;
        }

        /// <summary>
        /// Récupère la liste des VMInvite pour affichage
        /// </summary>
        public void ChargerInvites()
        {
            this.listeVMInvite.Clear();
            foreach (Invite invite in this.groupe.Invites)
            {
                VMInvite vmInvite = new VMInvite(invite);
                this.listeVMInvite.Add(vmInvite);
            }
            this.Notifier("ChargerVMInvites");
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
