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
        public void AjouterMenu(Menu menu, long idUtilisateur)
        {
            this.dao.AjouterMenu(menu, idUtilisateur);
        }

        public List<Menu> ListMenu(long idUtilisateur)
        {
            return this.dao.ListMenu(idUtilisateur);
        }

        public void ModifierMenu(Menu menu, long idUtilisateur)
        {
            this.dao.ModifierMenu(menu, idUtilisateur);
        }

        public void SupprimerMenu(long idMenu, long idUtilisateur)
        {
            this.dao.SupprimerMenu(idMenu, idUtilisateur);
        }

        public List<Menu> ChercherMenus(string menuRechercher, long idUtilisateur)
        {
            return this.dao.ChercherMenus(menuRechercher, idUtilisateur);
        }
        #endregion
    }
}
