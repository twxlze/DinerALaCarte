using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using IHM;
using IHM_Footies.Connexion;
using IHM_Footies.GroupeInvite;
using IHM_Footies.Invitations;
using IHM_Footies.Invite;
using IHM_Footies.Menu;
using IHM_Footies.Plat;
using IHM_Footies.Reglages;
using IHM_Footies.Statistique;
<<<<<<< HEAD
using IHM_Footies.TableauDeBord;
=======
using METIER_Footies.Metier;
>>>>>>> Test-Merge-TableauDeBord-Sprint3
using VM_Footies.VM;
using VM_Footies.VM_Page;

namespace IHM_Footies
{
    /// <summary>
    /// Classe de navigation entre les différentes fenêtres de l'application
    /// </summary>
    public static class Navigation
    {
        /// <summary>
        /// Permet de naviguer vers la fenêtre d'accueil
        /// </summary>
        /// <param name="fenetreActuelle"> La fenêtre actuelle à fermer</param>
        public static void AllerAccueil(Window fenetreActuelle)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            fenetreActuelle.Close();
        }

        #region Invite
        /// <summary>
        /// Permet de naviguer vers la fenêtre des invités
        /// </summary>
        /// <param name="fenetreActuelle"> La fenêtre actuelle à fermer</param>
        public static void AllerInvites(Window fenetreActuelle)
        {
            VuePageInvite vueInvites = new VuePageInvite();
            vueInvites.Show();
            fenetreActuelle.Close();
        }

        /// <summary>
        /// Permet de naviguer vers la fenêtre du formulaire d'invité
        /// </summary>
        /// <param name="fenetreActuelle"> La fenêtre actuelle à fermer</param>
        public static void AllerFormulaireInvite(Window fenetreActuelle)
        {
            VueFormulaireInvite vueFormulaireInvite = new VueFormulaireInvite();
            vueFormulaireInvite.Show();
            fenetreActuelle.Close();
        }

        public static void AllerDetailInvite(Window fenetreActuelle, VMInvite invite, string provenance = "Invite", VMInvitation invitationParent = null, VMGroupeInvite groupeParent = null)
        {
            VuePageInviteDetail fenetre = new VuePageInviteDetail(invite, provenance, invitationParent, groupeParent);
            fenetre.Show();
            fenetreActuelle.Close();
        }
        #endregion

        #region Plat
        /// <summary>
        /// Aller à la page des plats
        /// </summary>
        /// <param name="fenetreActuelle">La fenêtre actuelle à fermer</param>
        public static void AllerPlat(Window fenetreActuelle)
        {
            VuePagePlat vuePlats = new VuePagePlat();
            vuePlats.Show();
            fenetreActuelle.Close();
        }

        /// <summary>
        /// Permet de naviguer vers la fenêtre de détail d'un plat
        /// </summary>
        /// <param name="fenetreActuelle">La fenêtre actuelle à fermer</param>
        /// <param name="plat">Le plat à afficher en détail</param>
        /// <param name="provenance">La fenêtre de provenance (optionnel, par défaut "Plat")</param>
        public static void AllerDetailPlat(Window fenetreActuelle, VMPlat plat, string provenance = "Plat", VMInvitation invitationPrecedente = null, VMMenu menuParent = null)
        {
            VuePagePlatDetail fenetre = new VuePagePlatDetail(plat, provenance, invitationPrecedente, menuParent);
            fenetre.Show();
            fenetreActuelle.Close();
        }

        #endregion

        #region Groupe invités
        /// <summary>
        /// Permet de naviguer vers la fenêtre des groupes d'invités
        /// </summary>
        /// <param name="fenetreActuelle">La fenêtre actuelle à ferme</param>
        public static void AllerGroupesInvites(Window fenetreActuelle)
        {
            VuePageGroupeInvite vueGroupesInvites = new VuePageGroupeInvite();
            vueGroupesInvites.Show();
            fenetreActuelle.Close();
        }

        /// <summary>
        /// Permet de naviguer vers la fenêtre du formulaire de groupe d'invités
        /// </summary>
        /// <param name="fenetreActuelle">La fenêtre actuelle à ferme</param>
        public static void AllerFormulaireGroupeInvite(Window fenetreActuelle)
        {
            VueFormulaireGroupeInvite vueFormulaireGroupeInvite = new VueFormulaireGroupeInvite();
            vueFormulaireGroupeInvite.Show();
            fenetreActuelle.Close();
        }

        public static void AllerDetailGroupeInvite(Window fenetreActuelle, VMGroupeInvite groupeInvite, string provenance = "GroupeInvite", VMInvitation invitationParent = null)
        {
            VuePageDetailInviteDansGroupe fenetre = new VuePageDetailInviteDansGroupe(groupeInvite, provenance, invitationParent);
            fenetre.Show();
            fenetreActuelle.Close();
        }
        #endregion

        #region Menu

        /// <summary>
        /// Permet de naviguer vers la fenêtre de la page des menus
        /// </summary>
        /// <param name="fenetreActuelle"> La fenêtre actuelle à fermer </param>
        public static void AllerMenu(Window fenetreActuelle)
        {
            VuePageMenu vueMenu = new VuePageMenu();
            vueMenu.Show();
            fenetreActuelle.Close();
        }

        /// <summary>
        /// Permet de naviguer vers la fenêtre du formulaire menu
        /// </summary>
        /// <param name="fenetreActuelle"> La fenêtre actuelle à fermer </param>
        public static void AllerFormulaireMenu(Window fenetreActuelle)
        {
            VueFormulaireMenu vueFormulaireMenu = new VueFormulaireMenu();
            vueFormulaireMenu.Show();
            fenetreActuelle.Close();
        }

        /// <summary>
        /// Permet de naviguer vers la fenêtre de détail d'un menu
        /// </summary>
        /// <param name="fenetreActuelle"> La fenêtre actuelle à fermer </param>
        /// <param name="menu"> Le menu à afficher en détail </param>
        public static void AllerDetailMenu(Window fenetreActuelle, VMMenu menu, string provenance = "Menu", VMInvitation invitationPrecedente = null)
        {
            VuePageMenuDetail fenetre = new VuePageMenuDetail(menu, provenance, invitationPrecedente);
            fenetre.Show();
            fenetreActuelle.Close();
        }
        #endregion

        #region Stats
        /// <summary>
        /// Permet de naviguer vers la fenêtre de sélection des invite pour les statistiques
        /// </summary>
        /// <param name="fenetreActuelle"></param>
        public static void AllerSelectionInvite(Window fenetreActuelle)
        {
            VuePageSelectionStatistique vueSelection = new VuePageSelectionStatistique();
            vueSelection.Show();
            fenetreActuelle.Close();
        }

        /// <summary>
        /// Permet de naviguer vers la fenêtre des Statistiques
        /// </summary>
        /// <param name="fenetreActuelle"> La fenêtre actuelle à fermer</param>
        public static void AllerStatistique(Window fenetreActuelle, VmPageStatistique vmPageStatistique)
        {
            VuePageStatistique vuestats = new VuePageStatistique(vmPageStatistique);
            vuestats.Show();
            fenetreActuelle.Close();
        }
        #endregion

        #region tableau de bord
        /// <summary>
        /// Permet de naviguer vers la fenêtre de sélection des invite pour les statistiques
        /// </summary>
        /// <param name="fenetreActuelle"></param>
        public static void AllerTableaudebord(Window fenetreActuelle)
        {
            VuePageTableauDeBord vueSelection = new VuePageTableauDeBord();
            vueSelection.Show();
            fenetreActuelle.Close();
        }
        #endregion

        /// <summary>
        /// Aller à la page des réglages
        /// </summary>
        /// <param name="fenetreActuelle">La fenêtre actuelle</param>
        public static void AllerReglages(Window fenetreActuelle)
        {
            VuePageReglages vueReglages = new VuePageReglages();
            vueReglages.Show();
            fenetreActuelle.Close();
        }

        /// <summary>
        /// Permet de fermer la fenêtre actuelle
        /// </summary>
        /// <param name="fenetreActuelle"> La fenêtre actuelle à fermer</param>
        public static void FermerFenetre(Window fenetreActuelle)
        {
            fenetreActuelle.Close();
        }

        #region Invitation
        /// <summary>
        /// Aller à la page invitations
        /// </summary>
        /// <param name="fenetreActuelle">La fenêtre actuelle</param>
        public static void AllerInvitations(Window fenetreActuelle)
        {
            VuePageInvitation vueInvitations = new VuePageInvitation();
            vueInvitations.Show();
            fenetreActuelle.Close();
        }

        /// <summary>
        /// Aller au formulaire d'invitation
        /// </summary>
        /// <param name="fenetreActuelle"> LA fenêtre actuelle à fermer </param>
        public static void AllerFormulaireInvitation(Window fenetreActuelle)
        {
            VueFormulaireInvitation vueFormulaireInvitation = new VueFormulaireInvitation();
            vueFormulaireInvitation.Show();
            fenetreActuelle.Close();
        }

        public static void AllerFormulaireInvitation(Window fenetreActuelle, VMInvitation invitation)
        {
            VueFormulaireInvitation vueFormulaireInvitation = new VueFormulaireInvitation(invitation);
            vueFormulaireInvitation.Show();
            fenetreActuelle.Close();
        }

        /// <summary>
        /// Permet de naviguer vers la fenêtre de détail d'une invitation
        /// </summary>
        /// <param name="fenetreActuelle"> La fenêtre actuelle à fermer</param>
        /// <param name="invitation"> L'invitation à afficher en détail</param>
        /// <param name="provenance"> La fenêtre de provenance (optionnel, par défaut "Invitation")</param>
        public static void AllerDetailInvitation(Window fenetreActuelle, VMInvitation invitation, string provenance = "Invitation")
        {
            VuePageInvitationDetail fenetre = new VuePageInvitationDetail(invitation, provenance);
            fenetre.Show();
            fenetreActuelle.Close();
        }

        /// <summary>
        /// Aller au formulaire d'invitation des menus et des plats avec une invitation existante
        /// </summary>
        /// <param name="fenetreActuelle">La fenêtre actuelle à fermer</param>
        /// <param name="invitation">L'invitation à utiliser</param>
        public static void AllerFormulaireInvitationPlatMenu(Window fenetreActuelle, VMInvitation invitation)
        {
            VueFormulaireMenuEtPlat_Invitation vueFormulaireInvitation = new VueFormulaireMenuEtPlat_Invitation(invitation);
            vueFormulaireInvitation.Show();
            fenetreActuelle.Close();
        }
        #endregion

        #region Connexion 
        /// <summary>
        /// Aller à la page de connexion
        /// </summary>
        /// <param name="fenetreActuelle">La fenêtre actuelle</param>
        public static void AllerConnexion(Window fenetreActuelle)
        {
            VueConnexion vueConnexion = new VueConnexion();
            vueConnexion.Show();
            fenetreActuelle.Close();
        }

        /// <summary>
        /// Aller à la page de connexion
        /// </summary>
        /// <param name="fenetreActuelle">La fenêtre actuelle</param>
        public static void AllerCreerUtilisateur(Window fenetreActuelle)
        {
            VueCreationCompte vueCreationCompte = new VueCreationCompte();
            vueCreationCompte.Show();
            fenetreActuelle.Close();
        }

        /// <summary>
        /// Aller à la page des informations de l'utilisateur
        /// </summary>
        /// <param name="fenetreActuelle">La fenêtre actuelle</param>
        public static void AllerInformationUtilisateur(Window fenetreActuelle)
        {
            VueInformationUtilisateur vueInformationUtilisateur = new VueInformationUtilisateur();
            vueInformationUtilisateur.Show();
            fenetreActuelle.Close();
        }
        #endregion
    }
}
