using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using API_Footies.Data.DAO;
using API_Footies.Metier;

namespace UnitTest_Footies
{
    public class TestGroupeInvite
    {
        private const long ID_UTILISATEUR_TEST = 1;

        [Fact]
        public void AjouterGroupeInvite()
        {
            GroupeInviteDAO groupeInviteDAO = new GroupeInviteDAO();
            List<Invite> listeVide = new List<Invite>();
            GroupeInvites groupe = new GroupeInvites(0, "Groupe Test", listeVide);

            bool resultat = groupeInviteDAO.AjouterGroupeInvites(groupe, ID_UTILISATEUR_TEST);

            Assert.True(resultat);
            Assert.True(groupe.IdGroupeInvites > 0);

            groupeInviteDAO.SupprimerGroupeInvite(groupe.IdGroupeInvites, ID_UTILISATEUR_TEST);
        }

        [Fact]
        public void AjouterGroupeAvecInvites()
        {
            InviteDAO inviteDAO = new InviteDAO();
            GroupeInviteDAO groupeInviteDAO = new GroupeInviteDAO();

            Invite invite1 = new Invite(0, "Dupont", "Jean", "0600000000", "jean@test.com", null, null, null);
            inviteDAO.AjouterInvite(invite1, ID_UTILISATEUR_TEST);

            List<Invite> invites = new List<Invite>();
            invites.Add(invite1);

            GroupeInvites groupe = new GroupeInvites(0, "Groupe Avec Membres", invites);
            bool resultat = groupeInviteDAO.AjouterGroupeInvites(groupe, ID_UTILISATEUR_TEST);

            Assert.True(resultat);

            List<GroupeInvites> groupes = groupeInviteDAO.ListeGroupesInvites(ID_UTILISATEUR_TEST);
            GroupeInvites groupeRecupere = groupes.FirstOrDefault(g => g.IdGroupeInvites == groupe.IdGroupeInvites);

            Assert.NotNull(groupeRecupere);
            Assert.NotEmpty(groupeRecupere.Invites);
            Assert.Equal(invite1.Id, groupeRecupere.Invites[0].Id);

            groupeInviteDAO.SupprimerGroupeInvite(groupe.IdGroupeInvites, ID_UTILISATEUR_TEST);
            inviteDAO.SupprimerInvite(invite1.Id, ID_UTILISATEUR_TEST);
        }

        [Fact]
        public void ModifierGroupeInvite()
        {
            GroupeInviteDAO groupeInviteDAO = new GroupeInviteDAO();
            GroupeInvites groupe = new GroupeInvites(0, "Nom Original", new List<Invite>());
            groupeInviteDAO.AjouterGroupeInvites(groupe, ID_UTILISATEUR_TEST);

            groupe.Nom = "Nom Modifie";
            bool resultat = groupeInviteDAO.ModifierGroupe(groupe, ID_UTILISATEUR_TEST);

            Assert.True(resultat);

            List<GroupeInvites> groupes = groupeInviteDAO.ListeGroupesInvites(ID_UTILISATEUR_TEST);
            GroupeInvites groupeModifie = groupes.FirstOrDefault(g => g.IdGroupeInvites == groupe.IdGroupeInvites);

            Assert.NotNull(groupeModifie);
            Assert.Equal("Nom Modifie", groupeModifie.Nom);

            groupeInviteDAO.SupprimerGroupeInvite(groupe.IdGroupeInvites, ID_UTILISATEUR_TEST);
        }

        [Fact]
        public void SupprimerGroupeInvite()
        {
            GroupeInviteDAO groupeInviteDAO = new GroupeInviteDAO();
            GroupeInvites groupe = new GroupeInvites(0, "A Supprimer", new List<Invite>());
            groupeInviteDAO.AjouterGroupeInvites(groupe, ID_UTILISATEUR_TEST);
            long idGroupe = groupe.IdGroupeInvites;

            groupeInviteDAO.SupprimerGroupeInvite(idGroupe, ID_UTILISATEUR_TEST);

            List<GroupeInvites> groupes = groupeInviteDAO.ListeGroupesInvites(ID_UTILISATEUR_TEST);
            GroupeInvites groupeSupprime = groupes.FirstOrDefault(g => g.IdGroupeInvites == idGroupe);

            Assert.Null(groupeSupprime);
        }

        [Fact]
        public void ChercherGroupeInvite()
        {
            GroupeInviteDAO groupeDAO = new GroupeInviteDAO();
            GroupeInvites groupe1 = new GroupeInvites(0, "Groupe Alpha", new List<Invite>());
            GroupeInvites groupe2 = new GroupeInvites(0, "Groupe Beta", new List<Invite>());

            groupeDAO.AjouterGroupeInvites(groupe1, ID_UTILISATEUR_TEST);
            groupeDAO.AjouterGroupeInvites(groupe2, ID_UTILISATEUR_TEST);

            List<GroupeInvites> resultats = groupeDAO.ChercherGroupeInvites("Alpha", ID_UTILISATEUR_TEST);

            Assert.NotNull(resultats);
            Assert.Single(resultats);
            Assert.Equal("Groupe Alpha", resultats[0].Nom);

            groupeDAO.SupprimerGroupeInvite(groupe1.IdGroupeInvites, ID_UTILISATEUR_TEST);
            groupeDAO.SupprimerGroupeInvite(groupe2.IdGroupeInvites, ID_UTILISATEUR_TEST);
        }
    }
}