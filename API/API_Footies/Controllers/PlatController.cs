using System.Security.Principal;
using API_Footies.Metier;
using API_Footies.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API_Footies.Controllers
{
    /// <summary>
    /// Controlleur en charge de tout ce qui touche au plats
    /// </summary>

    [ApiController]
    [Route("Plats")]
    public class PlatController : ControllerBase
    {
        private IPlatService service;

        /// <summary>
        /// Constructeur
        /// </summary>
        public PlatController(IPlatService service)
        {
            this.service = service;
        }


        /// <summary>
        /// Ajoute un plat
        /// </summary>
        /// <param name="plat">plat à ajouter</param>
        /// <returns>Le plat avec Id modifié</returns>
        [HttpPost("Ajoutplat")]
        public Metier.Plat AjouterPlat(Metier.Plat plat, long idUtilisateur)
        {
            this.service.AjouterPlat(plat, idUtilisateur);
            return plat;
        }

        /// <summary>
        /// Modifier un plat
        /// </summary>
        /// <param name="plat">le plat</param>
        [HttpPut("ModifierPlat")]
        public void ModifierPlat(Metier.Plat plat, long idUtilisateur)
        {
            this.service.ModifierPlat(plat, idUtilisateur);
        }

        /// <summary>
        /// Supprimer un plat
        /// </summary>
        /// <param name="id"> id du plat à supprimé </param>
        [HttpDelete("SupprimerPlat")]
        public void SupprimerPlat(long id, long idUtilisateur)
        {
            this.service.SupprimerPlat(id, idUtilisateur);
        }

        /// <summary>
        /// Récupérer la liste des plats
        ///</summary>
        [HttpGet("ListePlat")]
        public List<Metier.Plat> ListPlat(long idUtilisateur)
        {
            return this.service.ListPlat(idUtilisateur);
        }

        /// <summary>
        /// Vérifie si un palt est associé à un ou plusieurs menu
        /// </summary>
        /// <param name="id">id du plat</param>
        /// <returns>True si le plat fait partie d'au moins un menu, False sinon</returns>
        [HttpGet("EstDansUnMenu")]
        public bool EstDansUnMenu(long id)
        {
            return this.service.EstDansUnMenu(id);
        }

        /// <summary>
        /// Recherche des plats via un texte de recherche
        /// </summary>
        /// <param name="texterecherche">Le texte permettant de rechercher un plat</param>
        /// <returns>Une liste de plats correspondant à la recherche</returns>
        [HttpGet("ChercherPlat")]
        public List<Metier.Plat> ChercherPlat(string texterecherche, long idUtilisateur)
        {
            return this.service.ChercherPlat(texterecherche, idUtilisateur);
        }

        /// <summary>
        /// Ajoute ou modifie une note et un commentaire pour un plat
        /// </summary>
        /// <param name="avis">L'objet contenant les IDs, la note et le commentaire</param>
        [HttpPost("AjouterAvis")]
        public IActionResult AjouterAvis(Avis avis)
        {
            try
            {
                this.service.AjouterAvis(avis);
                return Ok("Avis enregistré avec succès.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message); // Erreur 400 si note invalide (ex: 12/10)
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erreur serveur : " + ex.Message);
            }
        }

    }
}
