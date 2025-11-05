using API_Footies.Data.Interfaces;
using API_Footies.Services.Interfaces;

namespace API_Footies.Services.Realisations
{
    /// <summary>
    /// Classe TypeService qui sert de passthrough entre le controller et le DAO
    /// </summary>
    public class TypeService : ITypeService
    {
        #region Attributs

        private IPersonneDAO dao;

        #endregion

        #region constructeurs

        /// <summary>
        /// Constructeur de la classe TypeService
        /// </summary>
        /// <param name="dao"> injection de dépendance </param>
        public TypeService(IPersonneDAO dao)
        {
            this.dao = dao;
        }

        #endregion


        #region methodes

        /// <summary>
        /// Récupère l'identifiant unique d'un type en fonction de son nom.
        /// </summary>
        /// <param name="nom">Nom du type pour lequel l'identifiant doit être récupéré. Ne peut être nul ou vide.</param>
        /// <returns>Identifiant unique du type sous la forme long </returns>
        public long GetIdTypeByNom(string nom)
        {
            return dao.GetIdTypeByNom(nom);
        }


        #endregion
    }
}
