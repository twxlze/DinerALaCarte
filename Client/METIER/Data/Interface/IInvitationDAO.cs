using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using METIER_Footies.Metier;

namespace METIER_Footies.Data.Interface
{
    /// <summary>
    /// Interface pour la gestion des invitations
    /// </summary>
    public interface IInvitationDAO
    {
        /// <summary>
        /// Ajoute une invitation
        /// </summary>
        /// <param name="invitation"> Invitation à ajouter </param>
        /// <returns> Une réponse HTTP </returns>
        Task<HttpResponseMessage> AjouterInvitation(Invitation invitation);

        /// <summary>
        /// Modifie une invitation
        /// </summary>
        /// <param name="invitation"> L'invitation à modifier </param>
        /// <returns> Une réponse HTTP</returns>
        Task<HttpResponseMessage> ModifierInvitation(Invitation invitation);

        /// <summary>
        /// Obtient la liste de toutes les invitations
        /// </summary>
        /// <returns> La liste des invitations </returns>
        Task<List<Invitation>> ObtenirToutesLesInvitations();

        /// <summary>
        /// Supprime une invitation
        /// </summary>
        /// <param name="idInvitation">L'id de l'invitation</param>
        /// <returns> Une réponse HTPP</returns>
        Task<HttpResponseMessage> SupprimerInvitation(long idInvitation);
    }
}
