using System.Data;
using API_Footies.Data.Interfaces;
using API_Footies.Metier;

namespace API_Footies.Data.DAO
{
    public class UtilisateurDAO : IUtilisateurDAO
    {
        public Utilisateur RecupererUtilisateurParPseudo(string pseudo)
        {
            Utilisateur utilisateurTrouve = null;

            using (SQLiteConnector connection = new SQLiteConnector())
            {
                if (connection == null) throw new Exception("Erreur BDD");

                Dictionary<string, object> parameters = new Dictionary<string, object>()
                {
                    {"@Pseudo", pseudo }
                };

                DataTable table = connection.ExecuteQuery("SELECT * FROM Utilisateur WHERE Pseudo = @Pseudo", parameters);

                if (table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];
                    utilisateurTrouve = new Utilisateur();
                    utilisateurTrouve.Id = (long)row["IDUtilisateur"];
                    utilisateurTrouve.Pseudo = row["Pseudo"].ToString();
                    utilisateurTrouve.MotDePasseHash = row["MotDePasseHash"].ToString();
                    utilisateurTrouve.MotDePasseSel = row["MotDePasseSel"].ToString();
                }
            }
            return utilisateurTrouve;
        }

    }
}
