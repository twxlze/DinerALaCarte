using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using METIER_Footies.Metier;

namespace METIER_Footies.Data.Interfaces
{
    /// <summary>
    /// Interface pour les opérations de connexion
    /// </summary>
    public interface IConnexionDAO
    {
        /// <summary>
        /// Connexion d'un utilisateur
        /// </summary>
        /// <param name="identifiant"> les informations de l'utilisateur à connecter si les informations correspondent </param>
        /// <returns> L'utilisateur connecté avec son ID, ou null si la connexion a échoué </returns>
        Task<Utilisateur?> Connexion(Identifiant identifiant);

        /// <summary>
        /// Vérifie si un pseudo est disponible
        /// </summary>
        /// <param name="pseudo">le pseudo à vérifier</param>
        /// <returns>true si le pseudo est disponible</returns>
        Task<bool> VerifierPseudoDisponible(string pseudo);

        /// <summary>
        /// Créer un nouvel utilisateur
        /// </summary>
        /// <param name="utilisateur">L'utilisateur à créer</param>
        /// <returns></returns>
        Task<bool> CreerUnUtilisateur(Utilisateur utilisateur);

    }
}
