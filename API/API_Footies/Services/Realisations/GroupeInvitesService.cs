using API_Footies.Data.Interfaces;
using API_Footies.Metier;
using API_Footies.Services.Interfaces;

namespace API_Footies.Services.Realisations
{
    /// <summary>
    /// Service en charge de la gestion des groupes d'invités
    /// </summary>
    public class GroupeInvitesService : IGroupeInvitesService
    {
        #region Attributs
        private IGroupeInviteDAO _groupeInviteDAO;
        #endregion

        #region constructeurs
        /// <summary>
        /// Constructeur du service de gestion des groupes d'invités
        /// </summary>
        /// <param name="groupeInviteDAO">injection de dependances avec le DAO groupeInvite</param>
        public GroupeInvitesService( IGroupeInviteDAO groupeInviteDAO)
        {
            this._groupeInviteDAO = groupeInviteDAO;
        }
        #endregion

        #region Méthodes
        public bool AjouterGroupeInvites(GroupeInvites groupeInvites)
        {
            return this._groupeInviteDAO.AjouterGroupeInvites(groupeInvites);
        }

        public List<GroupeInvites> ListeGroupesInvites()
        {
            return this._groupeInviteDAO.ListeGroupesInvites();
        }

        public bool ModifierGroupeInvite(GroupeInvites groupeInvite)
        {
            return this._groupeInviteDAO.ModifierGroupe(groupeInvite);
        }
        public void SupprimerGroupe(long idGroupeInvite)
        {
            this._groupeInviteDAO.SupprimerGroupeInvite(idGroupeInvite);
        }
        #endregion
    }
}
