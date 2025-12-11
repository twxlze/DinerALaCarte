using System.ComponentModel;
using API_Footies.Metier;

namespace API_Footies.Data.Interfaces
{
    /// <summary>
    /// Définit la gestion des opérations liées aux plats
    /// </summary>
    public interface IPlatDAO
    {
        /// <summary>
        /// Ajouter un plat
        /// </summary>
        /// <param name="plat"> Plat à ajouter </param>
        /// <returns> True si ajouté False sinon </returns>
        bool AjouterPlat(Plat plat, long idUtilisateur);

        /// <summary>
        /// Modifier un plat
        /// </summary>
        /// <param name="plat">Le plat à modifier</param>
        /// <returns>True si modifié False sinon</returns>
        bool ModifierPlat(Plat plat, long idUtilisateur);

        /// <summary>
        /// Supprimer un plat
        /// </summary>
        /// <param name="id"> Id du plat à supprimer </param>
        void SupprimerPlat(long id, long idUtilisateur);

        /// <summary>
        // Liste des plats
        /// </summary>
        /// <returns> Liste des plats </returns>
        public List<Plat> ListPlat(long idUtilisateur);

        /// <summary>
        /// Vérifie si un plat est dans un menu
        /// </summary>
        /// <param name="idInvite"> Id du plat </param>
        /// <returns></returns>
        bool EstDansUnMenu(long idInvite);

        /// <summary>
        /// Cherche un plat via un texte de recherche
        /// </summary>
        /// <param name="texterecherche">Le texte permettant d'un plat</param>
        /// <returns>Une liste de plats correspondant à la recherche</returns>
        public List<Plat> ChercherPlat(string texterecherche, long idUtilisateur);

        /// <summary>
        /// Ajoute ou modifie un avis pour un plat donné par un invité
        /// </summary>
        bool AjouterAvis(long idPlat, long idInvite, int note, string commentaire);

    }
}
