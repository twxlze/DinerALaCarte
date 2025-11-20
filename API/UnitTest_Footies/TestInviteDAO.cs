using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API_Footies.Data.DAO;
using API_Footies.Metier;

namespace UnitTest_Footies
{
    public class TestInviteDAO
    {
        [Fact]
        public void AjouterInvite()
        {
            InviteDAO inviteDAO = new InviteDAO();
            Invite invite = new Invite(0, "Dupont", "Jean", "0612345678", "jean.dupont@email.com");

            bool resultat = inviteDAO.AjouterInvite(invite);

            Assert.True(resultat);
            Assert.True(invite.Id > 0);

            inviteDAO.SupprimerInvite(invite.Id);
        }

        [Fact]
        public void RecupererListeInvites()
        {
            InviteDAO inviteDAO = new InviteDAO();
            Invite invite1 = new Invite(0, "Martin", "Sophie", "0623456789", "sophie.martin@email.com");
            Invite invite2 = new Invite(0, "Bernard", "Pierre", "0634567890", "pierre.bernard@email.com");

            inviteDAO.AjouterInvite(invite1);
            inviteDAO.AjouterInvite(invite2);

            List<Invite> listeInvites = inviteDAO.ListInvite();

            Assert.NotNull(listeInvites);
            Assert.True(listeInvites.Count >= 2);
            Assert.Contains(listeInvites, i => i.Nom == "Martin");
            Assert.Contains(listeInvites, i => i.Nom == "Bernard");

            inviteDAO.SupprimerInvite(invite1.Id);
            inviteDAO.SupprimerInvite(invite2.Id);
        }

        [Fact]
        public void ModifierInvite()
        {
            InviteDAO inviteDAO = new InviteDAO();
            Invite invite = new Invite(0, "Durand", "Paul", "0645678901", "paul.durand@email.com");
            inviteDAO.AjouterInvite(invite);

            invite.Nom = "Durand-Modifie";
            invite.Prenom = "Paul-Modifie";
            invite.Telephone = "0656789012";
            invite.Email = "paul.modifie@email.com";
            bool resultat = inviteDAO.ModifierInvite(invite);

            Assert.True(resultat);
            List<Invite> listeInvites = inviteDAO.ListInvite();
            Invite inviteModifie = listeInvites.FirstOrDefault(i => i.Id == invite.Id);
            Assert.NotNull(inviteModifie);
            Assert.Equal("Durand-Modifie", inviteModifie.Nom);
            Assert.Equal("Paul-Modifie", inviteModifie.Prenom);
            Assert.Equal("0656789012", inviteModifie.Telephone);
            Assert.Equal("paul.modifie@email.com", inviteModifie.Email);

            inviteDAO.SupprimerInvite(invite.Id);
        }

        [Fact]
        public void SupprimerInvite()
        {
            InviteDAO inviteDAO = new InviteDAO();
            Invite invite = new Invite(0, "Petit", "Marie", "0667890123", "marie.petit@email.com");
            inviteDAO.AjouterInvite(invite);
            long idInvite = invite.Id;

            inviteDAO.SupprimerInvite(idInvite);

            List<Invite> listeInvites = inviteDAO.ListInvite();
            Invite inviteSupprime = listeInvites.FirstOrDefault(i => i.Id == idInvite);
            Assert.Null(inviteSupprime);
        }

        [Fact]
        public void EstDansUnGroupeRetourneFalse()
        {
            InviteDAO inviteDAO = new InviteDAO();
            Invite invite = new Invite(0, "Roux", "Luc", "0678901234", "luc.roux@email.com");
            inviteDAO.AjouterInvite(invite);

            bool resultat = inviteDAO.EstDansUnGroupe(invite.Id);

            Assert.False(resultat);

            inviteDAO.SupprimerInvite(invite.Id);
        }

        [Fact]
        public void ChercherInviteParNom()
        {
            InviteDAO inviteDAO = new InviteDAO();
            Invite invite1 = new Invite(0, "Moreau", "Alice", "0689012345", "alice.moreau@email.com");
            Invite invite2 = new Invite(0, "Morel", "Bob", "0690123456", "bob.morel@email.com");
            inviteDAO.AjouterInvite(invite1);
            inviteDAO.AjouterInvite(invite2);

            List<Invite> resultats = inviteDAO.ChercherInvite("More");

            Assert.NotNull(resultats);
            Assert.True(resultats.Count >= 2);
            Assert.Contains(resultats, i => i.Nom == "Moreau");
            Assert.Contains(resultats, i => i.Nom == "Morel");

            inviteDAO.SupprimerInvite(invite1.Id);
            inviteDAO.SupprimerInvite(invite2.Id);
        }

        [Fact]
        public void ChercherInviteParPrenom()
        {
            InviteDAO inviteDAO = new InviteDAO();
            Invite invite = new Invite(0, "Girard", "Julien", "0601234567", "julien.girard@email.com");
            inviteDAO.AjouterInvite(invite);

            List<Invite> resultats = inviteDAO.ChercherInvite("Julie");

            Assert.NotNull(resultats);
            Assert.True(resultats.Count >= 1);
            Assert.Contains(resultats, i => i.Prenom == "Julien");

            inviteDAO.SupprimerInvite(invite.Id);
        }

        [Fact]
        public void ChercherInviteAucunResultat()
        {
            InviteDAO inviteDAO = new InviteDAO();

            List<Invite> resultats = inviteDAO.ChercherInvite("TexteIntrouvable123");

            Assert.NotNull(resultats);
            Assert.Empty(resultats);
        }
    }
}
