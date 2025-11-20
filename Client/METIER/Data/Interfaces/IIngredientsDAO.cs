namespace METIER_Footies.Data.Interfaces
{
    /// <summary>
    /// Interface d'accès aux données pour OpenFoodFacts
    /// </summary>
    public interface IIngredientsDAO
    {
        /// <summary>
        /// Recherche des suggestions d'ingrédients dans OpenFoodFacts
        /// </summary>
        /// <param name="recherche">Texte de recherche</param>
        /// <returns>Liste de suggestions d'ingrédients</returns>
        Task<List<string>> RechercherIngredients(string recherche);
    }
}