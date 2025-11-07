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

        /// <summary>
        /// Récupère tous les groupes d'invités
        /// </summary>
        /// <returns>tous les groupes d'invite</returns>
        [HttpGet("RecupererTousGroupesInvites")]
        public List<GroupeInvites> RecupererTousGroupesInvites()
        {
            return _groupeInvitesService.RecupererTousGroupesInvites().ToList();
        }

        /// <summary>
        /// Ajoute un nouveau groupe d'invités
        /// </summary>
        /// <param name="groupeInvites">le groupe d'invite</param>
        /// <returns>un code pour designer si reussi ou pas</returns>
        [HttpPost("AjouterUnGroupeInvite")]
        public IActionResult AjouterGroupeInvite(GroupeInvites groupeInvites)
        {
            var groupeAjoute = _groupeInvitesService.AjouterGroupeInvite(groupeInvites);
            return Ok(groupeAjoute);
        }

        /// <summary>
        /// Ajouter un invité à un groupe d'invités
        /// </summary>
        /// <param name="idGroupeInvites"></param>
        /// <param name="invite"></param>
        /// <returns>un code pour designer si reussi ou pas</returns>
        [HttpPost("AjouterInviteAuGroupe/{idGroupeInvites}")]
        public IActionResult AjouterInviteAGroupe(long idGroupeInvites, Invite invite)
        {
            var groupeMisAJour = _groupeInvitesService.AjouterInviteAuGroupe(idGroupeInvites, invite);

            IActionResult resultat;

            if (groupeMisAJour == null)
            { resultat = NotFound(); }
            else
            { resultat = Ok(groupeMisAJour); }

            return resultat;
        }

        /// <summary>
        /// Modifie un groupe d'invités
        /// </summary>
        /// <param name="groupeInvite">le groupe</param>
        /// <returns>un code pour designer si reussi ou pas + le json de groupe</returns>
        [HttpPut("ModifierUnGroupe")]
        public IActionResult ModifierGroupe(GroupeInvites groupeInvite)
        {
            var groupeMisAJour = _groupeInvitesService.ModifierGroupe(groupeInvite);

            IActionResult resultat;

            if (groupeMisAJour == null)
            { resultat = NotFound(); }
            else
            { resultat = Ok(groupeMisAJour); }
            return resultat;
        }

        /// <summary>
        /// Récupère les invités d'un groupe via son ID
        /// </summary>
        /// <param name="idGroupeInvites">l'id du groupe</param>
        /// <returns>les invites du groupe</returns>
        [HttpGet("RecupererGroupeParId/{idGroupeInvites}")]
        public GroupeInvites RecupererGroupe(long idGroupeInvites)
        {
            return _groupeInvitesService.RecupereGroupeViaId(idGroupeInvites);
        }

        /// <summary>
        /// Supprime un groupe d'invités via son ID
        /// </summary>
        [HttpDelete("SupprimerGroupe/{idGroupeInvite}")]
        public IActionResult SupprimerGroupe(long idGroupeInvite)
        {
            GroupeInvites groupeSupprime = _groupeInvitesService.SupprimerGroupe(idGroupeInvite);
            IActionResult resultat;
            if (groupeSupprime == null)
            { resultat = NotFound(); }
            else
            { resultat = Ok(groupeSupprime); }
            return resultat;
        }

    }
}
