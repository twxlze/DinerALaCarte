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

        /// <summary>
        /// Vérifie si un pseudo est disponible
        /// </summary>
        /// <param name="pseudo">le pseudo à rechercher</param>
        /// <returns>true si le pseudo est disponible</returns>
        bool VerifierPseudoDisponible(string pseudo);

        /// <summary>
        /// Inscrit un nouvel utilisateur
        /// </summary>
        /// <param name="identifiant">L'identifiant de l'utilisateur à ajouter</param>
        /// <param name="utilisateur">l'utilisateur à ajouter</param>
        /// <returns></returns>
        bool Inscription(Identifiant identifiant, Utilisateur utilisateur);
    }
}
