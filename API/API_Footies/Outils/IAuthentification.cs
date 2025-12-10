namespace API_Footies.Outils
{
    /// <summary>
    /// Interface pour l'outil utilisé pour l'authentification
    /// </summary>
    public interface IAuthentification
    {
        /// <summary>
        /// Calcul le mot de passe hash
        /// </summary>
        /// <param name="motDePasse">le mot de passe utilisateur</param>
        /// <returns>le mot de passe hasher</returns>
        string CalculerHash(string motDePasse);
    }
}
