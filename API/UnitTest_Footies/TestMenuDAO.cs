using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using API_Footies.Data.DAO;
using API_Footies.Metier;
using API_Footies.Metier.Enum;

namespace UnitTest_Footies
{
    public class TestMenuDAO
    {
        private const long ID_UTILISATEUR_TEST = 1;

        [Fact]
        public void AjouterMenuSansPlat()
        {
            MenuDAO menuDAO = new MenuDAO();
            List<Plat> platsVides = new List<Plat>();
            Menu menu = new Menu(platsVides, 0, "Menu Test Sans Plat");

            bool resultat = menuDAO.AjouterMenu(menu, ID_UTILISATEUR_TEST);

            Assert.True(resultat);
            Assert.True(menu.IdMenu > 0);

            menuDAO.SupprimerMenu(menu.IdMenu, ID_UTILISATEUR_TEST);
        }

        [Fact]
        public void AjouterMenuAvecPlats()
        {
            PlatDAO platDAO = new PlatDAO();
            MenuDAO menuDAO = new MenuDAO();

            Plat plat1 = new Plat(0, "Salade César", "Salade romaine", CategoriePlat.entree, "", null);
            platDAO.AjouterPlat(plat1, ID_UTILISATEUR_TEST);

            Plat plat2 = new Plat(0, "Poulet rôti", "Poulet au four", CategoriePlat.plat, "", null);
            platDAO.AjouterPlat(plat2, ID_UTILISATEUR_TEST);

            List<Plat> plats = new List<Plat>();
            plats.Add(plat1);
            plats.Add(plat2);

            Menu menu = new Menu(plats, 0, "Menu Complet Test");
            bool resultat = menuDAO.AjouterMenu(menu, ID_UTILISATEUR_TEST);

            Assert.True(resultat);
            Assert.True(menu.IdMenu > 0);

            menuDAO.SupprimerMenu(menu.IdMenu, ID_UTILISATEUR_TEST);
            platDAO.SupprimerPlat(plat1.Id, ID_UTILISATEUR_TEST);
            platDAO.SupprimerPlat(plat2.Id, ID_UTILISATEUR_TEST);
        }

        [Fact]
        public void RecupererListeMenus()
        {
            MenuDAO menuDAO = new MenuDAO();
            List<Plat> platsVides = new List<Plat>();
            Menu menu1 = new Menu(platsVides, 0, "Menu Test 1");
            Menu menu2 = new Menu(platsVides, 0, "Menu Test 2");

            menuDAO.AjouterMenu(menu1, ID_UTILISATEUR_TEST);
            menuDAO.AjouterMenu(menu2, ID_UTILISATEUR_TEST);

            List<Menu> listeMenus = menuDAO.ListMenu(ID_UTILISATEUR_TEST);

            Assert.NotNull(listeMenus);
            Assert.True(listeMenus.Count >= 2);
            Assert.Contains(listeMenus, m => m.Nom == "Menu Test 1");
            Assert.Contains(listeMenus, m => m.Nom == "Menu Test 2");

            menuDAO.SupprimerMenu(menu1.IdMenu, ID_UTILISATEUR_TEST);
            menuDAO.SupprimerMenu(menu2.IdMenu, ID_UTILISATEUR_TEST);
        }

        [Fact]
        public void ModifierNomMenu()
        {
            MenuDAO menuDAO = new MenuDAO();
            List<Plat> platsVides = new List<Plat>();
            Menu menu = new Menu(platsVides, 0, "Ancien Nom");
            menuDAO.AjouterMenu(menu, ID_UTILISATEUR_TEST);

            menu.Nom = "Nouveau Nom";
            bool resultat = menuDAO.ModifierMenu(menu, ID_UTILISATEUR_TEST);

            Assert.True(resultat);
            List<Menu> listeMenus = menuDAO.ListMenu(ID_UTILISATEUR_TEST);
            Menu menuModifie = listeMenus.FirstOrDefault(m => m.IdMenu == menu.IdMenu);
            Assert.NotNull(menuModifie);
            Assert.Equal("Nouveau Nom", menuModifie.Nom);

            menuDAO.SupprimerMenu(menu.IdMenu, ID_UTILISATEUR_TEST);
        }

        [Fact]
        public void ModifierPlatsMenu()
        {
            PlatDAO platDAO = new PlatDAO();
            MenuDAO menuDAO = new MenuDAO();

            Plat plat1 = new Plat(0, "Tarte", "Tarte aux pommes", CategoriePlat.dessert, "", null);
            platDAO.AjouterPlat(plat1, ID_UTILISATEUR_TEST);

            Plat plat2 = new Plat(0, "Glace", "Glace vanille", CategoriePlat.dessert, "", null);
            platDAO.AjouterPlat(plat2, ID_UTILISATEUR_TEST);

            List<Plat> platsInitiaux = new List<Plat>();
            platsInitiaux.Add(plat1);
            Menu menu = new Menu(platsInitiaux, 0, "Menu Dessert");
            menuDAO.AjouterMenu(menu, ID_UTILISATEUR_TEST);

            List<Plat> nouveauxPlats = new List<Plat>();
            nouveauxPlats.Add(plat2);
            menu.Plat = nouveauxPlats;
            bool resultat = menuDAO.ModifierMenu(menu, ID_UTILISATEUR_TEST);

            Assert.True(resultat);
            List<Menu> listeMenus = menuDAO.ListMenu(ID_UTILISATEUR_TEST);
            Menu menuModifie = listeMenus.FirstOrDefault(m => m.IdMenu == menu.IdMenu);
            Assert.NotNull(menuModifie);
            Assert.Single(menuModifie.Plat);
            Assert.Equal(plat2.Id, menuModifie.Plat[0].Id);

            menuDAO.SupprimerMenu(menu.IdMenu, ID_UTILISATEUR_TEST);
            platDAO.SupprimerPlat(plat1.Id, ID_UTILISATEUR_TEST);
            platDAO.SupprimerPlat(plat2.Id, ID_UTILISATEUR_TEST);
        }

        [Fact]
        public void SupprimerMenu()
        {
            MenuDAO menuDAO = new MenuDAO();
            List<Plat> platsVides = new List<Plat>();
            Menu menu = new Menu(platsVides, 0, "Menu A Supprimer");
            menuDAO.AjouterMenu(menu, ID_UTILISATEUR_TEST);
            long idMenu = menu.IdMenu;

            menuDAO.SupprimerMenu(idMenu, ID_UTILISATEUR_TEST);

            List<Menu> listeMenus = menuDAO.ListMenu(ID_UTILISATEUR_TEST);
            Menu menuSupprime = listeMenus.FirstOrDefault(m => m.IdMenu == idMenu);
            Assert.Null(menuSupprime);
        }
    }
}