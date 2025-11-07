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
    public class GroupeInviteDAO : DAO
    {
        /// <summary>
        /// Ajoute un groupe d'invités
        /// </summary>
        /// <param name="groupeInvite">le groupe a ajouter</param>
        /// <returns>l'ajout a t'il reussi on s'en servira pour prevenir l'utilisateur</returns>
        public async Task<bool> AjouterGroupeInvite(GroupeInvites groupeInvite)
        {
                HttpResponseMessage reponseHttp = await PostAsync("GroupeInvites/AjouterUnGroupeInvite", groupeInvite);
                return reponseHttp.IsSuccessStatusCode;
        }

        /// <summary>
        /// Récupère tous les groupes d'invités c'est a dire leur nom et id et les invités dans chaque groupe
        /// </summary>
        /// <returns></returns>
        public async Task<List<GroupeInvites>> RecupererTousGroupenvites()
        {
            List<GroupeInvites> listeDesGroupesInvites = new List<GroupeInvites>();

            HttpResponseMessage reponseHttp = await this.GetAsync("GroupeInvites/RecupererTousGroupesInvites");
            if (reponseHttp.IsSuccessStatusCode)
            {
                string reponse = await reponseHttp.Content.ReadAsStringAsync();
                listeDesGroupesInvites = JsonSerializer.Deserialize<List<GroupeInvites>>(reponse, options);
            }
            return listeDesGroupesInvites;
        }

        /// <summary>
        /// Ajoute un invité dans un groupe d'invités.
        /// </summary>
        /// <param name="idGroupeInvites">Identifiant du groupe</param>
        /// <param name="invite">L'invité à ajouter</param>
        /// <returns>l'ajout a t'il reussi on s'en servira pour prevenir l'utilisateur</returns>
        public async Task<bool> AjouterInviteAuGroupe(long idGroupeInvites, Invite invite)
        {
            HttpResponseMessage reponseHttp = await PostAsync($"GroupeInvites/AjouterInviteAuGroupe/{idGroupeInvites}", invite);
            return reponseHttp.IsSuccessStatusCode;
        }

        /// <summary>
        /// Modifie un groupe d'invités existant.
        /// </summary>
        /// <param name="groupeInvite">Le groupe d'invités à modifier</param>
        /// <returns>true si la modification a réussi, false sinon</returns>
        public async Task<bool> ModifierGroupe(GroupeInvites groupeInvite)
        {
            HttpResponseMessage reponseHttp = await PutAsync("GroupeInvites/ModifierUnGroupe", groupeInvite);
            return reponseHttp.IsSuccessStatusCode;
        }

        /// <summary>
        /// Récupère un groupe d'invités via son identifiant unique.
        /// </summary>
        /// <param name="idGroupeInvites">Identifiant du groupe à récupérer</param>
        /// <returns>Le groupe d'invités correspondant, ou null s'il n'existe pas</returns>
        public async Task<GroupeInvites?> RecupererGroupeParId(long idGroupeInvites)
        {
            HttpResponseMessage reponseHttp = await GetAsync($"GroupeInvites/RecupererGroupeParId/{idGroupeInvites}");
            if (reponseHttp.IsSuccessStatusCode)
            {
                string reponse = await reponseHttp.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<GroupeInvites>(reponse, options);
            }

            return null;
        }

    }
}
