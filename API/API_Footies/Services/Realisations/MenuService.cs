using API_Footies.Data.Interfaces;
using API_Footies.Metier;
using API_Footies.Services.Interfaces;

namespace API_Footies.Services.Realisations
{
    public class MenuService : IMenuService
    {
        #region attributs

        private IMenuDAO dao;

        /// <summary>
        /// Initialise une nouvelle instance de la classe MenuService.
        /// </summary>
        /// <param name="dao">Injection de dépendance</param>
        public MenuService(IMenuDAO dao)
        {
            this.dao = dao;
        }

        #endregion

        #region Méthodes
        public void AjouterMenu(Menu menu)
        {
            this.dao.AjouterMenu(menu);
        }

        public List<Menu> ListMenu()
        {
            return this.dao.ListMenu();
        }

        public void ModifierMenu(Menu menu)
        {
            this.dao.ModifierMenu(menu);
        }

        public void SupprimerMenu(long idMenu)
        {
            this.dao.SupprimerMenu(idMenu);
        }

        public List<Menu> ChercherMenus(string menuRechercher)
        {
            return this.dao.ChercherMenus(menuRechercher);
        }
        #endregion
    }
}
