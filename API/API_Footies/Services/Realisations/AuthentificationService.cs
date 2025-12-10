using API_Footies.Data.Interfaces;
using API_Footies.Metier;
using API_Footies.Outils;
using API_Footies.Services.Interfaces;

namespace API_Footies.Services.Realisations
{
    public class AuthentificationService : IAuthentificationService
    {
        private IUtilisateurDAO dao;
        private IAuthentification securite;

        public AuthentificationService(IUtilisateurDAO dao, IAuthentification securite)
        {
            this.dao = dao;
            this.securite = securite;
        }

        public Utilisateur VerifierConnexion(string pseudo, string motDePasseClair)
        {
            Utilisateur utilisateurRetour = null;
            Identifiant identifiantBdd = this.dao.RecupererIdentifiantParPseudo(pseudo);

            if (identifiantBdd != null)
            {
                string hashCalcule = this.securite.CalculerHash(motDePasseClair);
                if (hashCalcule == identifiantBdd.MotDePasseHash)
                {
                    utilisateurRetour = this.dao.RecupererUtilisateurParPseudo(pseudo);
                }
            }

            return utilisateurRetour;
        }

        public bool VerifierPseudoDisponible(string pseudo)
        {
            Utilisateur utilisateurTrouve = this.dao.RecupererUtilisateurParPseudo(pseudo);
            return utilisateurTrouve == null;
        }
    }
}