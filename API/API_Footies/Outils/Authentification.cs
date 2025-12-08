using System.Security.Cryptography;
using System.Text;

namespace API_Footies.Outils
{
    /// <summary>
    /// Outil permettant d'utiliser les différentes fonction necessaires pour la sécuritée de la connexion
    /// </summary>
    public class Authentification : IAuthentification
    {
        public string CalculerHash(string motDePasse, string sel)
        {
            byte[] donnees = Encoding.UTF8.GetBytes(motDePasse + sel);
            byte[] hashBytes = SHA256.HashData(donnees);
            return Convert.ToBase64String(hashBytes);
        }

        public string GenererSel()
        {
            byte[] octets = RandomNumberGenerator.GetBytes(32);
            return Convert.ToBase64String(octets);
        }
    }
}
