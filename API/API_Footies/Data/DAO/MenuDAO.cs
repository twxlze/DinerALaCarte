using System.Data;
using API_Footies.Data.Interfaces;
using API_Footies.Metier;
using API_Footies.Metier.Enum;

namespace API_Footies.Data.DAO
{
    /// <summary>
    /// Classe en charge de tout ce qui touche les menus dans la base de données
    /// </summary>
    public class MenuDAO : IMenuDAO
    {
        public bool AjouterMenu(Menu menu, long idUtilisateur)
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
                        {"@Nom", menu.Nom },
                        {"@IdUtilisateur", idUtilisateur }
                    };

                    menu.IdMenu = connection.ExecuteInsert("INSERT INTO Menu (Nom, IdUtilisateur) VALUES (@Nom, @IdUtilisateur)", parameters);

                    foreach (Plat plat in menu.Plat)
                    {
                        Dictionary<string, object> parametersPlat = new Dictionary<string, object>()
                        {
                            {"@IdMenu", menu.IdMenu },
                            {"@IdPlat", plat.Id }
                        };
                        connection.ExecuteQuery("INSERT INTO Menu_Plat (IDMenu, IDPlat) VALUES (@IdMenu, @IdPlat)", parametersPlat);
                    }
                    ajoute = true;
                }
            }
            return ajoute;
        }

        public List<Menu> ListMenu(long idUtilisateur)
        {
            List<Menu> listeMenu = new List<Menu>();

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
                        {"@IdUtilisateur", idUtilisateur }
                    };

                    DataTable dataTable = connection.ExecuteQuery("SELECT * FROM Menu WHERE IdUtilisateur = @IdUtilisateur", parameters);

                    foreach (DataRow? row in dataTable.Rows)
                    {
                        long idMenu = (long)row["IDMenu"];
                        string nom = row["Nom"].ToString();

                        List<Plat> platsMenu = new List<Plat>();
                        Dictionary<string, object> parametersPlat = new Dictionary<string, object>()
                        {
                            {"@IdMenu", idMenu }
                        };

                        DataTable dataTablePlats = connection.ExecuteQuery(@"SELECT p.IDPlat, p.Nom, p.Description, p.Categorie, p.Ingredients  FROM Plat p INNER JOIN Menu_Plat mp ON p.IDPlat = mp.IDPlat  WHERE mp.IDMenu = @IdMenu",parametersPlat);

                        foreach (DataRow? rowPlat in dataTablePlats.Rows)
                        {
                            CategoriePlat categorie;
                            if (!Enum.TryParse(rowPlat["Categorie"].ToString(), true, out categorie))
                            {
                                categorie = CategoriePlat.plat;
                            }

                            string? ingredients = null;
                            if (dataTablePlats.Columns.Contains("Ingredients") && rowPlat["Ingredients"] != DBNull.Value)
                            {
                                ingredients = rowPlat["Ingredients"]?.ToString();
                            }

                            long idPlat = (long)rowPlat["IDPlat"];

                            List<NomAllergene> allergenesPlat = new List<NomAllergene>();
                            Dictionary<string, object> parametersAllergene = new Dictionary<string, object>()
                            {
                                {"@IdPlat", idPlat }
                            };

                            DataTable dataTableAllergenes = connection.ExecuteQuery(@"SELECT a.Nom FROM Allergene a INNER JOIN Plat_Allergene pa ON a.IDAllergene = pa.IDAllergene WHERE pa.IDPlat = @IdPlat",parametersAllergene);

                            foreach (DataRow? rowAllergene in dataTableAllergenes.Rows)
                            {
                                NomAllergene allergene;
                                if (Enum.TryParse(rowAllergene["Nom"].ToString(), true, out allergene))
                                {
                                    allergenesPlat.Add(allergene);
                                }
                            }
                            Plat plat = new Plat(idPlat,rowPlat["Nom"].ToString(), rowPlat["Description"]?.ToString() ?? "", categorie, ingredients, allergenesPlat.Count > 0 ? allergenesPlat : null);
                            platsMenu.Add(plat);
                        }
                        Menu menu = new Menu(platsMenu, idMenu, nom);
                        listeMenu.Add(menu);
                    }
                }
            }
            return listeMenu;
        }

        public bool ModifierMenu(Menu menu, long idUtilisateur)
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
                        {"@IdMenu", menu.IdMenu },
                        {"@Nom", menu.Nom },
                        {"@IdUtilisateur", idUtilisateur }
                    };

                    connection.ExecuteQuery("UPDATE Menu SET Nom = @Nom WHERE IDMenu = @IdMenu AND IdUtilisateur = @IdUtilisateur", parameters);

                    Dictionary<string, object> parametersDelete = new Dictionary<string, object>()
                    {
                        {"@IdMenu", menu.IdMenu }
                    };
                    connection.ExecuteQuery("DELETE FROM Menu_Plat WHERE IDMenu = @IdMenu", parametersDelete);

                    foreach (Plat plat in menu.Plat)
                    {
                        Dictionary<string, object> parametersPlat = new Dictionary<string, object>()
                        {
                            {"@IdMenu", menu.IdMenu },
                            {"@IdPlat", plat.Id }
                        };
                        connection.ExecuteQuery("INSERT INTO Menu_Plat (IDMenu, IDPlat) VALUES (@IdMenu, @IdPlat)", parametersPlat);
                    }
                    modifie = true;
                }
            }
            return modifie;
        }

        public void SupprimerMenu(long idMenu, long idUtilisateur)
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
                        {"@IdMenu", idMenu }
                    };
                    connection.ExecuteQuery("DELETE FROM Menu_Plat WHERE IDMenu = @IdMenu", parametersLiaison);
                    Dictionary<string, object> parameters = new Dictionary<string, object>()
                    {
                        {"@IdMenu", idMenu },
                        {"@IdUtilisateur", idUtilisateur }
                    };
                    connection.ExecuteQuery("DELETE FROM Menu WHERE IDMenu = @IdMenu AND IdUtilisateur = @IdUtilisateur", parameters);
                }
            }
        }

        public List<Menu> ChercherMenus(string menuRechercher, long idUtilisateur)
        {
            List<Menu> listeMenu = new List<Menu>();
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
                        {"@Texte", $"%{menuRechercher}%" },
                        {"@IdUtilisateur", idUtilisateur } 
                    };

                    DataTable dataTable = connection.ExecuteQuery("SELECT * FROM Menu WHERE Nom LIKE @Texte AND IdUtilisateur = @IdUtilisateur", parameters);

                    foreach (DataRow? row in dataTable.Rows)
                    {
                        long idMenu = (long)row["IDMenu"];
                        string nom = row["Nom"].ToString();
                        Menu menu = new Menu(new List<Plat>(), idMenu, nom);
                        listeMenu.Add(menu);
                    }
                }
            }
            return listeMenu;
        }
    }
}