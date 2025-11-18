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
    /// ViewModel pour un groupe d'invités sélectionné
    /// </summary>
    public class VMGroupeInviteSelectionne : INotifyPropertyChanged
    {
        #region Attributs 
        private GroupeInvites groupeInvite;
        private bool estSelectionne;
        #endregion

        #region Evénement
        public event PropertyChangedEventHandler? PropertyChanged;
        #endregion

        #region Propriétés
        /// <summary>
        /// Groupe d'invités encapsulé
        /// </summary>
        public GroupeInvites GroupeInvite => groupeInvite;

        /// <summary>
        /// Nom du groupe d'invités
        /// </summary>
        public string Nom => groupeInvite.Nom;

        /// <summary>
        /// État de sélection du groupe d'invités
        /// </summary>
        public bool EstSelectionne
        {
            get => estSelectionne;
            set
            {
                if (estSelectionne != value)
                {
                    estSelectionne = value;
                    Notify("GroupeSelectionne");
                }
            }
        }
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur de VMGroupeInviteSelectionne
        /// </summary>
        /// <param name="groupeInvite">Le groupe d'invités à encapsuler</param>
        /// <param name="estSelectionne">État initial de sélection</param>
        public VMGroupeInviteSelectionne(GroupeInvites groupeInvite, bool estSelectionne = false)
        {
            this.groupeInvite = groupeInvite;
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