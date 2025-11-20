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
    /// Fournit des méthodes d'accès aux données pour gérer les menus, notamment pour ajouter, récupérer, mettre à jour et supprimer des menus.
    /// </summary>
    public class MenuDAO : DAO, IMenuDAO
    {

        #region methodes 
        public async Task<HttpResponseMessage> AjouterMenu(Menu menu)
        {
            try
            {
                HttpResponseMessage reponseHttp = await PostAsync("Menus/AjoutMenu", menu);
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

            HttpResponseMessage reponseHttp = await this.GetAsync("Menus/ListeMenu");

            if (reponseHttp.IsSuccessStatusCode)
            {
                string reponse = await reponseHttp.Content.ReadAsStringAsync();
                listeDesMenus = JsonSerializer.Deserialize<List<Menu>>(reponse, options);
            }
            return listeDesMenus;
        }


        public async Task<HttpResponseMessage> SupprimerMenu(long idMenu)
        {
            try
            {
                HttpResponseMessage reponseHttp = await DeleteAsync($"Menus/SupprimerMenu?idMenu={idMenu}");
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
                HttpResponseMessage reponseHttp = await PutAsync("Menus/ModifierMenu", menu);
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
            HttpResponseMessage reponseHttp = await GetAsync($"Menus/ChercherMenus?menuRechercher={menuRechercher}");
            if (reponseHttp.IsSuccessStatusCode)
            {
                string reponse = await reponseHttp.Content.ReadAsStringAsync();
                listeDesMenus = JsonSerializer.Deserialize<List<Menu>>(reponse, options);
            }
            return listeDesMenus;
        }
        #endregion

    }

}
