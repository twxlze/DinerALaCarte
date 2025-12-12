using METIER_Footies.Metier;

namespace Test_Footies_METIER
{
    public class SessionServiceTest
    {
        [Fact]
        public void Instance_EstUniqueEtNonNulle()
        {
            SessionService instance1 = SessionService.Instance;
            SessionService instance2 = SessionService.Instance;

            Assert.NotNull(instance1);
            Assert.Same(instance1, instance2);
        }

        [Fact]
        public void GestionConnexion_FonctionneCorrectement()
        {
            SessionService session = SessionService.Instance;
            session.UtilisateurConnecte = null;

            Assert.False(session.EstConnecte, "Devrait être faux quand UtilisateurConnecte est null");

            Utilisateur user = new Utilisateur(1, "Joueur1", "Nom", "Prenom", "06", "mail");
            session.UtilisateurConnecte = user;

            Assert.NotNull(session.UtilisateurConnecte);
            Assert.True(session.EstConnecte, "Devrait être vrai une fois l'utilisateur défini");
            Assert.Equal("Joueur1", session.UtilisateurConnecte.Pseudo);

            session.UtilisateurConnecte = null;

            Assert.False(session.EstConnecte, "Devrait redevenir faux après déconnexion");
        }
    }
}