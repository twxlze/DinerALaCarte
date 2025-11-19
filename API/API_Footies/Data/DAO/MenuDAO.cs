using System.Data;
using API_Footies.Data.Interfaces;
using API_Footies.Metier;

namespace API_Footies.Data.DAO
{
    /// <summary>
    /// Classe en charge de tout ce qui touche les menus dans la base de données
    /// </summary>
    public class MenuDAO : IMenuDAO
    {
        public bool AjouterMenu(Menu menu)
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
                    var parameters = new Dictionary<string, object>()
                    {
                        {"@Nom", menu.Nom }
                    };
                    menu.IdMenu = connection.ExecuteInsert("INSERT INTO Menu (Nom) VALUES (@Nom)", parameters);

                    foreach (Plat plat in menu.Plat)
                    {
                        var parametersPlat = new Dictionary<string, object>()
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

        public List<Menu> ListMenu()
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
                    var dataTable = connection.ExecuteQuery("SELECT * FROM Menu");
                    foreach (DataRow? row in dataTable.Rows)
                    {
                        long idMenu = (long)row["IDMenu"];
                        string nom = row["Nom"].ToString();

                        List<Plat> platsMenu = new List<Plat>();
                        var parametersPlat = new Dictionary<string, object>()
                        {
                            {"@IdMenu", idMenu }
                        };

                        var dataTablePlats = connection.ExecuteQuery(
                            @"SELECT p.IDPlat, p.Nom, p.Description, p.Categorie, p.Ingredients 
                              FROM Plat p
                              INNER JOIN Menu_Plat mp ON p.IDPlat = mp.IDPlat 
                              WHERE mp.IDMenu = @IdMenu",
                            parametersPlat);

                        foreach (DataRow? rowPlat in dataTablePlats.Rows)
                        {
                            Plat.CategoriePlat categorie;
                            if (!Enum.TryParse(rowPlat["Categorie"].ToString(), true, out categorie))
                            {
                                categorie = Plat.CategoriePlat.plat;
                            }

                            string? ingredients = null;
                            if (dataTablePlats.Columns.Contains("Ingredients") && rowPlat["Ingredients"] != DBNull.Value)
                            {
                                ingredients = rowPlat["Ingredients"]?.ToString();
                            }

                            Plat plat = new Plat(
                                (long)rowPlat["IDPlat"],
                                rowPlat["Nom"].ToString(),
                                rowPlat["Description"]?.ToString() ?? "",
                                categorie,
                                ingredients
                            );
                            platsMenu.Add(plat);
                        }

                        Menu menu = new Menu(platsMenu, idMenu, nom);
                        listeMenu.Add(menu);
                    }
                }
            }

            return listeMenu;
        }

        public bool ModifierMenu(Menu menu)
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
                    var parameters = new Dictionary<string, object>()
                    {
                        {"@IdMenu", menu.IdMenu },
                        {"@Nom", menu.Nom }
                    };
                    connection.ExecuteQuery("UPDATE Menu SET Nom = @Nom WHERE IDMenu = @IdMenu", parameters);

                    var parametersDelete = new Dictionary<string, object>()
                    {
                        {"@IdMenu", menu.IdMenu }
                    };
                    connection.ExecuteQuery("DELETE FROM Menu_Plat WHERE IDMenu = @IdMenu", parametersDelete);

                    foreach (Plat plat in menu.Plat)
                    {
                        var parametersPlat = new Dictionary<string, object>()
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

        public void SupprimerMenu(long idMenu)
        {
            using (SQLiteConnector connection = new SQLiteConnector())
            {
                if (connection == null)
                {
                    throw new Exception("Erreur de connexion à la base de données");
                }
                else
                {
                    var parametersLiaison = new Dictionary<string, object>()
                    {
                        {"@IdMenu", idMenu }
                    };
                    connection.ExecuteQuery("DELETE FROM Menu_Plat WHERE IDMenu = @IdMenu", parametersLiaison);

                    var parameters = new Dictionary<string, object>()
                    {
                        {"@IdMenu", idMenu }
                    };
                    connection.ExecuteQuery("DELETE FROM Menu WHERE IDMenu = @IdMenu", parameters);
                }
            }
        }
    }
}