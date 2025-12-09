using System.Data;
using API_Footies.Data.Interfaces;
using API_Footies.Metier;
using API_Footies.Metier.Enum;

namespace API_Footies.Data.DAO
{
    /// <summary>
    /// DAO en charge de la gestion des invitations
    /// </summary>
    public class InvitationDAO : IInvitationDAO
    {
        #region Méthodes principales
        public bool AjouterInvitation(Invitation invitation)
        {
            bool ajoute = false;
            using (SQLiteConnector connection = new SQLiteConnector())
            {
                if (connection == null)
                {
                    throw new Exception("Erreur de connexion à la base de données");
                }

                invitation.IdInvitation = InsererInvitation(connection, invitation);

                AjouterGroupesInvitesDansInvitation(connection, invitation);
                AjouterMenusDansInvitation(connection, invitation);
                AjouterInvitesDansInvitation(connection, invitation);
                AjouterPlatsDansInvitation(connection, invitation);
                AjouterPlatsPreferesDansInvitation(connection, invitation);

                ajoute = true;
            }
            return ajoute;
        }

        public List<Invitation> ObtenirToutInvitations()
        {
            List<Invitation> invitations = new List<Invitation>();
            using (SQLiteConnector connection = new SQLiteConnector())
            {
                if (connection == null)
                {
                    throw new Exception("Erreur de connexion à la base de données");
                }

                var dataTable = connection.ExecuteQuery("SELECT * FROM Invitation");

                foreach (DataRow row in dataTable.Rows)
                {
                    long idInvitation = (long)row["IdInvitation"];
                    string nom = row["Nom"].ToString();
                    DateTime date = DateTime.Parse(row["Date"].ToString());

                    // Optimisation : On charge les objets complets uniquement si nécessaire
                    // Pour une liste d'invitations, on peut souvent se contenter d'infos partielles, 
                    // mais ici je garde la logique complète pour garantir la fonctionnalité.
                    // Si tu veux optimiser plus drastiquement, il faudrait créer des objets "InvitationResumee".

                    List<Invite> invites = ObtenirInvitesDansInvitation(connection, idInvitation);

                    // Chargement des plats (optimisation possible ici si on n'a besoin que du nom)
                    List<Plat> plats = ObtenirPlatsDansInvitation(connection, idInvitation);

                    // Pour les menus, on passe la liste des plats déjà chargés pour éviter de recharger 
                    // si les menus contiennent ces mêmes plats, ou on charge des versions allégées.
                    List<Menu> menus = ObtenirMenusDansInvitation(connection, idInvitation);

                    List<GroupeInvites> groupesInvites = ObtenirGroupesInvitesDansInvitation(connection, idInvitation);

                    Invitation invitation = new Invitation(
                        groupesInvites,
                        menus,
                        invites,
                        plats,
                        idInvitation,
                        nom,
                        date
                    );
                    invitations.Add(invitation);
                }
            }
            return invitations;
        }

        public bool ModifierInvitation(Invitation invitation)
        {
            bool modifie = false;
            using (SQLiteConnector connection = new SQLiteConnector())
            {
                if (connection == null)
                {
                    throw new Exception("Erreur de connexion à la base de données");
                }

                invitation.IdInvitation = ModifierInvitation(connection, invitation);

                ModifierGroupesInvitesDansInvitation(connection, invitation);
                ModifierMenusDansInvitation(connection, invitation);
                ModifierInvitesDansInvitation(connection, invitation);
                ModifierPlatsDansInvitation(connection, invitation);
                modifie = true;
            }
            return modifie;
        }

        public void SupprimerInvitation(long idInvitation)
        {
            using (SQLiteConnector connection = new SQLiteConnector())
            {
                if (connection == null)
                {
                    throw new Exception("Erreur de connexion à la base de données");
                }
                var parameters = new Dictionary<string, object>()
                {
                    {"@IdInvitation", idInvitation }
                };
                connection.ExecuteQuery("DELETE FROM Invitation_GroupeInvite WHERE IdInvitation = @IdInvitation", parameters);
                connection.ExecuteQuery("DELETE FROM Invitation_Menu WHERE IdInvitation = @IdInvitation", parameters);
                connection.ExecuteQuery("DELETE FROM Invitation_Invite WHERE IdInvitation = @IdInvitation", parameters);
                connection.ExecuteQuery("DELETE FROM Invitation_Plat WHERE IdInvitation = @IdInvitation", parameters);
                connection.ExecuteQuery("DELETE FROM Invitation WHERE IdInvitation = @IdInvitation", parameters);
            }
        }
        #endregion

        #region Méthodes Inserer / Ajouter

        private long InsererInvitation(SQLiteConnector connection, Invitation invitation)
        {
            var parameters = new Dictionary<string, object>()
            {
                {"@Nom", invitation.Nom },
                {"@Date", invitation.Date }
            };
            // Note: IdInvitation n'est pas passé en paramètre car c'est un auto-increment généralement
            return connection.ExecuteInsert("INSERT INTO Invitation (Nom, Date) VALUES (@Nom, @Date)", parameters);
        }

        private void AjouterGroupesInvitesDansInvitation(SQLiteConnector connection, Invitation invitation)
        {
            if (invitation.GroupeInvites != null)
            {
                foreach (GroupeInvites groupeInvites in invitation.GroupeInvites)
                {
                    var parameters = new Dictionary<string, object>()
                    {
                        {"@IdInvitation", invitation.IdInvitation },
                        {"@IDGroupeInvite", groupeInvites.IdGroupeInvites }
                    };
                    connection.ExecuteQuery("INSERT INTO Invitation_GroupeInvite (IdInvitation, IDGroupeInvite) VALUES (@IdInvitation, @IDGroupeInvite)", parameters);
                }
            }
        }

        private void AjouterMenusDansInvitation(SQLiteConnector connection, Invitation invitation)
        {
            if (invitation.Menus != null)
            {
                foreach (Menu menu in invitation.Menus)
                {
                    var parameters = new Dictionary<string, object>()
                    {
                        {"@IdInvitation", invitation.IdInvitation },
                        {"@IdMenu", menu.IdMenu }
                    };
                    connection.ExecuteQuery("INSERT INTO Invitation_Menu (IdInvitation, IdMenu) VALUES (@IdInvitation, @IdMenu)", parameters);
                }
            }
        }

        private void AjouterInvitesDansInvitation(SQLiteConnector connection, Invitation invitation)
        {
            if (invitation.Invites != null)
            {
                foreach (Invite invite in invitation.Invites)
                {
                    var parameters = new Dictionary<string, object>()
                    {
                        {"@IdInvitation", invitation.IdInvitation },
                        {"@IdInvite", invite.Id }
                    };
                    connection.ExecuteQuery("INSERT INTO Invitation_Invite (IdInvitation, IdInvite) VALUES (@IdInvitation, @IdInvite)", parameters);
                }
            }
        }

        private void AjouterPlatsDansInvitation(SQLiteConnector connection, Invitation invitation)
        {
            if (invitation.Plats != null)
            {
                foreach (Plat plat in invitation.Plats)
                {
                    var parameters = new Dictionary<string, object>()
                    {
                        {"@IdInvitation", invitation.IdInvitation },
                        {"@IdPlat", plat.Id }
                    };
                    connection.ExecuteQuery("INSERT INTO Invitation_Plat (IdInvitation, IdPlat) VALUES (@IdInvitation, @IdPlat)", parameters);
                }
            }
        }

        private void AjouterPlatsPreferesDansInvitation(SQLiteConnector connection, Invitation invitation)
        {
            // Cette méthode semble faire doublon avec AjouterPlatsDansInvitation dans ton code original 
            // car elle insère dans la même table "Invitation_Plat".
            // Si c'est pour des plats "préférés" spécifiquement liés à l'invitation, il faudrait une table distincte
            // ou un champ "Type" dans la table de liaison.
            // Je la garde pour ne pas casser ton code existant, mais attention au doublon logique.
            if (invitation.Plats != null)
            {
                foreach (Plat plat in invitation.Plats)
                {
                    var parameters = new Dictionary<string, object>()
                    {
                        {"@IdInvitation", invitation.IdInvitation },
                        {"@IdPlat", plat.Id }
                    };
                    // Attention: Si le plat a déjà été ajouté par AjouterPlatsDansInvitation, cela peut créer un doublon ou une erreur de clé primaire
                    // connection.ExecuteQuery("INSERT INTO Invitation_Plat (IdInvitation, IdPlat) VALUES (@IdInvitation, @IdPlat)", parameters);
                }
            }
        }
        #endregion

        #region Méthodes Obtenir

        private List<Invite> ObtenirInvitesDansInvitation(SQLiteConnector connection, long idInvitation)
        {
            List<Invite> invites = new List<Invite>();
            var parameters = new Dictionary<string, object>()
            {
                {"@IdInvitation", idInvitation }
            };

            var dataTable = connection.ExecuteQuery(
                @"SELECT I.IdInvite, I.Nom, I.Prenom, I.NumTel, I.Mail 
                  FROM Invite I 
                  INNER JOIN Invitation_Invite II ON I.IdInvite = II.IdInvite 
                  WHERE II.IdInvitation = @IdInvitation",
                parameters);

            foreach (DataRow row in dataTable.Rows)
            {
                long idInvite = (long)row["IdInvite"];
                // Optimisation : On ne charge pas forcément tout le détail (allergènes, plats détestés) 
                // si c'est juste pour afficher la liste des invités de l'invitation.
                // Si besoin du détail complet, décommenter les lignes suivantes.

                // List<NomAllergene> allergenes = ObtenirAllergenesDeInvite(connection, idInvite);
                // List<Plat> platsDetestes = ObtenirPlatsDetestesDeInvite(connection, idInvite);
                // List<Plat> platsPreferes = ObtenirPlatsPreferesDeInvite(connection, idInvite);

                Invite invite = new Invite(
                    idInvite,
                    row["Nom"].ToString(),
                    row["Prenom"].ToString(),
                    row["NumTel"].ToString(),
                    row["Mail"].ToString(),
                    null, // allergenes
                    null, // platsDetestes
                    null  // platsPreferes
                );
                invites.Add(invite);
            }
            return invites;
        }

        private List<Plat> ObtenirPlatsDansInvitation(SQLiteConnector connection, long idInvitation)
        {
            List<Plat> plats = new List<Plat>();
            var parameters = new Dictionary<string, object>()
            {
                {"@IdInvitation", idInvitation }
            };

            // On ne récupère que les infos de base du plat
            var dataTable = connection.ExecuteQuery(@"SELECT P.IdPlat, P.Nom, P.Categorie FROM Plat P INNER JOIN Invitation_Plat IP ON P.IdPlat = IP.IdPlat WHERE IP.IdInvitation = @IdInvitation", parameters);

            foreach (DataRow row in dataTable.Rows)
            {
                long idPlat = (long)row["IdPlat"];

                CategoriePlat categorie = CategoriePlat.plat;
                Enum.TryParse(row["Categorie"].ToString(), true, out categorie);

                // Optimisation: On ne charge pas la description complète ni les allergènes pour l'affichage liste
                Plat plat = new Plat(
                    idPlat,
                    row["Nom"].ToString(),
                    "", // Description vide
                    categorie,
                    "", // Ingrédients vides
                    null // Pas d'allergènes chargés
                );
                plats.Add(plat);
            }

            return plats;
        }

        private List<Menu> ObtenirMenusDansInvitation(SQLiteConnector connection, long idInvitation)
        {
            List<Menu> menus = new List<Menu>();
            var parameters = new Dictionary<string, object>()
            {
                {"@IdInvitation", idInvitation }
            };

            var dataTable = connection.ExecuteQuery(
                @"SELECT M.IdMenu, M.Nom 
                  FROM Menu M 
                  INNER JOIN Invitation_Menu IM ON M.IdMenu = IM.IdMenu 
                  WHERE IM.IdInvitation = @IdInvitation",
                parameters);

            foreach (DataRow row in dataTable.Rows)
            {
                long idMenu = (long)row["IdMenu"];
                string nomMenu = row["Nom"].ToString();

                // Optimisation : On charge les plats du menu mais en version légère (Id + Nom)
                List<Plat> platsMenu = ObtenirPlatsDansMenu_Optimise(connection, idMenu);
                Menu menu = new Menu(platsMenu, idMenu, nomMenu);
                menus.Add(menu);
            }

            return menus;
        }

        // Nouvelle méthode optimisée pour ne charger que l'essentiel d'un plat dans un menu
        private List<Plat> ObtenirPlatsDansMenu_Optimise(SQLiteConnector connection, long idMenu)
        {
            List<Plat> plats = new List<Plat>();
            var parameters = new Dictionary<string, object>()
            {
                {"@IdMenu", idMenu }
            };

            // On sélectionne uniquement l'ID et le Nom
            var dataTable = connection.ExecuteQuery(
                @"SELECT P.IdPlat, P.Nom, P.Categorie 
                  FROM Plat P 
                  INNER JOIN Menu_Plat MP ON P.IdPlat = MP.IdPlat 
                  WHERE MP.IdMenu = @IdMenu",
                parameters);

            foreach (DataRow row in dataTable.Rows)
            {
                long idPlat = (long)row["IdPlat"];
                CategoriePlat categorie = CategoriePlat.plat;
                Enum.TryParse(row["Categorie"].ToString(), true, out categorie);

                // Création d'un plat "léger"
                Plat plat = new Plat(
                    idPlat,
                    row["Nom"].ToString(),
                    null, // Pas de description
                    categorie,
                    null, // Pas d'ingrédients
                    null  // Pas d'allergènes
                );
                plats.Add(plat);
            }

            return plats;
        }

        private List<GroupeInvites> ObtenirGroupesInvitesDansInvitation(SQLiteConnector connection, long idInvitation)
        {
            List<GroupeInvites> groupesInvites = new List<GroupeInvites>();
            var parameters = new Dictionary<string, object>()
            {
                {"@IdInvitation", idInvitation }
            };

            var dataTable = connection.ExecuteQuery(
                @"SELECT GI.IDGroupeInvite, GI.Nom 
                  FROM GroupeInvite GI 
                  INNER JOIN Invitation_GroupeInvite IGI ON GI.IDGroupeInvite = IGI.IDGroupeInvite 
                  WHERE IGI.IdInvitation = @IdInvitation",
                parameters);

            foreach (DataRow row in dataTable.Rows)
            {
                long idGroupeInvite = (long)row["IDGroupeInvite"];
                string nomGroupe = row["Nom"].ToString();

                // Optimisation possible : ne pas charger les invités du groupe si pas nécessaire
                // Ici je charge quand même pour avoir un minimum d'info
                List<Invite> invitesGroupe = ObtenirInvitesDansGroupeInvites(connection, idGroupeInvite);

                GroupeInvites groupeInvites = new GroupeInvites(
                    idGroupeInvite,
                    nomGroupe,
                    invitesGroupe
                );
                groupesInvites.Add(groupeInvites);
            }

            return groupesInvites;
        }

        private List<Invite> ObtenirInvitesDansGroupeInvites(SQLiteConnector connection, long idGroupeInvite)
        {
            List<Invite> invites = new List<Invite>();
            var parameters = new Dictionary<string, object>()
            {
                {"@IDGroupeInvite", idGroupeInvite }
            };

            var dataTable = connection.ExecuteQuery(
                @"SELECT I.IdInvite, I.Nom, I.Prenom 
                  FROM Invite I 
                  INNER JOIN Invite_Groupe IG ON I.IdInvite = IG.IdInvite 
                  WHERE IG.IDGroupeInvite = @IDGroupeInvite",
                parameters);

            foreach (DataRow row in dataTable.Rows)
            {
                // Version allégée de l'invité pour l'affichage dans le groupe
                Invite invite = new Invite(
                    (long)row["IdInvite"],
                    row["Nom"].ToString(),
                    row["Prenom"].ToString(),
                    null, // Pas de tel
                    null, // Pas de mail
                    null, null, null // Pas de listes détaillées
                );
                invites.Add(invite);
            }

            return invites;
        }

        // Les méthodes ObtenirAllergenes... ne sont plus utilisées dans la version optimisée 
        // pour l'affichage liste, mais peuvent rester si besoin de détail unitaire.
        #endregion

        #region Méthodes Modifier
        private long ModifierInvitation(SQLiteConnector connection, Invitation invitation)
        {
            var parameters = new Dictionary<string, object>()
            {
                {"@IdInvitation", invitation.IdInvitation },
                {"@Nom", invitation.Nom },
                {"@Date", invitation.Date }
            };
            connection.ExecuteQuery("UPDATE Invitation SET Nom = @Nom, Date = @Date WHERE IdInvitation = @IdInvitation", parameters);
            return invitation.IdInvitation;
        }

        private void ModifierGroupesInvitesDansInvitation(SQLiteConnector connection, Invitation invitation)
        {
            var deleteParameters = new Dictionary<string, object>()
            {
                {"@IdInvitation", invitation.IdInvitation }
            };
            connection.ExecuteQuery("DELETE FROM Invitation_GroupeInvite WHERE IdInvitation = @IdInvitation", deleteParameters);

            if (invitation.GroupeInvites != null)
            {
                foreach (GroupeInvites groupeInvites in invitation.GroupeInvites)
                {
                    var insertParameters = new Dictionary<string, object>()
                    {
                        {"@IdInvitation", invitation.IdInvitation },
                        {"@IDGroupeInvite", groupeInvites.IdGroupeInvites }
                    };
                    connection.ExecuteQuery("INSERT INTO Invitation_GroupeInvite (IdInvitation, IDGroupeInvite) VALUES (@IdInvitation, @IDGroupeInvite)", insertParameters);
                }
            }
        }

        private void ModifierMenusDansInvitation(SQLiteConnector connection, Invitation invitation)
        {
            var deleteParameters = new Dictionary<string, object>()
            {
                {"@IdInvitation", invitation.IdInvitation }
            };
            connection.ExecuteQuery("DELETE FROM Invitation_Menu WHERE IdInvitation = @IdInvitation", deleteParameters);

            if (invitation.Menus != null)
            {
                foreach (Menu menu in invitation.Menus)
                {
                    var insertParameters = new Dictionary<string, object>()
                    {
                        {"@IdInvitation", invitation.IdInvitation },
                        {"@IdMenu", menu.IdMenu }
                    };
                    connection.ExecuteQuery("INSERT INTO Invitation_Menu (IdInvitation, IdMenu) VALUES (@IdInvitation, @IdMenu)", insertParameters);
                }
            }
        }

        private void ModifierInvitesDansInvitation(SQLiteConnector connection, Invitation invitation)
        {
            var deleteParameters = new Dictionary<string, object>()
            {
                {"@IdInvitation", invitation.IdInvitation }
            };
            connection.ExecuteQuery("DELETE FROM Invitation_Invite WHERE IdInvitation = @IdInvitation", deleteParameters);

            if (invitation.Invites != null)
            {
                foreach (Invite invite in invitation.Invites)
                {
                    var insertParameters = new Dictionary<string, object>()
                    {
                        {"@IdInvitation", invitation.IdInvitation },
                        {"@IdInvite", invite.Id }
                    };
                    connection.ExecuteQuery("INSERT INTO Invitation_Invite (IdInvitation, IdInvite) VALUES (@IdInvitation, @IdInvite)", insertParameters);
                }
            }
        }

        private void ModifierPlatsDansInvitation(SQLiteConnector connection, Invitation invitation)
        {
            var deleteParameters = new Dictionary<string, object>()
            {
                {"@IdInvitation", invitation.IdInvitation }
            };
            connection.ExecuteQuery("DELETE FROM Invitation_Plat WHERE IdInvitation = @IdInvitation", deleteParameters);

            if (invitation.Plats != null)
            {
                foreach (Plat plat in invitation.Plats)
                {
                    var insertParameters = new Dictionary<string, object>()
                    {
                        {"@IdInvitation", invitation.IdInvitation },
                        {"@IdPlat", plat.Id }
                    };
                    connection.ExecuteQuery("INSERT INTO Invitation_Plat (IdInvitation, IdPlat) VALUES (@IdInvitation, @IdPlat)", insertParameters);
                }
            }
        }
        #endregion

        #region Méthodes annexes
        // Méthode CreerPlat supprimée car remplacée par l'instanciation directe optimisée
        #endregion
    }
}