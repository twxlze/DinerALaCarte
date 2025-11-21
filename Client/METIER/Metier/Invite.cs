using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace METIER_Footies.Metier
{
    /// <summary>
    /// Représentation d'un invité avec son identité et coordonées
    /// </summary>
    public class Invite
    {
        #region Attributs
        private long id;
        private string nom;
        private string prenom;
        private string? telephone; // string pour simplifier avec l'API
        private string? email;
        private List<Enum.NomAllergene>? allergenes;
        #endregion

        #region Propriétés
        /// <summary>
        /// Id de l'invité
        /// </summary>
        public long Id
        {
            get => id;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("L'ID doit être un entier positif");
                }
                id = value;
            }
        }

        /// <summary>
        /// Nom de famille de l'invité
        /// </summary>
        public string Nom
        {
            get => nom;
            set
            {
                nom = value.ToUpper();
            }
        }

        /// <summary>
        // Prénom de l'invité
        /// </summary>
        public string Prenom
        {
            get => prenom;
            set
            {
                prenom = value;
            }
        }

        /// <summary>
        // Téléphone de l'invité
        /// </summary>
        public string Telephone
        {
            get => telephone ?? "";
            set
            {
                telephone = value;
            }
        }

        /// <summary>
        /// Identité complète de l'invité
        /// </summary>
        public string Identite => $"{prenom} {nom}";

        /// <summary>
        // Email de l'invité
        /// </summary>
        public string Email
        {
            get => email ?? "";
            set
            {
                email = value?.Trim();
            }
        }

        /// <summary>
        /// Retourne ou modifie la liste des allergènes du plat
        /// </summary>
        public List<Enum.NomAllergene>? Allergenes
        {
            get { return allergenes; }
            set { allergenes = value; }
        }
        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur d'un invité
        /// </summary>
        /// <param name="nom"> Nom de famille de l'invité </param>
        /// <param name="prenom"> Prénom de l'invité </param>
        /// <param name="telephone"> Téléphone de l'invité </param>
        /// <param name="email"> Email de l'invité </param>
        public Invite(long id, string nom, string prenom, string? telephone, string? email)
        {
            this.id = id;
            this.nom = nom;
            this.prenom = prenom;
            this.telephone = telephone;
            this.email = email;
        }

        /// <summary>
        /// Constructeur de copie d'un invité
        /// </summary>
        /// <param name="invite"> L'invité à copier </param>
        public Invite(Invite invite)
        {
            this.nom = invite.nom;
            this.prenom = invite.prenom;
            this.telephone = invite.telephone;
            this.email = invite.email;
        }

        public Invite()
        {
            this.nom = "";
            this.prenom = "";
            this.telephone = null;
            this.email = null;
        }
        #endregion
    }
}
