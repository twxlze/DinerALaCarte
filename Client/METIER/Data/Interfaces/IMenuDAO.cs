using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using METIER_Footies.Metier;

namespace METIER_Footies.Data.Interfaces
{
    public interface IMenuDAO
    {
        /// <summary>
        /// Ajoute un menu
        /// </summary>
        /// <param name="menu"> le menu à ajouter </param>
        /// <returns> Réponse http de l'API </returns>
        Task<HttpResponseMessage> AjouterMenu(Menu menu);

        /// <summary>
        /// Modifier un menu
        /// </summary>
        /// <param name="menu"> Le menu à modifier </param>
        /// <returns> Réponse http de l'API </returns>
        Task<HttpResponseMessage> ModifierMenu(Menu menu);

        // <summary>
        /// Obtient tous les menus
        /// </summary>
        /// <returns> Liste de tous les menus </returns>
        Task<List<Menu>> ObtenirTousLesMenus();

        /// <summary>
        /// Supprime un menu
        /// </summary>
        Task<HttpResponseMessage> SupprimerMenu(long idMenu);
    }
}
