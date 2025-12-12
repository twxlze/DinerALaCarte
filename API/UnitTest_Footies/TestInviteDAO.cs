using System;
using System.Collections.Generic;
using System.Linq;
using API_Footies.Data.DAO;
using API_Footies.Metier;

namespace UnitTest_Footies
{
    public class TestInviteDAO
    {
        private const long ID_UTILISATEUR_TEST = 1;

        [Fact]
        public void AjouterInvite()
        {
            InviteDAO inviteDAO = new InviteDAO();
            Invite invite = new Invite(0, "Dupont", "Jean", "0612345678", "jean.dupont@email.com", null, null, null);

            bool resultat = inviteDAO.AjouterInvite(invite, ID_UTILISATEUR_TEST);

            Assert.True(resultat);
            Assert.True(invite.Id > 0);

            inviteDAO.SupprimerInvite(invite.Id, ID_UTILISATEUR_TEST);
        }

        [Fact]
        public void RecupererListeInvites()
        {
            InviteDAO inviteDAO = new InviteDAO();
            Invite invite1 = new Invite(0, "Martin", "Sophie", "0623456789", "sophie.martin@email.com", null, null, null);
            Invite invite2 = new Invite(0, "Bernard", "Pierre", "0634567890", "pierre.bernard@email.com", null, null, null);

            inviteDAO.AjouterInvite(invite1, ID_UTILISATEUR_TEST);
            inviteDAO.AjouterInvite(invite2, ID_UTILISATEUR_TEST);

            List<Invite> listeInvites = inviteDAO.ListInvite(ID_UTILISATEUR_TEST);

            Assert.NotNull(listeInvites);
            Assert.True(listeInvites.Count >= 2);
            Assert.Contains(listeInvites, i => i.Nom == "Martin");
            Assert.Contains(listeInvites, i => i.Nom == "Bernard");

            inviteDAO.SupprimerInvite(invite1.Id, ID_UTILISATEUR_TEST);
            inviteDAO.SupprimerInvite(invite2.Id, ID_UTILISATEUR_TEST);
        }

        [Fact]
        public void ModifierInvite()
        {
            InviteDAO inviteDAO = new InviteDAO();
            Invite invite = new Invite(0, "Durand", "Paul", "0645678901", "paul.durand@email.com", null, null, null);
            inviteDAO.AjouterInvite(invite, ID_UTILISATEUR_TEST);

            invite.Nom = "Durand-Modifie";
            invite.Prenom = "Paul-Modifie";
            invite.Telephone = "0656789012";
            invite.Email = "paul.modifie@email.com";

            bool resultat = inviteDAO.ModifierInvite(invite, ID_UTILISATEUR_TEST);

            Assert.True(resultat);

            List<Invite> listeInvites = inviteDAO.ListInvite(ID_UTILISATEUR_TEST);
            Invite inviteModifie = listeInvites.FirstOrDefault(i => i.Id == invite.Id);

            Assert.NotNull(inviteModifie);
            Assert.Equal("Durand-Modifie", inviteModifie.Nom);
            Assert.Equal("Paul-Modifie", inviteModifie.Prenom);
            Assert.Equal("0656789012", inviteModifie.Telephone);
            Assert.Equal("paul.modifie@email.com", inviteModifie.Email);

            inviteDAO.SupprimerInvite(invite.Id, ID_UTILISATEUR_TEST);
        }

        [Fact]
        public void SupprimerInvite()
        {
            InviteDAO inviteDAO = new InviteDAO();
            Invite invite = new Invite(0, "Petit", "Marie", "0667890123", "marie.petit@email.com", null, null, null);
            inviteDAO.AjouterInvite(invite, ID_UTILISATEUR_TEST);
            long idInvite = invite.Id;

            inviteDAO.SupprimerInvite(idInvite, ID_UTILISATEUR_TEST);

            List<Invite> listeInvites = inviteDAO.ListInvite(ID_UTILISATEUR_TEST);
            Invite inviteSupprime = listeInvites.FirstOrDefault(i => i.Id == idInvite);
            Assert.Null(inviteSupprime);
        }

        [Fact]
        public void ChercherInviteParNom()
        {
            InviteDAO inviteDAO = new InviteDAO();
            Invite invite1 = new Invite(0, "Moreau", "Alice", "0689012345", "alice.moreau@email.com", null, null, null);
            Invite invite2 = new Invite(0, "Morel", "Bob", "0690123456", "bob.morel@email.com", null, null, null);

            inviteDAO.AjouterInvite(invite1, ID_UTILISATEUR_TEST);
            inviteDAO.AjouterInvite(invite2, ID_UTILISATEUR_TEST);

            List<Invite> resultats = inviteDAO.ChercherInvite("More", ID_UTILISATEUR_TEST);

            Assert.NotNull(resultats);
            Assert.True(resultats.Count >= 2);
            Assert.Contains(resultats, i => i.Nom == "Moreau");
            Assert.Contains(resultats, i => i.Nom == "Morel");

            inviteDAO.SupprimerInvite(invite1.Id, ID_UTILISATEUR_TEST);
            inviteDAO.SupprimerInvite(invite2.Id, ID_UTILISATEUR_TEST);
        }
    }
}