using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using METIER_Footies.Data.Interface;
using METIER_Footies.Metier;

namespace METIER_Footies.Data
{
    /// <summary>
    /// Classe caractérisant une invitation
    /// </summary>
    public class InvitationDAO : DAO, IInvitationDAO
    {
        
        public async Task<HttpResponseMessage> AjouterInvitation(Invitation invitation)
        {
            try
            {
                HttpResponseMessage reponseHttp = await PostAsync("Invitations/AjoutInvitation", invitation);
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
            HttpResponseMessage reponseHttp = await this.GetAsync("Invitations/ListeInvitations");
            if (reponseHttp.IsSuccessStatusCode)
            {
                string reponse = await reponseHttp.Content.ReadAsStringAsync();
                listeInvitations = JsonSerializer.Deserialize<List<Invitation>>(reponse, options);
            }
            return listeInvitations;
        }

        public async Task<HttpResponseMessage> SupprimerInvitation(long idInvitation)
        {
            try
            {
                HttpResponseMessage reponseHttp = await DeleteAsync($"Invitations/SupprimerInvitation?idInvitation={idInvitation}");
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
                HttpResponseMessage reponseHttp = await PutAsync("Invitations/ModifierInvitation", invitation);
                return reponseHttp;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la modification de l'invitation : " + ex.Message);
            }
        }
    }
}
