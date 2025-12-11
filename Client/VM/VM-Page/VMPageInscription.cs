using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using METIER_Footies.Data;
using METIER_Footies.Metier;
using VM_Footies.VM;

namespace VM_Footies.VM_Page
{
    public class VMPageInscription : INotifyPropertyChanged
    {
        #region Attributs
        private ConnexionDAO connexionDAO;
        private VMUtilisateur nouvelUtilisateur;
        private string messageErreur;
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        #region Constructeur
        /// <summary>
        /// Constructeur de VMPageInscription
        /// </summary>
        public VMPageInscription()
        {
            connexionDAO = new ConnexionDAO();
            nouvelUtilisateur = new VMUtilisateur();
        }
        #endregion

        #region Propriétés
        /// <summary>
        /// Renvoye ou modifie un utilisateur
        /// </summary>
        public VMUtilisateur NouvelUtilisateur
        {
            get => nouvelUtilisateur;
            set { 
                nouvelUtilisateur = value;
                Notify("NouvelUtilisateur"); 
            }
        }

        /// <summary>
        /// Renvoye ou modifie un message d'erreur
        /// </summary>
        public string MessageErreur
        {
            get => messageErreur;
            set { messageErreur = value; Notify("MessageErreur"); }
        }
        #endregion

        #region Méthode
        /// <summary>
        /// Inscrit un utilisateur
        /// </summary>
        /// <param name="motDePasse">le mot de passe de l'utilisateur</param>
        /// <returns>true si l'inscription à réussis</returns>
        public async Task<bool> Inscription(string motDePasse)
        {
            bool resultat = false;
            string erreursAccumulees = "";

            if (string.IsNullOrWhiteSpace(NouvelUtilisateur.Pseudo))
            {
                erreursAccumulees += "Le pseudo est obligatoire.\n";
            }

            if (string.IsNullOrWhiteSpace(motDePasse))
            {
                erreursAccumulees += "Le mot de passe est obligatoire.\n";
            }

            if (!string.IsNullOrWhiteSpace(NouvelUtilisateur.Telephone))
            {
                if (!long.TryParse(NouvelUtilisateur.Telephone, out _))
                {
                    erreursAccumulees += "Le numéro de téléphone doit contenir uniquement des chiffres.\n";
                }
                else if (NouvelUtilisateur.Telephone.Length != 10)
                {
                    erreursAccumulees += "Le numéro de téléphone doit avoir 10 chiffres.\n";
                }
            }
            if (!string.IsNullOrWhiteSpace(NouvelUtilisateur.Email))
            {
                if (!Regex.IsMatch(NouvelUtilisateur.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    erreursAccumulees += "L'adresse email n'est pas valide.\n";
                }
            }
            if (erreursAccumulees.Length > 0)
            {
                MessageErreur = erreursAccumulees;
                resultat = false;
            }
            else
            {
                try
                {
                    bool dispo = await connexionDAO.VerifierPseudoDisponible(NouvelUtilisateur.Pseudo);

                    if (!dispo)
                    {
                        MessageErreur = "Ce pseudo est déjà utilisé.";
                        resultat = false;
                    }
                    else
                    {
                        Identifiant id = new Identifiant(0, NouvelUtilisateur.Pseudo, motDePasse);
                        Utilisateur user = NouvelUtilisateur.Utilisateur;
                        resultat = await connexionDAO.Inscription(id, user);
                    }
                }
                catch (Exception ex)
                {
                    MessageErreur = "Erreur technique : " + ex.Message;
                    resultat = false;
                }
            }

            return resultat;
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
