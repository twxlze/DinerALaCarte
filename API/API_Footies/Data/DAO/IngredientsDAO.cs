using API_Footies.Data.Interfaces;
using Microsoft.Data.Sqlite;

namespace API_Footies.Data.DAO
{
    /// <summary>
    /// Classe en charge de tout ce qui touche les ingrédients dans la base de données OpenFoodFacts
    /// </summary>
    public class IngredientsDAO : IIngredientsDAO
    {
        #region Attributs
        private string connection;
        #endregion

        #region Constructeur
        public IngredientsDAO(IConfiguration configuration)
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

                    using (var pragmaCmd = new SqliteCommand("PRAGMA case_sensitive_like = OFF;", connection))
                    {
                        await pragmaCmd.ExecuteNonQueryAsync();
                    }

                    string query = @"
                    SELECT DISTINCT product_name, (LENGTH(product_name) - LENGTH(REPLACE(product_name, ' ', '')) + 1) as word_count
                    FROM produits 
                    WHERE product_name IS NOT NULL 
                    AND product_name != ''
                    AND product_name LIKE @recherche
                    AND product_name NOT GLOB '*[0-9]*'
                    AND word_count <= 3
                    ORDER BY word_count, LENGTH(product_name)
                    LIMIT 10";

                    using (var command = new SqliteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@recherche", $"%{recherche}%");

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                string productName = reader["product_name"]?.ToString();
                                if (!string.IsNullOrWhiteSpace(productName))
                                {
                                    resultat.Add(productName);
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
