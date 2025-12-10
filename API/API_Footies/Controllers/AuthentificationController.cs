using Microsoft.AspNetCore.Mvc;
using API_Footies.Metier;
using API_Footies.Services.Interfaces;

namespace API_Footies.Controllers
{
    [ApiController]
    [Route("authentification")]
    public class AuthentificationController : ControllerBase
    {
        private IAuthentificationService service;

        public AuthentificationController(IAuthentificationService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Vérifie la connexion d'un utilisateur
        /// </summary>
        /// <param name="identifiantRecu">l'identifiant recu</param>
        /// <returns>l'utilisateur correspondant</returns>
        [HttpPost("VerifierConnexion")]
        public IActionResult VerifierConnexion(Identifiant identifiantRecu)
        {
            IActionResult resultat;

            try
            {
                Utilisateur utilisateurConnecte = this.service.VerifierConnexion(identifiantRecu.Pseudo, identifiantRecu.MotDePasse);
                if (utilisateurConnecte != null)
                {
                    resultat = Ok(utilisateurConnecte);
                }
                else
                {
                    resultat = Unauthorized("Pseudo ou mot de passe incorrect.");
                }
            }
            catch (Exception ex)
            {
                resultat = StatusCode(500, "Erreur serveur : " + ex.Message);
            }
            return resultat;
        }

        /// <summary>
        /// Vérifie si un pseudo est disponible
        /// </summary>
        /// <param name="pseudo">le pseudo à rechercher</param>
        /// <returns></returns>
        [HttpPost("VerifierPseudoDisponible")]
        public IActionResult VerifierPseudoDisponible([FromBody]string pseudo)
        {
            IActionResult resultat;
            try
            {
                bool estDisponible = this.service.VerifierPseudoDisponible(pseudo);
                resultat = Ok(estDisponible);
            }
            catch (Exception ex)
            {
                resultat = StatusCode(500, "Erreur serveur : " + ex.Message);
            }
            return resultat;
        }
    }
}