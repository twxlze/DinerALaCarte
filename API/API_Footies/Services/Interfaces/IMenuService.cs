using API_Footies.Metier;

namespace API_Footies.Services.Interfaces
{
    /// <summary>
    /// Service en charge de la gestion des menus
    /// </summary>
    public interface IMenuService
    {
        /// <summary>
        /// Ajouter un menu dans la base de données
        /// </summary>
        /// <param name="menu"> menu à ajouter </param>
        /// <returns> menu ajouter </returns>
        void AjouterMenu(Menu menu);
        /// <summary>
        /// Modifier un menu
        /// </summary>
        /// <param name="menu">menu à modifier</param>
        void ModifierMenu(Menu menu);

        /// <summary>
        /// Récupérer la liste des menus
        /// </summary>
        /// <returns> liste des menus</returns>
        public List<Menu> ListMenu();

        /// Supprimer un menu de la base de données
        /// </summary>
        /// <param name="idMenu"> id du menu à supprimer </param>
        void SupprimerMenu(long idMenu);

        /// <summary>
        /// Chercher des menus par nom
        /// </summary>
        /// <param name="menuRechercher"> le nom ou une partie du nom du menu à rechercher </param>
        /// <returns> la liste des menus correspondants </returns>
        List<Menu> ChercherMenus(string menuRechercher);
    }
}
