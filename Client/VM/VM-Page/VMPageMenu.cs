using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using METIER_Footies.Data;
using METIER_Footies.Data.Interfaces;
using METIER_Footies.Metier;
using VM_Footies.VM;

namespace VM_Footies.VM_Page
{
        public class VMPageMenu : INotifyPropertyChanged
        {
            #region Attributs
            private List<VMMenu> listeVMMenu;
            private VMMenu menuSelectionne;
            private IMenuDAO menuDAO;
            #endregion

            #region Propriétés
            /// <summary>
            /// Menu sélectionné par l'utilisateur 
            /// </summary>
            public VMMenu MenuSelectionne
            {
                get { return menuSelectionne; }
                set { menuSelectionne = value; }
            }

            /// <summary>
            /// Liste des VMMenus 
            /// </summary>
            public List<VMMenu> VMMenu => listeVMMenu;
            #endregion

            public event PropertyChangedEventHandler? PropertyChanged;

            #region Constructeurs
            /// <summary>
            /// Constructeur par défaut d'une page de menu
            /// </summary>
            public VMPageMenu()
            {
                this.menuDAO = new MenuDAO();
                this.listeVMMenu = new List<VMMenu>();
            }
            #endregion

            #region Méthodes
            /// <summary>
            /// Charge la liste des menus depuis la base de données
            /// </summary>
            /// <returns> Tâche asynchrone </returns>
            public async Task ChargerMenusAsync()
            {
                this.listeVMMenu.Clear();

                List<Menu> menus = await this.menuDAO.ObtenirToutLesMenu();

                foreach (Menu menu in menus)
                {
                    VMMenu vmMenu = new VMMenu(menu);
                    this.listeVMMenu.Add(vmMenu);
                }
                this.listeVMMenu = this.listeVMMenu.OrderBy(vm => vm.Menu.Nom).ToList();
            }

            /// <summary>
            /// Charge les plats disponibles pour un menu
            /// </summary>
            /// <param name="menu">Le VMMenu pour lequel charger les plats</param>
            public async Task ChargerPlatsDansMenu(VMMenu menu)
            {
                try
                {
                    IPlatDAO platDAO = new PlatDAO();
                    await menu.ChargerPlats(platDAO);
                }
                catch (Exception ex)
                {
                    throw new Exception("Erreur lors du chargement des plats pour le menu : " + ex.Message);
                }
            }

            /// <summary>
            /// Ajoute un menu à la liste des menus
            /// </summary>
            /// <param name="vmmenu"> Le menu à ajouter </param>
            /// <exception cref="Exception"> Lance une exception si le menu avec le même nom existe déjà </exception>
            public async Task AjouterMenu(VMMenu vmmenu)
            {
                if (MenuExiste(vmmenu))
                {
                    throw new Exception("Un menu avec ce nom existe déjà.");
                }
                vmmenu.SynchroniserPlatsSelectionnes();
                await this.menuDAO.AjouterMenu(vmmenu.Menu);
                this.listeVMMenu.Add(vmmenu);
                this.Notify("VMMenu");
            }

            /// <summary>
            /// Modifie un menu dans la liste des menus
            /// </summary>
            /// <param name="menu"></param>
            public async Task ModifierMenu(VMMenu menu)
            {
                if (menu != null)
                {
                    menu.SynchroniserPlatsSelectionnes();

                    await this.menuDAO.ModifierMenu(menu.Menu);
                    this.Notify("VMMenu");
                }
            }

            /// <summary>
            /// Supprime le menu sélectionné de la liste des menus
            /// </summary>
            /// <returns> true si la suppression a réussi, false sinon </returns>
            public async Task<bool> SupprimerMenu()
            {
                bool suppressionReussie = false;

                if (this.menuSelectionne != null)
                {
                    long id = this.menuSelectionne.Menu.IdMenu;

                    if (id != 0)
                    {
                        // bool estUtilise = await this.MenuDAO.EstDansUneInvitation(id);
                        // if (!estUtilise)
                        // {
                        await this.menuDAO.SupprimerMenu(id);
                        this.listeVMMenu.Remove(this.menuSelectionne);
                        this.menuSelectionne = null;
                        suppressionReussie = true;
                        // }
                    }
                    else
                    {
                        this.listeVMMenu.Remove(this.menuSelectionne);
                        this.menuSelectionne = null;
                        suppressionReussie = true;
                    }
                }

                return suppressionReussie;
            }

            /// <summary>
            /// Vérifie si un menu existe déjà dans la liste des menus avec le même nom
            /// </summary>
            /// <param name="menu"> Le menu à vérifier </param>
            /// <returns> True si le menu existe, False sinon </returns>
            public bool MenuExiste(VMMenu menu)
            {
                return this.listeVMMenu.Any(m => m.Menu.Nom.Equals(menu.Menu.Nom, StringComparison.OrdinalIgnoreCase));
            }

            /// <summary>
            /// Notifie le changement d'une propriété
            /// </summary>
            /// <param name="message"></param>
            private void Notify(string message)
            {
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(message));
            }
            #endregion
        }
    }
