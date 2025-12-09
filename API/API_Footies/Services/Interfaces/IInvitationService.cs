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
        void AjouterInvitation(Invitation invitation);

        /// <summary>
        /// Modifier une invitation dans la base de données
        /// </summary>
        /// <param name="invitation"> Invitation à modifier </param>
        void ModifierInvitation(Invitation invitation);

        /// <summary>
        /// Récupérer toutes les invitations dans la base de données
        /// </summary>
        /// <returns> La liste de toutes les invitations </returns>
        List<Invitation> ObtenirToutInvitations();

        /// <summary>
        /// Supprimer une invitation de la base de données
        /// </summary>
        /// <param name="idInvitation"> id de l'invitation à supprimer </param>
        void SupprimerInvitation(long idInvitation);

        /// <summary>
        /// Chercher des invitations par leur nom
        /// </summary>
        /// <param name="InvitationsRechercher"> le nom ou une partie du nom de l'invitation à rechercher </param>
        /// <returns> la liste des invitations correspondants </returns>
        public List<Invitation> ChercherInvitations(string InvitationsRechercher);
    }
}