using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using marioProgetto.Persistence;
using marioProgettoRepos.Core;
using marioProgettoRepos.Core.Models;

namespace marioProgettoRepos.Persistence
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly MarioProgettoDbContext _context;
        public PhotoRepository(MarioProgettoDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Photo>> GetPhotos(int veichleId)
        {
            return await _context.Photos.
      }
    }
}