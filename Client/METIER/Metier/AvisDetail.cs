using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace METIER_Footies.Metier
{
    public class AvisDetail
    {

        #region attributs 

        private string nomInvite;
        private string prenomInvite;
        private string? commentaire;
        private string nomPlat;
        private int note;

        #endregion

        #region proprietes

        /// <summary>
        /// Nom de l'invité ayant donnée l'avis
        /// </summary>
        public string NomInvite
        {
            get => nomInvite;
            set => nomInvite = value;
        }

        /// <summary>
        /// Prenom de l'invité ayant donnée l'avis
        /// </summary>
        public string PrenomInvite
        {
            get => prenomInvite;
            set => prenomInvite = value;
        }

        /// <summary>
        /// Commentaire donné par l'invité sur le plat
        /// </summary>
        public string? Commentaire
        {
            get => commentaire;
            set => commentaire = value;
        }

        /// <summary>
        /// Nom du plat ayant reçue l'avis
        /// </summary>
        public string NomPlat
        {
            get => nomPlat;
            set => nomPlat = value;
        }

        /// <summary>
        /// Note du plat donnée par l'invité
        /// </summary>
        public int Note
        {
            get => note;
            set => note = value;
        }

        #endregion


        #region constructeurs


        public AvisDetail(string nomInvite, string prenomInvite, string? commentaire, string nomPlat, int note)
        {
            this.nomInvite = nomInvite;
            this.prenomInvite = prenomInvite;
            this.commentaire = commentaire;
            this.nomPlat = nomPlat;
            this.note = note;
        }

        #endregion

    }
}
