using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using METIER_Footies.Metier;
using VM_Footies.VM;
using static METIER_Footies.Metier.Plat;

namespace Test_Footies_METIER
{
    public class PlatTest
    {
        #region Test avec constructeurs

        [Fact]
        public void ConstructeurCopie()
        {
            Plat platOriginal = new Plat(3, "Coq au vin", "Plat traditionnel français", CategoriePlat.plat);

            Plat platCopie = new Plat(platOriginal);

            Assert.Equal(platOriginal.Id, platCopie.Id);
            Assert.Equal(platOriginal.Nom, platCopie.Nom);
            Assert.Equal(platOriginal.Description, platCopie.Description);
            Assert.Equal(platOriginal.Categorie, platCopie.Categorie);
        }

        [Fact]
        public void ConstructeurParDefaut()
        {
            Plat plat = new Plat();

            Assert.Equal("", plat.Nom);
            Assert.Equal("", plat.Description);
            Assert.Equal(CategoriePlat.entree, plat.Categorie);
        }

        [Fact]
        public void PlatAvecToutesLesCategories()
        {
            Plat aperitif = new Plat(1, "Chips", "Apéritif simple", CategoriePlat.aperitif);
            Plat entree = new Plat(2, "Soupe", "Entrée chaude", CategoriePlat.entree);
            Plat platPrincipal = new Plat(3, "Steak", "Plat principal", CategoriePlat.plat);
            Plat dessert = new Plat(4, "Mousse", "Dessert léger", CategoriePlat.dessert);

            Assert.Equal(CategoriePlat.aperitif, aperitif.Categorie);
            Assert.Equal(CategoriePlat.entree, entree.Categorie);
            Assert.Equal(CategoriePlat.plat, platPrincipal.Categorie);
            Assert.Equal(CategoriePlat.dessert, dessert.Categorie);
        }

        #endregion

        #region Test avec exceptions

        [Fact]
        public void Id_AvecValeurNegative()
        {
            Plat plat = new Plat(0, "Plat test", "Description test", CategoriePlat.plat);

            Assert.Throws<ArgumentException>(() => plat.Id = -1);
        }

        [Fact]
        public void Id_AvecValeurZero()
        {
            Plat plat = new Plat(0, "Nouveau plat", "Sans ID", CategoriePlat.entree);
            Assert.Equal(0, plat.Id);
        }

        [Fact]
        public void Nom_ConvertitEnMajuscules()
        {
            Plat plat = new Plat(1, "pizza margherita", "Pizza italienne", CategoriePlat.plat);
            plat.Nom = "pâtes carbonara";
            Assert.Equal("PÂTES CARBONARA", plat.Nom);
        }

        [Fact]
        public void Description_PeutEtreModifiee()
        {
            Plat plat = new Plat(1, "Plat", "Description initiale", CategoriePlat.plat);
            plat.Description = "Nouvelle Description Mixte";
            Assert.Equal("Nouvelle Description Mixte", plat.Description);
        }

        [Fact]
        public void Categorie_PeutEtreModifiee()
        {
            Plat plat = new Plat(1, "Plat polyvalent", "Description", CategoriePlat.aperitif);

            plat.Categorie = CategoriePlat.entree;
            Assert.Equal(CategoriePlat.entree, plat.Categorie);

            plat.Categorie = CategoriePlat.plat;
            Assert.Equal(CategoriePlat.plat, plat.Categorie);

            plat.Categorie = CategoriePlat.dessert;
            Assert.Equal(CategoriePlat.dessert, plat.Categorie);

            plat.Categorie = CategoriePlat.aperitif;
            Assert.Equal(CategoriePlat.aperitif, plat.Categorie);
        }

        #endregion

        #region Test avec VMPlat

        [Fact]
        public void VMPlat_AvecPlatValide()
        {
            Plat plat = new Plat(1, "Tiramisu", "Dessert italien", CategoriePlat.dessert);

            VMPlat vmPlat = new VMPlat(plat);

            Assert.Equal(plat.Id, vmPlat.Id);
            Assert.Equal(plat.Nom, vmPlat.Nom);
            Assert.Equal(plat.Description, vmPlat.Description);
            Assert.Equal(plat.Categorie, vmPlat.Categorie);
        }

        [Fact]
        public void VMPlat_ModifierProprietes()
        {
            Plat plat = new Plat(1, "Plat initial", "Description initiale", CategoriePlat.entree);
            VMPlat vmPlat = new VMPlat(plat);
            vmPlat.Nom = "Plat modifié";
            vmPlat.Description = "Description modifiée";
            vmPlat.Categorie = CategoriePlat.plat;
            Assert.Equal("PLAT MODIFIÉ", vmPlat.Nom);
            Assert.Equal("Description modifiée", vmPlat.Description);
            Assert.Equal(CategoriePlat.plat, vmPlat.Categorie);
        }

        #endregion

        #region Tests de validation métier supplémentaires

        [Fact]
        public void Plat_ModifierNom()
        {
            Plat plat = new Plat(10, "Nom initial", "Description", CategoriePlat.plat);
            plat.Nom = "Nouveau nom";

            Assert.Equal(10, plat.Id);
            Assert.Equal("NOUVEAU NOM", plat.Nom);
            Assert.Equal(CategoriePlat.plat, plat.Categorie);
        }

        [Fact]
        public void Plat_AvecNomVide()
        {
            Plat plat = new Plat(1, "", "Description", CategoriePlat.aperitif);

            Assert.Equal("", plat.Nom);
        }

        [Fact]
        public void VMPlat_ModifierPlat()
        {
            Plat platOriginal = new Plat(1, "Plat A", "Description A", CategoriePlat.entree);
            VMPlat vmPlatOriginal = new VMPlat(platOriginal);

            Plat platNouveau = new Plat(1, "Plat B", "Description B", CategoriePlat.dessert);
            VMPlat vmPlatNouveau = new VMPlat(platNouveau);

            vmPlatOriginal.ModifierPlat(vmPlatNouveau);
            Assert.Equal("PLAT B", vmPlatOriginal.Nom);
            Assert.Equal("Description B", vmPlatOriginal.Description);
            Assert.Equal(CategoriePlat.dessert, vmPlatOriginal.Categorie);
        }

        #endregion
    }
}
