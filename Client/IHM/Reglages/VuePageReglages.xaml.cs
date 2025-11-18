using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace IHM_Footies.Reglages
{
    /// <summary>
    /// Logique d'interaction pour VuePageReglages.xaml
    /// </summary>
    public partial class VuePageReglages : Window
    {
        public VuePageReglages()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ChargerLangueSauvegardee();
        }

        private void ChargerLangueSauvegardee()
        {
            try
            {
                string currentCulture = IHM_Footies.Properties.Settings.Default.Language;
                
                if (string.IsNullOrEmpty(currentCulture))
                {
                    currentCulture = "fr-FR";
                }
                
                foreach (ComboBoxItem item in LanguageComboBox.Items)
                {
                    if (item.Tag?.ToString() == currentCulture)
                    {
                        LanguageComboBox.SelectedItem = item;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BoutonEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (LanguageComboBox.SelectedItem is ComboBoxItem selectedItem)
                {
                    string cultureName = selectedItem.Tag?.ToString();
                    
                    if (!string.IsNullOrEmpty(cultureName))
                    {
                        // Sauvegarder la langue
                        ChangerLangue(cultureName);
                        
                        // Demander si l'utilisateur veut redémarrer pour appliquer les changements de langue ou non
                        string message = cultureName == "fr-FR" 
                            ? "La langue a été changée.\n\nVoulez-vous redémarrer l'application pour appliquer les changements?"
                            : "Language has been changed.\n\nDo you want to restart the application to apply changes?";
                        
                        MessageBoxResult result = MessageBox.Show(
                            message,
                            "Changement de langue / Language Change",
                            MessageBoxButton.YesNo,
                            MessageBoxImage.Question);

                        if (result == MessageBoxResult.Yes)
                        {
                            RedemarrerApplication();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Change la culture de l'application et sauvegarde le choix
        /// </summary>
        private void ChangerLangue(string cultureName)
        {
            try
            {
                CultureInfo newCulture = new CultureInfo(cultureName);
                Thread.CurrentThread.CurrentCulture = newCulture;
                Thread.CurrentThread.CurrentUICulture = newCulture;
                
                IHM_Footies.Ressources.Strings.Culture = newCulture;
                
                IHM_Footies.Properties.Settings.Default.Language = cultureName;
                IHM_Footies.Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                throw new Exception($"(Erreur!) Impossible de changer la langue : {ex.Message}");
            }
        }

        /// <summary>
        /// Redémarre l'application
        /// </summary>
        private void RedemarrerApplication()
        {
            try
            {
                string exePath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                System.Diagnostics.Process.Start(exePath);
                Application.Current.Shutdown();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du redémarrage : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #region Boutons de navigation
        /// <summary>
        /// Bouton pour aller à la vue d'accueil
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonAccueil_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerAccueil(this);
        }


        /// <summary>
        /// Bouton pour aller à la vue des plats
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonPlat_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerPlat(this);
        }

        /// <summary>
        /// Bouton pour fermer la fenêtre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButonFermerFenetre_Click(object sender, RoutedEventArgs e)
        {
            Navigation.FermerFenetre(this);
        }

        /// <summary>
        /// Bouton pour aller à la vue des invités
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonAllerInvite_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerInvites(this);
        }

        /// <summary>
        /// Bouton pour aller à la page groupe invité
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoutonAllerGroupeInvite_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AllerGroupesInvites(this);
        }
        #endregion
    }
}
