using METIER_Footies.Metier;
using VM_Footies.VM;

namespace Test_Footies_METIER
{
    public class InviteTest
    {
        #region Test avec constructeurs

        [Fact]
        public void ConstructeurCopie()
        {
            Invite inviteOriginal = new Invite(5, "Martin", "Sophie", "0698765432", "sophie@gmail.com");

            Invite inviteCopie = new Invite(inviteOriginal);

            Assert.Equal(inviteOriginal.Nom, inviteCopie.Nom);
            Assert.Equal(inviteOriginal.Prenom, inviteCopie.Prenom);
            Assert.Equal(inviteOriginal.Telephone, inviteCopie.Telephone);
            Assert.Equal(inviteOriginal.Email, inviteCopie.Email);
        }

        [Fact]
        public void ConstructeurParDefaut()
        {
            Invite invite = new Invite();

            Assert.Equal("", invite.Nom);
            Assert.Equal("", invite.Prenom);
            Assert.Equal("", invite.Telephone);
            Assert.Equal("", invite.Email);
        }

        [Fact]
        public void InviteAvecTelephoneEmailNull()
        {
            Invite invite1 = new Invite(1, "Dupont", "Jean", null, "jean@gmail.com");
            Invite invite2 = new Invite(2, "Martin", "Paul", "0612345678", null);
            Invite invite3 = new Invite(3, "Durand", "Marie", null, null);

            Assert.Equal("", invite1.Telephone);
            Assert.Equal("", invite2.Email);
            Assert.Equal("", invite3.Telephone);
            Assert.Equal("", invite3.Email);
        }

        #endregion

        #region Test avec exceptions

        [Fact]
        public void Id_AvecValeurNegative()
        {
            Invite invite = new Invite(1, "Dupont", "Jean", null, null);

            Assert.Throws<ArgumentException>(() => invite.Id = -1);
        }

        [Fact]
        public void Id_AvecValeurZero()
        {
            Invite invite = new Invite(1, "Dupont", "Jean", null, null);

            Assert.Throws<ArgumentException>(() => invite.Id = 0);
        }

        [Fact]
        public void Nom_ConvertitEnMajuscules()
        {
            Invite invite = new Invite(1, "dupont", "Jean", null, null);

            invite.Nom = "martin";

            Assert.Equal("MARTIN", invite.Nom);
        }

        [Fact]
        public void Email_EstTrimme()
        {
            Invite invite = new Invite(1, "Dupont", "Jean", null, null);

            invite.Email = "  jean@gmail.com  ";

            Assert.Equal("jean@gmail.com", invite.Email);
        }

        [Fact]
        public void Prenom_PeutEtreModifie()
        {
            Invite invite = new Invite(1, "Dupont", "Jean", null, null);

            invite.Prenom = "Pierre";

            Assert.Equal("Pierre", invite.Prenom);
        }

        [Fact]
        public void Telephone_PeutEtreModifie()
        {
            Invite invite = new Invite(1, "Dupont", "Jean", "0612345678", null);

            invite.Telephone = "0698765432";

            Assert.Equal("0698765432", invite.Telephone);
        }

        #endregion

        #region Test avec VMInvite

        [Fact]
        public void VMInvite_AvecInviteValide()
        {
            Invite invite = new Invite(1, "Dupont", "Jean", "0612345678", "jean@gmail.com");

            VMInvite vmInvite = new VMInvite(invite);

            Assert.Equal(invite.Id, vmInvite.Id);
            Assert.Equal(invite.Nom, vmInvite.Nom);
            Assert.Equal(invite.Prenom, vmInvite.Prenom);
            Assert.Equal(invite.Telephone, vmInvite.Telephone);
            Assert.Equal(invite.Email, vmInvite.Email);
            Assert.Equal(invite.Identite, vmInvite.Identite);
        }

        #endregion

        #region Tests de validation métier supplémentaires

        [Fact]
        public void Identite_RetourneFormatCorrect()
        {
            Invite invite = new Invite(1, "DUPONT", "Jean", null, null);

            Assert.Equal("Jean DUPONT", invite.Identite);
        }

        [Fact]
        public void Invite_AvecNomVide()
        {
            Invite invite = new Invite(1, "", "Jean", null, null);

            Assert.Equal("", invite.Nom);
        }

        [Fact]
        public void Invite_AvecCaracteresSpeciaux()
        {
            Invite invite = new Invite(1, "O'CONNOR", "Jean", null, null);

            Assert.Equal("O'CONNOR", invite.Nom);
        }

        #endregion
    }
}