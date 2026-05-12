namespace API_Footies.Outils
{
    /// <summary>
    /// Classe permettant de lire les configurations dans le fichier ini
    /// </summary>
    public class ConfigHelper : IConfigHelper
    {
        public string LireSelDansIni()
        {
            string cheminFichier = "Config/config.ini";
            string sel = "";

            if (File.Exists(cheminFichier))
            {
                string[] lignes = File.ReadAllLines(cheminFichier);
                foreach (string ligne in lignes)
                {
                    if (ligne.StartsWith("Sel="))
                    {
                        sel = ligne.Substring(4);
                    }
                }
            }

            if (string.IsNullOrEmpty(sel))
            {
                throw new Exception("Le fichier config.ini est introuvable ou le Sel est manquant.");
            }

            return sel;
        }
    }
}
