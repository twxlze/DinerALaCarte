using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using METIER_Footies.Data;
using METIER_Footies.Data.Interfaces;
using METIER_Footies.Metier;
using VM_Footies.VM;
using VM_Footies.VM_Element_Selectionne;

namespace VM_Footies
{
    public class VMPageGroupeInvite : INotifyPropertyChanged
    {
        #region Attributs
        private List<VMGroupeInvite> listeVMGroupeInvite;
        private VMGroupeInvite groupeSelectionne;
        private IGroupeInviteDAO groupeDAO;
        private VMPageInvite vmPageInvite;
        #endregion

        public event PropertyChangedEventHandler? PropertyChanged;

        #region Propriétés
        /// <summary>
        /// Groupe sélectionné par l'utilisateur
        /// </summary>
        public VMGroupeInvite GroupeSelectionne
        {
            get => groupeSelectionne;
            set
            {
                groupeSelectionne = value;
                Notify("GroupeSelectionne");
            }
        }

        /// <summary>
        /// Liste des VMInvite dans le groupe sélectionné
        /// </summary>
        public List<VMInvite> ListeVMInviteGroupe
        {
            get
            {
                List<VMInvite> invites = new List<VMInvite>();
                if (this.GroupeSelectionne != null)
                {
                    foreach (VMInviteSelectionne vmInviteSel in this.GroupeSelectionne.InvitesListe)
                    {
                        if (vmInviteSel.EstSelectionne)
                        {
                            invites.Add(new VMInvite(vmInviteSel.Invite));
                        }
                    }
                }
                return invites;
            }
        }

        /// <summary>
        /// Nom du groupe sélectionné
        /// Sert à afficher le nom du groupe pour la VueDetailInviteDansGroupe
        /// </summary>
        public string NomGroupeSelectionne
        {
            get
            {
                if (this.GroupeSelectionne != null)
                {
                    return this.GroupeSelectionne.Groupe.Nom;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// Liste des VMGroupeInvite
        /// </summary>
        public List<VMGroupeInvite> VMGroupeInvite => listeVMGroupeInvite;

        /// <summary>
        /// Groupe des invités
        /// </summary>
        public GroupeInvites GroupeInvites => this.GroupeInvites;

        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur par défaut d'une page de groupe d'invité
        /// </summary>
        public VMPageGroupeInvite()
        {
            this.groupeDAO = new GroupeInviteDAO();
            this.listeVMGroupeInvite = new List<VMGroupeInvite>();
            this.vmPageInvite = new VMPageInvite();
        }
        #endregion


        #region Méthodes publiques
        /// <summary>
        /// Récupère la liste des VMGroupeInvite pour affichage
        /// </summary>
        public async Task ChargerGroupeInvites()
        {
            this.listeVMGroupeInvite.Clear();
            List<GroupeInvites> groupes = await this.groupeDAO.ListeGroupeInvites();

            foreach (GroupeInvites g in groupes)
            {
                VMGroupeInvite vmGroupe = new VMGroupeInvite(g);
                this.listeVMGroupeInvite.Add(vmGroupe);
            }
            this.listeVMGroupeInvite = this.listeVMGroupeInvite.OrderBy(vm => vm.Groupe.Nom).ToList();
        }

        /// <summary>
        /// Charge les invités dans le groupe spécifié
        /// </summary>
        /// <param name="groupe"> Le groupe dans lequel charger les invités </param>
        /// <returns> Tâche asynchrone </returns>
        /// <exception cref="Exception"> Lance une exception en cas d'erreur lors du chargement des invités </exception>
        public async Task ChargerInvitesDansGroupe(VMGroupeInvite groupe)
        {
            try
            {
                await this.vmPageInvite.ChargerInvites();
                HashSet<long> idDesInvitesGroupe = new HashSet<long>();
                if (groupe.Groupe.Invites != null)
                {
                    foreach (Invite invite in groupe.Groupe.Invites)
                    {
                        idDesInvitesGroupe.Add(invite.Id);
                    }
                }
                groupe.InvitesListe.Clear();

                foreach (VMInvite vmInvite in this.vmPageInvite.VMInvites)
                {
                    bool estSelectionne = idDesInvitesGroupe.Contains(vmInvite.Id);
                    VMInviteSelectionne vmInviteSelectionne = new VMInviteSelectionne(vmInvite.Invite, estSelectionne);
                    groupe.GestionnaireEvenement(vmInviteSelectionne);
                    groupe.InvitesListe.Add(vmInviteSelectionne);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erreur lors du chargement des invités dans le groupe : {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Ajoute un nouveau groupe côté serveur et met à jour le ViewModel local si succès
        /// </summary>
        /// <param name="vmGroupe"> Le groupe à ajouter </param>
        /// <returns> Tâche asynchrone </returns>
        /// <exception cref="Exception"> Lance une exception si le groupe existe déjà </exception>
        public async Task AjouterGroupe(VMGroupeInvite vmGroupe)
        {
            if (this.GroupeExiste(vmGroupe))
            {
                throw new Exception("Un groupe avec ce nom existe déjà.");
            }
            vmGroupe.SynchroniserInvitesSelectionnes();
            await this.groupeDAO.AjouterGroupeInvite(vmGroupe.Groupe);
            this.listeVMGroupeInvite.Add(vmGroupe);
            this.Notify("VMGroupeInvite");
        }

        /// <summary>
        /// Modifie le groupe côté serveur et met à jour le ViewModel local si succès
        /// </summary>
        /// <param name="groupe">Le groupe avec les nouvelles informations</param>
        /// <returns>true si la modification a réussi, false sinon</returns>
        public async Task ModifierGroupe(VMGroupeInvite groupe)
        {
            if (groupe != null)
            {
                groupe.SynchroniserInvitesSelectionnes();
                await this.groupeDAO.ModifierGroupe(groupe.Groupe);
                this.Notify("VMGroupeInvite");
            }
        }

        /// <summary>
        /// Supprime le groupe sélectionné côté serveur et met à jour le ViewModel local si succès
        /// </summary>
        /// <returns> true si la suppression a réussi, false sinon </returns>
        public async Task<bool> SupprimerGroupe()
        {
            bool suppressionReussie = false;
            if (this.GroupeSelectionne != null)
            {
                long idGroupe = this.GroupeSelectionne.Groupe.IdGroupeInvites;
                if (idGroupe != 0)
                {
                    await this.groupeDAO.SupprimerGroupeInvite(idGroupe);
                    this.listeVMGroupeInvite.Remove(this.GroupeSelectionne);
                    this.GroupeSelectionne = null;
                    suppressionReussie = true;
                }
                else
                {
                    this.listeVMGroupeInvite.Remove(this.GroupeSelectionne);
                    this.GroupeSelectionne = null;
                    suppressionReussie = true;
                }
            }
            return suppressionReussie;
        }

        /// <summary>
        /// Vérifie si un groupe avec le même nom existe déjà
        /// </summary>
        /// <param name="groupe"> Le groupe à vérifier </param>
        /// <returns> True si le groupe existe, False sinon </returns>
        public bool GroupeExiste(VMGroupeInvite groupe)
        {
            return this.listeVMGroupeInvite.Any(g => g.Groupe.Nom.Equals(groupe.Groupe.Nom, StringComparison.OrdinalIgnoreCase));
        }
        #endregion


        #region méthodes privées
        /// <summary>
        /// Notifie l'UI d'un changement de propriété
        /// </summary>
        /// <param name="propriete">Nom de la propriété modifiée</param>
        private void Notify(string propriete)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propriete));
        }
        #endregion

    }
}
