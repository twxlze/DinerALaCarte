using API_Footies.Metier;

namespace API_Footies.Services.Interfaces
{
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

    }
}
