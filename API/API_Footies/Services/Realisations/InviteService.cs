using API_Footies.Data.Interfaces;
using API_Footies.Metier;
using API_Footies.Services.Interfaces;

namespace API_Footies.Services.Realisations
{
    /// <summary>
    /// Fournit des services pour gérer les invitations
    /// </summary>
    public class InviteService : IInviteService
    {
        #region attributs

        private IInviteDAO dao;

        #endregion

        #region Constructeur

        /// <summary>
        /// Initialise une nouvelle instance de la classe InviteService.
        /// </summary>
        /// <param name="dao">Injection de dépendance</param>
        /// <param name="typeService">Service utilisé pour gérer les opérations liées aux types associées aux invitations.</param>
        public InviteService(IInviteDAO dao)
        {
            this.dao = dao;
        }
        #endregion

        #region methodes
        public void AjouterInvite(Invite invite, long IdUtilisateur)
        {
            this.dao.AjouterInvite(invite, IdUtilisateur);
        }

        public void ModifierInvite(Invite invite, long IdUtilisateur)
        {
            this.dao.ModifierInvite(invite, IdUtilisateur);
        }

        public List<Invite> ListInvite(long IdUtilisateur)
        {
            return this.dao.ListInvite(IdUtilisateur);
        }

        public void SupprimerInvite(long id, long IdUtilisateur)
        {
            this.dao.SupprimerInvite(id, IdUtilisateur);
        }
        public bool EstDansUnGroupe(long idInvite, long IdUtilisateur)
        {
            return this.dao.EstDansUnGroupe(idInvite, IdUtilisateur);
        }

        public List<Invite> ChercherInvite(string texterecherche, long IdUtilisateur)
        {
            return this.dao.ChercherInvite(texterecherche, IdUtilisateur);
        }
        #endregion
    }
}
