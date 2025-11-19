using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using METIER_Footies.Data.Interfaces;
using METIER_Footies.Metier;

namespace METIER_Footies.Data
{
    /// <summary>
    // Classe d'accès aux données pour les plats avec la base de données
    /// </summary>
    public class PlatDAO : DAO, IPlatDAO
    {
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

        public async Task<List<Plat>> ChercherPlat(string texteRecherche)
        {
            List<Plat> listeDesPlats = new List<Plat>();
            HttpResponseMessage reponseHttp = await GetAsync($"Plats/ChercherPlat?texterecherche={texteRecherche}");
            if (reponseHttp.IsSuccessStatusCode)
            {
                string reponse = await reponseHttp.Content.ReadAsStringAsync();
                listeDesPlats = JsonSerializer.Deserialize<List<Plat>>(reponse, options);
            }
            return listeDesPlats;
        }

    }
}