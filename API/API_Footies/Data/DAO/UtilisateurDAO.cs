using System.Data;
using API_Footies.Data.Interfaces;
using API_Footies.Metier;

namespace API_Footies.Data.DAO
{
    public class UtilisateurDAO : IUtilisateurDAO
    {
        public Identifiant RecupererIdentifiantParPseudo(string pseudo)
        {
            Identifiant identifiantTrouve = null;
            using (SQLiteConnector connection = new SQLiteConnector())
            {
                if (connection == null) throw new Exception("Erreur BDD");

                Dictionary<string, object> parameters = new Dictionary<string, object>()
                {
                    {"@Pseudo", pseudo }
                };
                DataTable table = connection.ExecuteQuery("SELECT * FROM Identifiant WHERE Pseudo = @Pseudo", parameters);

                if (table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];
                    identifiantTrouve = new Identifiant();
                    identifiantTrouve.Id = (long)row["IDIdentifiant"];
                    identifiantTrouve.Pseudo = row["Pseudo"].ToString();
                    identifiantTrouve.MotDePasseHash = row["MotDePasseHash"].ToString();
                }
            }
            return identifiantTrouve;
        }

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
                    string? nom = row["Nom"] as string;
                    string? prenom = row["Prenom"] as string;
                    string? mail = row["Mail"] as string;
                    string? numTel = row["NumTel"] as string;

                    utilisateurTrouve = new Utilisateur(
                        (long)row["IDUtilisateur"],
                        row["Pseudo"].ToString(),
                        nom,
                        prenom,
                        mail,
                        numTel
                    );
                }
            }
            return utilisateurTrouve;
        }
    }
}