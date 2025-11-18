using API_Footies.Data.Interfaces;
using API_Footies.Metier;
using API_Footies.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace API_Footies.Controllers
{
    /// <summary>
    /// Contrôleur pour la gestion des groupes d'invités
    /// </summary>
    [ApiController]
    [Route("GroupeInvites")]
    public class GroupeInvitesController : ControllerBase
    {
        private IGroupeInvitesService _groupeInvitesService;

        /// <summary>
        /// Constructeur du contrôleur de gestion des groupes d'invités
        /// </summary>
        /// <param name="groupeInvitesService">injection de dependance avec les services GroupeInvites</param>
        public GroupeInvitesController(IGroupeInvitesService groupeInvitesService)
        {
            this._groupeInvitesService = groupeInvitesService;
        }

        #region Méthodes
        /// <summary>
        /// Ajoute un nouveau groupe d'invités
        /// </summary>
        /// <param name="groupeInvites">le groupe d'invite</param>
        /// <returns>un code pour designer si reussi ou pas</returns>
        [HttpPost("AjoutGroupeInvite")]
        public IActionResult AjouterGroupeInvite(GroupeInvites groupeInvites)
        {
            bool ajouter = this._groupeInvitesService.AjouterGroupeInvites(groupeInvites);

            IActionResult resultat;
            if (ajouter == false)
            { resultat = NotFound(); }
            else
            { resultat = Ok(groupeInvites); }

            return resultat;
        }

        /// <summary>
        /// Modifie un groupe d'invités
        /// </summary>
        /// <param name="groupeInvite">le groupe</param>
        /// <returns>un code pour designer si reussi ou pas + le json de groupe</returns>
        [HttpPut("ModifierGroupeInvite")]
        public IActionResult ModifierGroupe(GroupeInvites groupeInvite)
        {
            bool modifie = this._groupeInvitesService.ModifierGroupeInvite(groupeInvite);

            IActionResult resultat;
            if (modifie == false)
            { resultat = NotFound(); }
            else
            { resultat = Ok(groupeInvite); }

            return resultat;
        }

        /// <summary>
        /// Supprime un groupe d'invités via son ID
        /// </summary>
        [HttpDelete("SupprimerGroupeInvite")]
        public void SupprimerGroupe(long idGroupeInvite)
        {
            this._groupeInvitesService.SupprimerGroupe(idGroupeInvite);
        }

        /// <summary>
        /// Récupérer la liste des groupes d'invités
        ///</summary>
        [HttpGet("ListeGroupeInvites")]
        public List<GroupeInvites> ListGroupeInvites()
        {
            return this._groupeInvitesService.ListeGroupesInvites();
        }
        #endregion

    }
}
