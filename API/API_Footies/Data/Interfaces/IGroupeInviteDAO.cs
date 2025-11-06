using API_Footies.Metier;

namespace API_Footies.Data.Interfaces
{
    /// <summary>
    /// Interface pour le DAO en charge de la gestion des groupes d'invités
    /// </summary>
    public interface IGroupeInviteDAO
    {

        /// <summary>
        /// Ajoute un groupe d'invités dans la base de données
        /// </summary>
        /// <param name="groupeInvites"> le groupe a ajouter representer par son nom </param>
        GroupeInvites AjouterGroupeInvite(GroupeInvites groupeInvites);

        /// <summary>
        /// Ajouter un invité à un groupe d'invités
        /// <param name="idGroupeInvites">l'id du groupe d'invités</param>
        /// <param name="invite">l'invité à ajouter</param>
        GroupeInvites AjouterInviteAuGroupe(long idGroupeInvites, Invite invite);

        /// <summary>
        /// Récupère tous les groupes d'invités
        /// </summary>
        /// <returns> les groupes d'invite</returns>
        List<GroupeInvites> RecupererTousGroupesInvites();

        /// <summary>
        /// Récupère les invités d'un groupe via son ID
        /// </summary>
        /// <param name="idGroupeInvites"></param>
        /// <returns>le groupe avec son nom et ses invites</returns>
        GroupeInvites RecupereGroupeViaId(long idGroupeInvite);

        /// <summary>
        /// Modifie un groupe d'invités dans la base de données
        /// </summary>
        /// <param name="groupeInvite">le groupe d'invite</param>
        /// <returns>le groupe d'invite modifier</returns>
        GroupeInvites ModifierGroupe(GroupeInvites groupeInvite);

    }
}
