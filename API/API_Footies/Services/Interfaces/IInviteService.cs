using API_Footies.Metier;

namespace API_Footies.Services.Interfaces
{
    /// <summary>
    /// Service en charge de la gestion des invités
    /// </summary>
    public interface IInviteService
    {
        /// <summary>
        /// Ajouter un invité dans la base de données
        /// </summary>
        /// <param name="invite"> Invite à ajouter </param>
        /// <returns> Invité ajouter </returns>
        void AjouterInvite(Invite invite);
        /// <summary>
        /// Modifier un invité
        /// </summary>
        /// <param name="invite">invite à modifier</param>
        void ModifierInvite(Invite invite);

        /// <summary>
        /// Récupérer la liste des invités
        /// </summary>
        /// <returns> liste des invités</returns>
        public List<Invite> ListInvite();
        /// Supprimer un invité de la base de données
        /// </summary>
        /// <param name="id"> id de l'invité à supprimer </param>
        void SupprimerInvite(long id);

        /// <summary>
        /// Vérifie si un invité est associé à un ou plusieurs groupes
        /// </summary>
        /// <param name="idInvite">L'id de l'invité</param>
        /// <returns>True si l'invité fait partie d'au moins un groupe, False sinon</returns>
        bool EstDansUnGroupe(long idInvite);
    }
}
