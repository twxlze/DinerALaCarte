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
    public class VMPagePlat : INotifyPropertyChanged
    {
        #region Attributs
        private List<VMPlat> listeVMPlat;
        private VMPlat platSelectionne;
        private PlatDAO PlatDAO;
        #endregion

        #region Propriétés
        public VMPlat PlatSelectionne
        {
            get { return platSelectionne; }
            set { platSelectionne = value; }
        }
        #endregion

        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        // Liste des VMPlats 
        /// </summary>
        public List<VMPlat> VMPlat => listeVMPlat;

        #region Constructeurs
        /// <summary>
        // Constructeur par défaut d'une page d'un plat
        /// </summary>
        public VMPagePlat()
        {
            this.PlatDAO = new PlatDAO();
            this.listeVMPlat = new List<VMPlat>();

            /*
            foreach (Plat plat in PlatDAO.ObtenirTout().Result)
            {
                this.listeVMPlat.Add(new VMPlat(plat));
            }
            */
        }
        #endregion

        #region Méthodes
        /// <summary>
        /// Charge la liste des plats depuis la base de données
        /// </summary>
        /// <returns> Tâche asynchrone </returns>
        public async Task ChargerPlatsAsync()
        {
            this.listeVMPlat.Clear();

            List<Plat> plats = await this.PlatDAO.ObtenirTout();

            foreach (Plat plat in plats)
            {
                VMPlat vmPlat = new VMPlat(plat);
                this.listeVMPlat.Add(vmPlat);
            }
            this.Notify("VMPlat");
        }

        /// <summary>
        /// Charge la liste des plats depuis la base de données
        /// </summary>
        public async void ChargerPlats()
        {
            await ChargerPlatsAsync();
        }

        /// <summary>
        /// Ajoute un plat à la liste des plats
        /// </summary>
        /// <param name="vmplat"> Le plat à ajouter </param>
        public void AjouterPlat(VMPlat vmplat)
        {
            this.PlatDAO.AjouterPlat(vmplat.Plat);
            this.listeVMPlat.Add(vmplat);
            this.Notify("VMplat");
        }

        /// <summary>
        /// Modifie un plat dans la liste des plats
        /// </summary>
        /// <param name="plat"></param>
        public async void ModifierPlat(VMPlat plat)
        {
            if (plat != null)
            {
                await this.PlatDAO.ModifierPlat(plat.Plat);
                this.Notify("VMPlat");
            }
        }

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

        private void Notify(string message)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(message));
        }
        #endregion
    }
}
