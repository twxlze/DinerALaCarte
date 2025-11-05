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
    }
}
