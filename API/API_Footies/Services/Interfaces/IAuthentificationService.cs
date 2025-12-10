using API_Footies.Metier;

namespace API_Footies.Services.Interfaces
{
    public interface IAuthentificationService
    {
        /// <summary>
        /// Vérifie si la connexion de l'utilisateur est bonne (bon mdp pour cette utilisateur)
        /// </summary>
        /// <param name="pseudo">le pseudo de l'utilisateur</param>
        /// <param name="mdp">le mot de passe de l'utilisateur</param>
        /// <returns></returns>
        Utilisateur VerifierConnexion(string pseudo, string mdp);
    }
}
