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

        public bool AjouterInvite(Invite invite)
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
                        {"@Nom",invite.Nom },
                        {"@Prenom",invite.Prenom },
                        {"@Telephone",invite.Telephone },
                        {"@Email",invite.Email }
                    };
                    invite.Id = connection.ExecuteInsert("INSERT INTO Invite (Nom,Prenom,NumTel,Mail) VALUES (@Nom,@Prenom,@Telephone, @Email)", parameters);
                    
                    if (invite.Allergenes != null && invite.Allergenes.Count > 0)
                    {
                        foreach (NomAllergene allergene in invite.Allergenes)
                        {
                           var paramatersAllergene = new Dictionary<string, object>()
                           {
                               {"@Nom", allergene.ToString() }
                           };
                            var dataTableAllergene = connection.ExecuteQuery("SELECT IDAllergene FROM Allergene WHERE Nom = @Nom", paramatersAllergene);
                            if (dataTableAllergene.Rows.Count > 0)
                            {
                                long idAllergene = (long)dataTableAllergene.Rows[0]["IDAllergene"];
                                var paramatersInviteAllergene = new Dictionary<string, object>()
                                {
                                    {"@IdInvite", invite.Id },
                                    {"@IdAllergene", idAllergene }
                                };
                                connection.ExecuteQuery("INSERT INTO Invite_Allergene (IdInvite, IdAllergene) VALUES (@IdInvite, @IdAllergene)", paramatersInviteAllergene);
                            }
                        }
                    }
                    ajoute = true;
                }
            }
            return ajoute;
        }

        public bool ModifierInvite(Invite invite)
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
                        {"@Email", invite.Email }
                    };

                    connection.ExecuteQuery("UPDATE Invite SET Nom = @Nom, Prenom = @Prenom, NumTel = @Telephone, Mail = @Email WHERE IDInvite = @Id", parameters);
                    modifie = true;
                }
            }
            return modifie;
        }



        public List<Invite> ListInvite()
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
                    DataTable dataTable = connection.ExecuteQuery("SELECT * FROM Invite");
                    foreach (DataRow? row in dataTable.Rows)
                    {

                        Invite invite = new Invite((long)row["idInvite"], row["nom"].ToString(), row["prenom"].ToString(), row["NumTel"].ToString(), row["mail"].ToString());

                        listeInvite.Add(invite);
                    }
                }
            }

            return listeInvite;
        }



        public void SupprimerInvite(long id)
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
                {"@Id", id }
            };
                    connection.ExecuteQuery("DELETE FROM Invite WHERE idInvite=@Id", parameters);
                }
            }
        }


        public bool EstDansUnGroupe(long idInvite)
        {
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

        public List<Invite> ChercherInvite(string texterecherche)
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
                        {"@TexteRecherche", $"%{texterecherche}%" }
                    };
                    DataTable dataTable = connection.ExecuteQuery("SELECT * FROM Invite WHERE Nom LIKE @TexteRecherche OR Prenom LIKE @TexteRecherche", parameters);
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
