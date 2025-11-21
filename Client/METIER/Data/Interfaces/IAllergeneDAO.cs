using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using METIER_Footies.Metier;

namespace METIER_Footies.Data.Interfaces
{
    /// <summary>
    /// Interface pour les opérations des allergenes
    /// </summary>
    public interface IAllergeneDAO
    {
        List<Allergene> ListeAllergene();
    }
}
