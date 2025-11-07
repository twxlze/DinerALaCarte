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
    /// une liste d'invités qui represente un groupe d'invités
    /// </summary>
    public class GroupeInvites
    {
        #region --- Attributs ---
        private List<Invite> invites = new List<Invite>();
        private long idGroupeInvites;
        private string nom;
        #endregion

        #region --- Propriétés ---
        /// <summary>
        /// Retourne ou modifie la liste des invités du groupe
        /// </summary>
        public List<Invite> Invites
        {
            get { return invites; }
            set { invites = value; }
        }
        /// <summary>
        /// Retourne ou modifie l'id du groupe d'invités
        /// </summary>
        public long IdGroupeInvites
        {
            get { return idGroupeInvites; }
            set { idGroupeInvites = value; }
        }
        /// <summary>
        /// Retourne ou modifie le nom du groupe d'invités
        /// </summary>
        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }   
        #endregion

    }
}
