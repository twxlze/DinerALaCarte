using API_Footies.Metier;
using API_Footies.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API_Footies.Controllers
{
    /// <summary>
    /// Controller permettant la gestion de l'authentification
    /// </summary>
    [ApiController]
    [Route("authentification")]
    public class AuthentificationController : ControllerBase
    {
        #region Attributs
        private IAuthentificationService service;
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur du controlleur de l'authentification
        /// </summary>
        public AuthentificationController(IAuthentificationService service)
        {
            this.service = service;
        }
        #endregion

        #region Méthodes

        /// <summary>
        /// Vérification de la connexion
        /// </summary>
        /// <param name=""> invitation à ajouter</param>
        /// <returns> L'invitation ajoutée </returns>
        [HttpPost("VerifierConnexion")]
        public IActionResult Login(Utilisateur utilisateurRecu)
        {
            IActionResult resultat;
            try
            {
                Utilisateur utilisateurConnecte = this.service.VerifierConnexion(utilisateurRecu.Pseudo, utilisateurRecu.MotDePasse);

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
        #endregion
    }
}
