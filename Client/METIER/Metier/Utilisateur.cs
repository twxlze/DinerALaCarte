using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace METIER_Footies.Metier
{
    public class Utilisateur
    {
        #region Attributs
        private long idUtilisateur;
        private string pseudo;
        private string? nom;
        private string? prenom;
        private string? numTel;
        private string? mail;
        #endregion
        #region Propriétés
        /// <summary>
        /// Retourne ou modifie l'id de l'utilisateur
        /// </summary>
        public long IdUtilisateur
        {
            get { return idUtilisateur; }
            set { idUtilisateur = value; }
        }
        /// <summary>
        /// Retourne ou modifie le pseudo de l'utilisateur
        /// </summary>
        public string Pseudo
        {
            get { return pseudo; }
            set { pseudo = value; }
        }

        /// <summary>
        /// Retourne ou modifie le nom de l'utilisateur
        /// </summary>
        public string? Nom
        {
            get { return nom; }
            set { nom = value; }
        }

        /// <summary>
        /// Retourne ou modifie le prénom de l'utilisateur
        /// </summary>
        public string? Prenom
        {
            get { return prenom; }
            set { prenom = value; }
        }

        /// <summary>
        /// Retourne ou modifie le numéro de téléphone de l'utilisateur
        /// </summary>
        public string? NumTel
        {
            get { return numTel; }
            set { numTel = value; }
        }

        /// <summary>
        /// Retourne ou modifie l'email de l'utilisateur
        /// </summary>
        public string? Mail
        {
            get { return mail; }
            set { mail = value; }
        }
        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur d'un utilisateur
        /// </summary>
        /// <param name="idUtilisateur"> L'id de l'utilisateur </param>
        /// <param name="mail"> L'email de l'utilisateur </param>
        /// <param name="nom"> Le nom de l'utilisateur </param>
        /// <param name="numTel"> Le numéro de téléphone de l'utilisateur </param>
        /// <param name="prenom"> Le prénom de l'utilisateur </param>
        /// <param name="pseudo"> Le pseudo de l'utilisateur </param>
        public Utilisateur(long idUtilisateur, string pseudo,  string? nom, string? prenom, string? numTel, string? mail)
        {
            this.IdUtilisateur = idUtilisateur;
            this.Pseudo = pseudo;
            this.Nom = nom;
            this.Prenom = prenom;
            this.NumTel = numTel;
            this.Mail = mail;
        }
        #endregion
    }
}
