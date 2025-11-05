using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace METIER_Footies
{
    public class Invite
    {
        #region Attributs
        private string nom;
        private string prenom;
        private string? telephone; // string pour simplifier avec l'API
        private string? email;
        #endregion

        #region Propriétés
        public string Nom {  get => nom; set => nom = value; }
        public string Prenom {  get => prenom; set => prenom = value; }
        public string Telephone { get => telephone; set => telephone = value; }
        public string Email { get => email; set => email = value; }
        #endregion

        #region Constructeurs
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
