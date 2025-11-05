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

        #endregion

        /// <summary>
        /// Initialise une nouvelle instance de la classe InviteService.
        /// </summary>
        /// <param name="dao">Injection de dépendance</param>
        /// <param name="typeService">Service utilisé pour gérer les opérations liées aux types associées aux invitations.</param>
        public InviteService(IInviteDAO dao)
        {
            this.dao = dao;
        }

        #region methods
        /// <summary>
        /// Ajoute un invité
        /// </summary>
        /// <param name="invite">l'inviter à ajouté</param>
        public void AjouterInvite(Invite invite)
        {
            this.dao.AjouterInvite(invite);
        }
        /// <summary>
        /// Modifie un invité
        /// </summary>
        /// <param name="invite">l'invité</param>
        public void ModifierInvite(Invite invite)
        {
            this.dao.ModifierInvite(invite);
        }
        #endregion

    }
}
