using System.Security.Cryptography;
using System.Text;

namespace API_Footies.Outils
{
    /// <summary>
    /// Outil permettant d'utiliser les différentes fonction necessaires pour la sécuritée de la connexion
    /// </summary>
    public class Authentification : IAuthentification
    {
        private string sel;
        public Authentification(IConfigHelper configHelper)
        {
            this.sel = configHelper.LireSelDansIni();
        }

        public string CalculerHash(string motDePasse)
        {
            byte[] donnees = Encoding.UTF8.GetBytes(motDePasse + this.sel);
            byte[] hashBytes = SHA256.HashData(donnees);
            return Convert.ToBase64String(hashBytes);
        }
    }
}
