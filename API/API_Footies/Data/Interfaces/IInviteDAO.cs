using API_Footies.Metier;

namespace API_Footies.Data.Interfaces
{
    /// <summary>
    /// Définit la gestion des opérations liées aux invités
    /// </summary>
    public interface IInviteDAO
    {
        /// <summary>
        /// Ajouter un invité
        /// </summary>
        /// <param name="invite"> Invité à ajouter </param>
        /// <returns> True si ajouté False sinon </returns>
        bool AjouterInvite(Invite invite);
        /// <summary>
        /// Modifier un invité
        /// </summary>
        /// <param name="invite">L'invité</param>

        bool ModifierInvite(Invite invite);
    }
}
