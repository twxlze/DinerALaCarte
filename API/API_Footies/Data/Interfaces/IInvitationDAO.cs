using API_Footies.Metier;

namespace API_Footies.Data.Interfaces
{
    public interface IInvitationDAO
    {
        /// <summary>
        /// Ajouter une invitation dans la base de données
        /// </summary>
        /// <param name="invitation"> Invitation à ajouter </param>
        /// <returns> True si l'invitation a été ajoutée, False sinon </returns>
        bool AjouterInvitation(Invitation invitation);

        /// <summary>
        /// Modifier une invitation dans la base de données
        /// </summary>
        /// <param name="invitation"> Invitation à modifier </param>
        /// <returns> True si l'invitation a été modifiée, False sinon </returns>
        bool ModifierInvitation(Invitation invitation);

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
    }
}