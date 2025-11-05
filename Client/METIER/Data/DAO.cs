using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace METIER_Footies.Data
{
    /// <summary>
    // Classe abstraite d'accès aux données pour la base de données
    /// </summary>
    public abstract class DAO
    {
        #region Attributs
        private HttpClient httpClient;
        private string adressAPI;
        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur par défaut d'un DAO
        /// </summary>
        public DAO()
        {
            GetUrlApi();
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
        public async Task<HttpResponseMessage> PostAsync(string demande, object objet)
        {
            string adresseEnvoi = adressAPI + demande;
            return await httpClient.PostAsJsonAsync(adresseEnvoi, objet);
        }
        #endregion

        #region Méthodes privées

        /// <summary>
        /// Configure l'URL de l'API à partir du fichier appsettings.json
        /// </summary>
        /// <remarks> Si le fichier n'existe pas ou que l'URL ne marche pas, renvoie une exception</remarks>
        private void GetUrlApi()
        {
            // Configuration pour lire le fichier appsettings.json

            var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();

            //Récupère l'URL de l'API dans la section JSON qui correspond 
            this.adressAPI = Convert.ToString(config.GetSection("API:url")) ?? throw new NullReferenceException();
        }
        #endregion
    }
}
