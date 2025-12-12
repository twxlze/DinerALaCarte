using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using METIER_Footies.Metier;
using VM_Footies.VM;
using System.Security.Permissions;

namespace VM_Footies.VM_Page
{
    /// <summary>
    /// VM pour la page des avertissements d'une invitation
    /// </summary>
    public class VMPageAvertissementInvitation : INotifyPropertyChanged
    {
        #region Attributs
        private VMInvitation invitation;
        private List<AvertissementInvitation> avertissements;
        #endregion

        #region Propriétés
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// L'invitation associée aux avertissements
        /// </summary>
        public VMInvitation Invitation
        {
            get { return invitation; }
        }

        /// <summary>
        /// Liste des avertissements pour l'invitation
        /// </summary>
        public List<AvertissementInvitation> Avertissements
        {
            get { return avertissements; }
        }

        /// <summary>
        /// Avertissements d'allergies
        /// </summary>
        public List<AvertissementInvitation> Allergies
        {
            get
            {
                List<AvertissementInvitation> resultat = new List<AvertissementInvitation>();

                foreach (AvertissementInvitation avertissement in avertissements)
                {
                    if (avertissement.Type == AvertissementInvitation.TypeAvertissement.Allergie)
                    {
                        resultat.Add(avertissement);
                    }
                }
                return resultat;
            }
        }

        /// <summary>
        /// Avertissements de plats détestés
        /// </summary>
        public List<AvertissementInvitation> PlatsDetestes
        {
            get
            {
                List<AvertissementInvitation> resultat = new List<AvertissementInvitation>();
                foreach (AvertissementInvitation avertissement in avertissements)
                {
                    if (avertissement.Type == AvertissementInvitation.TypeAvertissement.PlatDeteste)
                    {
                        resultat.Add(avertissement);
                    }
                }
                return resultat;
            }
        }

        /// <summary>
        /// Avertissement de plats préférés (bon appétit !)
        /// </summary>
        public List<AvertissementInvitation> PlatsPreferes
        {
            get
            {
                List<AvertissementInvitation> resultat = new List<AvertissementInvitation>();
                foreach (AvertissementInvitation avertissement in avertissements)
                {
                    if (avertissement.Type == AvertissementInvitation.TypeAvertissement.PlatPrefere)
                    {
                        resultat.Add(avertissement);
                    }
                }
                return resultat;
            }
        }

        /// <summary>
        /// Indique s'il y a des allergies
        /// </summary>
        public bool ADesAllergies => Allergies.Count > 0;

        /// <summary>
        /// Indique s'il y a des plats détestés
        /// </summary>
        public bool ADesPlatsDetestes => PlatsDetestes.Count > 0;

        /// <summary>
        /// Indique s'il y a des plats préférés
        /// </summary>
        public bool ADesPlatsPreferes => PlatsPreferes.Count > 0;

        /// <summary>
        /// Indique s'il y a au moins un avertissement bloquant (allergie ou plat détesté car on souhaite que les invités profitent du moment)
        /// </summary>
        public bool ADesAvertissementsBloquants => ADesAllergies || ADesPlatsDetestes;
        #endregion

        #region Constructeur 
        /// <summary>
        /// Constructeur de la VM
        /// </summary>
        /// <param name="invitation">Invitation à analyser</param>
        public VMPageAvertissementInvitation(VMInvitation invitation)
        {
            this.invitation = invitation;
            this.avertissements = new List<AvertissementInvitation>();
            AnalyserInvitation();
        }
        #endregion

        #region Méthodes 
        /// <summary>
        /// Analyse l'invitation pour détecter tous les avertissements
        /// </summary>
        private void AnalyserInvitation()
        {
            avertissements.Clear();

            List<Invite> tousLesInvites = new List<Invite>();
            List<long> idsInvitesTraites = new List<long>();

            foreach (Invite invite in invitation.Invitation.Invites)
            {
                tousLesInvites.Add(invite);
                idsInvitesTraites.Add(invite.Id);
            }

            foreach (GroupeInvites groupe in invitation.Invitation.GroupeInvites)
            {
                if (groupe.Invites != null)
                {
                    foreach (Invite inviteDeGroupe in groupe.Invites)
                    {
                        if (!idsInvitesTraites.Contains(inviteDeGroupe.Id))
                        {
                            tousLesInvites.Add(inviteDeGroupe);
                            idsInvitesTraites.Add(inviteDeGroupe.Id);
                        }
                    }
                }
            }

            List<Plat> tousLesPlats = new List<Plat>(invitation.Invitation.Plats);
            Dictionary<Plat, Menu> platVersMenu = new Dictionary<Plat, Menu>();

            foreach (Menu menu in invitation.Invitation.Menus)
            {
                if (menu.Plat != null)
                {
                    foreach (Plat platDuMenu in menu.Plat)
                    {
                        if (!platVersMenu.ContainsKey(platDuMenu))
                        {
                            tousLesPlats.Add(platDuMenu);
                            platVersMenu.Add(platDuMenu, menu);
                        }
                    }
                }
            }

            foreach (Invite invite in tousLesInvites)
            {
                foreach (Plat plat in tousLesPlats)
                {
                    Menu menuAssocie = null;
                    if (platVersMenu.ContainsKey(plat))
                    {
                        menuAssocie = platVersMenu[plat];
                    }

                    if (invite.Allergenes != null && plat.Allergenes != null)
                    {
                        bool estAllergique = invite.Allergenes.Any(a => plat.Allergenes.Contains(a));

                        if (estAllergique)
                        {
                            AvertissementInvitation avertissement = new AvertissementInvitation(AvertissementInvitation.TypeAvertissement.Allergie,invite,plat,menuAssocie);
                            avertissements.Add(avertissement);
                        }
                    }

                    if (invite.PlatsDetestes != null)
                    {
                        bool detesteLePlat = invite.PlatsDetestes.Any(p => p.Id == plat.Id);

                        if (detesteLePlat)
                        {
                            AvertissementInvitation avertissement = new AvertissementInvitation(AvertissementInvitation.TypeAvertissement.PlatDeteste,invite,plat,menuAssocie);
                            avertissements.Add(avertissement);
                        }
                    }

                    if (invite.PlatsPreferes != null)
                    {
                        bool aimeLePlat = invite.PlatsPreferes.Any(p => p.Id == plat.Id);

                        if (aimeLePlat)
                        {
                            AvertissementInvitation avertissement = new AvertissementInvitation(AvertissementInvitation.TypeAvertissement.PlatPrefere,invite,plat,menuAssocie);
                            avertissements.Add(avertissement);
                        }
                    }
                }
            }

            Notify("Avertissements");
            Notify("Allergies");
            Notify("PlatsDetestes");
            Notify("PlatsPreferes");
            Notify("ADesAllergies");
            Notify("ADesPlatsDetestes");
            Notify("ADesPlatsPreferes");
            Notify("ADesAvertissementsBloquants");
        }

        private void Notify(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
