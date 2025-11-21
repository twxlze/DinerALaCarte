using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using METIER_Footies.Metier;

namespace VM_Footies.VM
{
    public class VMAllergene : INotifyPropertyChanged
    {
        #region Attributs
        private Allergene allergene;
        #endregion

        #region Evenement
        public event PropertyChangedEventHandler? PropertyChanged;
        #endregion

        #region Propriétés
        /// <summary>
        /// Allergène encapsulé
        /// </summary>
        public Allergene Allergene => allergene;

        public long Id
        {
            get { return allergene.ID; }
        }

        public string Nom
        {
            get { return allergene.Nom; }
            set
            {
               allergene.Nom = value;
               Notify("Nom");
            }
        }
        #endregion

        #region Constructeur
        /// <summary>
        /// Notifie le changement de propriété
        /// </summary>
        /// <param name="allergene"> Nom de la propriété modifiée </param>
        public VMAllergene(Allergene allergene)
        {
            this.allergene = allergene;
        }

        public VMAllergene()
        {
            allergene = new Allergene();
        }
        #endregion

        #region Méthodes
        private void Notify(string message) 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(message));
        }
        #endregion
    }
}
