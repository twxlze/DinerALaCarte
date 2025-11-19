namespace API_Footies.Services.Interfaces
{
    /// <summary>
    /// Fournit des services pour interagir avec l'API OpenFoodFacts
    /// </summary>
    public interface IOpenFoodFactsService
    {
        /// <summary>
        /// Recherche des ingrédients correspondant à la chaîne de recherche donnée
        /// </summary>
        /// <param name="recherche"> Chaîne de recherche pour les ingrédients </param>
        /// <returns> Liste des ingrédients correspondants </returns>
        Task<List<string>> RechercherIngredients(string recherche);
    }
}