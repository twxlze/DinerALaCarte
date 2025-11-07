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
        #region attributs

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

        #region methodes
        public void AjouterPlat(Plat plat)
        {
            this.dao.AjouterPlat(plat);
        }

        public void ModifierPlat(Plat plat)
        {
            this.dao.ModifierPlat(plat);
        }

        public void SupprimerPlat(long id)
        {
            this.dao.SupprimerPlat(id);
        }
        public List<Plat> ListPlat()
        {
            return this.dao.ListPlat();
        }

        public bool EstDansUnMenu(long idInvite)
        {
            return this.dao.EstDansUnMenu(idInvite);
        }
        #endregion

    }
}
