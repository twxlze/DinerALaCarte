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
                        {"@Nom", invite.Nom },
                        {"@Prenom", invite.Prenom },
                        {"@Telephone", invite.Telephone },
                        {"@Email", invite.Email }
                    };
                    invite.Id = connection.ExecuteInsert("INSERT INTO Invite (Nom,Prenom,NumTel,Mail) VALUES (@Nom,@Prenom,@Telephone,@Email)", parameters);

                    if (invite.Allergenes != null && invite.Allergenes.Count > 0)
                    {
                        foreach (NomAllergene allergene in invite.Allergenes)
                        {
                            Dictionary<string, object> parametersAllergene = new Dictionary<string, object>()
                            {
                                {"@Nom", allergene.ToString() }
                            };
                            DataTable dataTableAllergene = connection.ExecuteQuery("SELECT IDAllergene FROM Allergene WHERE Nom = @Nom", parametersAllergene);

                            if (dataTableAllergene.Rows.Count > 0)
                            {
                                long idAllergene = (long)dataTableAllergene.Rows[0]["IDAllergene"];
                                Dictionary<string, object> parametersLiaison = new Dictionary<string, object>()
                                {
                                    {"@IdInvite", invite.Id },
                                    {"@IdAllergene", idAllergene }
                                };
                                connection.ExecuteQuery("INSERT INTO Invite_Allergene (IdInvite, IdAllergene) VALUES (@IdInvite, @IdAllergene)", parametersLiaison);
                            }
                        }
                    }

                    if (invite.PlatsDetestes != null && invite.PlatsDetestes.Count > 0)
                    {
                        foreach (Plat plat in invite.PlatsDetestes)
                        {
                            if (plat.Id > 0)
                            {
                                Dictionary<string, object> parametersLiaison = new Dictionary<string, object>()
                                {
                                    {"@IdInvite", invite.Id },
                                    {"@IdPlat", plat.Id }
                                };
                                connection.ExecuteQuery("INSERT INTO Invite_PlatDeteste (IdInvite, IdPlat) VALUES (@IdInvite, @IdPlat)", parametersLiaison);
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

                    Dictionary<string, object> parametersDeleteAllergenes = new Dictionary<string, object>()
                    {
                        {"@IdInvite", invite.Id }
                    };
                    connection.ExecuteQuery("DELETE FROM Invite_Allergene WHERE IdInvite = @IdInvite", parametersDeleteAllergenes);

                    if (invite.Allergenes != null && invite.Allergenes.Count > 0)
                    {
                        foreach (NomAllergene allergene in invite.Allergenes)
                        {
                            Dictionary<string, object> parametersAllergene = new Dictionary<string, object>()
                            {
                                {"@Nom", allergene.ToString() }
                            };
                            DataTable dataTableAllergene = connection.ExecuteQuery("SELECT IDAllergene FROM Allergene WHERE Nom = @Nom", parametersAllergene);

                            if (dataTableAllergene.Rows.Count > 0)
                            {
                                long idAllergene = (long)dataTableAllergene.Rows[0]["IDAllergene"];
                                Dictionary<string, object> parametersLiaison = new Dictionary<string, object>()
                                {
                                    {"@IdInvite", invite.Id },
                                    {"@IdAllergene", idAllergene }
                                };
                                connection.ExecuteQuery("INSERT INTO Invite_Allergene (IdInvite, IdAllergene) VALUES (@IdInvite, @IdAllergene)", parametersLiaison);
                            }
                        }
                    }

                    Dictionary<string, object> parametersDeletePlats = new Dictionary<string, object>()
                    {
                        {"@IdInvite", invite.Id }
                    };
                    connection.ExecuteQuery("DELETE FROM Invite_PlatDeteste WHERE IdInvite = @IdInvite", parametersDeletePlats);

                    if (invite.PlatsDetestes != null && invite.PlatsDetestes.Count > 0)
                    {
                        foreach (Plat plat in invite.PlatsDetestes)
                        {
                            if (plat.Id > 0)
                            {
                                Dictionary<string, object> parametersLiaison = new Dictionary<string, object>()
                                {
                                    {"@IdInvite", invite.Id },
                                    {"@IdPlat", plat.Id }
                                };
                                connection.ExecuteQuery("INSERT INTO Invite_PlatDeteste (IdInvite, IdPlat) VALUES (@IdInvite, @IdPlat)", parametersLiaison);
                            }
                        }
                    }

                    modifie = true;
                }
            }
            return modifie;
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
                    Dictionary<string, object> parametersLiaison = new Dictionary<string, object>()
                    {
                        {"@IdInvite", id }
                    };
                    connection.ExecuteQuery("DELETE FROM Invite_Allergene WHERE IdInvite = @IdInvite", parametersLiaison);
                    connection.ExecuteQuery("DELETE FROM Invite_PlatDeteste WHERE IdInvite = @IdInvite", parametersLiaison);

                    Dictionary<string, object> parameters = new Dictionary<string, object>()
                    {
                        {"@Id", id }
                    };
                    connection.ExecuteQuery("DELETE FROM Invite WHERE IDInvite = @Id", parameters);
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
                    foreach (DataRow row in dataTable.Rows)
                    {
                        long idInvite = (long)row["IDInvite"];

                        List<NomAllergene> allergenesInvite = new List<NomAllergene>();
                        Dictionary<string, object> parametersAllergene = new Dictionary<string, object>()
                        {
                            {"@IdInvite", idInvite }
                        };

                        DataTable dataTableAllergenes = connection.ExecuteQuery(@"SELECT a.Nom FROM Allergene a INNER JOIN Invite_Allergene ia ON a.IDAllergene = ia.IdAllergene WHERE ia.IdInvite = @IdInvite", parametersAllergene);

                        foreach (DataRow rowAllergene in dataTableAllergenes.Rows)
                        {
                            NomAllergene allergene;
                            if (Enum.TryParse(rowAllergene["Nom"].ToString(), true, out allergene))
                            {
                                allergenesInvite.Add(allergene);
                            }
                        }

                        List<Plat> platsDetestes = new List<Plat>();
                        Dictionary<string, object> parametersPlats = new Dictionary<string, object>()
                        {
                            {"@IdInvite", idInvite }
                        };

                        DataTable dataTablePlats = connection.ExecuteQuery(@"SELECT p.IDPlat, p.Nom, p.Description, p.Categorie, p.Ingredients FROM Plat p INNER JOIN Invite_PlatDeteste ipd ON p.IDPlat = ipd.IdPlat WHERE ipd.IdInvite = @IdInvite", parametersPlats);

                        foreach (DataRow rowPlat in dataTablePlats.Rows)
                        {
                            CategoriePlat categorie;
                            if (!Enum.TryParse(rowPlat["Categorie"].ToString(), true, out categorie))
                            {
                                categorie = CategoriePlat.plat;
                            }

                            long idPlat = (long)rowPlat["IDPlat"];

                            Plat plat = new Plat(idPlat, rowPlat["Nom"].ToString(), rowPlat["Description"]?.ToString(), categorie, rowPlat["Ingredients"]?.ToString(), null);
                            platsDetestes.Add(plat);
                        }

                        Invite invite = new Invite(idInvite, row["Nom"].ToString(), row["Prenom"].ToString(), row["NumTel"].ToString(), row["Mail"].ToString(), allergenesInvite.Count > 0 ? allergenesInvite : null, platsDetestes.Count > 0 ? platsDetestes : null);
                        listeInvite.Add(invite);
                    }
                }
            }
            return listeInvite;
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
                        {"@Texte", $"%{texterecherche}%"}
                    };

                    DataTable dataTable = connection.ExecuteQuery("SELECT * FROM Invite WHERE Nom LIKE @Texte OR Prenom LIKE @Texte", parameters);

                    foreach (DataRow row in dataTable.Rows)
                    {
                        long idInvite = (long)row["IDInvite"];

                        List<NomAllergene> allergenesInvite = new List<NomAllergene>();
                        Dictionary<string, object> parametersAllergene = new Dictionary<string, object>()
                        {
                            {"@IdInvite", idInvite }
                        };

                        DataTable dataTableAllergenes = connection.ExecuteQuery(@"SELECT a.Nom FROM Allergene a INNER JOIN Invite_Allergene ia ON a.IDAllergene = ia.IdAllergene WHERE ia.IdInvite = @IdInvite", parametersAllergene);

                        foreach (DataRow rowAllergene in dataTableAllergenes.Rows)
                        {
                            NomAllergene allergene;
                            if (Enum.TryParse(rowAllergene["Nom"].ToString(), true, out allergene))
                            {
                                allergenesInvite.Add(allergene);
                            }
                        }

                        List<Plat> platsDetestes = new List<Plat>();
                        Dictionary<string, object> parametersPlats = new Dictionary<string, object>()
                        {
                            {"@IdInvite", idInvite }
                        };

                        DataTable dataTablePlats = connection.ExecuteQuery(@"SELECT p.IDPlat, p.Nom, p.Description, p.Categorie, p.Ingredients FROM Plat p INNER JOIN Invite_PlatDeteste ipd ON p.IDPlat = ipd.IdPlat WHERE ipd.IdInvite = @IdInvite", parametersPlats);

                        foreach (DataRow rowPlat in dataTablePlats.Rows)
                        {
                            CategoriePlat categorie;
                            if (!Enum.TryParse(rowPlat["Categorie"].ToString(), true, out categorie))
                            {
                                categorie = CategoriePlat.plat;
                            }

                            long idPlat = (long)rowPlat["IDPlat"];
                            Plat plat = new Plat(idPlat, rowPlat["Nom"].ToString(), rowPlat["Description"]?.ToString(), categorie, rowPlat["Ingredients"]?.ToString(), null);
                            platsDetestes.Add(plat);
                        }

                        Invite invite = new Invite(idInvite, row["Nom"].ToString(), row["Prenom"].ToString(), row["NumTel"].ToString(), row["Mail"].ToString(), allergenesInvite.Count > 0 ? allergenesInvite : null, platsDetestes.Count > 0 ? platsDetestes : null);
                        listeInvite.Add(invite);
                    }
                }
            }
            return listeInvite;
        }
    }
}