using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using METIER_Footies.Metier;

namespace METIER_Footies.Data.Interfaces
{
    public interface IInviteDAO
    {
        /// <summary>
        /// Ajoute un invité
        /// </summary>
        /// <param name="invite"> l'invité à ajouter </param>
        /// <returns> Réponse http de l'API </returns>
        Task<HttpResponseMessage> AjouterInvite(Invite invite);

        /// <summary>
        /// Vérifie si un invité est associé à un ou plusieurs groupes
        /// </summary>
        /// <param name="idInvite">L'id de l'invité</param>
        /// <returns>True si l'invité fait partie d'au moins un groupe, False sinon</returns>
        Task<bool> EstDansUnGroupe(long idInvite);

        /// <summary>
        /// Modifier un invité
        /// </summary>
        /// <param name="invite"> L'invité à modifier </param>
        /// <returns> Réponse http de l'API </returns>
        Task<HttpResponseMessage> ModifierInvite(Invite invite);

        /// <summary>
        /// Obtient tous les invités
        /// </summary>
        /// <returns> Liste de tous les invités </returns>
        Task<List<Invite>> ObtenirTout();

        /// <summary>
        /// Supprime un invité
        /// </summary>
        Task<HttpResponseMessage> SupprimerInvite(long idInvite);
    }
}
