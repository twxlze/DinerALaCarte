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
        private IGroupeInviteDAO _groupeInviteDAO;
        /// <summary>
        /// Constructeur du service de gestion des groupes d'invités
        /// </summary>
        /// <param name="groupeInviteDAO">injection de dependances avec le DAO groupeInvite</param>
        public GroupeInvitesService( IGroupeInviteDAO groupeInviteDAO)
        {
            this._groupeInviteDAO = groupeInviteDAO;
        }

        public GroupeInvites AjouterGroupeInvite(GroupeInvites groupeInvites)
        {
            return _groupeInviteDAO.AjouterGroupeInvite(groupeInvites);
        }

        public GroupeInvites AjouterInviteAuGroupe(long idGroupeInvites, Invite invite)
        {
            return _groupeInviteDAO.AjouterInviteAuGroupe(idGroupeInvites, invite);
        }

        public GroupeInvites ModifierGroupe(GroupeInvites groupeInvite)
        {
            return _groupeInviteDAO.ModifierGroupe(groupeInvite);
        }

        public GroupeInvites RecupereGroupeViaId(long idGroupeInvite)
        {
            return _groupeInviteDAO.RecupereGroupeViaId(idGroupeInvite);
        }

        public List<GroupeInvites> RecupererTousGroupesInvites()
        {
            return _groupeInviteDAO.RecupererTousGroupesInvites();
        }

        public GroupeInvites SupprimerGroupe(long idGroupeInvite)
        {
            return _groupeInviteDAO.SupprimerGroupeInvite(idGroupeInvite);
        }
    }
}
