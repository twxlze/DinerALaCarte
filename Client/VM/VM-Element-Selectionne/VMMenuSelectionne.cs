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
    /// ViewModel pour un menu sélectionné
    /// </summary>
    public class VMMenuSelectionne : INotifyPropertyChanged
    {
        #region Attributs 
        private Menu menu;
        private bool estSelectionne;
        #endregion

        #region Evénement
        public event PropertyChangedEventHandler? PropertyChanged;
        #endregion

        #region Propriétés
        /// <summary>
        /// Menu encapsulé
        /// </summary>
        public Menu Menu => menu;

        /// <summary>
        /// Nom du menu
        /// </summary>
        public string Nom => menu.Nom;

        /// <summary>
        /// État de sélection du menu
        /// </summary>
        public bool EstSelectionne
        {
            get => estSelectionne;
            set
            {
                if (estSelectionne != value)
                {
                    estSelectionne = value;
                    Notify("MenuSelectionne");
                }
            }
        }
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur de VMMenuSelectionne
        /// </summary>
        /// <param name="menu">Le menu à encapsuler</param>
        /// <param name="estSelectionne">État initial de sélection</param>
        public VMMenuSelectionne(Menu menu, bool estSelectionne = false)
        {
            this.menu = menu;
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
