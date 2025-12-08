using System.Data;
using Microsoft.Data.Sqlite;

namespace API_Footies.Data
{
    /// <summary>
    /// Connecteur à la bdd
    /// </summary>
    public class SQLiteConnector : IDisposable
    {
        private SqliteConnection connection;

        /// <summary>
        /// Création de la connection
        /// </summary>
        public SQLiteConnector()
        {
            this.connection = new SqliteConnection("Data Source=DataBase.db");
        }

        /// <summary>
        /// Exécute une requête
        /// </summary>
        /// <param name="query">Requête</param>
        /// <param name="parameters">Paramètres</param>
        /// <returns>La réponse de la bdd</returns>
        public DataTable ExecuteQuery(string query, Dictionary<string, object> parameters = null)
        {
            this.connection.Open();
            DataTable dataTable = new DataTable();

            using (SqliteCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                if (parameters != null)
                {
                    foreach (KeyValuePair<string, object> parameter in parameters)
                    {
                        command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                    }
                }

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    dataTable.Load(reader);
                }
            }

            this.connection.Close();
            return dataTable;
        }

        /// <summary>
        /// Execute un insert et renvoie l'id de celui-ci
        /// </summary>
        /// <param name="query">La requête d'insert</param>
        /// <param name="parameters">Le dictionnaire des paramètres</param>
        /// <returns>L'id de la ligne inséré</returns>
        public long ExecuteInsert(string query, Dictionary<string, object> parameters = null)
        {
            this.connection.Open();
            long id = -1;

            using (SqliteCommand command = connection.CreateCommand())
            {
                command.CommandText = query + "; SELECT last_insert_rowid();";
                if (parameters != null)
                {
                    foreach (KeyValuePair<string, object> parameter in parameters)
                    {
                        command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                    }
                }

                id = Convert.ToInt64(command.ExecuteScalar());
            }
            this.connection.Close();
            return id;
        }

        /// <summary>
        /// Fermeture
        /// </summary>
        public void Dispose()
        {
            if (connection != null)
            {
                if (connection.State != System.Data.ConnectionState.Closed) connection.Close();
                connection.Dispose();
                connection = null;
            }
        }
    }
}
