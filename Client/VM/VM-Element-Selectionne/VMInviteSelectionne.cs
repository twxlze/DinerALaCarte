using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using METIER_Footies.Metier;

namespace VM_Footies.VM_Element_Selectionne
{
    public class VMInviteSelectionne : INotifyPropertyChanged
    {
        #region Attributs
        private Invite invite;
        private bool estSelectionne;
        #endregion

        #region Evenement
        public event PropertyChangedEventHandler? PropertyChanged;
        #endregion

        #region Propriétés
        /// <summary>
        /// Invité encapsulé
        /// </summary>
        public Invite Invite => invite;


        /// <summary>
        /// Identité (nom + prénom) de l'invité
        /// </summary>
        public string Identite => invite.Identite;

        /// <summary>
        /// État de sélection de l'invité
        /// </summary>
        public bool EstSelectionne
        {
            get => estSelectionne;
            set
            {
                if (estSelectionne != value)
                {
                    estSelectionne = value;
                    Notify("InviteSelectionne");
                }
            }
        }
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur de VMInviteSelectionne
        /// </summary>
        /// <param name="invite"> L'invité à encapsuler </param>
        /// <param name="estSelectionne"> État initial de sélection </param>
        public VMInviteSelectionne(Invite invite, bool estSelectionne = false)
        {
            this.invite = invite;
            this.estSelectionne = estSelectionne;
        }
        #endregion

        #region Méthodes
        /// <summary>
        /// Notifie l'UI d'un changement de propriété
        /// </summary>
        /// <param name="propertyName"> Nom de la propriété modifiée </param>
        private void Notify(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
