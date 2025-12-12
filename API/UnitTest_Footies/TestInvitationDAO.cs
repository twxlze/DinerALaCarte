using API_Footies.Controllers;
using API_Footies.Data.DAO;
using API_Footies.Metier;
using API_Footies.Services.Realisations;
using System.Collections.Generic;
using System;
using System.Linq;

namespace UnitTest_Footies
{
    public class TestInvitationDAO
    {
        private const long ID_UTILISATEUR_TEST = 1;

        [Fact]
        public void AjouterInvitation_Integration()
        {
            InvitationDAO dao = new InvitationDAO();
            InvitationService service = new InvitationService(dao);
            InvitationController controller = new InvitationController(service);

            Invitation invitation = new Invitation();
            invitation.Nom = "Soirée Integration";
            invitation.Date = DateTime.Now.AddDays(10);
            invitation.Remarque = "Test";

            Invitation resultat = controller.AjouterInvitation(invitation, ID_UTILISATEUR_TEST);

            Assert.NotNull(resultat);
            Assert.True(resultat.IdInvitation > 0);
            Assert.Equal("Soirée Integration", resultat.Nom);

            controller.SupprimerInvitation(resultat.IdInvitation, ID_UTILISATEUR_TEST);
        }

        [Fact]
        public void ListeInvitations_Integration()
        {
            InvitationDAO dao = new InvitationDAO();
            InvitationService service = new InvitationService(dao);
            InvitationController controller = new InvitationController(service);

            Invitation invit1 = new Invitation();
            invit1.Nom = "Invit A";
            invit1.Date = DateTime.Now;

            controller.AjouterInvitation(invit1, ID_UTILISATEUR_TEST);

            List<Invitation> liste = controller.ObtenirToutInvitations(ID_UTILISATEUR_TEST);

            Assert.NotNull(liste);
            Assert.True(liste.Count > 0);

            bool trouve = liste.Any(i => i.IdInvitation == invit1.IdInvitation);
            Assert.True(trouve);

            controller.SupprimerInvitation(invit1.IdInvitation, ID_UTILISATEUR_TEST);
        }

        [Fact]
        public void SupprimerInvitation_Integration()
        {
            InvitationDAO dao = new InvitationDAO();
            InvitationService service = new InvitationService(dao);
            InvitationController controller = new InvitationController(service);

            Invitation invit = new Invitation();
            invit.Nom = "A Supprimer";
            invit.Date = DateTime.Now;

            controller.AjouterInvitation(invit, ID_UTILISATEUR_TEST);
            long id = invit.IdInvitation;


            List<Invitation> liste = controller.ObtenirToutInvitations(ID_UTILISATEUR_TEST);

            bool trouve = liste.Any(i => i.IdInvitation == id);
            Assert.False(trouve);
        }
    }
}