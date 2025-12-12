using METIER_Footies.Metier;

namespace Test_Footies_METIER
{
    public class UtilisateurTest
    {
        [Fact]
        public void Constructeur_InitialiseCorrectement_AvecValeurs()
        {
            long id = 10;
            string pseudo = "Sysy";
            string nom = "Dague";
            string prenom = "Sylvye";
            string tel = "0612345789";
            string mail = "sysy@etiqmail.com";

            Utilisateur utilisateur = new Utilisateur(id, pseudo, nom, prenom, tel, mail);

            Assert.Equal(id, utilisateur.IdUtilisateur);
            Assert.Equal(pseudo, utilisateur.Pseudo);
            Assert.Equal(nom, utilisateur.Nom);
            Assert.Equal(prenom, utilisateur.Prenom);
            Assert.Equal(tel, utilisateur.NumTel);
            Assert.Equal(mail, utilisateur.Mail);
        }

        [Fact]
        public void Constructeur_AccepteValeursNulles()
        {
            Utilisateur utilisateur = new Utilisateur(5, "Inconnu", null, null, null, null);

            Assert.Equal(5, utilisateur.IdUtilisateur);
            Assert.Equal("Inconnu", utilisateur.Pseudo);
            Assert.Null(utilisateur.Nom);
            Assert.Null(utilisateur.Prenom);
            Assert.Null(utilisateur.NumTel);
            Assert.Null(utilisateur.Mail);
        }

        [Fact]
        public void Proprietes_SontModifiables()
        {
            Utilisateur utilisateur = new Utilisateur(1, "Test", null, null, null, null);

            utilisateur.Pseudo = "NouveauPseudo";
            utilisateur.Mail = "nouveau@test.com";

            Assert.Equal("NouveauPseudo", utilisateur.Pseudo);
            Assert.Equal("nouveau@test.com", utilisateur.Mail);
        }
    }
}