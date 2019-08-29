using System.Threading.Tasks;
using marioProgetto.Models;

namespace marioProgetto.Persistence
{
    public interface IVehicleRepository
    {
        Task<Veichle> GetVeichle(int id, bool includeResource = true);
        void Add(Veichle veichle);
        void Remove(Veichle veichle);
    }
}