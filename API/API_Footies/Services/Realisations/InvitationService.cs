using API_Footies.Data.Interfaces;
using API_Footies.Metier;
using API_Footies.Services.Interfaces;

namespace API_Footies.Services.Realisations
{
    /// <summary>
    /// Fournit des services pour gérer les invitations
    /// </summary>
    public class InvitationService : IInvitationService
    {
        #region Attributs
        private IInvitationDAO invitationDAO;
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur du service d'invitation
        /// </summary>
        /// <param name="dao"> Injection de dépendance </param>
        public InvitationService(IInvitationDAO dao)
        {
            this.invitationDAO = dao;
        }
        #endregion

        #region Méthodes
        public void AjouterInvitation(Invitation invitation)
        {
            this.invitationDAO.AjouterInvitation(invitation);
        }

        public void ModifierInvitation(Invitation invitation)
        {
            this.invitationDAO.ModifierInvitation(invitation);
        }

        public List<Invitation> ObtenirToutInvitations()
        {
            return this.invitationDAO.ObtenirToutInvitations();
        }

        public void SupprimerInvitation(long idInvitation)
        {
            this.invitationDAO.SupprimerInvitation(idInvitation);
        }
        #endregion
    }
}
