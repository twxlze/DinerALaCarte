using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using METIER_Footies.Data.Interfaces;
using METIER_Footies.Metier;

namespace METIER_Footies.Data
/// <summary>
// Classe d'accès aux données pour les allergenes avec la base de données
/// </summary>
{
    internal class AllergeneDAO : DAO, IAllergeneDAO
    {
        public async Task<List<Allergene>> ListeAllergene()
        {
            List<Allergene> listeDesAllergenes = new List<Allergene>();

            HttpResponseMessage reponseHttp = await GetAsync("Allergene/ListeAllergene");

            if (reponseHttp.IsSuccessStatusCode)
            {
                string reponse = await reponseHttp.Content.ReadAsStringAsync();
                listeDesAllergenes = JsonSerializer.Deserialize<List<Allergene>>(reponse, options);
            }
            return listeDesAllergenes;
        }

    }
}
