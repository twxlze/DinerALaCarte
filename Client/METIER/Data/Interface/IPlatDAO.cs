using METIER_Footies.Metier;

namespace METIER_Footies.Data.Interface
{
    public interface IPlatDAO
    {
        /// <summary>
        /// Ajoute un plat
        /// </summary>
        /// <param name="plat"> le plat à ajouter </param>
        /// <returns> Réponse http de l'API </returns>
        Task<HttpResponseMessage> AjouterPlat(Plat plat);

        /// <summary>
        /// Vérifie si un plat est associé à un ou plusieurs menus
        /// </summary>
        /// <param name="idPlat">L'id du plat</param>
        /// <returns>True si le plat fait partie d'au moins un menu, False sinon</returns>
        Task<bool> EstDansUnMenu(long idPlat);

        /// <summary>
        /// Modifier un plat
        /// </summary>
        /// <param name="plat"> Le plat à modifier </param>
        /// <returns> Réponse http de l'API </returns>
        Task<HttpResponseMessage> ModifierPlat(Plat plat);

        /// <summary>
        /// Obtient tous les plats
        /// </summary>
        /// <returns> Liste de tous les plats </returns>
        Task<List<Plat>> ObtenirTout();

        /// <summary>
        /// Supprime un invité
        /// </summary>
        Task<HttpResponseMessage> SupprimerPlat(long idPlat);
    }
}