using System.Threading.Tasks;
using marioProgetto.Models;

namespace marioProgetto.Persistence
{
    public interface IVehicleRepository
    {
        Task<Veichle> GetVeichle(int id);
    }
}