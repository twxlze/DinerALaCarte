using API_Footies.Data.Interfaces;
using API_Footies.Services.Interfaces;
using Microsoft.Data.Sqlite;

namespace API_Footies.Services.Realisations
{
    public class IngredientsService : IIngredientsService
    {
        #region Attributs
        private IIngredientsDAO ingredientsDAO;
        #endregion

        #region Constructeur
        public IngredientsService(IIngredientsDAO ingredientsDAO)
        {
            this.ingredientsDAO = ingredientsDAO;
        }

        #endregion

        #region Méthodes
        public Task<List<string>> RechercherIngredients(string recherche)
        {
            return ingredientsDAO.RechercherIngredients(recherche);
        }
        #endregion
    }
}
