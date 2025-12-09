using System.Data;
using API_Footies.Data.Interfaces;
using API_Footies.Metier;
using API_Footies.Metier.Enum;

namespace API_Footies.Data.DAO
{
    /// <summary>
    /// Classe en charge de tout ce qui touche les invités dans la base de données
    /// </summary>
    public class InviteDAO : IInviteDAO
    {
        public bool AjouterInvite(Invite invite, long idUtilisateur)
        {
            bool ajoute = false;
            using (SQLiteConnector connection = new SQLiteConnector())
            {
                if (connection == null)
                {
                    throw new Exception("Erreur de connexion à la base de données");
                }
                else
                {
                    Dictionary<string, object> parameters = new Dictionary<string, object>()
                    {
                        {"@Nom", invite.Nom },
                        {"@Prenom", invite.Prenom },
                        {"@Telephone", invite.Telephone },
                        {"@Email", invite.Email },
                        {"@IdUtilisateur", idUtilisateur } // Ajouté
                    };

                    // On insère bien l'ID de l'utilisateur
                    invite.Id = connection.ExecuteInsert("INSERT INTO Invite (Nom, Prenom, NumTel, Mail, IdUtilisateur) VALUES (@Nom, @Prenom, @Telephone, @Email, @IdUtilisateur)", parameters);
                    ajoute = true;
                }
            }
            return ajoute;
        }

        public bool ModifierInvite(Invite invite, long idUtilisateur)
        {
            bool modifie = false;
            using (SQLiteConnector connection = new SQLiteConnector())
            {
                if (connection == null)
                {
                    throw new Exception("Erreur de connexion à la base de données");
                }
                else
                {
                    Dictionary<string, object> parameters = new Dictionary<string, object>()
                    {
                        {"@Id", invite.Id },
                        {"@Nom", invite.Nom },
                        {"@Prenom", invite.Prenom },
                        {"@Telephone", invite.Telephone },
                        {"@Email", invite.Email },
                        {"@IdUtilisateur", idUtilisateur } // Ajouté pour sécurité
                    };

                    // On vérifie que l'ID correspond ET que l'utilisateur est le bon
                    connection.ExecuteQuery("UPDATE Invite SET Nom = @Nom, Prenom = @Prenom, NumTel = @Telephone, Mail = @Email WHERE IDInvite = @Id AND IdUtilisateur = @IdUtilisateur", parameters);
                    modifie = true;
                }
            }
            return modifie;
        }

        public List<Invite> ListInvite(long idUtilisateur)
        {
            List<Invite> listeInvite = new List<Invite>();

            using (SQLiteConnector connection = new SQLiteConnector())
            {
                if (connection == null)
                {
                    throw new Exception("Erreur de connexion à la base de données");
                }
                else
                {
                    // Paramètre pour filtrer par utilisateur
                    Dictionary<string, object> parameters = new Dictionary<string, object>()
                    {
                        {"@IdUtilisateur", idUtilisateur }
                    };

                    // Ajout du WHERE IdUtilisateur
                    DataTable dataTable = connection.ExecuteQuery("SELECT * FROM Invite WHERE IdUtilisateur = @IdUtilisateur", parameters);

                    foreach (DataRow? row in dataTable.Rows)
                    {
                        Invite invite = new Invite((long)row["idInvite"], row["nom"].ToString(), row["prenom"].ToString(), row["NumTel"].ToString(), row["mail"].ToString());
                        listeInvite.Add(invite);
                    }
                }
            }
            return listeInvite;
        }

        public void SupprimerInvite(long id, long idUtilisateur)
        {
            using (SQLiteConnector connection = new SQLiteConnector())
            {
                if (connection == null)
                {
                    throw new Exception("Erreur de connexion à la base de données");
                }
                else
                {
                    Dictionary<string, object> parameters = new Dictionary<string, object>()
                    {
                        {"@Id", id },
                        {"@IdUtilisateur", idUtilisateur } // Ajouté
                    };

                    // Sécurité : On ne supprime que si l'ID correspond ET que c'est le bon utilisateur
                    connection.ExecuteQuery("DELETE FROM Invite WHERE idInvite = @Id AND IdUtilisateur = @IdUtilisateur", parameters);
                }
            }
        }

        public bool EstDansUnGroupe(long idInvite, long idUtilisateur)
        {
            // Note : Pour EstDansUnGroupe, techniquement la table de liaison Invite_Groupe n'a pas IdUtilisateur,
            // mais on pourrait vérifier si l'invité appartient bien à l'utilisateur avant de vérifier le groupe.
            // Pour faire simple ici, on suppose que l'ID de l'invité est valide.

            bool resultat = false;
            using (SQLiteConnector connection = new SQLiteConnector())
            {
                if (connection == null)
                {
                    throw new Exception("Erreur de connexion à la base de données");
                }
                else
                {
                    Dictionary<string, object> parameters = new Dictionary<string, object>()
                    {
                        {"@IdInvite", idInvite }
                    };

                    DataTable dataTable = connection.ExecuteQuery("SELECT COUNT(*) as NombreGroupes FROM Invite_Groupe WHERE IdInvite = @IdInvite", parameters);

                    if (dataTable.Rows.Count > 0)
                    {
                        int nombreGroupes = Convert.ToInt32(dataTable.Rows[0]["NombreGroupes"]);
                        resultat = nombreGroupes > 0;
                    }
                }
            }
            return resultat;
        }

        public List<Invite> ChercherInvite(string texterecherche, long idUtilisateur)
        {
            List<Invite> listeInvite = new List<Invite>();
            using (SQLiteConnector connection = new SQLiteConnector())
            {
                if (connection == null)
                {
                    throw new Exception("Erreur de connexion à la base de données");
                }
                else
                {
                    Dictionary<string, object> parameters = new Dictionary<string, object>()
                    {
                        {"@TexteRecherche", $"%{texterecherche}%" },
                        {"@IdUtilisateur", idUtilisateur } // Ajouté
                    };

                    // Ajout des parenthèses autour des OR pour que le AND IdUtilisateur s'applique à tout le monde
                    // (Nom OU Prenom) ET Utilisateur
                    string sql = "SELECT * FROM Invite WHERE (Nom LIKE @TexteRecherche OR Prenom LIKE @TexteRecherche) AND IdUtilisateur = @IdUtilisateur";

                    DataTable dataTable = connection.ExecuteQuery(sql, parameters);

                    foreach (DataRow? row in dataTable.Rows)
                    {
                        Invite invite = new Invite((long)row["idInvite"], row["nom"].ToString(), row["prenom"].ToString(), row["NumTel"].ToString(), row["mail"].ToString());
                        listeInvite.Add(invite);
                    }
                }
            }
            return listeInvite;
        }
    }
}