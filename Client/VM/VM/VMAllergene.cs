using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using METIER_Footies.Enum;
using METIER_Footies.Metier;

namespace VM_Footies.VM
{
    public class VMAllergene : INotifyPropertyChanged
    {
        #region Attributs
        private NomAllergene allergene;
        private bool estSelectionne;
        #endregion

        public event PropertyChangedEventHandler? PropertyChanged;

        #region Propriétés
        /// <summary>
        /// Le nom affiché dans la liste (ex: Gluten)
        /// </summary>
        public string Nom
        {
            get
            {
                return allergene.ToString();
            }
        }

        /// <summary>
        /// La valeur réelle de l'enum
        /// </summary>
        public NomAllergene Allergene
        {
            get
            {
                return allergene;
            }
        }

        /// <summary>
        /// État de la case à cocher (Binding IsChecked)
        /// </summary>
        public bool EstSelectionne
        {
            get
            {
                return estSelectionne;
            }
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
        /// Constructeur de VMAllergene pour un allergène donné
        /// </summary>
        /// <param name="allergene"> Le nom de l'allergène</param>
        /// <param name="estSelectionne"> L'état initial de sélection</param>
        public VMAllergene(NomAllergene allergene, bool estSelectionne)
        {
            this.allergene = allergene;
            this.estSelectionne = estSelectionne;
        }
        #endregion

        #region Méthodes
        private void Notify(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
