using System.Collections.Generic;
using System.Threading.Tasks;
using marioProgettoRepos.Core.Models;

namespace marioProgettoRepos.Core
{
    public interface IPhotoRepository
    {
         Task<IEnumerable<Photo>> GetPhotos (int veichleId);
    }
}