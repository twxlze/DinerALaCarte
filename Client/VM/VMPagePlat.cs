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
        private PlatDAO PlatDAO;
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

            foreach (Plat plat in PlatDAO.ObtenirTout().Result)
            {
                this.listeVMPlat.Add(new VMPlat(plat));
            }
        }
        #endregion

        #region Méthodes
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

        private void Notify(string message)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(message));
        }
        #endregion
    }
}
