using System.Diagnostics.Metrics;
using METIER_Footies.Enum;
using METIER_Footies.Metier;

namespace Test_Footies_METIER
{
    /// <summary>
    /// Classe de test pour les avertissements lors de invitations
    /// </summary>
    public class AvertissementInvitationTest
    {

        #region Données de test communes

        private Invite CreerInviteTest()
        {
            return new Invite(1, "DUPONT", "Jean", "0600000000", "jean@test.com");
        }

        private Plat CreerPlatTest(string nomPlat)
        {
            Plat plat = new Plat();
            plat.Nom = nomPlat;
            plat.Id = 10;
            return plat;
        }

        private Menu CreerMenuTest(string nomMenu)
        {
            Menu menu = new Menu();
            menu.Nom = nomMenu;
            menu.IdMenu = 100;
            return menu;
        }

        #endregion

        #region Tests Constructeur & Propriétés

        [Fact]
        public void Constructeur_InitialiseCorrectement()
        {
            Invite invite = CreerInviteTest();
            Plat plat = CreerPlatTest("Pizza");
            Menu menu = CreerMenuTest("Menu Italien");
            AvertissementInvitation.TypeAvertissement type = AvertissementInvitation.TypeAvertissement.Allergie;

            AvertissementInvitation avertissement = new AvertissementInvitation(type, invite, plat, menu);

            Assert.Equal(type, avertissement.Type);
            Assert.Same(invite, avertissement.Invite);
            Assert.Same(plat, avertissement.Plat);
            Assert.Same(menu, avertissement.Menu);
        }

        [Fact]
        public void Constructeur_SansMenu_MenuEstNull()
        {
            Invite invite = CreerInviteTest();
            Plat plat = CreerPlatTest("Pizza");

            AvertissementInvitation avertissement = new AvertissementInvitation(AvertissementInvitation.TypeAvertissement.PlatDeteste, invite, plat);

            Assert.Null(avertissement.Menu);
        }

        [Fact]
        public void Proprietes_SontModifiables()
        {
            AvertissementInvitation avertissement = new AvertissementInvitation();
            Invite invite = CreerInviteTest();
            Plat plat = CreerPlatTest("Salade");

            avertissement.Type = AvertissementInvitation.TypeAvertissement.PlatPrefere;
            avertissement.Invite = invite;
            avertissement.Plat = plat;

            Assert.Equal(AvertissementInvitation.TypeAvertissement.PlatPrefere, avertissement.Type);
            Assert.Equal("Salade", avertissement.Plat.Nom);
            Assert.Equal("Jean", avertissement.Invite.Prenom);
        }

        #endregion

        #region Tests Génération de Message

        [Fact]
        public void Message_Allergie_SansMenu_GenereFormatCorrect()
        {
            Invite invite = CreerInviteTest();
            Plat plat = CreerPlatTest("Cacahouètes");
            AvertissementInvitation avertissement = new AvertissementInvitation(AvertissementInvitation.TypeAvertissement.Allergie, invite, plat);

            string message = avertissement.Message;

            Assert.Equal("Jean DUPONT ⚠️ Cacahouètes", message);
        }

        [Fact]
        public void Message_Allergie_AvecMenu_GenereFormatCorrect()
        {
            Invite invite = CreerInviteTest();
            Plat plat = CreerPlatTest("Crevettes");
            Menu menu = CreerMenuTest("Menu Etiq");
            AvertissementInvitation avertissement = new AvertissementInvitation(AvertissementInvitation.TypeAvertissement.Allergie, invite, plat, menu);

            string message = avertissement.Message;

            Assert.Equal("Jean DUPONT ⚠️ Crevettes (menu : Menu Etiq)", message);
        }

        [Fact]
        public void Message_PlatDeteste_GenereFormatCorrect()
        {
            Invite invite = CreerInviteTest();
            Plat plat = CreerPlatTest("Brocolis");
            AvertissementInvitation avertissement = new AvertissementInvitation(AvertissementInvitation.TypeAvertissement.PlatDeteste, invite, plat);

            string message = avertissement.Message;

            Assert.Equal("Jean DUPONT 😞 Brocolis", message);
        }

        [Fact]
        public void Message_PlatPrefere_GenereFormatCorrect()
        {
            Invite invite = CreerInviteTest();
            Plat plat = CreerPlatTest("Lasagnes");
            AvertissementInvitation avertissement = new AvertissementInvitation(AvertissementInvitation.TypeAvertissement.PlatPrefere, invite, plat);

            string message = avertissement.Message;

            Assert.Equal("Jean DUPONT 😊 Lasagnes", message);
        }

        [Fact]
        public void Message_TypeInconnu_RetourneChaineVide()
        {
            Invite invite = CreerInviteTest();
            Plat plat = CreerPlatTest("Rien");
            AvertissementInvitation.TypeAvertissement typeInvalide = (AvertissementInvitation.TypeAvertissement)999;

            AvertissementInvitation avertissement = new AvertissementInvitation(typeInvalide, invite, plat);

            string message = avertissement.Message;

            Assert.Equal("", message);
        }

        #endregion
    }
}