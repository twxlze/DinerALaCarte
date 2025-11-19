namespace API_Footies.Services.Interfaces
{
    public interface IIngredientsService
    {
        Task<List<string>> RechercherIngredients(string recherche);
    }
}