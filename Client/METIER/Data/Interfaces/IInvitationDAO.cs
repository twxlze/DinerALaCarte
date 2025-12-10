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
        /// Ajouter une invitation
        /// </summary>
        /// <param name="invitation"> L'invitation à ajouter </param>
        /// <returns>Une réponse HTTP</returns>
        /// <exception cref="Exception">Lancé si une erreur se produit lors de l'envoi de l'invitation. Le message d'exception comprend des détails sur l'erreur.</exception>
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
        /// <param name="idInvitation">L'id de l'invitation à supprimer </param>
        /// <returns> Une réponse HTPP</returns>
        Task<HttpResponseMessage> SupprimerInvitation(long idInvitation);

        /// <summary>
        /// Recherche des invitations via un texte de recherche
        /// </summary>
        /// <param name="InvitationRechercher">le texte en question </param>
        /// <returns>une liste de groupe invite dont le nom resemble au texte de la barre de recherche</returns>
        Task<List<Invitation>> ChercherInvitation(string InvitationRechercher);
    }

}
