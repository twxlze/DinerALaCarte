using API_Footies.Metier;

namespace API_Footies.Data.Interfaces
{
    /// <summary>
    /// Interface pour les opérations des allergenes
    /// </summary>
    public interface IAllergeneDAO
    {
        List<Allergene> ListeAllergene();
    }
}
