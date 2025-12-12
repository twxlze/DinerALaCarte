using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using METIER_Footies.Metier;

namespace METIER_Footies.Data.Interfaces
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

        /// <summary>
        /// Cherche un plat via un texte de recherche
        /// </summary>
        /// <param name="texterecherche">Le texte permettant de un plat</param>
        /// <returns>Une liste de plats correspondant à la recherche</returns>
        Task<List<Plat>> ChercherPlat(string texterecherche);

        /// <summary>
        /// Ajoute un avis pour un plat
        /// </summary>
        /// <param name="avis"> avis du plat </param>
        /// <returns> réponse HTTP de l'API </returns>
        public Task<HttpResponseMessage> AjouterAvis(Avis avis);

    }
}

