using System.Data;
using API_Footies.Data.Interfaces;
using API_Footies.Metier;

namespace API_Footies.Data.DAO
{
    /// <summary>
    /// Classe en charge de tout ce qui touche les invités dans la base de données
    /// </summary>
    public class InviteDAO : IInviteDAO
    {

        public bool AjouterInvite(Invite invite)
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
                    {"@Nom",invite.Nom },
                    {"@Prenom",invite.Prenom },
                    {"@Telephone",invite.Telephone },
                    {"@Email",invite.Email }

                    };
                    invite.Id = connection.ExecuteInsert("INSERT INTO Invite (Nom,Prenom,NumTel,Mail) VALUES (@Nom,@Prenom,@Telephone, @Email)", parameters);
                    ajoute = true;
                }

            }
            return ajoute;
        }

        public bool ModifierInvite(Invite invite)
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
                        {"@Id", invite.Id },
                        {"@Nom", invite.Nom },
                        {"@Prenom", invite.Prenom },
                        {"@Telephone", invite.Telephone },
                        {"@Email", invite.Email }
                    };

                    connection.ExecuteQuery("UPDATE Invite SET Nom = @Nom, Prenom = @Prenom, NumTel = @Telephone, Mail = @Email WHERE IDInvite = @Id", parameters);
                    modifie = true;
                }
            }
            return modifie;
        }



        public List<Invite> ListInvite()
        {
            List<Invite> listeInvite = new List<Invite>();

            using (SQLiteConnector connection = new SQLiteConnector())
            {
                if (connection == null)
                {
                    throw new Exception("Erreur de connexion à la base de données");
                }
                else
                {
                    var dataTable = connection.ExecuteQuery("SELECT * FROM Invite");
                    foreach (DataRow? row in dataTable.Rows)
                    {

                        Invite invite = new Invite((long)row["idInvite"], row["nom"].ToString(), row["prenom"].ToString(), row["NumTel"].ToString(), row["mail"].ToString());

                        listeInvite.Add(invite);
                    }
                }
            }

            return listeInvite;
        }



        public void SupprimerInvite(long id)
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
                    connection.ExecuteQuery("DELETE FROM Invite WHERE idInvite=@Id", parameters);
                }
            }
        }




    }
}
