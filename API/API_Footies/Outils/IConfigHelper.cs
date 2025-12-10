namespace API_Footies.Outils
{
    /// <summary>
    /// Interface pour l'outil utilisé pour lire les configurations dans le fichier ini
    /// </summary>
    public interface IConfigHelper
    {
        /// <summary>
        /// Permet de lire le sel dans le fichier ini de configuration
        /// </summary>
        /// <returns>le sel du fichier config.ini</returns>
        string LireSelDansIni();
    }
}
