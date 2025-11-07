using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        /// Ajouter un plat 
        /// </summary>
        /// <param name="invite"> le plat à ajouter </param>
        /// <returns> Réponse http de l'API </returns>
        public async Task<HttpResponseMessage> AjouterPlat(Plat plat)
        {
            HttpResponseMessage reponseHttp = await PostAsync("Plat/AjouterPlat", plat);
            return reponseHttp;
        }

        /// <summary>
        /// Modifier un plat
        /// </summary>
        /// <param name="invite"> Le plat à modifier </param>
        /// <returns> Réponse http de l'API </returns>
        public async Task<HttpResponseMessage> ModifierPlat(Plat plat)
        {
            try
            {
                HttpResponseMessage reponseHttp = await PutAsync("Invites/ModifierInvite", plat);
                return reponseHttp;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la modification du plat : " + ex.Message);
            }
        }

        /// <summary>
        /// Obtient tous les plats
        /// </summary>
        /// <returns> Liste de tous les plats </returns>
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
