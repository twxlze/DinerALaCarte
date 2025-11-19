using API_Footies.Services.Interfaces;
using Microsoft.Data.Sqlite;

namespace API_Footies.Services.Realisations
{
    public class OpenFoodFactsService : IOpenFoodFactsService
    {
        #region Attributs
        private string connection;
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur du service OpenFoodFacts
        /// </summary>
        /// <param name="configuration"> Injection de dépendance pour la configuration </param>
        public OpenFoodFactsService(IConfiguration configuration)
        {
            connection = "Data Source=openfoodfacts.db";
        }
        #endregion

        #region Méthodes
        public async Task<List<string>> RechercherIngredients(string recherche)
        {
            List<string> resultat = new List<string>();
            try
            {
                using (var connection = new SqliteConnection(this.connection))
                {
                    await connection.OpenAsync();

                    string query = @"
                        SELECT DISTINCT ingredients_text 
                        FROM produits 
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
                                    resultat.Add(ingredientsText);
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
            return resultat;
        }
        #endregion
    }
}
