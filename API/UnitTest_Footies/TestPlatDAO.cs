using API_Footies.Controllers;
using API_Footies.Data.DAO;
using API_Footies.Metier;
using API_Footies.Services.Realisations;
using API_Footies.Metier.Enum;
using System.Collections.Generic;
using System.Linq;

namespace UnitTest_Footies
{
    public class TestPlatDAO
    {
        private const long ID_UTILISATEUR_TEST = 1;

        [Fact]
        public void AjouterPlat_Integration()
        {
            PlatDAO dao = new PlatDAO();
            PlatService service = new PlatService(dao);
            PlatController controller = new PlatController(service);

            Plat plat = new Plat();
            plat.Nom = "Pizza Test";
            plat.Categorie = CategoriePlat.plat;
            plat.Description = "Description test";
            plat.Ingredients = "Farine, Eau";

            Plat resultat = controller.AjouterPlat(plat, ID_UTILISATEUR_TEST);

            Assert.NotNull(resultat);
            Assert.True(resultat.Id > 0);
            Assert.Equal("Pizza Test", resultat.Nom);

            controller.SupprimerPlat(resultat.Id, ID_UTILISATEUR_TEST);
        }

        [Fact]
        public void ListePlat_Integration()
        {
            PlatDAO dao = new PlatDAO();
            PlatService service = new PlatService(dao);
            PlatController controller = new PlatController(service);

            Plat p1 = new Plat();
            p1.Nom = "Plat A";
            p1.Categorie = CategoriePlat.entree;

            controller.AjouterPlat(p1, ID_UTILISATEUR_TEST);

            List<Plat> liste = controller.ListPlat(ID_UTILISATEUR_TEST);

            Assert.NotNull(liste);

            bool trouve = liste.Any(x => x.Id == p1.Id);
            Assert.True(trouve);

            controller.SupprimerPlat(p1.Id, ID_UTILISATEUR_TEST);
        }

        [Fact]
        public void ModifierPlat_Integration()
        {
            PlatDAO dao = new PlatDAO();
            PlatService service = new PlatService(dao);
            PlatController controller = new PlatController(service);

            Plat plat = new Plat();
            plat.Nom = "Original";
            plat.Categorie = CategoriePlat.dessert;

            controller.AjouterPlat(plat, ID_UTILISATEUR_TEST);

            plat.Nom = "Modifie";
            controller.ModifierPlat(plat, ID_UTILISATEUR_TEST);

            List<Plat> liste = controller.ListPlat(ID_UTILISATEUR_TEST);
            Plat verif = liste.FirstOrDefault(p => p.Id == plat.Id);

            Assert.NotNull(verif);
            Assert.Equal("Modifie", verif.Nom);

            controller.SupprimerPlat(plat.Id, ID_UTILISATEUR_TEST);
        }
    }
}