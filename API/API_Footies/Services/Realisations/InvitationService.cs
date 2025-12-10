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
        public void AjouterInvitation(Invitation invitation, long IdUtilisateur)
        {
            this.invitationDAO.AjouterInvitation(invitation, IdUtilisateur);
        }

        public void ModifierInvitation(Invitation invitation, long IdUtilisateur)
        {
            this.invitationDAO.ModifierInvitation(invitation, IdUtilisateur);
        }

        public List<Invitation> ObtenirToutInvitations(long IdUtilisateur)
        {
            return this.invitationDAO.ObtenirToutInvitations(IdUtilisateur);
        }

        public void SupprimerInvitation(long idInvitation, long IdUtilisateur)
        {
            this.invitationDAO.SupprimerInvitation(idInvitation, IdUtilisateur);
        }

        public List<Invitation> ChercherInvitations(string InvitationsRechercher)
        {
            return this.invitationDAO.ChercherInvitations(InvitationsRechercher);
        }
        #endregion
    }
}
