using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using METIER_Footies.Data.Interfaces;

namespace VM_Footies.VM_Page
{
    public class VMPageConnexion : INotifyPropertyChanged
    {
        #region attributs
        private IConnexionDAO connexionDAO;
        #endregion
        #region propriétés

        #endregion
        public event PropertyChangedEventHandler? PropertyChanged;
        #region constructeurs
        public VMPageConnexion()
        {
        }
        #endregion
        #region méthodes
        /// <summary>
        /// Supprime le plat sélectionné de la liste des plats
        /// </summary>
        /// <returns> true si la suppression a réussi, false sinon </returns>
        public async Task<bool> Connexion()
        {
            bool connexionReussite = false;
            await this.connexionDAO.Connexion();
            return suppressionReussie;
        }

        /// <summary>
        /// Notifie le changement d'une propriété
        /// </summary>
        /// <param name="message"></param>
        private void Notify(string message)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(message));
        }

        #endregion
    }
}
