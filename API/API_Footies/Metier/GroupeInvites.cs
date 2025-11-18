namespace API_Footies.Metier
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

        /// <summary>
        /// Constructeur de la classe GroupeInvites
        /// </summary>
        /// <param name="idGroupeInvites"> id du groupe d'invités </param>
        /// <param name="nom"> nom du groupe d'invités </param>
        /// <param name="invites"> liste des invités du groupe </param>
        public GroupeInvites(long idGroupeInvites, string nom, List<Invite> invites)
        {
            this.idGroupeInvites = idGroupeInvites;
            this.nom = nom;
            this.invites = invites;
        }

        public GroupeInvites(long idGroupeInvites, string nom)
        {
            this.idGroupeInvites = idGroupeInvites;
            this.nom = nom;
            this.invites = new List<Invite>();
        }

        public GroupeInvites()
        {
            this.invites = new List<Invite>();
        }

    }
}
