using API_Footies.Metier;
using API_Footies.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API_Footies.Controllers
{
    /// <summary>
    /// Controlleur en charge de tout ce qui touche au menus
    /// </summary>

    [ApiController]
    [Route("Menus")]
    public class MenuController : ControllerBase
    {
        private IMenuService service;

        /// <summary>
        /// Constructeur
        /// </summary>
        public MenuController(IMenuService service)
        {
            this.service = service;
        }


        /// <summary>
        /// Ajoute un Menu
        /// </summary>
        /// <param name="menu">menu à ajouter</param>
        /// <returns>Le menu avec Id modifié</returns>
        [HttpPost("AjoutMenu")]
        public Metier.Menu AjouterMenu(Menu menu)
        {
            this.service.AjouterMenu(menu);
            return menu;
        }

        /// <summary>
        /// Modifier un menu
        /// </summary>
        /// <param name="menu">le menus</param>
        [HttpPut("ModifierMenu")]
        public void ModifierMenu(Metier.Menu menu)
        {
            this.service.ModifierMenu(menu);
        }

        /// <summary>
        /// Supprimer un menu
        /// </summary>
        /// <param name="idMenu"> id du menu à supprimé </param>
        /// <returns></returns>
        [HttpDelete("SupprimerMenu")]
        public void SupprimerMenu(long idMenu)
        {
            this.service.SupprimerMenu(idMenu);
        }


        /// <summary>
        /// Récupérer la liste des menu
        ///</summary>
        [HttpGet("ListeMenu")]
        public List<Metier.Menu> ListMenu()
        {
            return this.service.ListMenu();
        }
    }
}
