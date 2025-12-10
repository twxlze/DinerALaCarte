using System.ComponentModel;
using API_Footies.Metier;

namespace API_Footies.Services.Interfaces
{
    /// <summary>
    /// Fournit des services pour gérer les plats
    /// </summary>
    public interface IPlatService
    {
        /// <summary>
        /// Ajouter un plat dans la base de données
        /// </summary>
        /// <param name="plat"> Plat à ajouter </param>
        /// <returns> Plat ajouter </returns>
        void AjouterPlat(Plat plat, long idUtilisateur);

        /// <summary>
        /// Modifier un plat
        /// </summary>
        /// <param name="plat">Plat à modifier</param>
        void ModifierPlat(Plat plat, long idUtilisateur);

        /// <summary>
        /// Supprimer un plat
        /// </summary>
        /// <param name="id"> Id du plat à supprimer </param>
        void SupprimerPlat(long id, long idUtilisateur);

        /// <summary>
        /// Liste des plats
        /// </summary>
        /// <returns> Liste des plats </returns>
        public List<Plat> ListPlat(long idUtilisateur);

        /// <summary>
        /// Vérifie si un plat est dans un menu
        /// </summary>
        /// <param name="idInvite"> Id du plat </param>
        /// <returns> retourne true si le plat est dans un menu </returns>
        bool EstDansUnMenu(long idInvite);

        /// <summary>
        /// Cherche un plat via un texte de recherche
        /// </summary>
        /// <param name="texterecherche">Le texte permettant d'un plat</param>
        /// <returns>Une liste de plats correspondant à la recherche</returns>
        public List<Plat> ChercherPlat(string texterecherche,long idUtilisateur);
    }
}