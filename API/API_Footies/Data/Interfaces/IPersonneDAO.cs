namespace API_Footies.Data.Interfaces
{
    /// <summary>
    /// Définit les méthodes pour accéder et récupérer des informations dans une base de données
    /// </summary>
    public interface IPersonneDAO
    {
        /// <summary>
        /// Renvoie la liste de tous les invités
        /// </summary>
        /// <returns>La liste de tous les invités </returns>
        Int64 GetIdTypeByNom(string nom);

    }
}
