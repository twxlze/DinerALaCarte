using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using METIER_Footies.Metier;
using VM_Footies.VM;
using METIER_Footies.Enum;
using static METIER_Footies.Metier.Plat;

namespace Test_Footies_METIER
{
    public class MenuTest
    {
        #region Test avec constructeurs

        [Fact]
        public void ConstructeurCopie()
        {
            Menu menuOriginal = new Menu
            {
                IdMenu = 1,
                Nom = "Menu Gastronomique",
                Plat = new List<Plat>
                {
                    new Plat(1, "Salade", "Entrée fraîche", CategoriePlat.entree),
                    new Plat(2, "Steak", "Plat principal", CategoriePlat.plat)
                }
            };

            Menu menuCopie = new Menu(menuOriginal);

            Assert.Equal(menuOriginal.IdMenu, menuCopie.IdMenu);
            Assert.Equal(menuOriginal.Nom, menuCopie.Nom);
            Assert.Equal(menuOriginal.Plat.Count, menuCopie.Plat.Count);
        }

        [Fact]
        public void ConstructeurParDefaut()
        {
            Menu menu = new Menu();

            Assert.Equal("", menu.Nom);
            Assert.NotNull(menu.Plat);
            Assert.Empty(menu.Plat);
        }

        [Fact]
        public void MenuAvecToutesLesCategories()
        {
            Menu menu = new Menu
            {
                IdMenu = 1,
                Nom = "Menu Complet",
                Plat = new List<Plat>
                {
                    new Plat(1, "Chips", "Apéritif", CategoriePlat.aperitif),
                    new Plat(2, "Soupe", "Entrée", CategoriePlat.entree),
                    new Plat(3, "Steak", "Plat principal", CategoriePlat.plat),
                    new Plat(4, "Mousse", "Dessert", CategoriePlat.dessert)
                }
            };

            Assert.Equal(4, menu.Plat.Count);
            Assert.Contains(menu.Plat, p => p.Categorie == CategoriePlat.aperitif);
            Assert.Contains(menu.Plat, p => p.Categorie == CategoriePlat.entree);
            Assert.Contains(menu.Plat, p => p.Categorie == CategoriePlat.plat);
            Assert.Contains(menu.Plat, p => p.Categorie == CategoriePlat.dessert);
        }

        #endregion

        #region Test avec exceptions

        [Fact]
        public void IdMenu_AvecValeurNegative()
        {
            Menu menu = new Menu { IdMenu = 1, Nom = "Menu" };

            Assert.Throws<ArgumentException>(() => menu.IdMenu = -1);
        }

        [Fact]
        public void IdMenu_AvecValeurZero()
        {
            Menu menu = new Menu { IdMenu = 1, Nom = "Menu" };

            Assert.Throws<ArgumentException>(() => menu.IdMenu = 0);
        }

        [Fact]
        public void Nom_PeutEtreModifie()
        {
            Menu menu = new Menu { IdMenu = 1, Nom = "Menu Initial" };

            menu.Nom = "Menu Modifié";

            Assert.Equal("Menu Modifié", menu.Nom);
        }

        [Fact]
        public void Plat_AjoutDiminueListe()
        {
            Menu menu = new Menu { IdMenu = 1, Nom = "Menu" };
            Plat plat1 = new Plat(1, "Salade", "Entrée", CategoriePlat.entree);
            Plat plat2 = new Plat(2, "Steak", "Plat", CategoriePlat.plat);

            menu.Plat.Add(plat1);
            menu.Plat.Add(plat2);
            Assert.Equal(2, menu.Plat.Count);

            menu.Plat.Remove(plat1);

            Assert.Single(menu.Plat);
            Assert.DoesNotContain(plat1, menu.Plat);
            Assert.Contains(plat2, menu.Plat);
        }

        #endregion

        #region Test avec VMMenu

        [Fact]
        public void VMMenu_AvecMenuValide()
        {
            Menu menu = new Menu
            {
                IdMenu = 1,
                Nom = "Menu Déjeuner",
                Plat = new List<Plat>
                {
                    new Plat(1, "Salade", "Entrée", CategoriePlat.entree)
                }
            };

            VMMenu vmMenu = new VMMenu(menu);

            Assert.Equal(menu.Nom, vmMenu.Nom);
            Assert.Equal(menu.Plat.Count, vmMenu.Plats.Count);
            Assert.NotNull(vmMenu.PlatsAperitif);
            Assert.NotNull(vmMenu.PlatsEntree);
            Assert.NotNull(vmMenu.PlatsPlat);
            Assert.NotNull(vmMenu.PlatsDessert);
        }

        #endregion

        #region Tests de validation métier supplémentaires

        [Fact]
        public void Menu_AvecNomVide()
        {
            Menu menu = new Menu { IdMenu = 1, Nom = "" };

            Assert.Equal("", menu.Nom);
        }

        [Fact]
        public void Menu_AvecCaracteresSpeciaux()
        {
            Menu menu = new Menu
            {
                IdMenu = 1,
                Nom = "Menu d'été 2024 - Spécialités & Saveurs"
            };

            Assert.Equal("Menu d'été 2024 - Spécialités & Saveurs", menu.Nom);
        }

        [Fact]
        public void Menu_PlusieursPlatsMemeCategorie()
        {
            Menu menu = new Menu
            {
                IdMenu = 1,
                Nom = "Menu Desserts",
                Plat = new List<Plat>
                {
                    new Plat(1, "Tiramisu", "Dessert italien", CategoriePlat.dessert),
                    new Plat(2, "Mousse au chocolat", "Dessert français", CategoriePlat.dessert),
                    new Plat(3, "Crème brûlée", "Dessert classique", CategoriePlat.dessert)
                }
            };

            Assert.Equal(3, menu.Plat.Count);
            Assert.All(menu.Plat, plat => Assert.Equal(CategoriePlat.dessert, plat.Categorie));
        }

        #endregion
    }
}