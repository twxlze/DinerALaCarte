namespace API_Footies.Services.Interfaces
{
    public interface IOpenFoodFactsService
    {
        Task<List<string>> RechercherIngredients(string recherche);
    }
}