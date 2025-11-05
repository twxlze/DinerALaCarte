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

                    {"@Id", typeDAO.GetIdTypeByNom(invite.Nom) },
                    {"@Nom",invite.Nom },
                    {"@Prenom",invite.Prenom },
                    {"@Telephone",invite.Telephone },
                    {"@Email",invite.Email }

                    };
                    invite.Id = connection.ExecuteInsert("INSERT INTO Invite (idInvite,nom,prenom,telephone,email) VALUES (@Id,@Nom,@Prenom,@Telephone, @Email)", parameters);
                    ajoute = true;
                }

            }
            return ajoute;
        }
    }
}
