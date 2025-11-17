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
        private string adressAPI = "https://10.128.207.31:8081/";
        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur par défaut d'un DAO
        /// </summary>
        public DAO()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain,
            sslPolicyErrors) => {
                if (cert.GetCertHashString() == "CB73C7199CFECD6220039863157B859E13E36B63") // empreinte SHA-1
                {
                    return true;
                }
                return false;
            };
            this.httpClient = new HttpClient(handler);
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
        /// Récupération d'une donnée de façon asynchrone à l'API : GET pour obtenir des données
        /// </summary>
        /// <param name="demande">adresse de la demande</param>
        /// <returns>Réponse http de l'API</returns>
        public async Task<HttpResponseMessage> GetAsync(string demande)
        {
            string adresseEnvoi = adressAPI + demande;
            return await httpClient.GetAsync(adresseEnvoi);
        }

        /// <summary>
        /// Envoi d'une donnée de façon asynchrone à l'API : POST pour envoyer des données
        /// </summary>
        /// <param name="demande">adresse de la demande</param>
        /// <param name="objet">objet à envoyer dans le body</param>
        /// <returns>Réponse http de l'API</returns>
        public async Task<HttpResponseMessage> PostAsync(string demande, object objet)
        {
            string adresseEnvoi = adressAPI + demande;
            return await httpClient.PostAsJsonAsync(adresseEnvoi, objet);
        }

        /// <summary>
        /// Mise à jour d'une donnée de façon asynchrone à l'API : PUT pour modifier des données
        /// </summary>
        /// <param name="demande"> adresse de la demande</param>
        /// <param name="objet"> objet à envoyer dans le body</param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PutAsync(string demande, object objet)
        {
            string adresseEnvoi = adressAPI + demande;
            return await httpClient.PutAsJsonAsync(adresseEnvoi, objet);
        }

        /// <summary>
        /// Suppression d'une donnée de façon asynchrone à l'API : DELETE pour supprimer des données
        /// </summary>
        /// <param name="demande"> adresse de la demande</param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> DeleteAsync(string demande)
        {
            string adresseEnvoi = adressAPI + demande;
            return await httpClient.DeleteAsync(adresseEnvoi);
        }
        #endregion

    }
}