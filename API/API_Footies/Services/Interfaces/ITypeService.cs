namespace API_Footies.Services.Interfaces
{
    /// <summary>
    /// Interface pour le service de gestion des types permettant de renvoyer différentes informations sur les types.
    /// </summary>
    public interface ITypeService
    {
        /// <summary>
        /// Renvoie la liste de tous les invités
        /// </summary>
        /// <returns>La liste de tous les invités </returns>
        Int64 GetIdTypeByNom(string nom);

    }
}
