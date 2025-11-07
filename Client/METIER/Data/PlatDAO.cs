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
        /// Ajoute un plat
        /// </summary>
        /// <param name="plat"> le plat à ajouter </param>
        /// <returns> Réponse http de l'API </returns>
        public async Task<HttpResponseMessage> AjouterPlat(Plat plat)
        {
            try
            {
                HttpResponseMessage reponseHttp = await PostAsync("Plats/AjoutPlat", plat);
                return reponseHttp;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de l'ajout du plat : " + ex.Message);
            }
        }

        /// <summary>
        /// Obtient tous les plats
        /// </summary>
        /// <returns> Liste de tous les plats </returns>
        public async Task<List<Plat>> ObtenirTout()
        {
            List<Plat> listeDesPlats = new List<Plat>();

            HttpResponseMessage reponseHttp = await this.GetAsync("Plats/ListePlat");

            if (reponseHttp.IsSuccessStatusCode)
            {
                string reponse = await reponseHttp.Content.ReadAsStringAsync();
                listeDesPlats = JsonSerializer.Deserialize<List<Plat>>(reponse, options);
            }
            return listeDesPlats;
        }

        /// <summary>
        /// Supprime un invité
        /// </summary>
        public async Task<HttpResponseMessage> SupprimerPlat(long idPlat)
        {
            try
            {
                HttpResponseMessage reponseHttp = await DeleteAsync($"Plats/SupprimerPlat?id={idPlat}");
                return reponseHttp;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la suppression du plat : " + ex.Message);
            }
        }

        /// <summary>
        /// Modifier un plat
        /// </summary>
        /// <param name="plat"> Le plat à modifier </param>
        /// <returns> Réponse http de l'API </returns>
        public async Task<HttpResponseMessage> ModifierPlat(Plat plat)
        {
            try
            {
                HttpResponseMessage reponseHttp = await PutAsync("Plats/ModifierPlat", plat);
                return reponseHttp;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la modification du plat : " + ex.Message);
            }
        }

        /// <summary>
        /// Vérifie si un plat est associé à un ou plusieurs menus
        /// </summary>
        /// <param name="idPlat">L'id du plat</param>
        /// <returns>True si le plat fait partie d'au moins un menu, False sinon</returns>
        public async Task<bool> EstDansUnMenu(long idPlat)
        {
            bool resultat = false;
            try
            {
                HttpResponseMessage reponseHttp = await GetAsync($"Plats/EstDansUnMenu?id={idPlat}");

                if (reponseHttp.IsSuccessStatusCode)
                {
                    string reponse = await reponseHttp.Content.ReadAsStringAsync();
                    resultat = JsonSerializer.Deserialize<bool>(reponse, options);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la vérification de l'association du plat aux menus : " + ex.Message);
            }
            return resultat;
        }

    }
}