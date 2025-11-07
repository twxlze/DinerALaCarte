using System.ComponentModel;
using API_Footies.Metier;

namespace API_Footies.Data.Interfaces
{
    /// <summary>
    /// Définit la gestion des opérations liées aux plats
    /// </summary>
    public interface IPlatDAO
    {
        /// <summary>
        /// Ajouter un plat
        /// </summary>
        /// <param name="plat"> Plat à ajouter </param>
        /// <returns> True si ajouté False sinon </returns>
        bool AjouterPlat(Plat plat);

        /// <summary>
        /// Modifier un plat
        /// </summary>
        /// <param name="plat">Le plat à modifier</param>
        /// <returns>True si modifié False sinon</returns>
        bool ModifierPlat(Plat plat);
    }
}
