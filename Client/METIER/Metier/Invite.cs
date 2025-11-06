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
        private int id;
        private string nom;
        private string prenom;
        private string? telephone; // string pour simplifier avec l'API
        private string? email;
        #endregion

        #region Propriétés
        /// <summary>
        /// Id de l'invité
        /// </summary>
        public int Id 
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
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Le nom ne peut pas être vide");
                }
                nom = value;
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
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Le prénom ne peut pas être vide");
                }
                prenom = value;
            }
        }

        /// <summary>
        // Téléphone de l'invité
        /// </summary>
        public string Telephone 
        { 
            get => telephone;
            set
            {
                if (value != null)
                {
                    if (value.Length != 10)
                    {
                        throw new ArgumentException("Le numéro de téléphone doit avoir 10 chiffres");
                    }
                    if (!long.TryParse(value, out _)) // out _ = on jette la valeur convertie // on veut juste le true/false
                    {
                        throw new ArgumentException("Le numéro de téléphone doit contenir uniquement des chiffres");
                    }
                }
                telephone = value;
            }
        }

        /// <summary>
        // Email de l'invité
        /// </summary>
        public string Email
        {
            get => email; 
            set
            {
                if (value != null && !string.IsNullOrWhiteSpace(value))
                {
                    if (!Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$")) // Ne cherchez pas à comprendre (vive le Regex ^^)
                    {
                        throw new ArgumentException("L'adresse email n'est pas valide");
                    }
                }
                email = value?.Trim();
            }
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
        public Invite(int id, string nom, string prenom, string? telephone, string? email)
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
