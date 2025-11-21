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
        bool AjouterGroupeInvites(GroupeInvites groupeInvites);

        /// <summary>
        /// Récupère tous les groupes d'invités
        /// </summary>
        /// <returns> les groupes d'invite</returns>
        List<GroupeInvites> ListeGroupesInvites();

        /// <summary>
        /// Modifie un groupe d'invités dans la base de données
        /// </summary>
        /// <param name="groupeInvite">le groupe d'invite</param>
        /// <returns>le groupe d'invite modifier</returns>
        bool ModifierGroupe(GroupeInvites groupeInvite);

        /// <summary>
        /// SUpprime un groupe d'invités via son ID
        /// </summary>
        /// <param name="idGroupeInvite">L'id du groupe à supprimer</param>
        void SupprimerGroupeInvite(long idGroupeInvite);

        /// <summary>
        /// Recherche des groupes d'invités via un texte de recherche
        /// </summary>
        /// <param name="GroupeInvitesRechercher">le texte en question </param>
        /// <returns>une liste de groupe invite dont le nom resemble au texte de la barre de recherche</returns>
        List<GroupeInvites> ChercherGroupeInvites(string GroupeInvitesRechercher);

    }
}
