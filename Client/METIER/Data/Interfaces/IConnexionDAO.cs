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
        /// <param name="Utilisateur"> l'utilisateur à connecter si les informations conrresponde</param>
        /// <returns> La réponse HTTP de l'API </returns>
        Task<HttpResponseMessage> Connexion(Utilisateur utilisateur);

    }
}
