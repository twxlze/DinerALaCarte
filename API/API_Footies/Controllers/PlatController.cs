using API_Footies.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API_Footies.Controllers
{
    /// <summary>
    /// Controlleur en charge de tout ce qui touche au plats
    /// </summary>

    [ApiController]
    [Route("Plats")]
    public class PlatController : ControllerBase
    {
        //Service en charge des plats
        private IInviteService service;

        /// <summary>
        /// Constructeur
        /// </summary>
        public PlatController(IInviteService service)
        {
            this.service = service;
        }


        /// <summary>
        /// Ajoute un invité
        /// </summary>
        /// <param name="invite">invité à ajouter</param>
        /// <returns>L'invité avec Id modifié</returns>
        [HttpPost("AjoutInvite")]
        public Metier.Invite AjouterInvite(Metier.Invite invite)
        {
            this.service.AjouterInvite(invite);
            return invite;
        }

        /// <summary>
        /// Modifier un invité
        /// </summary>
        /// <param name="invite">l'invité</param>
        [HttpPut("ModifierInvite")]
        public void ModifierInvite(Metier.Invite invite)
        {
            this.service.ModifierInvite(invite);
        }

    }
}
