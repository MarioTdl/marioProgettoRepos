using System;
using System.IO;
using System.Threading.Tasks;
using marioProgetto.Core;
using marioProgetto.Models;
using marioProgettoRepos.Core;
using marioProgettoRepos.Core.Models;
using Microsoft.AspNetCore.Http;

namespace marioProgettoRepos.Persistence
{
    public class PhotoService : IPhotoService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PhotoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Photo> UploadPhoto(Veichle veichle, IFormFile file, string uploadFolderPath)
        {
            //se non e presente la directory la crea
            if (!Directory.Exists(uploadFolderPath))
                Directory.CreateDirectory(uploadFolderPath);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            var photo = new Photo { FileName = fileName };
            veichle.Photos.Add(photo);
            await _unitOfWork.CompleteAsync();
            return photo;
        }
    }
}