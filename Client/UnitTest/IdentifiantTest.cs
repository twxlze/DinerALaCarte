using METIER_Footies.Metier;

namespace Test_Footies_METIER
{
    public class IdentifiantTest
    {
        [Fact]
        public void Constructeur_InitialiseCorrectement()
        {
            long id = 1;
            string pseudo = "Etiq";
            string mdp = "RenardOrange";

            Identifiant identifiant = new Identifiant(id, pseudo, mdp);

            Assert.Equal(id, identifiant.Id);
            Assert.Equal(pseudo, identifiant.Pseudo);
            Assert.Equal(mdp, identifiant.MotDePasse);
        }

        [Fact]
        public void Proprietes_SontModifiables()
        {
            Identifiant identifiant = new Identifiant(1, "VieuxNom", "VieuxPass");

            identifiant.Id = 2;
            identifiant.Pseudo = "NouveauNom";
            identifiant.MotDePasse = "NouveauPass";

            Assert.Equal(2, identifiant.Id);
            Assert.Equal("NouveauNom", identifiant.Pseudo);
            Assert.Equal("NouveauPass", identifiant.MotDePasse);
        }
    }
}