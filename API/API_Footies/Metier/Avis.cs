namespace API_Footies.Metier
{
    public class Avis
    {
        /// <summary>
        /// Id du plat auquel on ajoute un avis
        /// </summary>
        public long IdPlat { get; set; }

        /// <summary>
        /// id de l'invité qui ajoute un avis
        /// </summary>
        public long IdInvite { get; set; }

        /// <summary>
        /// Note donnée au plat 
        /// </summary>
        public int Note { get; set; }

        /// <summary>
        /// Commentaire sur le plat
        /// </summary>
        public string? Commentaire { get; set; }
    }
}
