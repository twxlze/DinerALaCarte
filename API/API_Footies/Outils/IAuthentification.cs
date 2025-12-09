namespace API_Footies.Outils
{
    /// <summary>
    /// Interface pour l'outil utilisé pour l'authentification
    /// </summary>
    public interface IAuthentification
    {
        /// <summary>
        /// permet de créer le sel pour la sécurisation du mot de passe
        /// </summary>
        /// <returns>le sel</returns>
        string GenererSel();
        /// <summary>
        /// Calcul le mot de passe hash
        /// </summary>
        /// <param name="motDePasse">le mot de passe utilisateur</param>
        /// <param name="sel">le sel unique</param>
        /// <returns>le mot de passe hasher</returns>
        string CalculerHash(string motDePasse, string sel);
    }
}
