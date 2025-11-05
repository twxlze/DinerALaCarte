using System.Data;
using API_Footies.Data.Interfaces;

namespace API_Footies.Data.DAO
{
    /// <summary>
    /// Récupère l'identifiant unique d'un type en fonction de son nom.
    /// </summary>
    public class PersonneDAO : IPersonneDAO
    {
        public long GetIdTypeByNom(string nom)
        {
            Int64 id = -1;
            using (SQLiteConnector connection = new SQLiteConnector())
            {
                var parameters = new Dictionary<string, object>()
                {
                    {"@nom",nom }
                };
                var data = connection.ExecuteQuery("SELECT * FROM Invite WHERE nom=@nom", parameters);
                if (data.Rows.Count > 0)
                {
                    id = data.Rows[0].Field<Int64>("idInvite");
                }

            }
            return id;
        }
    }
}
