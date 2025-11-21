using METIER_Footies.Data.Interfaces;
using METIER_Footies.Metier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace METIER_Footies.Data
{
    /// <summary>
    ///  Classe d'accès aux données pour les groupes d'invités avec la base de données
    /// </summary>
    public class GroupeInviteDAO : DAO, IGroupeInviteDAO
    {
        /// <summary>
        /// Ajoute un groupe d'invités
        /// </summary>
        /// <param name="groupeInvite">le groupe a ajouter</param>
        /// <returns>l'ajout a t'il reussi on s'en servira pour prevenir l'utilisateur</returns>
        public async Task<HttpResponseMessage> AjouterGroupeInvite(GroupeInvites groupeInvite)
        {
            try
            {
                HttpResponseMessage reponseHttp = await PostAsync("GroupeInvites/AjoutGroupeInvite", groupeInvite);
                return reponseHttp;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de l'ajout du groupe d'invités : " + ex.Message);
            }
        }

        public async Task<List<GroupeInvites>> ChercherGroupeInvites(string GroupeInvitesRechercher)
        {
            List<GroupeInvites> listeDesGroupeInvites = new List<GroupeInvites>();
            HttpResponseMessage reponseHttp = await GetAsync($"GroupeInvites/ChercherGroupeInvites?GroupeInvitesRechercher={GroupeInvitesRechercher}");
            if (reponseHttp.IsSuccessStatusCode)
            {
                string reponse = await reponseHttp.Content.ReadAsStringAsync();
                listeDesGroupeInvites = JsonSerializer.Deserialize<List<GroupeInvites>>(reponse, options);
            }
            return listeDesGroupeInvites;
        }


        /// <summary>
        /// Récupère tous les groupes d'invités c'est a dire leur nom et id et les invités dans chaque groupe
        /// </summary>
        /// <returns>la liste des groupe invite</returns>
        public async Task<List<GroupeInvites>> ListeGroupeInvites()
        {
            List<GroupeInvites> listeDesGroupesInvites = new List<GroupeInvites>();

            HttpResponseMessage reponseHttp = await this.GetAsync("GroupeInvites/ListeGroupeInvites");
            if (reponseHttp.IsSuccessStatusCode)
            {
                string reponse = await reponseHttp.Content.ReadAsStringAsync();
                listeDesGroupesInvites = JsonSerializer.Deserialize<List<GroupeInvites>>(reponse, options);
            }
            return listeDesGroupesInvites;
        }


        /// <summary>
        /// Modifie un groupe d'invités existant.
        /// </summary>
        /// <param name="groupeInvite">Le groupe d'invités à modifier</param>
        /// <returns>si la modif a reussi ou pas</returns>
        public async Task<HttpResponseMessage> ModifierGroupe(GroupeInvites groupeInvite)
        {
            try
            {
                HttpResponseMessage reponseHttp = await PutAsync("GroupeInvites/ModifierGroupeInvite", groupeInvite);
                return reponseHttp;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la modification du groupe d'invités : " + ex.Message);
            }
        }

        /// <summary>
        /// Supprime un groupe d'invités via son identifiant unique.
        /// </summary>
        /// <param name="idGroupeInvites">Identifiant du groupe à récupérer</param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> SupprimerGroupeInvite(long idGroupeInvite)
        {
            try
            {
                HttpResponseMessage reponseHttp = await DeleteAsync($"GroupeInvites/SupprimerGroupeInvite?idGroupeInvite={idGroupeInvite}");
                return reponseHttp;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la suppression du groupe d'invités : " + ex.Message);
            }
        }

    }
}
