using API_Footies.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API_Footies.Controllers
{
    /// <summary>
    /// Controlleur en charge de tout ce qui touche aux invitations
    /// </summary>
    [ApiController]
    [Route("Invitations")]
    public class InvitationController : ControllerBase
    {
        #region Attributs
        private IInvitationService service;
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur du controlleur d'invitation
        /// </summary>
        public InvitationController(IInvitationService service)
        {
            this.service = service;
        }
        #endregion

        #region Méthodes
        /// <summary>
        /// Ajoute une Invitation
        /// </summary>
        /// <param name="invitation"> invitation à ajouter</param>
        /// <returns> L'invitation ajoutée </returns>
        [HttpPost("AjoutInvitation")]
        public Metier.Invitation AjouterInvitation(Metier.Invitation invitation, long IdUtilisateur)
        {
            this.service.AjouterInvitation(invitation, IdUtilisateur);
            return invitation;
        }

        /// <summary>
        /// Modifier une Invitation
        /// </summary>
        /// <param name="invitation"> l'invitation modifiée </param>
        [HttpPut("ModifierInvitation")]
        public void ModifierInvitation(Metier.Invitation invitation, long IdUtilisateur)
        {
            this.service.ModifierInvitation(invitation, IdUtilisateur);
        }

        /// <summary>
        /// Supprimer une Invitation
        /// </summary>
        /// <param name="idInvitation"> id de l'invitation à supprimer </param>
        [HttpDelete("SupprimerInvitation")]
        public void SupprimerInvitation(long idInvitation, long IdUtilisateur)
        {
            this.service.SupprimerInvitation(idInvitation, IdUtilisateur);
        }

        /// <summary>
        /// Obtenir toutes les Invitations
        /// </summary>
        /// <returns> La liste de toutes les invitations </returns>
        [HttpGet("ListeInvitations")]
        public List<Metier.Invitation> ObtenirToutInvitations(long IdUtilisateur)
        {
            return this.service.ObtenirToutInvitations(IdUtilisateur);
        }
        #endregion
    }
}
