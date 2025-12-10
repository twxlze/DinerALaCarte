using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM_Footies.VM;

namespace VM_Footies.VM_Element_Selectionne
{
    /// <summary>
    /// Modèle de vue pour les statistiques d'invite selectionner
    /// </summary>
    public class VMinviteStats : INotifyPropertyChanged
    {
        #region Attributs
        private VMInvite _invite;
        private bool _estSelectionne;
        #endregion

        #region Propriétés
        public string Identite => _invite.Identite;

        /// <summary>
        /// Invite associée
        /// </summary>
        public VMInvite Invite
        {
            get { return _invite; }
            set
            {
                _invite = value;
                Notify("Invite");
            }
        }
        /// <summary>
        /// Indique si l'invité est sélectionné
        /// </summary>
        public bool EstSelectionne
        {
            get { return _estSelectionne; }
            set
            {
                _estSelectionne = value;
                Notify("EstSelectionne");
            }
        }

        /// <summary>
        /// Événement déclenché lorsqu'une propriété change
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur de VMinviteStats
        /// </summary>
        /// <param name="invite">prend un vminvite</param>
        public VMinviteStats(VMInvite invite)
        {
            _invite = invite;
            _estSelectionne = false;
        }
        #endregion

        #region Méthodes protégées
        protected void Notify(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
