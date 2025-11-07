using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using METIER_Footies.Metier;
using static METIER_Footies.Metier.Plat;

namespace VM_Footies
{
    /// <summary>
    /// Classe ViewModel pour un plat
    /// </summary>
    public class VMPlat : INotifyPropertyChanged
    {
        #region Attributs
        private Plat plat;
        #endregion


        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Invite associée au VMPlat
        /// </summary>
        public Plat Plat => this.plat;


        #region Propriétés
        /// <summary>
        // Nom du plat
        /// </summary>
        /// <remarks> Le set notifie le changement de la propriété </remarks>
        public string Nom
        {
            get => this.plat.Nom;
            set
            {
                this.plat.Nom = value;
                this.Notify("Nom");
            }
        }

        /// <summary>
        /// Description du plat
        /// </summary>
        /// <remarks> Le set notifie le changement de la propriété </remarks>
        public string Description
        {
            get => this.plat.Description;
            set
            {
                this.plat.Description = value;
                this.Notify("Description");
            }
        }

        /// <summary>
        /// Catégorie du plat
        /// </summary>
        /// <remarks> Le set notifie le changement de la propriété </remarks>
        public CategoriePlat Categorie
        {
            get => this.plat.Categorie;
            set
            {
                this.plat.Categorie = value;
                this.Notify("Categorie");
            }
        }
        #endregion

        #region Constructeurs
        /// <summary>
        // Constructeur d'un VMInvite à partir d'un Invite
        /// </summary>
        /// <param name="invite"></param>
        public VMPlat(Plat plat)
        {
            this.plat = plat;
        }
        #endregion

        #region Méthodes

        /// <summary>
        /// Modifie les informations du plat
        /// </summary>
        /// <param name="plat"> Le plat avec les nouvelles informations </param>
        public void ModifierPlat(VMPlat plat)
        {
            this.Nom = plat.Nom;
            this.Description = plat.Description;
            this.Categorie = plat.Categorie;
        }

        /// <summary>
        // Notifie le changement d'une propriété
        /// </summary>
        /// <param name="message"> Nom de la propriété changée </param>
        private void Notify(string message)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(message));
        }
        #endregion
    }
}
