namespace API_Footies.Data.Interfaces
{
    /// <summary>
    /// Interface pour les opérations de données liées aux ingrédients dans la base de données OpenFoodFacts
    /// </summary>
    public interface IIngredientsDAO
    {
        /// <summary>
        /// Recherche des ingrédients dans la base de données OpenFoodFacts
        /// </summary>
        /// <param name="recherche"> Terme de recherche pour les ingrédients </param>
        /// <returns> Liste des ingrédients correspondants au terme de recherche </returns>
        Task<List<string>> RechercherIngredients(string recherche);
    }
}