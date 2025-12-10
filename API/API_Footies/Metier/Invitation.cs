namespace API_Footies.Metier
{
    /// <summary>
    /// Représente une invitation
    /// </summary>
    public class Invitation
    {
        #region --- Attributs ---
        private List<GroupeInvites> groupesInvites = new List<GroupeInvites>();
        private List<Menu> menus = new List<Menu>();
        private List<Invite> invites = new List<Invite>();
        private List<Plat> plats = new List<Plat>();
        private long idInvitation;
        private string nom;
        private DateTime date;
        private string? remarques;
        #endregion

        #region --- Propriétés ---
        /// <summary>
        /// Retourne ou modifie la liste des groupes d'invités
        /// </summary>
        public List<GroupeInvites> GroupeInvites
        {
            get { return groupesInvites; }
            set { groupesInvites = value; }
        }

        /// <summary>
        /// Retourne ou modifie la liste des menus
        /// </summary>
        public List<Menu> Menus
        {
            get { return menus; }
            set { menus = value; }
        }

        /// <summary>
        /// Retourne ou modifie la liste des invités
        /// </summary>
        public List<Invite> Invites
        {
            get { return invites; }
            set { invites = value; }
        }

        /// <summary>
        /// Retourne ou modifie la liste des plats
        /// </summary>
        public List<Plat> Plats
        {
            get { return plats; }
            set { plats = value; }
        }

        /// <summary>
        /// Retourne ou modifie l'id de l'invitation
        /// </summary>
        public long IdInvitation
        {
            get { return idInvitation; }
            set { idInvitation = value; }
        }

        /// <summary>
        /// Retourne ou modifie la date de l'invitation
        /// </summary>
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        /// <summary>
        /// Retourne ou modifie le nom de l'invitation
        /// </summary>
        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }

        /// <summary>
        /// Retourne ou modifie les remarques de l'invitation
        /// </summary>
        public string? Remarques
        {
            get { return remarques; }
            set { remarques = value; }
        }
        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur de l'invitation
        /// </summary>
        public Invitation(List<GroupeInvites> groupesInvites, List<Menu> menus, List<Invite> invites, List<Plat> plats, long idInvitation, string nom, DateTime date, string? remarques = null)
        {
            this.groupesInvites = groupesInvites;
            this.menus = menus;
            this.invites = invites;
            this.plats = plats;
            this.idInvitation = idInvitation;
            this.nom = nom;
            this.date = date;
            this.remarques = remarques;
        }

        /// <summary>
        /// Constructeur par défaut de l'invitation
        /// </summary>
        public Invitation()
        {
            this.groupesInvites = new List<GroupeInvites>();
            this.menus = new List<Menu>();
            this.invites = new List<Invite>();
            this.plats = new List<Plat>();
            this.remarques = "";
        }
        #endregion
    }
}
