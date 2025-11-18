using API_Footies.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API_Footies.Controllers
{
    [ApiController]
    [Route("Invitation")]
    public class InvitationController : ControllerBase
    {
        #region Attributs
        private IInvitationService service;
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur
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
        public Metier.Invitation AjouterInvitation(Metier.Invitation invitation)
        {
            this.service.AjouterInvitation(invitation);
            return invitation;
        }

        /// <summary>
        /// Modifier une Invitation
        /// </summary>
        /// <param name="invitation"> l'invitation modifiée </param>
        [HttpPut("ModifierInvitation")]
        public void ModifierInvitation(Metier.Invitation invitation)
        {
            this.service.ModifierInvitation(invitation);
        }

        /// <summary>
        /// Supprimer une Invitation
        /// </summary>
        /// <param name="idInvitation"> id de l'invitation à supprimer </param>
        [HttpDelete("SupprimerInvitation")]
        public void SupprimerInvitation(long idInvitation)
        {
            this.service.SupprimerInvitation(idInvitation);
        }

        /// <summary>
        /// Obtenir toutes les Invitations
        /// </summary>
        /// <returns> La liste de toutes les invitations </returns>
        [HttpGet("ObtenirToutInvitations")]
        public List<Metier.Invitation> ObtenirToutInvitations()
        {
            return this.service.ObtenirToutInvitations();
        }
        #endregion
    }
}
