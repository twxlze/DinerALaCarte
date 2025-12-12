using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace METIER_Footies.Metier
{
    public class Avis
    {
        #region attributs

        private long idPlat;
        private long idInvite;
        private int note;
        private string? commentaire;

        #endregion

        #region proprietes

        /// <summary>
        /// Id du plat auquel l'avis est associé
        /// </summary>
        public long IdPlat 
        { 
            get => idPlat; 
            set => idPlat = value; 
        }

        /// <summary>
        /// Id de l'invité qui met la note
        /// </summary>
        public long IdInvite 
        { 
            get => idInvite; 
            set => idInvite = value; 
        }

        /// <summary>
        /// Note du plat par l'invité
        /// </summary>
        public int Note 
        { 
            get => note; 
            set => note = value; 
        }

        /// <summary>
        /// Commentaire de l'invité sur le plat (facultatif)
        /// </summary>
        public string? Commentaire 
        { 
            get => commentaire; 
            set => commentaire = value; 
        }


        #endregion


        #region constructeurs

        public Avis(long idPlat, long idInvite, int note, string? commentaire = null)
        {
            this.idPlat = idPlat;
            this.idInvite = idInvite;
            this.note = note;
            this.commentaire = commentaire;
        }

        #endregion


    }
}
