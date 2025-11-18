using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using METIER_Footies.Metier;

namespace VM_Footies.VM_Element_Selectionne
{
    /// <summary>
    /// ViewModel pour un plat sélectionné
    /// </summary>
    public class VMPlatSelectionne : INotifyPropertyChanged
    {
        #region Attributs 
        private Plat plat;
        private bool estSelectionne;
        #endregion

        #region Evénement
        public event PropertyChangedEventHandler? PropertyChanged;
        #endregion

        #region Propriétés
        /// <summary>
        /// Plat encapsulé
        /// </summary>
        public Plat Plat => plat;

        /// <summary>
        /// Nom du plat
        /// </summary>
        public string Nom => plat.Nom;

        /// <summary>
        /// État de sélection du plat
        /// </summary>
        public bool EstSelectionne
        {
            get => estSelectionne;
            set
            {
                if (estSelectionne != value)
                {
                    estSelectionne = value;
                    Notify("EstSelectionne");
                }
            }
        }
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur de VMPlatSelectionne
        /// </summary>
        /// <param name="plat">Le plat à encapsuler</param>
        /// <param name="estSelectionne">État initial de sélection</param>
        public VMPlatSelectionne(Plat plat, bool estSelectionne = false)
        {
            this.plat = plat;
            this.estSelectionne = estSelectionne;
        }
        #endregion

        #region Méthodes
        /// <summary>
        /// Notifie l'UI d'un changement de propriété
        /// </summary>
        /// <param name="message">Nom de la propriété modifiée</param>
        private void Notify(string message)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(message));
        }
        #endregion
    }
}
