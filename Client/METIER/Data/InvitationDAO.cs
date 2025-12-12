using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using METIER_Footies.Data.Interfaces;
using METIER_Footies.Metier;

namespace METIER_Footies.Data
{
    /// <summary>
    /// Classe caractérisant une invitation (DAO Client)
    /// </summary>
    public class InvitationDAO : DAO, IInvitationDAO
    {

        public async Task<HttpResponseMessage> AjouterInvitation(Invitation invitation)
        {
            try
            {
                long idUtilisateur = SessionService.Instance.UtilisateurConnecte.IdUtilisateur;
                string url = $"Invitations/AjoutInvitation?IdUtilisateur={idUtilisateur}";

                HttpResponseMessage reponseHttp = await PostAsync(url, invitation);
                return reponseHttp;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de l'envoi de l'invitation : " + ex.Message);
            }
        }

        public async Task<List<Invitation>> ObtenirToutesLesInvitations()
        {
            List<Invitation> listeInvitations = new List<Invitation>();

            try
            {
                long idUtilisateur = SessionService.Instance.UtilisateurConnecte.IdUtilisateur;

                string url = $"Invitations/ListeInvitations?IdUtilisateur={idUtilisateur}";

                HttpResponseMessage reponseHttp = await this.GetAsync(url);

                if (reponseHttp.IsSuccessStatusCode)
                {
                    string reponse = await reponseHttp.Content.ReadAsStringAsync();
                    listeInvitations = JsonSerializer.Deserialize<List<Invitation>>(reponse, options);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la récupération des invitations : " + ex.Message);
            }

            return listeInvitations;
        }

        public async Task<HttpResponseMessage> SupprimerInvitation(long idInvitation)
        {
            try
            {
                long idUtilisateur = SessionService.Instance.UtilisateurConnecte.IdUtilisateur;
                string url = $"Invitations/SupprimerInvitation?idInvitation={idInvitation}&IdUtilisateur={idUtilisateur}";

                HttpResponseMessage reponseHttp = await DeleteAsync(url);
                return reponseHttp;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la suppression de l'invitation : " + ex.Message);
            }
        }

        public async Task<HttpResponseMessage> ModifierInvitation(Invitation invitation)
        {
            try
            {
                long idUtilisateur = SessionService.Instance.UtilisateurConnecte.IdUtilisateur;

                string url = $"Invitations/ModifierInvitation?IdUtilisateur={idUtilisateur}";

                HttpResponseMessage reponseHttp = await PutAsync(url, invitation);
                return reponseHttp;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la modification de l'invitation : " + ex.Message);
            }
        }

        public async Task<List<Invitation>> ChercherInvitation(string InvitationsRechercher)
        {
            List<Invitation> listeDesInvitations = new List<Invitation>();

            try
            {
                long idUtilisateur = SessionService.Instance.UtilisateurConnecte.IdUtilisateur;
                string url = $"Invitations/ChercherInvitations?InvitationsRechercher={InvitationsRechercher}&IdUtilisateur={idUtilisateur}";

                HttpResponseMessage reponseHttp = await GetAsync(url);

                if (reponseHttp.IsSuccessStatusCode)
                {
                    string reponse = await reponseHttp.Content.ReadAsStringAsync();
                    listeDesInvitations = JsonSerializer.Deserialize<List<Invitation>>(reponse, options);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la recherche des invitations : " + ex.Message);
            }

            return listeDesInvitations;
        }

        public async Task<HttpResponseMessage> AjouterCommentairePlat(string commentaire)
        {
            try
            {
                long idUtilisateur = SessionService.Instance.UtilisateurConnecte.IdUtilisateur;
                string url = $"Invitations/AjoutCommentairePlat?IdUtilisateur={idUtilisateur}";
                HttpResponseMessage reponseHttp = await PostAsync(url, commentaire);
                return reponseHttp;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de l'ajout du commentaire sur le plat : " + ex.Message);
            }
        }

        public async Task<HttpResponseMessage> AjouterNotePlat(int note)
        {
            try
            {
                long idUtilisateur = SessionService.Instance.UtilisateurConnecte.IdUtilisateur;
                string url = $"Invitations/AjoutCommentairePlat?IdUtilisateur={idUtilisateur}";
                HttpResponseMessage reponseHttp = await PostAsync(url, note);
                return reponseHttp;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de l'ajout de la note sur le plat : " + ex.Message);
            }
        }

        public async Task<List<AvisDetail>> ObtenirAvisPourInvitation(long idInvitation)
        {
            try
            {
                List<AvisDetail> listeAvis = new List<AvisDetail>();
                string url = $"Invitations/ListeAvis?idInvitation={idInvitation}";
                HttpResponseMessage response = await GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    listeAvis = JsonSerializer.Deserialize<List<AvisDetail>>(json, options);
                }
                return listeAvis;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur récupération avis : " + ex.Message);
            }
        }

    }
}