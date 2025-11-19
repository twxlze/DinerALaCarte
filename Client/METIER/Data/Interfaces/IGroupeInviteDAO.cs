using METIER_Footies.Metier;

namespace METIER_Footies.Data.Interfaces
{
    public interface IGroupeInviteDAO
    {
        /// <summary>
        /// Ajoute un nouveau groupe d'invités
        /// </summary>
        /// <param name="groupeInvite"> Le groupe d'invités à ajouter </param>
        /// <returns> La réponse HTTP de l'API </returns>
        Task<HttpResponseMessage> AjouterGroupeInvite(GroupeInvites groupeInvite);

        /// <summary>
        /// Modifie un groupe d'invités existant
        /// </summary>
        /// <param name="groupeInvite"> Le groupe d'invités à modifier </param>
        /// <returns> La réponse HTTP de l'API </returns>
        Task<HttpResponseMessage> ModifierGroupe(GroupeInvites groupeInvite);

        /// <summary>
        /// Récupère tous les groupes d'invités
        /// </summary>
        /// <returns> Liste de tous les groupes d'invités </returns>
        Task<List<GroupeInvites>> ListeGroupeInvites();

        /// <summary>
        /// Supprime un groupe d'invités par son identifiant
        /// </summary>
        /// <param name="idGroupeInvite"> L'identifiant du groupe d'invités à supprimer </param>
        /// <returns> La réponse HTTP de l'API </returns>
        Task<HttpResponseMessage> SupprimerGroupeInvite(long idGroupeInvite);
    }
}