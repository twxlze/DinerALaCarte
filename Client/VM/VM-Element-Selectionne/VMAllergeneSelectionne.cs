using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using METIER_Footies.Enum;

namespace VM_Footies.VM_Element_Selectionne
{
    /// <summary>
    /// ViewModel pour gérer la sélection d'un allergène
    /// </summary>
    public class VMAllergeneSelectionne : INotifyPropertyChanged
    {
        #region Attributs
        private NomAllergene allergene;
        private bool estSelectionne;
        #endregion

        #region Evenement
        public event PropertyChangedEventHandler? PropertyChanged;
        #endregion

        #region Propriétés
        /// <summary>
        /// Allergène encapsulé
        /// </summary>
        public NomAllergene Allergene => allergene;

        /// <summary>
        /// Nom de l'allergène pour l'affichage
        /// </summary>
        public string Nom => allergene.ToString();

        /// <summary>
        /// État de sélection de l'allergène
        /// </summary>
        public bool EstSelectionne
        {
            get => estSelectionne;
            set
            {
                if (estSelectionne != value)
                {
                    estSelectionne = value;
                    Notify("AllergeneSelectionne");
                }
            }
        }
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur de VMAllergeneSelectionne
        /// </summary>
        /// <param name="allergene">L'allergène à encapsuler</param>
        /// <param name="estSelectionne">État initial de sélection</param>
        public VMAllergeneSelectionne(NomAllergene allergene, bool estSelectionne = false)
        {
            this.allergene = allergene;
            this.estSelectionne = estSelectionne;
        }
        #endregion

        #region Méthodes
        /// <summary>
        /// Notifie l'UI d'un changement de propriété
        /// </summary>
        /// <param name="propertyName">Nom de la propriété modifiée</param>
        private void Notify(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}