using System.ComponentModel;
using API_Footies.Metier;

namespace API_Footies.Services.Interfaces
{
    public interface IPlatService
    {
        /// <summary>
        /// Ajouter un plat dans la base de données
        /// </summary>
        /// <param name="plat"> Plat à ajouter </param>
        /// <returns> Plat ajouter </returns>
        void AjouterPlat(Plat plat);


    }
}