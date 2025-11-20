using METIER_Footies.Metier;
using VM_Footies.VM;

namespace Test_Footies_METIER
{
    public class GroupeInviteTest
    {
        #region Test avec constructeurs

        [Fact]
        public void ConstructeurCopie()
        {
            GroupeInvites groupeOriginal = new GroupeInvites
            {
                IdGroupeInvites = 1,
                Nom = "Groupe Famille",
                Invites = new List<Invite>
                {
                    new Invite(1, "Dupont", "Jean", "0612345678", "jean@gmail.com"),
                    new Invite(2, "Martin", "Sophie", "0698765432", "sophie@gmail.com")
                }
            };

            GroupeInvites groupeCopie = new GroupeInvites(groupeOriginal);

            Assert.Equal(groupeOriginal.IdGroupeInvites, groupeCopie.IdGroupeInvites);
            Assert.Equal(groupeOriginal.Nom, groupeCopie.Nom);
            Assert.Equal(groupeOriginal.Invites.Count, groupeCopie.Invites.Count);
        }

        [Fact]
        public void ConstructeurParDefaut()
        {
            GroupeInvites groupe = new GroupeInvites();

            Assert.Equal("", groupe.Nom);
            Assert.NotNull(groupe.Invites);
            Assert.Empty(groupe.Invites);
        }


        #endregion

        #region Test avec exceptions

        [Fact]
        public void IdGroupeInvites_PeutEtreModifie()
        {
            GroupeInvites groupe = new GroupeInvites { IdGroupeInvites = 1, Nom = "Groupe" };

            groupe.IdGroupeInvites = 100;

            Assert.Equal(100, groupe.IdGroupeInvites);
        }

        [Fact]
        public void Nom_PeutEtreModifie()
        {
            GroupeInvites groupe = new GroupeInvites { IdGroupeInvites = 1, Nom = "Groupe Initial" };

            groupe.Nom = "Groupe Modifié";

            Assert.Equal("Groupe Modifié", groupe.Nom);
        }

        [Fact]
        public void Invites_AjoutDiminueListe()
        {
            GroupeInvites groupe = new GroupeInvites { IdGroupeInvites = 1, Nom = "Groupe" };
            Invite invite1 = new Invite(1, "Dupont", "Jean", "0612345678", "jean@gmail.com");
            Invite invite2 = new Invite(2, "Martin", "Sophie", "0698765432", "sophie@gmail.com");

            groupe.Invites.Add(invite1);
            groupe.Invites.Add(invite2);
            Assert.Equal(2, groupe.Invites.Count);

            groupe.Invites.Remove(invite1);

            Assert.Single(groupe.Invites);
            Assert.DoesNotContain(invite1, groupe.Invites);
            Assert.Contains(invite2, groupe.Invites);
        }

        #endregion

        #region Test avec VMGroupeInvite

        [Fact]
        public void VMGroupeInvite_AvecGroupeValide()
        {
            GroupeInvites groupe = new GroupeInvites
            {
                IdGroupeInvites = 1,
                Nom = "Groupe Amis",
                Invites = new List<Invite>
                {
                    new Invite(1, "Dupont", "Jean", "0612345678", "jean@gmail.com")
                }
            };

            VMGroupeInvite vmGroupe = new VMGroupeInvite(groupe);

            Assert.Equal(groupe.Nom, vmGroupe.Nom);
            Assert.Equal(groupe.Invites.Count, vmGroupe.Invites.Count);
            Assert.NotNull(vmGroupe.InvitesListe);
        }

        #endregion

        #region Tests de validation métier supplémentaires

        [Fact]
        public void GroupeInvite_AvecNomVide()
        {
            GroupeInvites groupe = new GroupeInvites { IdGroupeInvites = 1, Nom = "" };

            Assert.Equal("", groupe.Nom);
        }

        [Fact]
        public void GroupeInvite_AvecCaracteresSpeciaux()
        {
            GroupeInvites groupe = new GroupeInvites
            {
                IdGroupeInvites = 1,
                Nom = "Groupe d'été 2024 - Famille & Amis"
            };

            Assert.Equal("Groupe d'été 2024 - Famille & Amis", groupe.Nom);
        }

        [Fact]
        public void GroupeInvite_AvecInvitesDupliques()
        {
            Invite invite = new Invite(1, "Dupont", "Jean", "0612345678", "jean@gmail.com");

            GroupeInvites groupe = new GroupeInvites
            {
                IdGroupeInvites = 1,
                Nom = "Groupe Doublons",
                Invites = new List<Invite> { invite, invite, invite }
            };

            Assert.Equal(3, groupe.Invites.Count);
            Assert.All(groupe.Invites, i => Assert.Same(invite, i));
        }

        [Fact]
        public void GroupeInvite_ListeInvites_EstModifiable()
        {
            GroupeInvites groupe = new GroupeInvites
            {
                IdGroupeInvites = 1,
                Nom = "Groupe",
                Invites = new List<Invite>()
            };

            Invite invite1 = new Invite(1, "Dupont", "Jean", "0612345678", "jean@gmail.com");
            Invite invite2 = new Invite(2, "Martin", "Sophie", "0698765432", "sophie@gmail.com");

            groupe.Invites.Add(invite1);
            groupe.Invites.Add(invite2);

            Assert.Equal(2, groupe.Invites.Count);
            Assert.Contains(invite1, groupe.Invites);
            Assert.Contains(invite2, groupe.Invites);
        }

        #endregion
    }
}