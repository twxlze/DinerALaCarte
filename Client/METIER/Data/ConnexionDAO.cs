using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using METIER_Footies.Data.Interfaces;
using METIER_Footies.Metier;

namespace METIER_Footies.Data
{
    public class ConnexionDAO : DAO, IConnexionDAO
    {
        public async Task<HttpResponseMessage> Connexion(Utilisateur utilisateur)
        {
            try
            {
                //return await Task.FromResult(new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                //{
                //    Content = new StringContent("{\"success\": true}", Encoding.UTF8, "application/json")
                //});
                HttpResponseMessage reponseHttp = await DeleteAsync($"Authentification/VerifierConnexion?Utilisateur={utilisateur}");
                return reponseHttp;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la connexion : " + ex.Message);
            }
        }
    }
}
