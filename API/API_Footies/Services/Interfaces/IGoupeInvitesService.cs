using API_Footies.Metier;

namespace API_Footies.Services.Interfaces
{
    /// <summary>
    /// Interface pour le service en charge de la gestion des groupes d'invités
    /// </summary>
    public interface IGroupeInvitesService
    {
        /// <summary>
        /// créer un groupe d'invités dans la base de données
        /// </summary>
        /// <param name="groupeInvites"> le groupe a ajouter</param>
        bool AjouterGroupeInvites(GroupeInvites groupeInvites, long IdUtilisateur);

        /// <summary>
        /// Récupère tous les groupes d'invités
        /// </summary>
        /// <returns> les groupes d'invite</returns>
        List<GroupeInvites> ListeGroupesInvites( long IdUtilisateur);

        /// <summary>
        /// Modifie un groupe d'invités dans la base de données
        /// </summary>
        /// <param name="groupeInvite">le groupe d'invite</param>
        /// <returns>le groupe d'invite modifier</returns>
        bool ModifierGroupeInvite(GroupeInvites groupeInvite, long IdUtilisateur);

        /// <summary>
        ///  Supprime un groupe d'invités via son ID
        ///</summary>
        void SupprimerGroupe(long idGroupeInvite, long IdUtilisateur);

        /// <summary>
        /// Recherche des groupes d'invités via un texte de recherche
        /// </summary>
        /// <param name="GroupeInvitesRechercher">le texte en question </param>
        /// <returns>une liste de groupe invite dont le nom resemble au texte de la barre de recherche</returns>
        List<GroupeInvites> ChercherGroupeInvites(string GroupeInvitesRechercher, long IdUtilisateur);
    }
}
