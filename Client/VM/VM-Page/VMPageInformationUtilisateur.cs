using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using METIER_Footies.Metier;

namespace VM_Footies.VM_Page
{
    public class VMPageInformationUtilisateur : INotifyPropertyChanged
    {
        #region Attributs
        private Utilisateur utilisateur;
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        #region
        /// <summary>
        /// Modifie ou retourne l'utilisateur connecté
        /// </summary>
        public Utilisateur Utilisateur
        {
            get => utilisateur;
            set
            {
                utilisateur = value;
                Notify("Utilisateur");
            }
        }
        #endregion
        #region Constructeur
        /// <summary>
        /// Constructeur de la VMPageInformationUtilisateur
        /// </summary>
        public VMPageInformationUtilisateur()
        {
            if (SessionService.Instance.EstConnecte)
            {
                this.Utilisateur = SessionService.Instance.UtilisateurConnecte;
            }
            else
            {
                this.Utilisateur = new Utilisateur(0, "Inconnu", "", "", "", "");
            }
        }
        #endregion
        #region Méthodes
        /// <summary>
        /// Vide la session utilisateur
        /// </summary>
        public void Deconnecter()
        {
            SessionService.Instance.UtilisateurConnecte = null;
        }

        private void Notify(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
