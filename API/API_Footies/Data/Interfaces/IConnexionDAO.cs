using API_Footies.Metier;

namespace API_Footies.Data.Interfaces
{
    public interface IConnexionDAO 
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

        /// <summary>
        /// Permet d'ajouter un identifiant et un utilisateur dans la base de données (oblgatoire de réaliser les deux ensembles car ils ne sont pas séparable)
        /// </summary>
        /// <param name="identifiant">les identifiant de l'utilisateur</param>
        /// <param name="utilisateur">l'utilisateur à ajouter</param>
        /// <returns>true si tout à bien été ajouter</returns>
        bool AjouterIdentifiantEtUtilisateur(Identifiant identifiant, Utilisateur utilisateur);
    }
}
