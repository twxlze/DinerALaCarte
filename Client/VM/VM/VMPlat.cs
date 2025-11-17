using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using METIER_Footies.Metier;
using static METIER_Footies.Metier.Plat;

namespace VM_Footies.VM
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
        public Plat Plat => plat;


        #region Propriétés
        /// <summary>
        /// Id du plat
        /// </summary>
        public long Id
        {
            get => plat.Id;
        }

        /// <summary>
        // Nom du plat
        /// </summary>
        /// <remarks> Le set notifie le changement de la propriété </remarks>
        public string Nom
        {
            get => plat.Nom;
            set
            {
                plat.Nom = value;
                Notify("Nom");
                Notify("Identite");
            }
        }

        /// <summary>
        /// Description d'un plat
        /// </summary>
        /// <remarks> Le set notifie le changement de la propriété </remarks>
        public string Description
        {
            get => plat.Description;
            set
            {
                plat.Description = value;
                Notify("Description");
            }
        }


        /// <summary>
        /// catégorie du plat
        /// </summary>
        /// <remarks> Le set notifie le changement de la propriété </remarks>
        public CategoriePlat Categorie
        {
            get => plat.Categorie;
            set
            {
                plat.Categorie = value;
                Notify("Categorie");
                Notify("Identite");
            }
        }


        /// <summary>
        /// Information du plat (Nom + catégorie)
        /// </summary>
        public string Identite { get => $"{Nom} catégorie : {Categorie}"; }
        #endregion

        #region Constructeurs
        /// <summary>
        // Constructeur d'un VMPlat à partir d'un plat
        /// </summary>
        /// <param name="plat">le plat</param>
        public VMPlat(Plat plat)
        {
            this.plat = plat;
        }

        /// <summary>
        /// Constructeur d'un VMPlat à partir d'un autre VMPlat
        /// </summary>
        /// <param name="modele"> Le VMPlat à copier </param>
        public VMPlat(VMPlat modele)
        {
            plat = new Plat(modele.plat);
        }

        /// <summary>
        /// Constructeur par défaut d'un VMPlat
        /// </summary>
        public VMPlat()
        {
            plat = new Plat();
        }
        #endregion

        #region Méthodes
        /// <summary>
        // Notifie le changement d'une propriété
        /// </summary>
        /// <param name="message"> Nom de la propriété changée </param>
        private void Notify(string message)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(message));
        }

        /// <summary>
        /// Modifie les informations du plat
        /// </summary>
        /// <param name="plat"> le plat avec les nouvelles informations </param>
        public void ModifierPlat(VMPlat plat)
        {
            Nom = plat.Nom;
            Description = plat.Description;
            Categorie = plat.Categorie;
        }
        #endregion
    }
}