using System.Threading.Tasks;
using marioProgetto.Models;
using marioProgettoRepos.Core.Models;
using Microsoft.AspNetCore.Http;

namespace marioProgettoRepos.Core
{
    public interface IPhotoService
    {
        Task<Photo> UploadPhoto(Veichle veichle,IFormFile file,string uploadFolderPath);
    }
}