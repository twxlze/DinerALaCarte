using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using METIER_Footies.Metier;

namespace VM_Footies.VM
{
    public class VMUtilisateur : INotifyPropertyChanged
    {
        #region Attribut
        private Utilisateur utilisateur;
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        #region Constructeur
        /// <summary>
        /// Constructeur pars défaut de VMUtilisateur
        /// </summary>
        public VMUtilisateur()
        {
            utilisateur = new Utilisateur(0, "", "", "", "", "");
        }
        /// <summary>
        /// Constructeur pars copie de VmUtilisateur
        /// </summary>
        /// <param name="u">Un Utilisateur</param>
        public VMUtilisateur(Utilisateur u)
        {
            utilisateur = u;
        }
        #endregion

        #region Propriété
        /// <summary>
        /// Renvoye l'utilisateur
        /// </summary>
        public Utilisateur Utilisateur => utilisateur;

        /// <summary>
        /// Renvoye ou modifie le pseudo de l'utilisateur
        /// </summary>
        public string Pseudo
        {
            get => utilisateur.Pseudo;
            set { 
                utilisateur.Pseudo = value; 
                Notify("Pseudo");
            }
        }

        /// <summary>
        /// Renvoye ou modifie le nom de l'utilisateur
        /// </summary>
        public string Nom
        {
            get => utilisateur.Nom;
            set { 
                utilisateur.Nom = value;
                Notify("Nom"); 
            }
        }

        /// <summary>
        /// Renvoye ou modifie le prénom de l'utilisateur
        /// </summary>
        public string Prenom
        {
            get => utilisateur.Prenom;
            set { 
                utilisateur.Prenom = value;
                Notify("Prenom");
            }
        }

        /// <summary>
        /// Renvoye ou modifie le mail de l'utilisateur
        /// </summary>
        public string Email
        {
            get => utilisateur.Mail;
            set { 
                utilisateur.Mail = value;
                Notify("Email"); 
            }
        }

        /// <summary>
        /// Renvoye ou modifie le numéro de téléphone de l'utilisateur
        /// </summary>
        public string Telephone
        {
            get => utilisateur.NumTel;
            set { 
                utilisateur.NumTel = value;
                Notify("Telephone");
            }
        }
        #endregion

        #region Méthodes
        private void Notify(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}