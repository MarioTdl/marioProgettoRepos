using System.Threading.Tasks;
using marioProgetto.Core;
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
        public async Task<Veichle> GetVeichle(int id, bool includeResource = true)
        {
            if (!includeResource)
                return await _context.Veichles.FindAsync(id);

            return await _context.Veichles
           .Include(v => v.Features)
           .ThenInclude(vf => vf.Feature)
           .Include(i => i.Model)
           .ThenInclude(m => m.Make)
           .SingleOrDefaultAsync(p => p.Id == id);
        }
        public void Add(Veichle veichle)
        {
            _context.Veichles.Add(veichle);
        }
        public void Remove(Veichle veichle)
        {
            _context.Veichles.Remove(veichle);
        }
    }
}