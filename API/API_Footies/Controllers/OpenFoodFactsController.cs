using API_Footies.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API_Footies.Controllers
{
    /// <summary>
    /// Controlleur en charge de l'intégration avec OpenFoodFacts
    /// </summary>
    [ApiController]
    [Route("OpenFoodFacts")]
    public class OpenFoodFactsController : ControllerBase
    {
        #region Attributs
        private IOpenFoodFactsService service;
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur du controlleur OpenFoodFacts
        /// </summary>
        /// <param name="service"> Injection de dépendance du service OpenFoodFacts </param>
        public OpenFoodFactsController(IOpenFoodFactsService service)
        {
            this.service = service;
        }
        #endregion

        #region Méthodes

        /// <summary>
        /// Recherche des suggestions d'ingrédients dans OpenFoodFacts
        /// </summary>
        /// <param name="recherche">Texte de recherche</param>
        /// <returns>Liste de suggestions d'ingrédients</returns>
        [HttpGet("RechercherIngredients")]
        public async Task<List<string>> RechercherIngredients(string recherche)
        {
            if (string.IsNullOrWhiteSpace(recherche))
            {
                return new List<string>();
            }
            return await service.RechercherIngredients(recherche);
        }
        #endregion
    }
}
