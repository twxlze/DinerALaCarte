using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using METIER_Footies.Data.Interfaces;
using METIER_Footies.Metier;

namespace METIER_Footies.Data
{
    public class ConnexionDAO : DAO, IConnexionDAO
    {
        /// <summary>
        /// Connexion d'un utilisateur
        /// </summary>
        /// <param name="utilisateur"> l'utilisateur à connecter </param>
        /// <returns> L'utilisateur connecté avec son ID, ou null si la connexion a échoué </returns>
        public async Task<Utilisateur?> Connexion(Identifiant identifiant)
        {
            Utilisateur? utilisateurConnecte = null;

            try
            {
                HttpResponseMessage reponseHttp = await PostAsync("Authentification/VerifierConnexion", identifiant);

                if (reponseHttp.IsSuccessStatusCode)
                {
                    string reponse = await reponseHttp.Content.ReadAsStringAsync();
                    utilisateurConnecte = JsonSerializer.Deserialize<Utilisateur>(reponse, options);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la connexion : " + ex.Message);
            }

            return utilisateurConnecte;
        }
    }
}
