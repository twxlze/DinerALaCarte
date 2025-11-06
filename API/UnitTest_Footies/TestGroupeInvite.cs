using API_Footies.Controllers;
using API_Footies.Data.Interfaces;
using API_Footies.Metier;
using API_Footies.Services.Interfaces;
using API_Footies.Services.Realisations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace UnitTest_Footies
{
    /// <summary>
    /// Tests unitaires pour la gestion des groupes d'invités
    /// </summary>
    public class TestGroupeInvite
    {

        [Fact]
        public void TestCreationGroupeInvite()
        {
            var nomGroupe = "GroupeTest";
            var listeInvites = new List<string> { "Invite1", "Invite2", "Invite3" };
            GroupeInvites groupeInvite = new GroupeInvites();
            groupeInvite.Nom = nomGroupe;
            groupeInvite.Invites = new List<Invite>();
            foreach (var nomInvite in listeInvites)
            {
                Invite invite = new Invite { Nom = nomInvite, Email = "truc@gmail.com", Prenom = nomGroupe + " Truc", Telephone = "454461046146" };
                groupeInvite.Invites.Add(invite);
            }
            Assert.Equal(nomGroupe, groupeInvite.Nom);
            Assert.Equal(3, groupeInvite.Invites.Count);
            Assert.Contains(groupeInvite.Invites, i => i.Nom == "Invite1");
            Assert.True(groupeInvite.Invites.Any());
        }
    }
}
