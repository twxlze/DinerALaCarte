using METIER_Footies.Metier;

namespace UnitTest
{
    public class InviteTest
    {
        #region Test avec constructeurs

        [Fact]
        public void Constructeur_AvecDonneesValides_CreeInvite()
        {
            Invite invite = new Invite("Dupont", "Jean", "0612345678", "jean.dupont@gmail.com");

            Assert.Equal("Dupont", invite.Nom);
            Assert.Equal("Jean", invite.Prenom);
            Assert.Equal("0612345678", invite.Telephone);
            Assert.Equal("jean.dupont@gmail.com", invite.Email);
        }

        [Fact]
        public void InviteAvecTelephoneEmailNull()
        {
            Invite invite = new Invite("Dupont", "Jean", null, "jean@gmail.com");
            Invite invite2 = new Invite("Jean", "Dupont", "0612345678", null);
            Invite invite3 = new Invite("Oui", "Jean", null, null);

            Assert.Null(invite.Telephone);
            Assert.Null(invite2.Email);
            Assert.Null(invite3.Telephone);
            Assert.Null(invite3.Email);
        }

        #endregion

        #region Test avec exceptions
        [Fact]
        public void InviteSansNom_ThrowArgumentException()
        {
            Invite invite = new Invite("", "Jean", null, null);
            Assert.Throws<ArgumentException>(() => invite.Nom = null);
        }

        [Fact]
        public void InviteSansPrenom_ThrowArgumentException()
        {
            Invite invite = new Invite("Jean", "", null, null);
            Assert.Throws<ArgumentException>(() => invite.Prenom = null);
        }

        [Fact]
        public void TelephoneInvalide_ThrowArgumentException()
        {
            Invite invite = new Invite("Dupont", "Jean", "12345ABCD", null);
            Assert.Throws<ArgumentException>(() => invite.Telephone = "12345ABCD");
        }

        [Fact]
        public void EmailInvalide_ThrowArgumentException()
        {
            Invite invite = new Invite("Dupont", "Jean", null, "jean.gmail.com");
            Assert.Throws<ArgumentException>(() => invite.Email = "jean.gmail.com");
        }
        #endregion
    }
}