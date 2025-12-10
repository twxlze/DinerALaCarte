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
    /// <summary>
    /// Classe DAO pour la gestion de la connexion des utilisateurs
    /// </summary>
    public class ConnexionDAO : DAO, IConnexionDAO
    {
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

        public async Task<bool> VerifierPseudoDisponible(string pseudo)
        {
            bool resultat = false;
            try
            {
                HttpResponseMessage reponseHttp = await GetAsync($"Authentification/VerifierConnexion?pseudo={pseudo}");

                if (reponseHttp.IsSuccessStatusCode)
                {
                    string reponse = await reponseHttp.Content.ReadAsStringAsync();
                    resultat = JsonSerializer.Deserialize<bool>(reponse, options);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la vérification de la disponibilité du pseudonyme : " + ex.Message);
            }
            return resultat;
        }

        public async Task<bool> CreerUnUtilisateur(Utilisateur utilisateur)
        {
            bool resultat = false;
            try
            {
                HttpResponseMessage reponseHttp = await PostAsync($"Authentification/CreerUnUtilisateur", utilisateur);

                if (reponseHttp.IsSuccessStatusCode)
                {
                    string reponse = await reponseHttp.Content.ReadAsStringAsync();
                    resultat = JsonSerializer.Deserialize<bool>(reponse, options);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la création du compte : " + ex.Message);
            }
            return resultat;
        }

        
    }
}
