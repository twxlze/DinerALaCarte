using API_Footies.Services.Interfaces;
using Microsoft.Data.Sqlite;

namespace API_Footies.Services.Realisations
{
    public class OpenFoodFactsService : IOpenFoodFactsService
    {
        private readonly string _connectionString;

        public OpenFoodFactsService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("openfoodfacts") ?? "Data Source=openfoodfacts.db";
        }

        public async Task<List<string>> RechercherIngredients(string recherche)
        {
            List<string> suggestions = new List<string>();

            try
            {
                using (var connection = new SqliteConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string query = @"
                        SELECT DISTINCT ingredients_text 
                        FROM products 
                        WHERE ingredients_text IS NOT NULL 
                        AND ingredients_text != ''
                        AND LOWER(ingredients_text) LIKE LOWER(@recherche)
                        LIMIT 20";

                    using (var command = new SqliteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@recherche", $"%{recherche}%");

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                string ingredientsText = reader["ingredients_text"]?.ToString();
                                if (!string.IsNullOrWhiteSpace(ingredientsText))
                                {
                                    suggestions.Add(ingredientsText);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la recherche des ingrédients dans la base de données OpenFoodFacts.", ex);
            }


                return suggestions;
        }
    }
}
