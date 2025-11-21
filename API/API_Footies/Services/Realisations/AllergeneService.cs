using API_Footies.Data.Interfaces;
using API_Footies.Metier;
using API_Footies.Services.Interfaces;

namespace API_Footies.Services.Realisations
{
    /// <summary>
    /// Service en charge de la gestion des allergenes
    /// </summary>
    public class AllergeneService : IAllergeneService
    {
        #region Attribut
        private IAllergeneDAO _allergeneDAO;
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur de AllergeneService
        /// </summary>
        /// <param name="allergeneDAO">Le dao de allergene</param>
        public AllergeneService(IAllergeneDAO allergeneDAO) {
            this._allergeneDAO = allergeneDAO;
        }
        #endregion

        #region Methode
        public List<Allergene> ListAllergene()
        {
            return this._allergeneDAO.ListeAllergene();
        }
        #endregion
    }
}
