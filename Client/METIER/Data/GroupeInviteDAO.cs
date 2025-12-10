using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using METIER_Footies.Metier;
using METIER_Footies.Data.Interfaces;

namespace METIER_Footies.Data
{
    public class GroupeInviteDAO : DAO, IGroupeInviteDAO
    {
        /// <summary>
        /// Ajoute un groupe d'invités
        /// </summary>
        public async Task<HttpResponseMessage> AjouterGroupeInvite(GroupeInvites groupeInvite)
        {
            try
            {
                long idUtilisateur = SessionService.Instance.UtilisateurConnecte.Id;

                string url = $"GroupeInvites/AjoutGroupeInvite?IdUtilisateur={idUtilisateur}";

                HttpResponseMessage reponseHttp = await PostAsync(url, groupeInvite);
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

            long idUtilisateur = SessionService.Instance.UtilisateurConnecte.Id;

            string url = $"GroupeInvites/ChercherGroupeInvites?GroupeInvitesRechercher={GroupeInvitesRechercher}&IdUtilisateur={idUtilisateur}";

            HttpResponseMessage reponseHttp = await GetAsync(url);

            if (reponseHttp.IsSuccessStatusCode)
            {
                string reponse = await reponseHttp.Content.ReadAsStringAsync();
                listeDesGroupeInvites = JsonSerializer.Deserialize<List<GroupeInvites>>(reponse, options);
            }
            return listeDesGroupeInvites;
        }

        /// <summary>
        /// Récupère tous les groupes d'invités
        /// </summary>
        public async Task<List<GroupeInvites>> ListeGroupeInvites()
        {
            List<GroupeInvites> listeDesGroupesInvites = new List<GroupeInvites>();

            long idUtilisateur = SessionService.Instance.UtilisateurConnecte.Id;

            string url = $"GroupeInvites/ListeGroupeInvites?IdUtilisateur={idUtilisateur}";

            HttpResponseMessage reponseHttp = await this.GetAsync(url);

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
        public async Task<HttpResponseMessage> ModifierGroupe(GroupeInvites groupeInvite)
        {
            try
            {
                long idUtilisateur = SessionService.Instance.UtilisateurConnecte.Id;

                string url = $"GroupeInvites/ModifierGroupeInvite?IdUtilisateur={idUtilisateur}";

                HttpResponseMessage reponseHttp = await PutAsync(url, groupeInvite);
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
        public async Task<HttpResponseMessage> SupprimerGroupeInvite(long idGroupeInvite)
        {
            try
            {
                long idUtilisateur = SessionService.Instance.UtilisateurConnecte.Id;

                string url = $"GroupeInvites/SupprimerGroupeInvite?idGroupeInvite={idGroupeInvite}&IdUtilisateur={idUtilisateur}";

                HttpResponseMessage reponseHttp = await DeleteAsync(url);
                return reponseHttp;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la suppression du groupe d'invités : " + ex.Message);
            }
        }
    }
}