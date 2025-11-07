using System.Security.Principal;
using API_Footies.Metier;
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
        private IPlatService service;

        /// <summary>
        /// Constructeur
        /// </summary>
        public PlatController(IPlatService service)
        {
            this.service = service;
        }


        /// <summary>
        /// Ajoute un plat
        /// </summary>
        /// <param name="plat">plat à ajouter</param>
        /// <returns>Le plat avec Id modifié</returns>
        [HttpPost("AjoutPlat")]
        [ProducesResponseType(type: typeof(Plat), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AjouterPlat(Metier.Plat plat)
        {
            IActionResult result;

            try
            {
                if (string.IsNullOrWhiteSpace(plat.Nom))
                {
                    result = BadRequest("Veuillez donner un nom");
                }
                else
                {
                    this.service.AjouterPlat(plat);
                    result = Created(" ", plat);
                }
            }
            catch (Exception ex)
            {
                result = BadRequest($"Erreur lors de l'ajout du plat : {ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// Modifier un plat
        /// </summary>
        /// <param name="plat">le plat</param>
        [HttpPut("ModifierPlat")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ModifierPlat(Metier.Plat plat)
        {  
            IActionResult result;

            try
            {
                if (string.IsNullOrWhiteSpace(plat.Nom))
                {
                    result = BadRequest("Veuillez indiquer le nom");
                }
                else
                {
                    this.service.ModifierPlat(plat);
                    result = Ok();
                }
            }
            catch (Exception ex)
            {
                result = BadRequest($"Erreur lors de la modification du plat : {ex.Message}");
            }

            return result;
        }

    }
}
