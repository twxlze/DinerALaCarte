using System.Data;
using API_Footies.Metier;

namespace API_Footies.Data.Interfaces
{
    /// <summary>
    /// interface pour le DAO en charge de la gestion des utilisateurs
    /// </summary>
    public interface IUtilisateurDAO
    {
        /// <summary>
        /// Permet de récupérer un utilisateur par son pseudo
        /// </summary>
        /// <param name="pseudo">le pseudo de l'utilisateur</param>
        /// <returns>l'utilisateur avec sont pseudo</returns>
        Utilisateur RecupererUtilisateurParPseudo(string pseudo);

        /// <summary>
        /// Permet de récupérer l'identifiant d'un utilisateur par son pseudo
        /// </summary>
        /// <param name="pseudo">le pseudo</param>
        /// <returns>l'identifiant de l'utilisateur</returns>
        Identifiant RecupererIdentifiantParPseudo(string pseudo);
    }
}
