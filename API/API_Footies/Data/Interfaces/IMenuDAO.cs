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
        bool AjouterMenu(Menu menu);

        /// <summary>
        /// Modifier un menu
        /// </summary>
        /// <param name="menu">le menu</param>
        bool ModifierMenu(Menu menu);

        /// <summary>
        /// Récupérer la liste des menus de la base de données
        /// </summary>
        /// <returns> liste des menus </returns>
        public List<Menu> ListMenu();

        /// Supprimer un menus
        /// </summary>
        /// <param name="id"> id du menu à supprimer </param>
        public void SupprimerMenu(long idMenu);
    }
}
