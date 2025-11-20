using METIER_Footies.Metier;
using METIER_Footies.Enum;
using VM_Footies.VM;
using static METIER_Footies.Metier.Plat;

namespace Test_Footies_METIER
{
    public class InvitationTest
    {
        #region Test avec constructeurs

        [Fact]
        public void ConstructeurCopie()
        {
            Invitation invitationOriginal = new Invitation
            {
                IdInvitation = 1,
                Nom = "Mariage Sophie & Jean",
                Date = new DateTime(2024, 12, 25),
                Invites = new List<Invite>
                {
                    new Invite(1, "Dupont", "Jean", "0612345678", "jean@gmail.com")
                },
                GroupeInvites = new List<GroupeInvites>
                {
                    new GroupeInvites { IdGroupeInvites = 1, Nom = "Famille" }
                },
                Menus = new List<Menu>
                {
                    new Menu { IdMenu = 1, Nom = "Menu Gastronomique" }
                },
                Plats = new List<Plat>
                {
                    new Plat(1, "Salade", "Entrée", CategoriePlat.entree)
                }
            };

            Invitation invitationCopie = new Invitation(invitationOriginal);

            Assert.Equal(invitationOriginal.IdInvitation, invitationCopie.IdInvitation);
            Assert.Equal(invitationOriginal.Nom, invitationCopie.Nom);
            Assert.Equal(invitationOriginal.Date, invitationCopie.Date);
            Assert.Equal(invitationOriginal.Invites.Count, invitationCopie.Invites.Count);
            Assert.Equal(invitationOriginal.GroupeInvites.Count, invitationCopie.GroupeInvites.Count);
            Assert.Equal(invitationOriginal.Menus.Count, invitationCopie.Menus.Count);
            Assert.Equal(invitationOriginal.Plats.Count, invitationCopie.Plats.Count);
        }

        [Fact]
        public void ConstructeurParDefaut()
        {
            Invitation invitation = new Invitation();

            Assert.Equal("", invitation.Nom);
            Assert.NotNull(invitation.Invites);
            Assert.Empty(invitation.Invites);
            Assert.NotNull(invitation.GroupeInvites);
            Assert.Empty(invitation.GroupeInvites);
            Assert.NotNull(invitation.Menus);
            Assert.Empty(invitation.Menus);
            Assert.NotNull(invitation.Plats);
            Assert.Empty(invitation.Plats);
            Assert.NotEqual(DateTime.MinValue, invitation.Date);
        }

        [Fact]
        public void InvitationAvecToutesLesListes()
        {
            Invitation invitation = new Invitation
            {
                IdInvitation = 1,
                Nom = "Événement Complet",
                Date = DateTime.Now,
                Invites = new List<Invite>
                {
                    new Invite(1, "Dupont", "Jean", null, null),
                    new Invite(2, "Martin", "Sophie", null, null)
                },
                GroupeInvites = new List<GroupeInvites>
                {
                    new GroupeInvites { IdGroupeInvites = 1, Nom = "Famille" }
                },
                Menus = new List<Menu>
                {
                    new Menu { IdMenu = 1, Nom = "Menu Principal" }
                },
                Plats = new List<Plat>
                {
                    new Plat(1, "Entrée", "Description", CategoriePlat.entree),
                    new Plat(2, "Plat", "Description", CategoriePlat.plat)
                }
            };

            Assert.Equal(2, invitation.Invites.Count);
            Assert.Single(invitation.GroupeInvites);
            Assert.Single(invitation.Menus);
            Assert.Equal(2, invitation.Plats.Count);
        }

        #endregion

        #region Test avec exceptions

        [Fact]
        public void IdInvitation_AvecValeurNegative()
        {
            Invitation invitation = new Invitation { IdInvitation = 1, Nom = "Test" };

            Assert.Throws<ArgumentException>(() => invitation.IdInvitation = -1);
        }

        [Fact]
        public void IdInvitation_AvecValeurZero()
        {
            Invitation invitation = new Invitation { IdInvitation = 1, Nom = "Test" };

            Assert.Throws<ArgumentException>(() => invitation.IdInvitation = 0);
        }

        [Fact]
        public void Nom_PeutEtreModifie()
        {
            Invitation invitation = new Invitation { IdInvitation = 1, Nom = "Nom Initial" };

            invitation.Nom = "Nom Modifié";

            Assert.Equal("Nom Modifié", invitation.Nom);
        }

        [Fact]
        public void Date_PeutEtreModifiee()
        {
            Invitation invitation = new Invitation { IdInvitation = 1 };
            DateTime nouvelleDate = new DateTime(2025, 6, 15);

            invitation.Date = nouvelleDate;

            Assert.Equal(nouvelleDate, invitation.Date);
        }

        #endregion

        #region Test avec collections

        [Fact]
        public void Invites_AjoutDiminueListe()
        {
            Invitation invitation = new Invitation { IdInvitation = 1, Nom = "Test" };
            Invite invite1 = new Invite(1, "Dupont", "Jean", null, null);
            Invite invite2 = new Invite(2, "Martin", "Sophie", null, null);

            invitation.Invites.Add(invite1);
            invitation.Invites.Add(invite2);
            Assert.Equal(2, invitation.Invites.Count);

            invitation.Invites.Remove(invite1);

            Assert.Single(invitation.Invites);
            Assert.DoesNotContain(invite1, invitation.Invites);
            Assert.Contains(invite2, invitation.Invites);
        }

        [Fact]
        public void GroupeInvites_AjoutDiminueListe()
        {
            Invitation invitation = new Invitation { IdInvitation = 1, Nom = "Test" };
            GroupeInvites groupe1 = new GroupeInvites { IdGroupeInvites = 1, Nom = "Famille" };
            GroupeInvites groupe2 = new GroupeInvites { IdGroupeInvites = 2, Nom = "Amis" };

            invitation.GroupeInvites.Add(groupe1);
            invitation.GroupeInvites.Add(groupe2);
            Assert.Equal(2, invitation.GroupeInvites.Count);

            invitation.GroupeInvites.Remove(groupe1);

            Assert.Single(invitation.GroupeInvites);
            Assert.DoesNotContain(groupe1, invitation.GroupeInvites);
            Assert.Contains(groupe2, invitation.GroupeInvites);
        }

        [Fact]
        public void Menus_AjoutDiminueListe()
        {
            Invitation invitation = new Invitation { IdInvitation = 1, Nom = "Test" };
            Menu menu1 = new Menu { IdMenu = 1, Nom = "Menu 1" };
            Menu menu2 = new Menu { IdMenu = 2, Nom = "Menu 2" };

            invitation.Menus.Add(menu1);
            invitation.Menus.Add(menu2);
            Assert.Equal(2, invitation.Menus.Count);

            invitation.Menus.Remove(menu1);

            Assert.Single(invitation.Menus);
            Assert.DoesNotContain(menu1, invitation.Menus);
            Assert.Contains(menu2, invitation.Menus);
        }

        [Fact]
        public void Plats_AjoutDiminueListe()
        {
            Invitation invitation = new Invitation { IdInvitation = 1, Nom = "Test" };
            Plat plat1 = new Plat(1, "Entrée", "Description", CategoriePlat.entree);
            Plat plat2 = new Plat(2, "Plat", "Description", CategoriePlat.plat);

            invitation.Plats.Add(plat1);
            invitation.Plats.Add(plat2);
            Assert.Equal(2, invitation.Plats.Count);

            invitation.Plats.Remove(plat1);

            Assert.Single(invitation.Plats);
            Assert.DoesNotContain(plat1, invitation.Plats);
            Assert.Contains(plat2, invitation.Plats);
        }

        #endregion

        #region Tests de validation métier supplémentaires

        [Fact]
        public void Invitation_AvecNomVide()
        {
            Invitation invitation = new Invitation { IdInvitation = 1, Nom = "" };

            Assert.Equal("", invitation.Nom);
        }

        [Fact]
        public void Invitation_AvecCaracteresSpeciaux()
        {
            Invitation invitation = new Invitation
            {
                IdInvitation = 1,
                Nom = "Mariage Sophie & Jean - Été 2024"
            };

            Assert.Equal("Mariage Sophie & Jean - Été 2024", invitation.Nom);
        }

        [Fact]
        public void Invitation_DateDansLeFutur()
        {
            DateTime dateFuture = DateTime.Now.AddMonths(6);
            Invitation invitation = new Invitation
            {
                IdInvitation = 1,
                Nom = "Événement Futur",
                Date = dateFuture
            };

            Assert.True(invitation.Date > DateTime.Now);
        }

        [Fact]
        public void Invitation_DateDansLePasse()
        {
            DateTime datePasse = DateTime.Now.AddMonths(-6);
            Invitation invitation = new Invitation
            {
                IdInvitation = 1,
                Nom = "Événement Passé",
                Date = datePasse
            };

            Assert.True(invitation.Date < DateTime.Now);
        }

        #endregion
    }
}