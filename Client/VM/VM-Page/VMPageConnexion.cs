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
        #region attributs
        private IConnexionDAO connexionDAO;
        private string pseudo;
        private string motDePasse;
        private string messageErreur;
        #endregion

        #region propriétés
        /// <summary>
        /// Retourne ou modifie le pseudo saisi
        /// </summary>
        public string Pseudo
        {
            get { return pseudo; }
            set
            {
                pseudo = value;
                Notify("Pseudo");
            }
        }

        /// <summary>
        /// Retourne ou modifie le mot de passe saisi
        /// </summary>
        public string MotDePasse
        {
            get { return motDePasse; }
            set
            {
                motDePasse = value;
                Notify("MotDePasse");
            }
        }

        /// <summary>
        /// Retourne ou modifie le message d'erreur
        /// </summary>
        public string MessageErreur
        {
            get { return messageErreur; }
            set
            {
                messageErreur = value;
                Notify("MessageErreur");
            }
        }
        #endregion

        public event PropertyChangedEventHandler? PropertyChanged;

        #region constructeurs
        /// <summary>
        /// Constructeur par défaut de la page de connexion
        /// </summary>
        public VMPageConnexion()
        {
            this.connexionDAO = new ConnexionDAO();
            this.pseudo = string.Empty;
            this.motDePasse = string.Empty;
            this.messageErreur = string.Empty;
        }
        #endregion

        #region méthodes
        /// <summary>
        /// Vérifie les champs et tente la connexion
        /// </summary>
        /// <returns> true si la connexion a réussi, false sinon </returns>
        public async Task<bool> Connexion()
        {
            bool connexionReussie = false;

            if (ValiderChamps())
            {
                Identifiant identifiant = new Identifiant(0, this.pseudo, this.motDePasse);
                Utilisateur? utilisateurConnecte = await this.connexionDAO.Connexion(identifiant);

                if (utilisateurConnecte != null)
                {
                    SessionService.Instance.UtilisateurConnecte = utilisateurConnecte;

                    connexionReussie = true;
                    this.MessageErreur = string.Empty;
                }
                else
                {
                    this.MessageErreur = "Pseudo ou mot de passe incorrect";
                }
            }

            return connexionReussie;
        }

        public async Task<bool> Inscription()
        {
            bool inscriptionReussie = false;
            if (ValiderChamps())
            {
                bool disponible = await this.connexionDAO.VerifierPseudoDisponible(this.pseudo);
                if (!disponible)
                {
                    this.MessageErreur = "Le pseudo est déjà utilisé";
                }
                else
                {
                    this.MessageErreur = string.Empty;
                    Identifiant nouvelUtilisateur = new Identifiant(0, this.pseudo, this.motDePasse);
                }
            }
            return inscriptionReussie;
        }

        /// <summary>
        /// Valide que les champs pseudo et mot de passe ne sont pas vides
        /// </summary>
        /// <returns> true si les champs sont valides, false sinon </returns>
        private bool ValiderChamps()
        {
            bool champsValides = false;

            if (string.IsNullOrWhiteSpace(this.pseudo))
            {
                this.MessageErreur = "Le pseudo est obligatoire";
            }
            else if (string.IsNullOrWhiteSpace(this.motDePasse))
            {
                this.MessageErreur = "Le mot de passe est obligatoire";
            }
            else
            {
                champsValides = true;
                this.MessageErreur = string.Empty;
            }

            return champsValides;
        }

        /// <summary>
        /// Notifie le changement d'une propriété
        /// </summary>
        /// <param name="message"> Nom de la propriété changée </param>
        private void Notify(string message)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(message));
        }
        #endregion
    }
}
