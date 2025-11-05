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
            PersonneDAO typeDAO = new PersonneDAO();
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
    }
}
