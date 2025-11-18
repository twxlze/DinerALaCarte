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
        void AjouterGroupeInvites(GroupeInvites groupeInvites);

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
        void ModifierGroupeInvite(GroupeInvites groupeInvite);

        /// <summary>
        ///  Supprime un groupe d'invités via son ID
        ///</summary>
        void SupprimerGroupe(long idGroupeInvite);
    }
}
