using METIER_Footies.Metier;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM_Footies.VM;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;

namespace VM_Footies.VM_Page
{
    /// <summary>
    /// Modèle de vue pour les statistiques d'invite selectionner
    /// </summary>
    public class VMinviteStats : INotifyPropertyChanged
    {
        #region Attributs
        private VMInvite _invite;
        private bool _estSelectionne;
        #endregion

        #region Propriétés
        public string Identite => _invite.Identite;

        /// <summary>
        /// Invite associée
        /// </summary>
        public VMInvite Invite
        {
            get { return _invite; }
            set
            {
                _invite = value;
                Notify("Invite");
            }
        }
        /// <summary>
        /// Indique si l'invité est sélectionné
        /// </summary>
        public bool EstSelectionne
        {
            get { return _estSelectionne; }
            set
            {
                _estSelectionne = value;
                Notify("EstSelectionne");
            }
        }

        /// <summary>
        /// Événement déclenché lorsqu'une propriété change
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur de VMinviteStats
        /// </summary>
        /// <param name="invite">prend un vminvite</param>
        public VMinviteStats(VMInvite invite)
        {
            _invite = invite;
            _estSelectionne = false;
        }
        #endregion

        #region Méthodes protégées
        protected void Notify(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }

    /// <summary>
    /// Modèle de vue pour la page des statistiques
    /// </summary>
    public class VmPageStatistique : INotifyPropertyChanged
    {
        #region Attributs
        private VMPageInvitation _invitation;
        private VMPageInvite _invite;
        private bool _toutSelectionner;
        private string texteRecherche;
        private PlotModel statistiqueModel;
        private ObservableCollection<VMinviteStats> _invitesStats;
        #endregion

        #region Propriétés
        /// <summary>
        /// Liste de tous les invités pour les statistiques
        /// </summary>
        public ObservableCollection<VMinviteStats> InvitesStats
        {
            get { return _invitesStats; }
            set
            {
                _invitesStats = value;
                Notify("InvitesStats");
            }
        }

        /// <summary>
        /// Liste des invités selectionnés
        /// </summary>
        public List<VMInvite> InvitesSelectionnes
        {
            get
            {
                List<VMInvite> invites = new List<VMInvite>();
                foreach (VMinviteStats inviteStat in _invitesStats)
                {
                    if (inviteStat.EstSelectionne == true)
                    {
                        invites.Add(inviteStat.Invite);
                    }
                }
                return invites;
            }
        }


        /// <summary>
        /// Indique si tous les invités sont sélectionnés
        /// </summary>
        public bool ToutSelectionner
        {
            get => _toutSelectionner;
            set
            {
                _toutSelectionner = value;

                _invitesStats.Clear();
                foreach (VMInvite invite in _invite.VMInvites)
                {
                    VMinviteStats vmStats = new VMinviteStats(invite)
                    {
                        EstSelectionne = value
                    };

                    _invitesStats.Add(vmStats);
                }

                Notify("ToutSelectionner");
            }
        }

        /// <summary>
        /// Texte de recherche pour filtrer les invités
        /// </summary>
        public string TexteRechercheGroupe
        {
            get { return texteRecherche; }
            set
            {
                texteRecherche = value;
                Notify("TexteRechercheGroupe");
            }
        }

        /// <summary>
        /// Modèle de graphique pour les statistiques
        /// </summary>
        public PlotModel StatistiqueModel
        {
            get
            {
                return this.statistiqueModel;
            }
            set
            {
                this.statistiqueModel = value;
                Notify("StatistiqueModel");
            }
        }

        /// <summary>
        /// Événement déclenché lorsqu'une propriété change
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// exposer les invitations
        /// </summary>
        public VMPageInvitation Invitation { get { return _invitation; } }

        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur de VmPageStatistique
        /// </summary>
        /// <param name="vMPageInvitation">prend en parametre les invitations</param>
        public VmPageStatistique()
        {
            this._invitation = new VMPageInvitation();
            this._invite = new VMPageInvite();
            this._invitesStats = new ObservableCollection<VMinviteStats>();
            this._toutSelectionner = false;
            this.texteRecherche = string.Empty;
            ChargerDonneesInvite();
        }
        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Initialise les statistiques des invités
        /// </summary>
        public void InitialiserStatsInvites()
        {
            _invitesStats.Clear();
            foreach (VMInvite invite in _invite.VMInvites)
            {
                VMinviteStats vmStats = new VMinviteStats(invite);
                _invitesStats.Add(vmStats);
            }
        }

        /// <summary>
        /// Charge les données des invités pour initialiser les statistiques
        /// </summary>
        /// <returns>une tache</returns>
        public async void ChargerDonneesInvite()
        {
            await _invite.ChargerInvites();
            InitialiserStatsInvites();
        }

        /// <summary>
        /// Génère les données statistiques pour la fréquence de la venue de chaque invité
        /// </summary>
        /// <returns>une tache</returns>
        public async Task<Dictionary<string, int>> GenererDonneesStatistiques()
        {
            await _invitation.ChargerInvitations();

            Dictionary<string, int> statistiques = new Dictionary<string, int>();

            foreach (VMInvite vMInvite in InvitesSelectionnes)
            {
                statistiques.Add(vMInvite.Invite.Identite, 0);
            }


            foreach (VMInvitation invitation in this._invitation.VMInvitations)
            {

                List<Invite> nomInvite = invitation.Invites;
                foreach (Invite invite in nomInvite)
                {
                    string nom = invite.Identite;
                    if (statistiques.ContainsKey(nom))
                    {
                        statistiques[nom]++;
                    }
                }
                List<GroupeInvites> groupes = invitation.GroupeInvites;
                foreach (GroupeInvites groupe in groupes)
                {
                    foreach (Invite invite in groupe.Invites)
                    {
                        string nom = invite.Identite;
                        if (statistiques.ContainsKey(nom))
                        {
                            statistiques[nom]++;
                        }
                    }
                }
            }
            // trie le dictionnaire par valeur decroissante
            statistiques = statistiques.OrderByDescending(x => x.Value)
                                     .ToDictionary(x => x.Key, x => x.Value);
            return statistiques;
        }

        /// <summary>
        /// Recherche un invité dans les statistiques en fonction du texte recherché
        /// ici les invites correspondant au texte de recherche sont placés en haut de la liste
        /// et les autres en bas et la selection est conservée
        /// </summary>
        /// <param name="textrechercher">le text de recherche</param>
        public void RechercherInviteStatistique(string textrechercher)
        {
            List<VMinviteStats> invitesFiltres = new List<VMinviteStats>();
            List<VMinviteStats> invitesNonFiltres = new List<VMinviteStats>();

            foreach (VMinviteStats inviteStat in _invitesStats)
            {
                if (inviteStat.Invite.Identite.Contains(textrechercher, StringComparison.OrdinalIgnoreCase))
                {
                    invitesFiltres.Add(inviteStat);
                }
                else
                {
                    invitesNonFiltres.Add(inviteStat);
                }
            }

            //trier les deux listes par ordre alphabétique
            invitesFiltres = invitesFiltres.OrderBy(i => i.Invite.Identite).ToList();
            invitesNonFiltres = invitesNonFiltres.OrderBy(i => i.Invite.Identite).ToList();
            invitesFiltres.AddRange(invitesNonFiltres);

            // raffraichir la collection observable
            _invitesStats.Clear();
            foreach (VMinviteStats inviteStat in invitesFiltres)
            {
                _invitesStats.Add(inviteStat);
            }
        }

        /// <summary>
        /// Crée le modèle de statistique pour la fréquence de la venue de chaque invité (le graphique)
        /// </summary>
        public async void CreerStatistique()
        {
            PlotModel model = new PlotModel
            {
                Title = "Statistiques — fréquence de venue des invités",
                PlotAreaBackground = OxyColor.FromRgb(255, 250, 240),
                TitleFontSize = 18,
                TitleColor = OxyColors.DarkBlue
            };

            CategoryAxis categoryAxis = new CategoryAxis
            {
                Position = AxisPosition.Left,
                Title = "Invités",
                TitleFontSize = 18,
                TitleColor = OxyColors.DarkRed,
                TextColor = OxyColors.Black,
                FontSize = 14,
                IsZoomEnabled = false
            };

            LinearAxis valueAxis = new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Fréquence de venue",
                TitleFontSize = 18,
                TitleColor = OxyColors.DarkRed,
                TextColor = OxyColors.Black,
                FontSize = 14,
                AbsoluteMinimum = 0,
                MajorGridlineStyle = LineStyle.Solid,
                MajorGridlineColor = OxyColors.LightGray,
                MajorStep = 1,
                MinorStep = 1,
                StringFormat = "0"
            };

            model.Axes.Add(categoryAxis);
            model.Axes.Add(valueAxis);

            BarSeries barSeries = new BarSeries
            {
                Title = "Fréquence de venue : ",
                FillColor = OxyColor.FromArgb(255, 140, 47, 38),
                StrokeColor = OxyColors.Black,
                StrokeThickness = 1,
                TextColor = OxyColor.FromRgb(10, 10, 10),
                FontWeight = FontWeights.Bold, 
                LabelFormatString = "{0} Fois"
            };

            Dictionary<string, int> donneesStatistiques = await GenererDonneesStatistiques();

            int maxZoom = 0;
            foreach (KeyValuePair<string, int> element in donneesStatistiques)
            {
                categoryAxis.Labels.Add(element.Key);
                barSeries.Items.Add(new BarItem { Value = element.Value });
                if (element.Value > maxZoom)
                {
                    maxZoom = element.Value;
                }
            }

            valueAxis.AbsoluteMaximum = maxZoom + 5;
            valueAxis.Maximum = maxZoom + 1;
            model.Series.Add(barSeries);
            StatistiqueModel = model;
        }

        #endregion

        #region Méthodes protegées
        protected void Notify(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
