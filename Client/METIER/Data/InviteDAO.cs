using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using METIER_Footies.Metier;

namespace METIER_Footies.Data
{
    /// <summary>
    // Classe d'accès aux données pour les invités avec la base de données
    /// </summary>
    public class InviteDAO : DAO
    {
        /// <summary>
        /// Ajoute un invité
        /// </summary>
        /// <param name="invite"> l'invité à ajouter </param>
        /// <returns> Réponse http de l'API </returns>
        public async Task<HttpResponseMessage> AjouterInvite(Invite invite)
        {
            try
            {
                HttpResponseMessage reponseHttp = await PostAsync("Invites/AjoutInvite", invite);
                return reponseHttp;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de l'ajout de l'invité : " + ex.Message);
            }

        }

        /// <summary>
        /// Obtient tous les invités
        /// </summary>
        /// <returns> Liste de tous les invités </returns>
        public async Task<List<Invite>> ObtenirTout()
        {
            List<Invite> listeDesInvites = new List<Invite>();

            HttpResponseMessage reponseHttp = await this.GetAsync("Invites/ListeInvite");

            if (reponseHttp.IsSuccessStatusCode)
            {
                string reponse = await reponseHttp.Content.ReadAsStringAsync();
                listeDesInvites = JsonSerializer.Deserialize<List<Invite>>(reponse, options);
            }
            return listeDesInvites;
        }

        /// <summary>
        /// Supprime un invité
        /// </summary>
        public async Task<HttpResponseMessage> SupprimerInvite(long idInvite)
        {
            try
            {
                HttpResponseMessage reponseHttp = await DeleteAsync($"Invites/SupprimerInvite?id={idInvite}");
                return reponseHttp;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la suppression de l'invité : " + ex.Message);
            }
        }

        /// <summary>
        /// Modifier un invité
        /// </summary>
        /// <param name="invite"> L'invité à modifier </param>
        /// <returns> Réponse http de l'API </returns>
        public async Task<HttpResponseMessage> ModifierInvite(Invite invite)
        {
            try
            {
                HttpResponseMessage reponseHttp = await PutAsync("Invites/ModifierInvite", invite);
                return reponseHttp;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la modification de l'invité : " + ex.Message);
            }
        }

        /// <summary>
        /// Vérifie si un invité est associé à un ou plusieurs groupes
        /// </summary>
        /// <param name="idInvite">L'id de l'invité</param>
        /// <returns>True si l'invité fait partie d'au moins un groupe, False sinon</returns>
        public async Task<bool> EstDansUnGroupe(long idInvite)
        {
            bool resultat = false;
            try
            {
                HttpResponseMessage reponseHttp = await GetAsync($"Invites/EstDansUnGroupe?id={idInvite}");

                if (reponseHttp.IsSuccessStatusCode)
                {
                    string reponse = await reponseHttp.Content.ReadAsStringAsync();
                    resultat = JsonSerializer.Deserialize<bool>(reponse, options);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la vérification de l'association de l'invité aux groupes : " + ex.Message);
            }
            return resultat;
        }

    }
}
