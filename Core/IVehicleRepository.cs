using System.Collections.Generic;
using System.Threading.Tasks;
using marioProgetto.Models;
using marioProgettoRepos.Core.Models;

namespace marioProgetto.Core
{
    public interface IVehicleRepository
    {
        Task<Veichle> GetVeichle(int id, bool includeResource = true);
        void Add(Veichle veichle);
        void Remove(Veichle veichle);
        Task<QueryResult<Veichle>> GetVeichles(VeichleQuery filter);
    }
}