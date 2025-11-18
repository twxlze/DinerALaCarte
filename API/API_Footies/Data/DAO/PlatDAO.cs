using System.ComponentModel;
using System.Data;
using API_Footies.Data.Interfaces;
using API_Footies.Metier;
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
                    var parameters = new Dictionary<string, object>()
                    {
                        {"@Nom", plat.Nom },
                        {"@Categorie", plat.Categorie.ToString() },
                        {"@Description", plat.Description ?? ""},
                        {"@Ingredients", plat.Ingredients ?? ""}
                    };
                    plat.Id = connection.ExecuteInsert("INSERT INTO Plat (Nom,Categorie,Description,Ingredients) VALUES (@Nom,@Categorie,@Description,@Ingredients)", parameters);
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
                    var dataTable = connection.ExecuteQuery("SELECT * FROM Plat");
                    foreach (DataRow? row in dataTable.Rows)
                    {
                        CategoriePlat categorie;
                        if (!Enum.TryParse(row["categorie"].ToString(), true, out categorie))
                        {
                            categorie = CategoriePlat.plat;
                        }

                        Plat plat = new Plat((long)row["idPlat"], row["nom"].ToString(),row["description"].ToString(), categorie, row["ingredients"].ToString());
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
                    var parameters = new Dictionary<string, object>()
                    {
                        {"@Id", plat.Id },
                        {"@Nom", plat.Nom },
                        {"@Categorie", plat.Categorie.ToString() },
                        {"@Description", plat.Description ?? ""},
                        {"@Ingredients", plat.Ingredients ?? ""}
                    };
                    connection.ExecuteQuery("UPDATE Plat SET Nom = @Nom, Categorie = @Categorie, Description = @Description, Ingredients = @Ingredients WHERE IDPlat = @Id", parameters);
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
                    var parameters = new Dictionary<string, object>()
                    {
                        {"@Id", id }
                    };
                    connection.ExecuteQuery("DELETE FROM Plat WHERE idPlat=@Id", parameters);
                }
            }
        }
    }
}
