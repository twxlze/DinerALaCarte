using API_Footies.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API_Footies.Controllers
{
    [ApiController]
    [Route("OpenFoodFacts")]
    public class IngredientsController : ControllerBase
    {
        #region Attributs
        private readonly IIngredientsService service;
        #endregion

        #region Constructeur
        public IngredientsController(IIngredientsService service)
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
