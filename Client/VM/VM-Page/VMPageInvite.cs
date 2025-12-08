using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using METIER_Footies;
using METIER_Footies.Data;
using METIER_Footies.Data.Interfaces;
using METIER_Footies.Enum;
using METIER_Footies.Metier;
using VM_Footies.VM;
using VM_Footies.VM_Element_Selectionne;

namespace VM_Footies
{
    public class VMPageInvite : INotifyPropertyChanged
    {
        #region Attributs
        private List<VMInvite> listeVMInvite;
        private VMInvite inviteSelectionne;
        private IInviteDAO inviteDAO;
        private string texteRecherche;
        #endregion

        #region Propriétés 
        /// <summary>
        /// Invité sélectionné dans la liste
        /// </summary> 
        public VMInvite InviteSelectionne
        {
            get { return inviteSelectionne; }
            set
            {
                this.inviteSelectionne = value; 
                Notify("InviteSelectionne"); 
            }
        }

        /// <summary>
        /// Texte de recherche pour filtrer les invités
        /// </summary>
        public string TexteRecherche
            {
            get { return texteRecherche; }
            set
            {
                texteRecherche = value;
                Notify("TexteRecherche");
            }
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
        }
        #endregion

        #region Méthodes
        /// <summary>
        // Charge la liste des invités depuis la base de données
        /// </summary>
        public async Task ChargerInvites()
        {
            this.listeVMInvite.Clear();

            List<Invite> invites = await this.inviteDAO.ObtenirTout();
            foreach (Invite invite in invites)
            {
                VMInvite vmInvite = new VMInvite(invite);
                this.listeVMInvite.Add(vmInvite);
            }
            this.listeVMInvite = this.listeVMInvite.OrderBy(vm => vm.Invite.Prenom)
                                                   .ThenBy(vm => vm.Invite.Nom)
                                                   .ToList();
        }

        /// <summary>
        /// Ajoute un invité à la liste des invités
        /// </summary>
        /// <param name="invite"> Le invité à ajouter </param>
        /// <returns> Tâche asynchrone </returns>
        /// <exception cref="Exception"> Lance une exception si l'invité existe déjà </exception>
        public async Task AjouterInvite(VMInvite invite)
        {
            if (InviteExiste(invite))
            {
                throw new Exception("Un invité avec le même nom et prénom existe déjà");
            }

            await this.inviteDAO.AjouterInvite(invite.Invite);
            this.listeVMInvite.Add(invite);
            this.Notify("VMInvites");
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
        /// <param name="invite"> L'invité à modifier </param>
        public async Task ModifierInvite(VMInvite invite)
        {
            if (invite != null)
            {
                await this.inviteDAO.ModifierInvite(invite.Invite);
                this.Notify("VMInvites");
            }
        }

        public async Task ChargerAllergenesDansInvite(VMInvite invite)
        {
            try
            {
                var tousLesAllergenes = Enum.GetValues(typeof(NomAllergene)).Cast<NomAllergene>();
                HashSet<NomAllergene> allergenesSelectionnes = new HashSet<NomAllergene>();

                if (invite.Invite.Allergenes != null)
                {
                   foreach (NomAllergene allergene in invite.Invite.Allergenes)
                    {
                        allergenesSelectionnes.Add(allergene);
                    }
                }
                invite.AllergenesListe.Clear();
                foreach (NomAllergene allergene in tousLesAllergenes)
                {
                    bool estSelectionne = allergenesSelectionnes.Contains(allergene);
                    VMAllergeneSelectionne vmAllergeneSelectionne = new VMAllergeneSelectionne(allergene, estSelectionne);
                    invite.GestionnaireEvenement(vmAllergeneSelectionne);
                    invite.AllergenesListe.Add(vmAllergeneSelectionne);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors du chargement des allergènes pour l'invité : " + ex.Message);
            }
        }   

        /// <summary>
        /// Vérifie si un invité avec le même nom et prénom existe déjà
        /// </summary>
        /// <param name="invite">L'invité à vérifier</param>
        /// <returns>True si un doublon existe, False sinon</returns>
        public bool InviteExiste(VMInvite invite)
        {
            return this.listeVMInvite.Any(vm => vm.Invite.Nom.Equals(invite.Invite.Nom, StringComparison.OrdinalIgnoreCase) &&
                                                vm.Invite.Prenom.Equals(invite.Invite.Prenom, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        // Charge la liste des invités correspondant au paramètre de recherche depuis la base de données
        /// </summary>
        public async Task ChercherInvite(string recherchertexte)
        {
            this.listeVMInvite.Clear();

            List<Invite> invites = await this.inviteDAO.ChercherInvite(recherchertexte);
            foreach (Invite invite in invites)
            {
                VMInvite vmInvite = new VMInvite(invite);
                this.listeVMInvite.Add(vmInvite);
            }
            this.listeVMInvite = this.listeVMInvite.OrderBy(vm => vm.Invite.Prenom)
                                                   .ThenBy(vm => vm.Invite.Nom)
                                                   .ToList();
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
