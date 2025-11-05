using API_Footies.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API_Footies.Controllers
{
    /// <summary>
    /// Controlleur en charge de tout ce qui touche les articles (boissons, burgers...)
    /// </summary>

    [ApiController]
    [Route("Invites")]
    public class InviteController : ControllerBase
    {
        //Service en charge des articles
        private IInviteService service;

        /// <summary>
        /// Constructeur
        /// </summary>
        public InviteController(IInviteService service)
        {
            this.service = service;
        }


        /// <summary>
        /// Ajoute un invité
        /// </summary>
        /// <param name="article">invité à ajouter</param>
        /// <returns>L'invité avec Id modifié</returns>
        [HttpPost("AjoutInvite")]
        public Metier.Invite AjouterInvite(Metier.Invite invite)
        {
            this.service.AjouterInvite(invite);
            return invite;
        }

    }
}
