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
        private ObservableCollection<VMInvite> _invitesStats;
        private List<VMInvite> invitesSelectionnesSauvegardes;

        private VMPagePlat plat;
        private bool toutSelectionnerPlat;
        private string texteRecherchePlat;
        private ObservableCollection<VMPlat> platsStats;
        private List<VMPlat> platsSelectionnesSauvegardes;
        private PlotModel statistiqueModelPlat;

        private PlotModel statistiqueModelAimes;
        private PlotModel statistiqueModelDetestes;
        #endregion

        #region Propriétés
        /// <summary>
        /// Liste de tous les invités pour les statistiques
        /// </summary>
        public ObservableCollection<VMInvite> InvitesStats
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
                if (invitesSelectionnesSauvegardes != null)
                {
                    invites = invitesSelectionnesSauvegardes.Where(invite => invite.InviteSelectionne == true).ToList();
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
                if (_toutSelectionner != value)
                {
                    _toutSelectionner = value;

                    foreach (VMInvite invite in _invitesStats)
                    {
                        invite.InviteSelectionne = value;
                    }
                    Notify("ToutSelectionner");
                }
            }
        }

        /// <summary>
        /// Texte de recherche pour filtrer les invités
        /// </summary>
        public string TexteRechercheInviteStats
        {
            get { return texteRecherche; }
            set
            {
                texteRecherche = value;
                Notify("TexteRechercheInviteStats");
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
        /// Modèle de graphique pour les statistiques des plats
        /// </summary>
        public PlotModel StatistiqueModelPlat
        {
            get { return this.statistiqueModelPlat; }
            set
            {
                this.statistiqueModelPlat = value;
                Notify("StatistiqueModelPlat");
            }
        }

        /// <summary>
        /// Liste de tous les plats pour les statistiques
        /// </summary>
        public ObservableCollection<VMPlat> PlatsStats
        {
            get { return platsStats; }
            set
            {
                platsStats = value;
                Notify("PlatsStats");
            }
        }

        /// <summary>
        /// Liste des plats selectionnés
        /// </summary>
        public List<VMPlat> EstSelectionne
        {
            get
            {
                List<VMPlat> plats = new List<VMPlat>();
                if (platsSelectionnesSauvegardes != null)
                {
                    plats = platsSelectionnesSauvegardes.Where(plat => plat.EstSelectionne == true).ToList();
                }
                return plats;
            }
        }

        /// <summary>
        /// Indique si tous les plats sont sélectionnés
        /// </summary>
        public bool ToutSelectionnerPlat
        {
            get => this.toutSelectionnerPlat;
            set
            {
                if (this.toutSelectionnerPlat != value)
                {
                    this.toutSelectionnerPlat = value;
                    foreach (VMPlat plat in this.platsStats)
                    {
                        plat.EstSelectionne = value;
                    }
                    Notify("ToutSelectionnerPlat");
                }
            }
        }

        /// <summary>
        /// Texte de recherche pour filtrer les plats
        /// </summary>
        public string TexteRecherchePlatStats
        {
            get { return this.texteRecherchePlat; }
            set
            {
                this.texteRecherchePlat = value;
                Notify("TexteRecherchePlatStats");
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

        /// <summary>
        /// Modèle de graphique pour les statistiques des plats aimés
        /// </summary>
        public PlotModel StatistiqueModelAimes
        {
            get { return this.statistiqueModelAimes; }
            set
            {
                this.statistiqueModelAimes = value;
                Notify("StatistiqueModelAimes");
            }
        }

        /// <summary>
        /// Modèle de graphique pour les statistiques des plats détestés
        /// </summary>
        public PlotModel StatistiqueModelDetestes
        {
            get { return this.statistiqueModelDetestes; }
            set
            {
                this.statistiqueModelDetestes = value;
                Notify("StatistiqueModelDetestes");
            }
        }
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur de VmPageStatistique
        /// </summary>
        /// <param name="vMPageInvitation">prend en parametre les invitations</param>
        public VmPageStatistique()
        {
            this.Initialiser();
            ChargerDonneesInvite();
            ChargerDonneesPlat();
        }

        private void Initialiser()
        {
            this._invitation = new VMPageInvitation();
            this._invite = new VMPageInvite();
            this._invitesStats = new ObservableCollection<VMInvite>();
            this._toutSelectionner = false;
            this.texteRecherche = string.Empty;

            this.plat = new VMPagePlat();
            this.platsStats = new ObservableCollection<VMPlat>();
            this.toutSelectionnerPlat = false;
            this.texteRecherchePlat = string.Empty;
        }
        #endregion

        #region Méthodes stats - invités
        /// <summary>
        /// Initialise les statistiques des invités
        /// </summary>
        public void InitialiserStatsInvites()
        {
            _invitesStats.Clear();
            this.invitesSelectionnesSauvegardes = new List<VMInvite>();
            foreach (VMInvite invite in _invite.VMInvites)
            {
                VMInvite vmStats = new VMInvite(invite);
                invitesSelectionnesSauvegardes.Add(vmStats);
            }
            InvitesStats = new ObservableCollection<VMInvite>(invitesSelectionnesSauvegardes);
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
                statistiques.Add(vMInvite.Identite, 0);
            }


            foreach (VMInvitation invitation in this._invitation.VMInvitations)
            {
                foreach (Invite invite in invitation.Invites)
                {
                    string nom = invite.Identite;
                    if (statistiques.ContainsKey(nom))
                    {
                        statistiques[nom]++;
                    }
                }

                foreach (GroupeInvites groupe in invitation.GroupeInvites)
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
            if (string.IsNullOrWhiteSpace(textrechercher))
                this.InvitesStats = new ObservableCollection<VMInvite>(invitesSelectionnesSauvegardes);
            else
            {
                List<VMInvite> resultatsFiltres = invitesSelectionnesSauvegardes.Where(i => i.Identite.Contains(textrechercher, StringComparison.OrdinalIgnoreCase)).OrderBy(i => i.Identite).ToList();
                this.InvitesStats = new ObservableCollection<VMInvite>(resultatsFiltres);
            }
        }

        /// <summary>
        /// Crée le modèle de statistique pour la fréquence de la venue de chaque invité (le graphique)
        /// </summary>
        public async void CreerStatistique()
        {
            Dictionary<string, int> donnees = await GenererDonneesStatistiques();
            StatistiqueModel = GenererGraphique("Fréquence de venue des invités",OxyColor.FromRgb(255, 250, 240), OxyColors.DarkBlue,"Invités","Fréquence de venue",OxyColor.FromArgb(255, 140, 47, 38),donnees);
        }
        #endregion

        #region Méthodes stats - plats
        /// <summary>
        /// Initialise les statistiques des plats
        /// </summary>
        public void InitialiserStatsPlats()
        {
            this.platsStats.Clear();
            this.platsSelectionnesSauvegardes = new List<VMPlat>();
            foreach (VMPlat plat in this.plat.VMPlat)
            {
                VMPlat vmStats = new VMPlat(plat);
                platsSelectionnesSauvegardes.Add(vmStats);
            }
            PlatsStats = new ObservableCollection<VMPlat>(this.platsSelectionnesSauvegardes);
        }

        /// <summary>
        /// Charge les données des plats pour initialiser les statistiques
        /// </summary>
        public async void ChargerDonneesPlat()
        {
            await plat.ChargerPlats();
            InitialiserStatsPlats();
        }

        /// <summary>
        /// Génère les données statistiques pour la fréquence de sélection de chaque plat
        /// </summary>
        /// <returns> une tache</returns>
        public async Task<Dictionary<string, int>> GenererDonneesStatistiquesPlat()
        {
            await _invitation.ChargerInvitations();

            Dictionary<string, int> statistiques = new Dictionary<string, int>();

            foreach (VMPlat vMPlat in EstSelectionne)
            {
                statistiques.Add(vMPlat.Nom, 0);
            }

            foreach (VMInvitation invitation in this._invitation.VMInvitations)
            {
                foreach (Plat plat in invitation.Plats)
                {
                    string nom = plat.Nom;
                    if (statistiques.ContainsKey(nom))
                    {
                        statistiques[nom]++;
                    }
                }

                foreach (Menu menu in invitation.Menu)
                {
                    foreach (Plat plat in menu.Plat)
                    {
                        string nom = plat.Nom;
                        if (statistiques.ContainsKey(nom))
                        {
                            statistiques[nom]++;
                        }
                    }
                }
            }
            statistiques = statistiques.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            return statistiques;
        }

        /// <summary>
        /// recherche un plat dans les statistiques en fonction du texte recherché
        /// </summary>
        /// <param name="textrechercher"> le text de recherche</param>
        public void RechercherPlatStatistique(string textrechercher)
        {
            if (string.IsNullOrWhiteSpace(textrechercher))
                this.PlatsStats = new ObservableCollection<VMPlat>(this.platsSelectionnesSauvegardes);
            else
            {
                List<VMPlat> resultatsFiltres = this.platsSelectionnesSauvegardes.Where(i => i.Nom.Contains(textrechercher, StringComparison.OrdinalIgnoreCase)).OrderBy(i => i.Nom).ToList();
                this.PlatsStats = new ObservableCollection<VMPlat>(resultatsFiltres);
            }
        }

        /// <summary>
        /// Crée le modèle de statistique pour la fréquence de sélection de chaque plat (le graphique)
        /// </summary>
        public async void CreerStatistiquePlat()
        {
            Dictionary<string, int> donnees = await GenererDonneesStatistiquesPlat();
            StatistiqueModelPlat = GenererGraphique("Fréquence des plats sélectionnés",OxyColor.FromRgb(255, 250, 240), OxyColors.DarkBlue,"Plats","Fréquence de sélection",OxyColor.FromArgb(255, 46, 139, 87),donnees);
            CreerStatistiquePlatsAimes();
            CreerStatistiquePlatsDetestes();
        }
        #endregion

        #region Méthodes stats - plats aimés et détestés
        /// <summary>
        /// Génère les données pour les plats les plus aimés par les invités sélectionnés
        /// </summary>
        public Dictionary<string, int> GenererDonneesPlatsAimes()
        {
            Dictionary<string, int> stats = new Dictionary<string, int>();

            foreach (VMInvite vmInvite in this.invitesSelectionnesSauvegardes)
            {
                foreach (Plat plat in vmInvite.Invite.PlatsPreferes ?? Enumerable.Empty<Plat>())
                {
                    if (stats.ContainsKey(plat.Nom))
                        stats[plat.Nom]++;
                    else
                        stats.Add(plat.Nom, 1);
                }
            }
            return stats.OrderByDescending(x => x.Value).Take(5).ToDictionary(x => x.Key, x => x.Value);
        }

        /// <summary>
        /// Génère les données pour les plats les plus détestés par les invités sélectionnés
        /// </summary>
        public Dictionary<string, int> GenererDonneesPlatsDetestes()
        {
            Dictionary<string, int> stats = new Dictionary<string, int>();

            foreach (VMInvite vmInvite in this.invitesSelectionnesSauvegardes)
            {
                if (vmInvite.Invite.PlatsDetestes == null) continue;

                foreach (Plat plat in vmInvite.Invite.PlatsDetestes)
                {
                    if (stats.ContainsKey(plat.Nom))
                        stats[plat.Nom]++;
                    else
                        stats.Add(plat.Nom, 1);
                }
            }
            return stats.OrderByDescending(x => x.Value).Take(5).ToDictionary(x => x.Key, x => x.Value); // top 5
        }

        /// <summary>
        /// Crée le graphique des plats aimés
        /// </summary>
        public void CreerStatistiquePlatsAimes()
        {
            Dictionary<string, int> donnees = GenererDonneesPlatsAimes();
            StatistiqueModelAimes = GenererGraphique("Top des Plats Aimés",OxyColor.FromRgb(250, 255, 250),OxyColors.DarkGreen,"Plats", "Nombre d'approbations",OxyColors.SeaGreen, donnees);
        }

        /// <summary>
        /// Crée le graphique des plats détestés
        /// </summary>
        public void CreerStatistiquePlatsDetestes()
        {
            Dictionary<string, int> donnees = GenererDonneesPlatsDetestes();
            StatistiqueModelDetestes = GenererGraphique("Top des Plats Détestés",OxyColor.FromRgb(255, 250, 250),OxyColors.DarkRed, "Plats","Nombre de rejets",OxyColors.IndianRed,donnees );
        }
        #endregion

        #region Méthodes protegées / privées 
        private PlotModel GenererGraphique(string titre, OxyColor fond, OxyColor couleurTitre, string axeX, string axeY, OxyColor couleurBarres, Dictionary<string, int> donnees)
        {
            PlotModel model = new PlotModel
            {
                Title = titre,
                PlotAreaBackground = fond,
                TitleFontSize = 18,
                TitleColor = couleurTitre
            };

            CategoryAxis categoryAxis = new CategoryAxis
            {
                Position = AxisPosition.Left,
                Title = axeX,
                TitleFontSize = 14,
                TitleColor = OxyColors.DarkRed,
                TextColor = OxyColors.Black,
                FontSize = 12,
                IsZoomEnabled = false
            };

            LinearAxis valueAxis = new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Title = axeY,
                TitleFontSize = 14,
                TitleColor = OxyColors.DarkRed,
                TextColor = OxyColors.Black,
                FontSize = 12,
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
                Title = "Données : ",
                FillColor = couleurBarres,
                StrokeColor = OxyColors.Black,
                StrokeThickness = 1,
                TextColor = OxyColor.FromRgb(10, 10, 10),
                FontWeight = FontWeights.Bold,
                LabelFormatString = "{0}"
            };

            int maxZoom = 0;
            foreach (KeyValuePair<string, int> element in donnees)
            {
                categoryAxis.Labels.Add(element.Key);
                barSeries.Items.Add(new BarItem { Value = element.Value });
                if (element.Value > maxZoom) maxZoom = element.Value;
            }

            valueAxis.AbsoluteMaximum = maxZoom + 5;
            valueAxis.Maximum = maxZoom + 1;
            model.Series.Add(barSeries);

            return model;
        }

        protected void Notify(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
