using System;
using System.Globalization;
using System.Threading;
using System.Windows;

namespace IHM;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        ChargerLangue();
    }

    private void ChargerLangue()
    {
        try
        {
            string savedLanguage = IHM_Footies.Properties.Settings.Default.Language;

            if (string.IsNullOrEmpty(savedLanguage))
            {
                savedLanguage = "fr-FR";
            }

            CultureInfo culture = new CultureInfo(savedLanguage);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
            IHM_Footies.Ressources.Strings.Culture = culture;
        }
        catch (Exception ex)
        {
            CultureInfo culture = new CultureInfo("fr-FR");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
            IHM_Footies.Ressources.Strings.Culture = culture;

            System.Diagnostics.Debug.WriteLine($"Erreur : {ex.Message}");
        }
    }
}

