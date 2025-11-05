using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace METIER_Footies
{
    /// <summary>
    // Classe abstraite d'accès aux données pour la base de données
    /// </summary>
    public abstract class DAO
    {
        #region Attributs
        private HttpClient httpClient;
        private string adressAPI = "https://localhost:7230/";
        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur par défaut d'un DAO
        /// </summary>
        public DAO()
        {
            httpClient = new HttpClient();
        }
        #endregion

        #region Méthodes protégées
        /// <summary>
        /// Options de sérialisation JSON
        /// </summary>
        protected JsonSerializerOptions options => new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };
        #endregion

        #region Méthodes

        /// <summary>
        /// Récupération d'une donnée de façon asynchrone à l'API
        /// </summary>
        /// <param name="demande">adresse de la demande</param>
        /// <returns>Réponse http de l'API</returns>
        public async Task<HttpResponseMessage> GetAsync(string demande)
        {
            string adresseEnvoi = adressAPI + demande;
            return await httpClient.GetAsync(adresseEnvoi);
        }

        /// <summary>
        /// Envoi d'une donnée de façon asynchrone à l'API
        /// </summary>
        /// <param name="demande">adresse de la demande</param>
        /// <param name="objet">objet à envoyer dans le body</param>
        /// <returns>Réponse http de l'API</returns>
        public async Task<HttpResponseMessage> PostAsync(string demande, Object objet)
        {
            string adresseEnvoi = adressAPI + demande;
            return await httpClient.PostAsJsonAsync(adresseEnvoi, objet);
        }
        #endregion
    }
}
