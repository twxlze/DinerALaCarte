using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using METIER_Footies.Data.Interface;
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
                long idUtilisateur = SessionService.Instance.UtilisateurConnecte.Id;
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
                long idUtilisateur = SessionService.Instance.UtilisateurConnecte.Id;

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
                long idUtilisateur = SessionService.Instance.UtilisateurConnecte.Id;
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
                long idUtilisateur = SessionService.Instance.UtilisateurConnecte.Id;

                string url = $"Invitations/ModifierInvitation?IdUtilisateur={idUtilisateur}";

                HttpResponseMessage reponseHttp = await PutAsync(url, invitation);
                return reponseHttp;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la modification de l'invitation : " + ex.Message);
            }
        }
    }
}