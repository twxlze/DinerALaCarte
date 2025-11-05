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
        #region attributes

        private IInviteDAO dao;

        private ITypeService typeService;

        #endregion

        /// <summary>
        /// Initialise une nouvelle instance de la classe InviteService.
        /// </summary>
        /// <param name="dao">Injection de dépendance</param>
        /// <param name="typeService">Service utilisé pour gérer les opérations liées aux types associées aux invitations.</param>
        public InviteService(IInviteDAO dao, ITypeService typeService)
        {
            this.dao = dao;
            this.typeService = typeService;
        }

        #region methods

        public void AjouterInvite(Invite invite)
        {
            this.dao.AjouterInvite(invite);
        }

        #endregion

    }
}
