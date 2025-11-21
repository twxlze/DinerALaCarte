using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace METIER_Footies.Metier
{
    /// <summary>
    /// Classe représentant une invitation
    /// </summary>
    public class Invitation
    {
        #region --- Attributs ---
        private long idInvitation;
        private string nom;
        private List<GroupeInvites> groupeInvites;
        private List<Menu> menu;
        private List<Invite> invites;
        private List<Plat> plats;
        private DateTime date;
        #endregion

        #region --- Propriétés ---
        /// <summary>
        /// Id de l'invitation
        /// </summary>
        public long IdInvitation
        {
            get => idInvitation;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("L'ID doit être un entier positif");
                }
                idInvitation = value;
            }
        }

        /// <summary>
        /// Nom de l'invitation
        /// </summary>
        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }

        /// <summary>
        /// Liste des menus de l'invitation
        /// </summary>
        public List<Menu> Menus
        {
            get { return menu; }
            set { menu = value; }
        }

        /// <summary>
        /// Liste des groupes d'invités de l'invitation
        /// </summary>
        public List<GroupeInvites> GroupeInvites
        {
            get { return groupeInvites; }
            set { groupeInvites = value; }
        }

        /// <summary>
        /// Date de l'invitation
        /// </summary>
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        /// <summary>
        /// Liste des invités de l'invitation
        /// </summary>
        public List<Invite> Invites
        {
            get { return invites; }
            set { invites = value; }
        }


        /// <summary>
        /// Liste des plats de l'invitation
        /// </summary>
        public List<Plat> Plats
        {
            get { return plats; }
            set { plats = value; }
        }
        

        #endregion

        #region --- Constructeurs ---
        /// <summary>
        /// Constructeur par copie d'une invitation
        /// </summary>
        /// <param name="invitation"> Invitation à copier </param>
        public Invitation(Invitation invitation)
        {
            this.idInvitation = invitation.idInvitation;
            this.nom = invitation.nom;
            this.groupeInvites = invitation.groupeInvites;
            this.menu = invitation.menu;
            this.invites = invitation.invites;
            this.plats = invitation.plats;
            this.date = invitation.date;
        }

        /// <summary>
        /// Constructeur par défaut d'une invitation
        /// </summary>
        public Invitation()
        {
            this.nom = "";
            this.date = DateTime.Now;
            this.groupeInvites = new List<GroupeInvites>();
            this.menu = new List<Menu>();
            this.invites = new List<Invite>();
            this.plats = new List<Plat>();
        }
        #endregion

    }
}
