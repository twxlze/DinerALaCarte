namespace API_Footies.Metier
{
    public class AvisDetail
    {
        /// <summary>
        /// Nom de l'invité ayant laissé l'avis
        /// </summary>
        public string NomInvite { get; set; }

        /// <summary>
        /// Prenom de l'invité ayant laissé l'avis
        /// </summary>
        public string PrenomInvite { get; set; }

        /// <summary>
        /// Nom du plat concerné par l'avis
        /// </summary>
        public string NomPlat { get; set; }

        /// <summary>
        /// Note du plat entre 1 et 10
        /// </summary>
        public int Note { get; set; }

        /// <summary>
        /// Commentaire laissé par l'invité (facultatif
        /// </summary>
        public string? Commentaire { get; set; }
    }
}
