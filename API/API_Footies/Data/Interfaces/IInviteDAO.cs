using API_Footies.Metier;

namespace API_Footies.Data.Interfaces
{
    /// <summary>
    /// Définit la gestion des opérations liées aux invités
    /// </summary>
    public interface IInviteDAO
    {
        /// <summary>
        /// Ajouter un invité
        /// </summary>
        /// <param name="invite"> Invité à ajouter </param>
        /// <returns> True si ajouté False sinon </returns>
        bool AjouterInvite(Invite invite);

        /// <summary>
        /// Modifier un invité
        /// </summary>
        /// <param name="invite">L'invité</param>
        bool ModifierInvite(Invite invite);

        /// <summary>
        /// Récupérer la liste des invités de la base de données
        /// </summary>
        /// <returns> liste des invités </returns>
        public List<Invite> ListInvite();

        /// Supprimer un invité
        /// </summary>
        /// <param name="id"> id de l'invité à supprimer </param>
        public void SupprimerInvite(long id);

        /// <summary>
        /// Vérifie si un invité est associé à un ou plusieurs groupes
        /// </summary>
        /// <param name="idInvite">L'id de l'invité</param>
        /// <returns>True si l'invité fait partie d'au moins un groupe, False sinon</returns>
        bool EstDansUnGroupe(long idInvite);

        /// <summary>
        /// Cherche un inviter via un texte de recherche
        /// </summary>
        /// <param name="texterecherche">Le texte permettant de un invitée</param>
        /// <returns>Une liste d'invité correspondant à la recherche</returns>
        public List<Invite> ChercherInvite(string texterecherche);
    }
}
