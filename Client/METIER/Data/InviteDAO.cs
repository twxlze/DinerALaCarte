using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using METIER_Footies.Metier;
using METIER_Footies.Data.Interfaces;

namespace METIER_Footies.Data
{
    /// <summary>
    /// Classe d'accès aux données pour les invités avec la base de données
    /// </summary>
    public class InviteDAO : DAO, IInviteDAO
    {
        public async Task<HttpResponseMessage> AjouterInvite(Invite invite)
        {
            try
            {
                long idUtilisateur = SessionService.Instance.UtilisateurConnecte.IdUtilisateur;
                string url = $"Invites/AjoutInvite?IdUtilisateur={idUtilisateur}";

                HttpResponseMessage reponseHttp = await PostAsync(url, invite);
                return reponseHttp;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de l'ajout de l'invité : " + ex.Message);
            }
        }

        public async Task<List<Invite>> ObtenirTout()
        {
            List<Invite> listeDesInvites = new List<Invite>();

            long idUtilisateur = SessionService.Instance.UtilisateurConnecte.IdUtilisateur;
            string url = $"Invites/ListeInvite?IdUtilisateur={idUtilisateur}";

            HttpResponseMessage reponseHttp = await GetAsync(url);

            if (reponseHttp.IsSuccessStatusCode)
            {
                string reponse = await reponseHttp.Content.ReadAsStringAsync();
                listeDesInvites = JsonSerializer.Deserialize<List<Invite>>(reponse, options);
            }
            return listeDesInvites;
        }

        public async Task<HttpResponseMessage> SupprimerInvite(long idInvite)
        {
            try
            {
                long idUtilisateur = SessionService.Instance.UtilisateurConnecte.IdUtilisateur;
                string url = $"Invites/SupprimerInvite?id={idInvite}&IdUtilisateur={idUtilisateur}";

                HttpResponseMessage reponseHttp = await DeleteAsync(url);
                return reponseHttp;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la suppression de l'invité : " + ex.Message);
            }
        }


        public async Task<HttpResponseMessage> ModifierInvite(Invite invite)
        {
            try
            {
                long idUtilisateur = SessionService.Instance.UtilisateurConnecte.IdUtilisateur;
                string url = $"Invites/ModifierInvite?IdUtilisateur={idUtilisateur}";

                HttpResponseMessage reponseHttp = await PutAsync(url, invite);
                return reponseHttp;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la modification de l'invité : " + ex.Message);
            }
        }

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

        public async Task<List<Invite>> ChercherInvite(string texteRecherche)
        {
            List<Invite> listeDesInvites = new List<Invite>();

            long idUtilisateur = SessionService.Instance.UtilisateurConnecte.IdUtilisateur;
            string url = $"Invites/ChercherInvite?texterecherche={texteRecherche}&IdUtilisateur={idUtilisateur}";

            HttpResponseMessage reponseHttp = await GetAsync(url);
            if (reponseHttp.IsSuccessStatusCode)
            {
                string reponse = await reponseHttp.Content.ReadAsStringAsync();
                listeDesInvites = JsonSerializer.Deserialize<List<Invite>>(reponse, options);
            }
            return listeDesInvites;
        }
    }
}