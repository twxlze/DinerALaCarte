using System.Collections.ObjectModel;
using System.ComponentModel;
using METIER_Footies.Metier;

namespace VM_Footies.VM
{
    public class VMNotePlat : INotifyPropertyChanged
    {
        #region Attributs
        private Invitation invitation;
        private ObservableCollection<VMInvite> listeInvites;
        private ObservableCollection<VMPlat> listePlats;
        private VMInvite? inviteSelectionne;
        private VMPlat? platSelectionne;
        private string noteSaisie;
        private string commentaireSaisi;
        #endregion

        public event PropertyChangedEventHandler? PropertyChanged;

        #region Propriétés pour le Binding XAML

        /// <summary>
        /// Liste des invités d'une invitation
        /// </summary>
        public ObservableCollection<VMInvite> ListeInvites
        {
            get => listeInvites;
            set
            {
                listeInvites = value;
                Notify("ListeInvites");
            }
        }

        /// <summary>
        /// Liste des plats d'une invitation
        /// </summary>
        public ObservableCollection<VMPlat> ListePlats
        {
            get => listePlats;
            set
            {
                listePlats = value;
                Notify("ListePlats");
            }
        }

        /// <summary>
        /// Invité sélectionné dans la liste des invités
        /// </summary>
        public VMInvite? InviteSelectionne
        {
            get => inviteSelectionne;
            set
            {
                inviteSelectionne = value;
                Notify("InviteSelectionne");
            }
        }

        /// <summary>
        /// Plats sélectionné dans la liste des plats
        /// </summary>
        public VMPlat? PlatSelectionne
        {
            get => platSelectionne;
            set
            {
                platSelectionne = value;
                Notify("PlatSelectionne");
            }
        }

        /// <summary>
        /// Note saisi par l'utilisateur de 1 à 10
        /// </summary>
        public string NoteSaisie
        {
            get => noteSaisie;
            set
            {
                noteSaisie = value;
                Notify("NoteSaisie");
            }
        }

        /// <summary>
        /// Commentaire saisi par l'utilisateur
        /// </summary>
        public string CommentaireSaisi
        {
            get => commentaireSaisi;
            set
            {
                commentaireSaisi = value; 
                Notify("CommentaireSaisi");
            }
        }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public VMNotePlat()
        {
            this.listeInvites = new ObservableCollection<VMInvite>();
            this.listePlats = new ObservableCollection<VMPlat>();
            this.noteSaisie = "";
            this.commentaireSaisi = "";
        }

        /// <summary>
        /// Constructeur d'un VMNotePlat à partir d'une invitation 
        /// </summary>
        /// <param name="invitation"></param>
        public VMNotePlat(Invitation invitation)
        {
            this.invitation = invitation;
            this.listeInvites = new ObservableCollection<VMInvite>();

            this.listePlats = new ObservableCollection<VMPlat>();
            this.noteSaisie = "";
            this.commentaireSaisi = "";
        }

        /// <summary>
        /// Constructeur par copie d'un VMNotePlat
        /// </summary>
        /// <param name="vmNotePlat"> VMNotePlat à copier </param>
        public VMNotePlat(VMNotePlat vmNotePlat)
        {
            this.invitation = vmNotePlat.invitation;
            this.listeInvites = vmNotePlat.listeInvites;
            this.listePlats = vmNotePlat.listePlats;
            this.inviteSelectionne = vmNotePlat.inviteSelectionne;
            this.platSelectionne = vmNotePlat.platSelectionne;
            this.noteSaisie = vmNotePlat.noteSaisie;
            this.commentaireSaisi = vmNotePlat.commentaireSaisi;
        }

        #endregion

        #region méthodes privées
        
        private void Notify(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        #endregion
    }
}