using API_Footies.Controllers;
using API_Footies.Data.Interfaces;
using API_Footies.Metier;
using API_Footies.Outils;
using API_Footies.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API_Footies.Services.Realisations
{
    public class AuthentificationService : IAuthentificationService
    {
        #region Attributs
        private IUtilisateurDAO dao;
        private IAuthentification securite;
        #endregion
        #region Constructeur
        public AuthentificationService(IUtilisateurDAO dao, IAuthentification securite)
        {
            this.dao = dao;
            this.securite = securite;
        }
        #endregion
        #region Méthodes
        public Utilisateur VerifierConnexion(string pseudo, string mdp)
        {
            Utilisateur utilisateurRetour = null;
            Utilisateur utilisateurBdd = this.dao.RecupererUtilisateurParPseudo(pseudo);

            if (utilisateurBdd != null)
            {
                string hashTest = this.securite.CalculerHash(mdp, utilisateurBdd.MotDePasseSel);
                if (hashTest == utilisateurBdd.MotDePasseHash)
                {
                    utilisateurBdd.MotDePasse = "";
                    utilisateurBdd.MotDePasseHash = "";
                    utilisateurBdd.MotDePasseSel = "";
                    utilisateurRetour = utilisateurBdd;
                }
            }
            return utilisateurRetour;
        }
        #endregion
    }
}
