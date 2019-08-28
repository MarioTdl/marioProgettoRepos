using System.Threading.Tasks;
using marioProgetto.Models;
using Microsoft.EntityFrameworkCore;

namespace marioProgetto.Persistence
{

    public class VehicleRepository : IVehicleRepository
    {
        private readonly MarioProgettoDbContext _context;
        public VehicleRepository(MarioProgettoDbContext context)
        {
            _context = context;
        }
        public async Task<Veichle> GetVeichle(int id)
        {
            return await _context.Veichles
           .Include(v => v.Features)
           .ThenInclude(vf => vf.Feature)
           .Include(i => i.Model)
           .ThenInclude(m => m.Make)
           .SingleOrDefaultAsync(p => p.Id == id);
        }
    }
}