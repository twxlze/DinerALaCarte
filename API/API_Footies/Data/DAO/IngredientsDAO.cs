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
                if (!string.IsNullOrWhiteSpace(recherche))
                {
                    using (var connection = new SqliteConnection(this.connection))
                    {
                        await connection.OpenAsync();

                        using (var pragmaCmd = new SqliteCommand("PRAGMA case_sensitive_like = OFF;", connection))
                        {
                            await pragmaCmd.ExecuteNonQueryAsync();
                        }

                        // Je sais c'est compliqué mais c'est pour être sûr que ça correspond au meilleur nom d'ingrédient que l'utilisateur recherche
                        string query = @"
                        SELECT product_name,
                               CASE 
                                   WHEN product_name LIKE @rechercheExacte THEN 1 
                                   WHEN product_name LIKE @rechercheDebut THEN 2
                                   WHEN product_name LIKE @rechercheMot THEN 3
                                   ELSE 4
                               END as pertinence,
                               (LENGTH(product_name) - LENGTH(REPLACE(product_name, ' ', '')) + 1) as word_count
                        FROM produits 
                        WHERE product_name IS NOT NULL 
                        AND product_name != ''
                        AND product_name LIKE @recherche
                        AND product_name NOT GLOB '*[0-9]*'
                        AND (LENGTH(product_name) - LENGTH(REPLACE(product_name, ' ', '')) + 1) <= 3
                        ORDER BY pertinence, word_count, LENGTH(product_name)
                        LIMIT 20";

                        using (var command = new SqliteCommand(query, connection))
                        {
                            string rechercheLower = recherche.ToLower();
                            command.Parameters.AddWithValue("@recherche", $"%{rechercheLower}%");
                            command.Parameters.AddWithValue("@rechercheExacte", rechercheLower);
                            command.Parameters.AddWithValue("@rechercheDebut", $"{rechercheLower}%");
                            command.Parameters.AddWithValue("@rechercheMot", $"% {rechercheLower}%");

                            HashSet<string> produitsUniques = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                            using (var reader = await command.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync() && resultat.Count < 10)
                                {
                                    string productName = reader.GetString(0);

                                    if (!string.IsNullOrWhiteSpace(productName) && produitsUniques.Add(productName))
                                    {
                                        resultat.Add(productName);
                                    }
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
