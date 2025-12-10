using API_Footies.Metier;

namespace API_Footies.Services.Interfaces
{
    /// <summary>
    /// Fournit des services pour gérer les invitations
    /// </summary>
    public interface IInvitationService
    {
        /// <summary>
        /// Ajouter une invitation dans la base de données
        /// </summary>
        /// <param name="invitation"> Invitation à ajouter </param>
        void AjouterInvitation(Invitation invitation, long IdUtilisateur);

        /// <summary>
        /// Modifier une invitation dans la base de données
        /// </summary>
        /// <param name="invitation"> Invitation à modifier </param>
        void ModifierInvitation(Invitation invitation, long IdUtilisateur);

        /// <summary>
        /// Récupérer toutes les invitations dans la base de données
        /// </summary>
        /// <returns> La liste de toutes les invitations </returns>
        List<Invitation> ObtenirToutInvitations(long IdUtilisateur);

        /// <summary>
        /// Supprimer une invitation de la base de données
        /// </summary>
        /// <param name="idInvitation"> id de l'invitation à supprimer </param>
        void SupprimerInvitation(long idInvitation, long IdUtilisateur);
    }
}