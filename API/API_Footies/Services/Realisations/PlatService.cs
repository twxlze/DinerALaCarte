using System.ComponentModel;
using API_Footies.Data.Interfaces;
using API_Footies.Metier;
using API_Footies.Services.Interfaces;

namespace API_Footies.Services.Realisations
{
    /// <summary>
    /// Fournit des services pour gérer les plats
    /// </summary>
    public class PlatService : IPlatService
    {
        #region attributes

        private IPlatDAO dao;

        #endregion

        /// <summary>
        /// Initialise une nouvelle instance de la classe PlatService.
        /// </summary>
        /// <param name="dao">Injection de dépendance</param>
        /// <param name="typeService">Service utilisé pour gérer les opérations liées aux types associées aux plats</param>
        public PlatService(IPlatDAO dao)
        {
            this.dao = dao;
        }

        #region methods
        /// <summary>
        /// Ajoute un plat
        /// </summary>
        /// <param name="plat">plat à ajouté</param>
        public void AjouterPlat(Plat plat)
        {
            this.dao.AjouterPlat(plat);
        }
        #endregion

    }
}
