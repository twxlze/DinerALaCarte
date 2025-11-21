using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace METIER_Footies.Metier
{
    /// <summary>
    /// Classe représentant les allergenes
    /// </summary>
    public class Allergene
    {
        #region Attribut
        private long id;
        private string nom;
        #endregion

        #region Propriété
        /// <summary>
        /// Retourne ou modifie l'id de l'allergene
        /// </summary>
        public long ID { get { return id; } set { id = value; } }
        /// <summary>
        /// Retourne ou modifie le nom de l'allergene
        /// </summary>
        public string Nom { get { return nom; } set { nom = value; } }
        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur de Allergene
        /// </summary>
        /// <param name="id">l"id de l'allergene</param>
        /// <param name="nom">le nom de l'allergene</param>
        public Allergene(long id, string nom)
        {
            this.id = id;
            this.nom = nom;
        }
        /// <summary>
        /// Constructeur de allergene a vide
        /// </summary>
        public Allergene()
        {
        }
        #endregion
    }
}
