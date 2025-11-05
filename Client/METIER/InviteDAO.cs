using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace METIER_Footies
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
        public async Task<HttpResponseMessage> AddInvite(Invite invite)
        {
            HttpResponseMessage reponseHttp = await this.PostAsync("Invites/AjoutInvite", invite); 
            return reponseHttp;
        }
    }
}
