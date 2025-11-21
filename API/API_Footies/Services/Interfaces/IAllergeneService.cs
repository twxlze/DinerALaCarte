using API_Footies.Metier;

namespace API_Footies.Services.Interfaces
{
    /// <summary>
    /// Interface pour le service en charge de la gestion des allergenes
    /// </summary>
    public interface IAllergeneService
    {
        /// <summary>
        /// Liste de tout les allergenes
        /// </summary>
        /// <returns>la liste de tout les allergenes</returns>
        List<Allergene> ListAllergene();
    }
}
