using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace METIER_Footies.Metier
{
    /// <summary>
    /// Représentation d'un invité avec son identité et coordonées
    /// </summary>
    public class Invite
    {
        #region Attributs
        private string nom;
        private string prenom;
        private string? telephone; // string pour simplifier avec l'API
        private string? email;
        #endregion

        #region Propriétés
        /// <summary>
        /// Nom de famille de l'invité
        /// </summary>
        public string Nom {  get => nom; set => nom = value; }

        /// <summary>
        // Prénom de l'invité
        /// </summary>
        public string Prenom {  get => prenom; set => prenom = value; }

        /// <summary>
        // Téléphone de l'invité
        /// </summary>
        public string Telephone { get => telephone; set => telephone = value; }

        /// <summary>
        // Email de l'invité
        /// </summary>
        public string Email { get => email; set => email = value; }
        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur d'un invité
        /// </summary>
        /// <param name="nom"> Nom de famille de l'invité </param>
        /// <param name="prenom"> Prénom de l'invité </param>
        /// <param name="telephone"> Téléphone de l'invité </param>
        /// <param name="email"> Email de l'invité </param>
        public Invite(string nom, string prenom, string? telephone, string? email)
        {
            this.nom = nom;
            this.prenom = prenom;
            this.telephone = telephone;
            this.email = email;
        }
        #endregion
    }
}
