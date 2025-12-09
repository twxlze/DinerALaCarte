using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using METIER_Footies.Data.Interfaces;
using METIER_Footies.Metier;

namespace METIER_Footies.Data
{
    /// <summary>
    /// Fournit des méthodes d'accès aux données pour gérer les menus (Côté Client)
    /// </summary>
    public class MenuDAO : DAO, IMenuDAO
    {
        #region methodes 
        public async Task<HttpResponseMessage> AjouterMenu(Menu menu)
        {
            try
            {
                long idUtilisateur = SessionService.Instance.UtilisateurConnecte.Id;
                string url = $"Menus/AjoutMenu?IdUtilisateur={idUtilisateur}";

                HttpResponseMessage reponseHttp = await PostAsync(url, menu);
                return reponseHttp;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de l'ajout du menu : " + ex.Message);
            }
        }

        public async Task<List<Menu>> ObtenirTousLesMenus()
        {
            List<Menu> listeDesMenus = new List<Menu>();

            try
            {
                long idUtilisateur = SessionService.Instance.UtilisateurConnecte.Id;

                string url = $"Menus/ListeMenu?IdUtilisateur={idUtilisateur}";

                HttpResponseMessage reponseHttp = await this.GetAsync(url);

                if (reponseHttp.IsSuccessStatusCode)
                {
                    string reponse = await reponseHttp.Content.ReadAsStringAsync();
                    listeDesMenus = JsonSerializer.Deserialize<List<Menu>>(reponse, options);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la récupération des menus : " + ex.Message);
            }

            return listeDesMenus;
        }

        public async Task<HttpResponseMessage> SupprimerMenu(long idMenu)
        {
            try
            {
                long idUtilisateur = SessionService.Instance.UtilisateurConnecte.Id;

                string url = $"Menus/SupprimerMenu?idMenu={idMenu}&IdUtilisateur={idUtilisateur}";

                HttpResponseMessage reponseHttp = await DeleteAsync(url);
                return reponseHttp;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la suppression du menu : " + ex.Message);
            }
        }

        public async Task<HttpResponseMessage> ModifierMenu(Menu menu)
        {
            try
            {
                long idUtilisateur = SessionService.Instance.UtilisateurConnecte.Id;

                string url = $"Menus/ModifierMenu?IdUtilisateur={idUtilisateur}";

                HttpResponseMessage reponseHttp = await PutAsync(url, menu);
                return reponseHttp;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la modification du menu : " + ex.Message);
            }
        }

        public async Task<List<Menu>> ChercherMenus(string menuRechercher)
        {
            List<Menu> listeDesMenus = new List<Menu>();
            try
            {
                long idUtilisateur = SessionService.Instance.UtilisateurConnecte.Id;

                string url = $"Menus/ChercherMenus?menuRechercher={menuRechercher}&IdUtilisateur={idUtilisateur}";

                HttpResponseMessage reponseHttp = await GetAsync(url);

                if (reponseHttp.IsSuccessStatusCode)
                {
                    string reponse = await reponseHttp.Content.ReadAsStringAsync();
                    listeDesMenus = JsonSerializer.Deserialize<List<Menu>>(reponse, options);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la recherche des menus : " + ex.Message);
            }

            return listeDesMenus;
        }
        #endregion
    }
}