using System.ComponentModel;
using API_Footies.Data.Interfaces;
using API_Footies.Metier;
using API_Footies.Services.Interfaces;

namespace API_Footies.Services.Realisations
{
    /// <summary>
    /// Fournit des services pour gérer les plats
    /// </summary>
    public class PlatService : IPlatService
    {
        #region attributs

        private IPlatDAO dao;

        #endregion

        #region Constructeur

        /// <summary>
        /// Initialise une nouvelle instance de la classe PlatService.
        /// </summary>
        /// <param name="dao">Injection de dépendance</param>
        /// <param name="typeService">Service utilisé pour gérer les opérations liées aux types associées aux plats</param>
        public PlatService(IPlatDAO dao)
        {
            this.dao = dao;
        }
        #endregion

        #region methodes
        public void AjouterPlat(Plat plat, long idUtilisateur)
        {
            this.dao.AjouterPlat(plat, idUtilisateur);
        }

        public void ModifierPlat(Plat plat, long idUtilisateur)
        {
            this.dao.ModifierPlat(plat, idUtilisateur);
        }

        public void SupprimerPlat(long id, long idUtilisateur)
        {
            this.dao.SupprimerPlat(id, idUtilisateur);
        }
        public List<Plat> ListPlat(long idUtilisateur)
        {
            return this.dao.ListPlat(idUtilisateur);
        }

        public bool EstDansUnMenu(long idInvite)
        {
            return this.dao.EstDansUnMenu(idInvite);
        }

        public List<Plat> ChercherPlat(string texterecherche, long idUtilisateur)
        {
            return this.dao.ChercherPlat(texterecherche, idUtilisateur);
        }

        public void AjouterAvis(Metier.Avis avis)
        {
            if (avis.Note < 1 || avis.Note > 10)
            {
                throw new ArgumentException("La note doit être comprise entre 1 et 10.");
            }

            if (avis.IdPlat <= 0 || avis.IdInvite <= 0)
            {
                throw new ArgumentException("Il faut un invité et un plat séléctionné.");
            }
            this.dao.AjouterAvis(avis.IdPlat, avis.IdInvite, avis.Note, avis.Commentaire);
        }

        #endregion

    }
}
