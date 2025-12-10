using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using METIER_Footies.Data.Interfaces;
using METIER_Footies.Metier;

namespace METIER_Footies.Data
{
    /// <summary>
    /// Classe d'accès aux données pour les plats avec la base de données (Côté Client)
    /// </summary>
    public class PlatDAO : DAO, IPlatDAO
    {
        public async Task<HttpResponseMessage> AjouterPlat(Plat plat)
        {
            try
            {
                long idUtilisateur = SessionService.Instance.UtilisateurConnecte.Id;

                string url = $"Plats/AjoutPlat?IdUtilisateur={idUtilisateur}";

                HttpResponseMessage reponseHttp = await PostAsync(url, plat);
                return reponseHttp;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de l'ajout du plat : " + ex.Message);
            }
        }

        public async Task<List<Plat>> ObtenirTout()
        {
            List<Plat> listeDesPlats = new List<Plat>();

            try
            {
                long idUtilisateur = SessionService.Instance.UtilisateurConnecte.Id;

                string url = $"Plats/ListePlat?IdUtilisateur={idUtilisateur}";

                HttpResponseMessage reponseHttp = await this.GetAsync(url);

                if (reponseHttp.IsSuccessStatusCode)
                {
                    string reponse = await reponseHttp.Content.ReadAsStringAsync();
                    listeDesPlats = JsonSerializer.Deserialize<List<Plat>>(reponse, options);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la récupération des plats : " + ex.Message);
            }

            return listeDesPlats;
        }

        public async Task<HttpResponseMessage> SupprimerPlat(long idPlat)
        {
            try
            {
                long idUtilisateur = SessionService.Instance.UtilisateurConnecte.Id;

                string url = $"Plats/SupprimerPlat?id={idPlat}&IdUtilisateur={idUtilisateur}";

                HttpResponseMessage reponseHttp = await DeleteAsync(url);
                return reponseHttp;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la suppression du plat : " + ex.Message);
            }
        }

        public async Task<HttpResponseMessage> ModifierPlat(Plat plat)
        {
            try
            {
                long idUtilisateur = SessionService.Instance.UtilisateurConnecte.Id;

                string url = $"Plats/ModifierPlat?IdUtilisateur={idUtilisateur}";

                HttpResponseMessage reponseHttp = await PutAsync(url, plat);
                return reponseHttp;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la modification du plat : " + ex.Message);
            }
        }

        public async Task<bool> EstDansUnMenu(long idPlat)
        {
            bool resultat = false;
            try
            {
                string url = $"Plats/EstDansUnMenu?id={idPlat}";

                HttpResponseMessage reponseHttp = await GetAsync(url);

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

        public async Task<List<Plat>> ChercherPlat(string texteRecherche)
        {
            List<Plat> listeDesPlats = new List<Plat>();
            try
            {
                long idUtilisateur = SessionService.Instance.UtilisateurConnecte.Id;

                string url = $"Plats/ChercherPlat?texterecherche={texteRecherche}&IdUtilisateur={idUtilisateur}";

                HttpResponseMessage reponseHttp = await GetAsync(url);

                if (reponseHttp.IsSuccessStatusCode)
                {
                    string reponse = await reponseHttp.Content.ReadAsStringAsync();
                    listeDesPlats = JsonSerializer.Deserialize<List<Plat>>(reponse, options);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la recherche de plats : " + ex.Message);
            }
            return listeDesPlats;
        }
    }
}