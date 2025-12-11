using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using METIER_Footies.Data;
using METIER_Footies.Data.Interfaces;
using METIER_Footies.Metier;

namespace VM_Footies.VM_Page
{
    public class VMPageConnexion : INotifyPropertyChanged
    {
        #region Attributs
        private IConnexionDAO connexionDAO;
        private string pseudo;
        private string messageErreur;
        #endregion

        #region Propriétés 
        /// <summary>
        /// Renvoye ou modifie le pseudo de l'utilisateur
        /// </summary>
        public string Pseudo
        {
            get { return pseudo; }
            set { pseudo = value; Notify("Pseudo"); }
        }
        /// <summary>
        /// Renvoye ou modifie le message d'erreur
        /// </summary>
        public string MessageErreur
        {
            get { return messageErreur; }
            set { messageErreur = value; Notify("MessageErreur"); }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        #region Constructeur
        /// <summary>
        /// Constructeur de VMPageConnexion
        /// </summary>
        public VMPageConnexion()
        {
            this.connexionDAO = new ConnexionDAO();
        }
        #endregion

        #region Methodes 
        /// <summary>
        /// Connecte un utilisateur
        /// </summary>
        /// <param name="motDePasse">Le mot de passe de l'utilisateur</param>
        /// <returns>true si l'utilisateur</returns>
        public async Task<bool> Connexion(string motDePasse)
        {
            bool connexionReussie = false;

            if (!string.IsNullOrWhiteSpace(this.Pseudo) && !string.IsNullOrWhiteSpace(motDePasse))
            {
                Identifiant identifiant = new Identifiant(0, this.Pseudo, motDePasse);
                Utilisateur utilisateurConnecte = await this.connexionDAO.Connexion(identifiant);

                if (utilisateurConnecte != null)
                {
                    SessionService.Instance.UtilisateurConnecte = utilisateurConnecte;
                    connexionReussie = true;
                }
                else
                {
                    this.MessageErreur = "Pseudo ou mot de passe incorrect";
                }
            }
            else
            {
                this.MessageErreur = "Pseudo et mot de passe obligatoires";
            }

            return connexionReussie;
        }
        #endregion

        #region méthodes privées
        /// <summary>
        /// Notifie l'UI d'un changement de propriété
        /// </summary>
        /// <param name="propriete">Nom de la propriété modifiée</param>
        private void Notify(string propriete)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propriete));
        }
        #endregion
    }
}
