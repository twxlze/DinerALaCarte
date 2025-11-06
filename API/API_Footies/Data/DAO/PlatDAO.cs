using System.ComponentModel;
using System.Data;
using API_Footies.Data.Interfaces;
using API_Footies.Metier;

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
                    {"@Nom",plat.Nom },
                    {"@Categorie",plat.Categorie },
                    {"@Description",plat.Description}
                    };
                    plat.Id = connection.ExecuteInsert("INSERT INTO Plat (Nom,Categorie,Description) VALUES (@Nom,@Categorie,@Description)", parameters);
                    ajoute = true;
                }

            }
            return ajoute;
        }
    }
}
