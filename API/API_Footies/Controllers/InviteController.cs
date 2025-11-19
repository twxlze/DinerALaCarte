using API_Footies.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API_Footies.Controllers
{
    /// <summary>
    /// Controlleur en charge de tout ce qui touche au invités
    /// </summary>

    [ApiController]
    [Route("Invites")]
    public class InviteController : ControllerBase
    {
        private IInviteService service;

        /// <summary>
        /// Constructeur
        /// </summary>
        public InviteController(IInviteService service)
        {
            this.service = service;
        }


        /// <summary>
        /// Ajoute un invité
        /// </summary>
        /// <param name="invite">invité à ajouter</param>
        /// <returns>L'invité avec Id modifié</returns>
        [HttpPost("AjoutInvite")]
        public Metier.Invite AjouterInvite(Metier.Invite invite)
        {
            try
            {
                this.service.AjouterInvite(invite);
                return invite;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de l'ajout de l'invité : " + ex.Message);
            }
        }

        /// <summary>
        /// Modifier un invité
        /// </summary>
        /// <param name="invite">l'invité</param>
        [HttpPut("ModifierInvite")]
        public void ModifierInvite(Metier.Invite invite)
        {
            this.service.ModifierInvite(invite);
        }

        /// <summary>
        /// Supprimer un invité
        /// </summary>
        /// <param name="id"> id de l'invité à supprimé </param>
        [HttpDelete("SupprimerInvite")]
        public void SupprimerInvite(long id)
        {
            this.service.SupprimerInvite(id);
        }


        /// <summary>
        /// Récupérer la liste des invités
        ///</summary>
        [HttpGet("ListeInvite")]
        public List<Metier.Invite> ListInvite()
        {
            return this.service.ListInvite();
        }

        /// <summary>
        /// Vérifie si un invité est associé à un ou plusieurs groupes
        /// </summary>
        /// <param name="id">id de l'invité</param>
        /// <returns>True si l'invité fait partie d'au moins un groupe, False sinon</returns>
        [HttpGet("EstDansUnGroupe")]
        public bool EstDansUnGroupe(long id)
        {
            return this.service.EstDansUnGroupe(id);
        }
    }

}
