using System.ComponentModel;
using System.Data;
using API_Footies.Data.Interfaces;
using API_Footies.Metier;
using API_Footies.Metier.Enum;
using static API_Footies.Metier.Plat;

namespace API_Footies.Data.DAO
{
    /// <summary>
    /// Classe en charge de tout ce qui touche les plats dans la base de données
    /// </summary>
    public class PlatDAO : IPlatDAO
    {
        public bool AjouterPlat(Plat plat)
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
                        {"@Nom", plat.Nom },
                        {"@Categorie", plat.Categorie.ToString() },
                        {"@Description", plat.Description ?? ""},
                        {"@Ingredients", plat.Ingredients ?? ""}
                    };
                    plat.Id = connection.ExecuteInsert("INSERT INTO Plat (Nom,Categorie,Description,Ingredients) VALUES (@Nom,@Categorie,@Description,@Ingredients)", parameters);

                    if (plat.Allergenes != null && plat.Allergenes.Count > 0)
                    {
                        foreach (NomAllergene allergene in plat.Allergenes)
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
                                    {"@IdPlat", plat.Id },
                                    {"@IdAllergene", idAllergene }
                                };
                                connection.ExecuteQuery("INSERT INTO Plat_Allergene (IDPlat, IDAllergene) VALUES (@IdPlat, @IdAllergene)", parametersLiaison);
                            }
                        }
                    }
                    ajoute = true;
                }

            }
            return ajoute;
        }

        public bool EstDansUnMenu(long idPlat)
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
                     {"@IdPlat", idPlat }
                 };

                    DataTable dataTable = connection.ExecuteQuery("SELECT COUNT(*) as NombrePlats FROM Menu_Plat WHERE IdPlat = @IdPlat", parameters);

                    if (dataTable.Rows.Count > 0)
                    {
                        int nombrePlats = Convert.ToInt32(dataTable.Rows[0]["NombrePlats"]);
                        resultat = nombrePlats > 0;
                    }
                }
            }
            return resultat;
        }

        public List<Plat> ListPlat()
        {
            List<Plat> listePlat = new List<Plat>();
            using (SQLiteConnector connection = new SQLiteConnector())
            {
                if (connection == null)
                {
                    throw new Exception("Erreur de connexion à la base de données");
                }
                else
                {
                    DataTable dataTable = connection.ExecuteQuery("SELECT * FROM Plat");
                    foreach (DataRow? row in dataTable.Rows)
                    {
                        CategoriePlat categorie;
                        if (!Enum.TryParse(row["categorie"].ToString(), true, out categorie))
                        {
                            categorie = CategoriePlat.plat;
                        }

                        long idPlat = (long)row["idPlat"];

                        List<NomAllergene> allergenesPlat = new List<NomAllergene>();
                        Dictionary<string, object> parametersAllergene = new Dictionary<string, object>()
                        {
                            {"@IdPlat", idPlat }
                        };

                        DataTable dataTableAllergenes = connection.ExecuteQuery(
                            @"SELECT a.Nom 
                              FROM Allergene a
                              INNER JOIN Plat_Allergene pa ON a.IDAllergene = pa.IDAllergene 
                              WHERE pa.IDPlat = @IdPlat",
                            parametersAllergene);

                        foreach (DataRow? rowAllergene in dataTableAllergenes.Rows)
                        {
                            NomAllergene allergene;
                            if (Enum.TryParse(rowAllergene["Nom"].ToString(), true, out allergene))
                            {
                                allergenesPlat.Add(allergene);
                            }
                        }
                        Plat plat = new Plat(idPlat, row["nom"].ToString(), row["description"].ToString(), categorie, row["ingredients"].ToString(), allergenesPlat.Count > 0 ? allergenesPlat : null);
                        listePlat.Add(plat);
                    }
                }
            }
            return listePlat;
        }
        public bool ModifierPlat(Plat plat)
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
                        {"@Id", plat.Id },
                        {"@Nom", plat.Nom },
                        {"@Categorie", plat.Categorie.ToString() },
                        {"@Description", plat.Description ?? ""},
                        {"@Ingredients", plat.Ingredients ?? ""}
                    };
                    connection.ExecuteQuery("UPDATE Plat SET Nom = @Nom, Categorie = @Categorie, Description = @Description, Ingredients = @Ingredients WHERE IDPlat = @Id", parameters);

                    Dictionary<string, object> parametersDelete = new Dictionary<string, object>()
                    {
                        {"@IdPlat", plat.Id }
                    };
                    connection.ExecuteQuery("DELETE FROM Plat_Allergene WHERE IDPlat = @IdPlat", parametersDelete);

                    if (plat.Allergenes != null && plat.Allergenes.Count > 0)
                    {
                        foreach (NomAllergene allergene in plat.Allergenes)
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
                                    {"@IdPlat", plat.Id },
                                    {"@IdAllergene", idAllergene }
                                };
                                connection.ExecuteQuery("INSERT INTO Plat_Allergene (IDPlat, IDAllergene) VALUES (@IdPlat, @IdAllergene)", parametersLiaison);
                            }
                        }
                    }
                    modifie = true;
                }
            }
            return modifie;
        }

        public void SupprimerPlat(long id)
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
                        {"@IdPlat", id }
                    };
                    connection.ExecuteQuery("DELETE FROM Plat_Allergene WHERE IDPlat = @IdPlat", parametersLiaison);

                    Dictionary<string, object> parameters = new Dictionary<string, object>()
                    {
                        {"@Id", id }
                    };
                    connection.ExecuteQuery("DELETE FROM Plat WHERE idPlat=@Id", parameters);
                }
            }
        }

        public List<Plat> ChercherPlat(string texterecherche)
        {
            List<Plat> listePlat = new List<Plat>();
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

                    DataTable dataTable = connection.ExecuteQuery(
                        "SELECT * FROM Plat WHERE Nom LIKE @Texte OR Description LIKE @Texte",
                        parameters);

                    foreach (DataRow? row in dataTable.Rows)
                    {
                        CategoriePlat categorie;
                        if (!Enum.TryParse(row["categorie"].ToString(), true, out categorie))
                        {
                            categorie = CategoriePlat.plat;
                        }

                        long idPlat = (long)row["idPlat"];

                        List<NomAllergene> allergenesPlat = new List<NomAllergene>();
                        Dictionary<string, object> parametersAllergene = new Dictionary<string, object>()
                        {
                            {"@IdPlat", idPlat }
                        };

                        DataTable dataTableAllergenes = connection.ExecuteQuery(
                            @"SELECT a.Nom 
                              FROM Allergene a
                              INNER JOIN Plat_Allergene pa ON a.IDAllergene = pa.IDAllergene 
                              WHERE pa.IDPlat = @IdPlat",
                            parametersAllergene);

                        foreach (DataRow? rowAllergene in dataTableAllergenes.Rows)
                        {
                            NomAllergene allergene;
                            if (Enum.TryParse(rowAllergene["Nom"].ToString(), true, out allergene))
                            {
                                allergenesPlat.Add(allergene);
                            }
                        }

                        Plat plat = new Plat(idPlat, row["nom"].ToString(), row["description"]?.ToString(), categorie, row["Ingredients"]?.ToString(), allergenesPlat.Count > 0 ? allergenesPlat : null);
                        listePlat.Add(plat);
                    }
                }
            }
            return listePlat;
        }
    }
}
