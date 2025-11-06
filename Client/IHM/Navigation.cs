using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using IHM;

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
            VueInvites vueInvites = new VueInvites();
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
        /// Permet de fermer la fenêtre actuelle
        /// </summary>
        /// <param name="fenetreActuelle"> La fenêtre actuelle à fermer</param>
        public static void FermerFenetre(Window fenetreActuelle)
        {
            fenetreActuelle.Close();
        }
    }
}
