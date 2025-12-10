using API_Footies.Metier;

namespace API_Footies.Data.Interfaces
{
    /// <summary>
    /// Définit la gestion des opérations liées aux menus
    /// </summary>
    public interface IMenuDAO
    {
        /// <summary>
        /// Ajouter un menu
        /// </summary>
        /// <param name="menu"> menu à ajouter </param>
        /// <returns> True si ajouté False sinon </returns>
        bool AjouterMenu(Menu menu, long idUtilisateur);

        /// <summary>
        /// Modifier un menu
        /// </summary>
        /// <param name="menu">le menu</param>
        bool ModifierMenu(Menu menu, long idUtilisateur);

        /// <summary>
        /// Récupérer la liste des menus de la base de données
        /// </summary>
        /// <returns> liste des menus </returns>
        public List<Menu> ListMenu(long idUtilisateur);

        /// Supprimer un menus
        /// </summary>
        /// <param name="id"> id du menu à supprimer </param>
        public void SupprimerMenu(long idMenu, long idUtilisateur);

        /// <summary>
        /// Chercher des menus par nom
        /// </summary>
        /// <param name="menuRechercher"> le nom ou une partie du nom du menu à rechercher </param>
        /// <returns> la liste des menus correspondants </returns>
        List<Menu> ChercherMenus(string menuRechercher, long idUtilisateur);

    }
}
