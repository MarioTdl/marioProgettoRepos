using System.Threading.Tasks;
using marioProgetto.Core;

namespace marioProgetto.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MarioProgettoDbContext _context;
        public UnitOfWork(MarioProgettoDbContext context)
        {
            _context = context;
        }
        public async Task CompleteAsync()
        {
           await _context.SaveChangesAsync();
        }
    }
}