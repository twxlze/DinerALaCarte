using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using METIER_Footies.Data.Interfaces;

namespace METIER_Footies.Data
{
    /// <summary>
    /// Classe d'accès aux données pour OpenFoodFacts avec la base de données
    /// </summary>
    public class IngredientsDAO : DAO, IIngredientsDAO
    {
        public async Task<List<string>> RechercherIngredients(string recherche)
        {
            List<string> suggestions = new List<string>();

            if (string.IsNullOrWhiteSpace(recherche))
            {
                return suggestions;
            }

            try
            {
                string rechercheEncodee = Uri.EscapeDataString(recherche);
                string url = $"OpenFoodFacts/RechercherIngredients?recherche={rechercheEncodee}";

                HttpResponseMessage reponseHttp = await GetAsync(url);

                if (reponseHttp.IsSuccessStatusCode)
                {
                    string reponse = await reponseHttp.Content.ReadAsStringAsync();

                    List<string> resultat = JsonSerializer.Deserialize<List<string>>(reponse, options);

                    if (resultat != null)
                    {
                        suggestions = resultat;
                    }
                }
                else
                {
                    throw new Exception($"Erreur HTTP lors de la recherche des ingrédients : {reponseHttp.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la recherche des ingrédients : " + ex.Message);
            }
            return suggestions;
        }
    }
}