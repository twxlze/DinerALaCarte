namespace API_Footies.Metier
{
    /// <summary>
    /// Classe représentant un invité 
    /// </summary>
    public class Invite
    {
        #region --- Attributs ---
        private long id;
        private string nom;
        private string prenom;
        private string telephone;
        private string email;
        private List<Enum.NomAllergene>? allergenes;
        private List<Plat> platsDetestes = new List<Plat>();
        #endregion

        #region --- Propriétés ---
        /// <summary>
        /// Retourne ou modifie l'id de l'invité
        /// </summary>
        public long Id
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// Retourne ou modifie le nom de l'invité
        /// </summary>
        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }

        /// <summary>
        /// Retourne ou modifie le prénom de l'invité
        /// </summary>
        public string Prenom
        {
            get { return prenom; }
            set { prenom = value; }
        }

        /// <summary>
        /// Retourne ou modifie le téléphone de l'invité
        /// </summary>
        public string Telephone
        {
            get { return telephone; }
            set { telephone = value; }
        }

        /// <summary>
        /// Retourne ou modifie l'email de l'invité
        /// </summary>
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        /// <summary>
        /// Retourne ou modifie la liste des allergènes du plat
        /// </summary>
        public List<Enum.NomAllergene>? Allergenes
        {
            get { return allergenes; }
            set { allergenes = value; }
        }

        public List<Plat> PlatsDetestes
        {
            get { return platsDetestes; }
            set { platsDetestes = value; }
        }
        #endregion

        /// <summary>
        /// Constructeur d'un invité
        /// </summary>
        /// <param name="nom">nom de l'invité</param>
        /// <param name="prenom">prénom de l'invité</param>
        /// <param name="telephone">numéro de téléphone de l'invité</param>
        /// <param name="email">email de l'invité</param>
        public Invite(long id, string nom, string prenom, string telephone, string email, List<Enum.NomAllergene>? allergenes, List<Plat>? platsDetestes )
        {
            this.id = id;
            this.nom = nom;
            this.prenom = prenom;
            this.telephone = telephone;
            this.email = email;
            this.allergenes = allergenes;
            this.platsDetestes = platsDetestes;
        }

        /// <summary>
        /// Constructeur par défaut d'un invité
        /// </summary>
        public Invite()
        {
        }

    }
}