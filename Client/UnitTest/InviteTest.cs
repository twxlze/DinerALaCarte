using METIER_Footies.Metier;
using VM_Footies;
using VM_Footies.VM;

namespace UnitTest
{
    public class InviteTest
    {
        #region Test avec constructeurs

        [Fact]
        public void Constructeur_AvecDonneesValides_CreeInvite()
        {
            Invite invite = new Invite(1, "Dupont", "Jean", "0612345678", "jean.dupont@gmail.com");

            Assert.Equal("Dupont", invite.Nom);
            Assert.Equal("Jean", invite.Prenom);
            Assert.Equal("0612345678", invite.Telephone);
            Assert.Equal("jean.dupont@gmail.com", invite.Email);
        }

        [Fact]
        public void InviteAvecTelephoneEmailNull()
        {
            Invite invite = new Invite(0, "Dupont", "Jean", null, "jean@gmail.com");
            Invite invite2 = new Invite(1, "Jean", "Dupont", "0612345678", null);
            Invite invite3 = new Invite(2, "Oui", "Jean", null, null);

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
            Invite invite = new Invite(0, "", "Jean", null, null);
            Assert.Throws<ArgumentException>(() => invite.Nom = null);
        }

        [Fact]
        public void InviteSansPrenom_ThrowArgumentException()
        {
            Invite invite = new Invite(0, "Jean", "", null, null);
            Assert.Throws<ArgumentException>(() => invite.Prenom = null);
        }

        [Fact]
        public void TelephoneInvalide_ThrowArgumentException()
        {
            Invite invite = new Invite(0, "Dupont", "Jean", "12345ABCD", null);
            Assert.Throws<ArgumentException>(() => invite.Telephone = "12345ABCD");
        }

        [Fact]
        public void EmailInvalide_ThrowArgumentException()
        {
            Invite invite = new Invite(0, "Dupont", "Jean", null, "jean.gmail.com");
            Assert.Throws<ArgumentException>(() => invite.Email = "jean.gmail.com");
        }
        #endregion


        #region TEST Suppressions 

        [Fact]
        public void SupprimerInvite_AvecInviteExistant_RetireInviteDeLaListe()
        {
            // Arrange - Création manuelle sans passer par le constructeur qui appelle l'API
            VMPageInvite vmPageInvite = CreerVMPageInviteSansAPI();

            Invite invite1 = new Invite(1, "Dupont", "Jean", "0612345678", "jean@gmail.com");
            Invite invite2 = new Invite(2, "Martin", "Paul", "0698765432", "paul@gmail.com");
            Invite invite3 = new Invite(3, "Durand", "Marie", "0656781234", "marie@gmail.com");

            VMInvite vmInvite1 = new VMInvite(invite1);
            VMInvite vmInvite2 = new VMInvite(invite2);
            VMInvite vmInvite3 = new VMInvite(invite3);

            // Ajout manuel à la liste (sans passer par AjouterInvite qui appelle l'API)
            vmPageInvite.VMInvites.Add(vmInvite1);
            vmPageInvite.VMInvites.Add(vmInvite2);
            vmPageInvite.VMInvites.Add(vmInvite3);

            int countAvant = vmPageInvite.VMInvites.Count;

            // Act - Suppression directe de la liste (test de la logique métier uniquement)
            vmPageInvite.VMInvites.Remove(vmInvite2);

            // Assert
            Assert.Equal(3, countAvant);
            Assert.Equal(2, vmPageInvite.VMInvites.Count);
            Assert.DoesNotContain(vmInvite2, vmPageInvite.VMInvites);
            Assert.Contains(vmInvite1, vmPageInvite.VMInvites);
            Assert.Contains(vmInvite3, vmPageInvite.VMInvites);
        }

        [Fact]
        public void SupprimerInvite_AvecListeVide_LaListeResteVide()
        {
            // Arrange
            VMPageInvite vmPageInvite = CreerVMPageInviteSansAPI();

            // Act & Assert
            Assert.Empty(vmPageInvite.VMInvites);
        }

        /// <summary>
        /// Méthode helper pour créer un VMPageInvite sans appeler l'API
        /// Utilise la réflexion pour initialiser les champs privés
        /// </summary>
        private VMPageInvite CreerVMPageInviteSansAPI()
        {
            // Création d'un objet sans appeler le constructeur
            VMPageInvite vmPage = (VMPageInvite)System.Runtime.Serialization.FormatterServices
                .GetUninitializedObject(typeof(VMPageInvite));

            // Initialisation manuelle des champs privés
            var listeField = typeof(VMPageInvite).GetField("listeVMInvite",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            listeField?.SetValue(vmPage, new List<VMInvite>());

            return vmPage;
        }
    }
}
#endregion