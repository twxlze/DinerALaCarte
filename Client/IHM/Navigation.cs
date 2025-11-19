using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using IHM;
using IHM_Footies.Reglages;
using IHM_Footies.Menu;
using IHM_Footies.Invitations;

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fenetreActuelle"></param>
        public static void AllerPlat(Window fenetreActuelle)
        {
            VuePagePlat vuePlats = new VuePagePlat();
            vuePlats.Show();
            fenetreActuelle.Close();
        }

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
        }

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

    }
}
