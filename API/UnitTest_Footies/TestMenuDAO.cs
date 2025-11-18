using System;
using System.Collections.Generic;
using System.Linq;
using API_Footies.Data.DAO;
using API_Footies.Metier;

namespace UnitTest_Footies
{
    public class TestMenuDAO
    {
        [Fact]
        public void AjouterMenuSansPlat()
        {
            MenuDAO menuDAO = new MenuDAO();
            List<Plat> platsVides = new List<Plat>();
            Menu menu = new Menu(platsVides, 0, "Menu Test Sans Plat");

            bool resultat = menuDAO.AjouterMenu(menu);

            Assert.True(resultat);
            Assert.True(menu.IdMenu > 0);

            menuDAO.SupprimerMenu(menu.IdMenu);
        }

        [Fact]
        public void AjouterMenuAvecPlats()
        {
            PlatDAO platDAO = new PlatDAO();
            MenuDAO menuDAO = new MenuDAO();

            Plat plat1 = new Plat(0, "Salade César", "Salade romaine", Plat.CategoriePlat.entree);
            platDAO.AjouterPlat(plat1);
            Plat plat2 = new Plat(0, "Poulet rôti", "Poulet au four", Plat.CategoriePlat.plat);
            platDAO.AjouterPlat(plat2);

            List<Plat> plats = new List<Plat>();
            plats.Add(plat1);
            plats.Add(plat2);

            Menu menu = new Menu(plats, 0, "Menu Complet Test");
            bool resultat = menuDAO.AjouterMenu(menu);

            Assert.True(resultat);
            Assert.True(menu.IdMenu > 0);

            menuDAO.SupprimerMenu(menu.IdMenu);
            platDAO.SupprimerPlat(plat1.Id);
            platDAO.SupprimerPlat(plat2.Id);
        }

        [Fact]
        public void RecupererListeMenus()
        {
            MenuDAO menuDAO = new MenuDAO();
            List<Plat> platsVides = new List<Plat>();
            Menu menu1 = new Menu(platsVides, 0, "Menu Test 1");
            Menu menu2 = new Menu(platsVides, 0, "Menu Test 2");

            menuDAO.AjouterMenu(menu1);
            menuDAO.AjouterMenu(menu2);

            List<Menu> listeMenus = menuDAO.ListMenu();

            Assert.NotNull(listeMenus);
            Assert.True(listeMenus.Count >= 2);
            Assert.Contains(listeMenus, m => m.Nom == "Menu Test 1");
            Assert.Contains(listeMenus, m => m.Nom == "Menu Test 2");

            menuDAO.SupprimerMenu(menu1.IdMenu);
            menuDAO.SupprimerMenu(menu2.IdMenu);
        }

        [Fact]
        public void ModifierNomMenu()
        {
            MenuDAO menuDAO = new MenuDAO();
            List<Plat> platsVides = new List<Plat>();
            Menu menu = new Menu(platsVides, 0, "Ancien Nom");
            menuDAO.AjouterMenu(menu);

            menu.Nom = "Nouveau Nom";
            bool resultat = menuDAO.ModifierMenu(menu);

            Assert.True(resultat);
            List<Menu> listeMenus = menuDAO.ListMenu();
            Menu menuModifie = listeMenus.FirstOrDefault(m => m.IdMenu == menu.IdMenu);
            Assert.NotNull(menuModifie);
            Assert.Equal("Nouveau Nom", menuModifie.Nom);

            menuDAO.SupprimerMenu(menu.IdMenu);
        }

        [Fact]
        public void ModifierPlatsMenu()
        {
            PlatDAO platDAO = new PlatDAO();
            MenuDAO menuDAO = new MenuDAO();

            Plat plat1 = new Plat(0, "Tarte", "Tarte aux pommes", Plat.CategoriePlat.dessert);
            platDAO.AjouterPlat(plat1);

            Plat plat2 = new Plat(0, "Glace", "Glace vanille", Plat.CategoriePlat.dessert);
            platDAO.AjouterPlat(plat2);

            List<Plat> platsInitiaux = new List<Plat>();
            platsInitiaux.Add(plat1);
            Menu menu = new Menu(platsInitiaux, 0, "Menu Dessert");
            menuDAO.AjouterMenu(menu);

            List<Plat> nouveauxPlats = new List<Plat>();
            nouveauxPlats.Add(plat2);
            menu.Plat = nouveauxPlats;
            bool resultat = menuDAO.ModifierMenu(menu);

            Assert.True(resultat);
            List<Menu> listeMenus = menuDAO.ListMenu();
            Menu menuModifie = listeMenus.FirstOrDefault(m => m.IdMenu == menu.IdMenu);
            Assert.NotNull(menuModifie);
            Assert.Single(menuModifie.Plat);
            Assert.Equal(plat2.Id, menuModifie.Plat[0].Id);

            menuDAO.SupprimerMenu(menu.IdMenu);
            platDAO.SupprimerPlat(plat1.Id);
            platDAO.SupprimerPlat(plat2.Id);
        }

        [Fact]
        public void SupprimerMenu()
        {
            MenuDAO menuDAO = new MenuDAO();
            List<Plat> platsVides = new List<Plat>();
            Menu menu = new Menu(platsVides, 0, "Menu A Supprimer");
            menuDAO.AjouterMenu(menu);
            long idMenu = menu.IdMenu;

            menuDAO.SupprimerMenu(idMenu);

            List<Menu> listeMenus = menuDAO.ListMenu();
            Menu menuSupprime = listeMenus.FirstOrDefault(m => m.IdMenu == idMenu);
            Assert.Null(menuSupprime);
        }

        [Fact]
        public void SupprimerMenuAvecPlats()
        {
            PlatDAO platDAO = new PlatDAO();
            MenuDAO menuDAO = new MenuDAO();

            Plat plat = new Plat(0, "Soupe", "Soupe à l'oignon", Plat.CategoriePlat.entree);
            platDAO.AjouterPlat(plat);

            List<Plat> plats = new List<Plat>();
            plats.Add(plat);
            Menu menu = new Menu(plats, 0, "Menu Avec Plat");
            menuDAO.AjouterMenu(menu);
            long idMenu = menu.IdMenu;

            menuDAO.SupprimerMenu(idMenu);

            List<Menu> listeMenus = menuDAO.ListMenu();
            Menu menuSupprime = listeMenus.FirstOrDefault(m => m.IdMenu == idMenu);
            Assert.Null(menuSupprime);

            platDAO.SupprimerPlat(plat.Id);
        }
    }
}