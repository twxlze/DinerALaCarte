using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace METIER_Footies.Metier
{
    /// <summary>
    /// Représente un avertissement concernant un invité et un plat dans une invitation
    /// </summary>
    public class AvertissementInvitation
    {
        #region Enumérations
        /// <summary>
        /// Types d'avertissements possibles
        /// </summary>
        public enum TypeAvertissement
        {
            Allergie,
            PlatDeteste,
            PlatPrefere
        }
        #endregion

        #region Attributs

        private TypeAvertissement type;
        private Invite invite;
        private Plat plat;
        private Menu menu;

        #endregion

        #region Propriétés

        /// <summary>
        /// Type d'avertissement
        /// </summary>
        public TypeAvertissement Type
        {
            get { return type; }
            set { type = value; }
        }

        /// <summary>
        /// Invité concerné par l'avertissement
        /// </summary>
        public Invite Invite
        {
            get { return invite; }
            set { invite = value; }
        }

        /// <summary>
        /// Plat concerné par l'avertissement
        /// </summary>
        public Plat Plat
        {
            get { return plat; }
            set { plat = value; }
        }

        /// <summary>
        /// Menu contenant le plat (peut être null si le plat est individuel)
        /// </summary>
        public Menu Menu
        {
            get { return menu; }
            set { menu = value; }
        }

        /// <summary>
        /// Message d'avertissement formaté
        /// </summary>
        public string Message
        {
            get
            {
                string infoPlat = Plat.Nom;
                if (Menu != null)
                {
                    infoPlat = $"{Plat.Nom} (menu : {Menu.Nom})";
                }

                string resultat = "";

                switch (Type)
                {
                    case TypeAvertissement.Allergie:
                        resultat = $"⚠️ {Invite.Identite} est allergique au plat : {infoPlat}";
                        break;

                    case TypeAvertissement.PlatDeteste:
                        resultat = $"😞 {Invite.Identite} déteste le plat : {infoPlat}";
                        break;

                    case TypeAvertissement.PlatPrefere:
                        resultat = $"😊 {Invite.Identite} aime le plat : {infoPlat}";
                        break;

                    default:
                        resultat = "";
                        break;
                }

                return resultat;
            }
        }

        #endregion

        #region Constructeurs

        /// <summary>
        /// Constructeur
        /// </summary>
        public AvertissementInvitation(TypeAvertissement type, Invite invite, Plat plat, Menu menu = null)
        {
            this.type = type;
            this.invite = invite;
            this.plat = plat;
            this.menu = menu;
        }

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public AvertissementInvitation()
        {
        }

        #endregion
    }
}
