using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using METIER_Footies.Metier;

namespace METIER_Footies.Data
{
    /// <summary>
    // Classe d'accès aux données pour les plats avec la base de données
    /// </summary>
    public class PlatDAO : DAO
    {
        /// <summary>
        /// Ajoute un invité
        /// </summary>
        /// <param name="invite"> l'invité à ajouter </param>
        /// <returns> Réponse http de l'API </returns>
        public async Task<HttpResponseMessage> AjouterPlat(Plat plat)
        {
            HttpResponseMessage reponseHttp = await PostAsync("Plat/AjouterPlat", plat);
            return reponseHttp;
        }

        /// <summary>
        /// Obtient tous les invités
        /// </summary>
        /// <returns> Liste de tous les invités </returns>
        public async Task<List<Plat>> ObtenirTout()
        {
            List<Plat> listeDesPlats = new List<Plat>();

            HttpResponseMessage reponseHttp = await this.GetAsync("Plat/ListePlat");

            if (reponseHttp.IsSuccessStatusCode)
            {
                string reponse = await reponseHttp.Content.ReadAsStringAsync();
                listeDesPlats = JsonSerializer.Deserialize<List<Plat>>(reponse, options);
            }
            return listeDesPlats;
        }
    }
}
