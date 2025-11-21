using System.Data;
using API_Footies.Data.Interfaces;
using API_Footies.Metier;

namespace API_Footies.Data.DAO
{
    /// <summary>
    /// DAO en charge de la gestion des allergenes
    /// </summary>
    public class AllergeneDAO : IAllergeneDAO
    {
        public List<Allergene> ListeAllergene()
        {
            List<Allergene> listeAllergene = new List<Allergene>();

            using (SQLiteConnector connection = new SQLiteConnector())
            {
                if (connection == null)
                {
                    throw new Exception("Erreur de connexion à la base de données");
                }
                else
                {
                    var dataTable = connection.ExecuteQuery("SELECT * FROM Allergene");
                    foreach (DataRow? row in dataTable.Rows)
                    {

                        Allergene allergene = new Allergene((long)row["idAllergene"], row["nom"].ToString());

                        listeAllergene.Add(allergene);
                    }
                }
            }

            return listeAllergene;
        }
    }
    
}
