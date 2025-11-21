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
    public class VMPagePlat : INotifyPropertyChanged
    {
        #region Attributs
        private List<VMPlat> listeVMPlat;
        private VMPlat platSelectionne;
        private string texteRecherche;
        private IPlatDAO PlatDAO;
        #endregion

        #region Propriétés
        /// <summary>
        /// Plat sélectionné par l'utilisateur 
        /// </summary>
        public VMPlat PlatSelectionne
        {
            get { return platSelectionne; }
            set
            {
                this.platSelectionne = value;
                Notify("PlatSelectionne");
            }
        }

        /// <summary>
        /// Texte de recherche pour filtrer les plats
        // Liste des VMPlats 
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

        /// <summary>
        // Liste des VMPlats 
        /// </summary>
        public List<VMPlat> VMPlat => listeVMPlat;
        #endregion

        public event PropertyChangedEventHandler? PropertyChanged;

        #region Constructeurs
        /// <summary>
        // Constructeur par défaut d'une page d'un plat
        /// </summary>
        public VMPagePlat()
        {
            this.PlatDAO = new PlatDAO();
            this.listeVMPlat = new List<VMPlat>();
        }
        #endregion

        #region Méthodes
        /// <summary>
        /// Charge la liste des plats depuis la base de données
        /// </summary>
        /// <returns> Tâche asynchrone </returns>
        public async Task ChargerPlats()
        {
            this.listeVMPlat.Clear();

            List<Plat> plats = await this.PlatDAO.ObtenirTout();

            foreach (Plat plat in plats)
            {
                VMPlat vmPlat = new VMPlat(plat);
                this.listeVMPlat.Add(vmPlat);
            }
            this.listeVMPlat = this.listeVMPlat.OrderBy(vm => vm.Plat.Nom).ToList();
        }

        /// <summary>
        /// Ajoute un plat à la liste des plats
        /// </summary>
        /// <param name="vmplat"> Le plat à ajouter </param>
        /// <returns> Tâche asynchrone </returns>
        /// <exception cref="Exception"> Lance une exception si le plat existe déjà </exception>
        public async Task AjouterPlat(VMPlat vmplat)
        {
            if (PlatExiste(vmplat))
            {
                throw new Exception("Un plat avec ce nom existe déjà.");
            }
            await this.PlatDAO.AjouterPlat(vmplat.Plat);
            this.listeVMPlat.Add(vmplat);
            this.Notify("VMPlat");
        }

        /// <summary>
        /// Modifie un plat dans la liste des plats
        /// </summary>
        /// <param name="plat"></param>
        public async Task ModifierPlat(VMPlat plat)
        {
            if (plat != null)
            {
                await this.PlatDAO.ModifierPlat(plat.Plat);
                this.Notify("VMPlat");
            }
        }

        /// <summary>
        /// Supprime le plat sélectionné de la liste des plats
        /// </summary>
        /// <returns> true si la suppression a réussi, false sinon </returns>
        public async Task<bool> SupprimerPlat()
        {
            bool suppressionReussie = false;

            if (this.platSelectionne != null)
            {
                long id = this.platSelectionne.Plat.Id;

                if (id != 0)
                {
                    bool estDansUnGroupe = await this.PlatDAO.EstDansUnMenu(id);
                    if (!estDansUnGroupe)
                    {
                        await this.PlatDAO.SupprimerPlat(id);
                        this.listeVMPlat.Remove(this.platSelectionne);
                        this.platSelectionne = null;
                        suppressionReussie = true;
                    }
                }
                else
                {
                    this.listeVMPlat.Remove(this.platSelectionne);
                    this.platSelectionne = null;
                    suppressionReussie = true;
                }
            }

            return suppressionReussie;
        }

        /// <summary>
        /// Vérifie si un plat existe déjà dans la liste des plats
        /// </summary>
        /// <param name="plat"> Le plat à vérifier </param>
        /// <returns> True si le plat existe, False sinon </returns>
        public bool PlatExiste(VMPlat plat)
        {
            return this.listeVMPlat.Any(p => p.Plat.Nom.Equals(plat.Plat.Nom, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        // Charge la liste des plats correspondant au paramètre de recherche depuis la base de données
        /// </summary>
        public async Task ChercherPlat(string recherchertexte)
        {
            this.listeVMPlat.Clear();

            List<Plat> plats = await this.PlatDAO.ChercherPlat(recherchertexte);
            foreach (Plat plat in plats)
            {
                VMPlat vmPlat = new VMPlat(plat);
                this.listeVMPlat.Add(vmPlat);
            }
            this.listeVMPlat = this.listeVMPlat.OrderBy(vm => vm.Plat.Nom)
                                                   .ToList();
        }

        /// <summary>
        /// Notifie le changement d'une propriété
        /// </summary>
        /// <param name="message"></param>
        private void Notify(string message)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(message));
        }

        /// <summary>
        /// Charge tous les allergènes disponibles pour un plat
        /// </summary>
        /// <param name="plat">Le VMPlat pour lequel charger les allergènes</param>
        public void ChargerAllergenesDansPlat(VMPlat plat)
        {
            try
            {
                // Récupère tous les allergènes de l'énumération
                var tousLesAllergenes = System.Enum.GetValues(typeof(NomAllergene)).Cast<NomAllergene>();

                // Récupère les allergènes déjà sélectionnés pour ce plat
                HashSet<NomAllergene> allergenesSelectionnes = new HashSet<NomAllergene>();
                if (plat.Plat.Allergenes != null)
                {
                    foreach (NomAllergene allergene in plat.Plat.Allergenes)
                    {
                        allergenesSelectionnes.Add(allergene);
                    }
                }

                // Crée la liste des VMAllergeneSelectionne
                plat.AllergenesListe.Clear();
                foreach (NomAllergene allergene in tousLesAllergenes)
                {
                    bool estSelectionne = allergenesSelectionnes.Contains(allergene);
                    VMAllergeneSelectionne vmAllergeneSelectionne = new VMAllergeneSelectionne(allergene, estSelectionne);
                    plat.GestionnaireEvenement(vmAllergeneSelectionne);
                    plat.AllergenesListe.Add(vmAllergeneSelectionne);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors du chargement des allergènes pour le plat : " + ex.Message);
            }
        }
        #endregion
    }
}
