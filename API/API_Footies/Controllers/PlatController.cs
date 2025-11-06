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
        public Metier.Plat AjouterPlat(Metier.Plat plat)
        {
            this.service.AjouterPlat(plat);
            return plat;
        }

    }
}
