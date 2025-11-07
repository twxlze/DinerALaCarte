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
        //Service en charge des plats
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
        public Metier.Plat AjouterInvite(Metier.Plat plat)
        {
            this.service.AjouterPlat(plat);
            return plat;
        }

        /// <summary>
        /// Modifier un plat
        /// </summary>
        /// <param name="plat">le plat</param>
        [HttpPut("ModifierPlat")]
        public void ModifierPlat(Metier.Plat plat)
        {
            this.service.ModifierPlat(plat);
        }

        /// <summary>
        /// Supprimer un plat
        /// </summary>
        /// <param name="id"> id du plat à supprimé </param>
        [HttpDelete("SupprimerPlat")]
        public void SupprimerPlat(long id)
        {
            this.service.SupprimerPlat(id);
        }

        /// <summary>
        /// Récupérer la liste des plats
        ///</summary>
        [HttpGet("ListePlat")]
        public List<Metier.Plat> ListPlat()
        {
            return this.service.ListPlat();
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
    }
}
