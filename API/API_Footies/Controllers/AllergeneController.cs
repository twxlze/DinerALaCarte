using API_Footies.Metier;
using API_Footies.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API_Footies.Controllers
{
    /// <summary>
    /// Controlleur en charge de tout ce qui touche aux allergenes
    /// </summary>
    [ApiController]
    [Route("Allergene")]
    public class AllergeneController : ControllerBase
    {
        #region Attributs
        private IAllergeneService service;
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur du controlleur d'allergene
        /// </summary>
        public AllergeneController(IAllergeneService service)
        {
            this.service = service;
        }
        #endregion

        #region Méthodes
        /// <summary>
        /// liste des allergenes
        /// </summary>
        /// <returns> la liste des allergenes </returns>
        [HttpPost("ListeAllergene")]
        public List<Allergene> ListeAllergene()
        {
            return this.service.ListAllergene();
        }
        #endregion
    }
}
