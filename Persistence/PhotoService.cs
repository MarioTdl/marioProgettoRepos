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
        private readonly IPhotoStorage _photoStorage;
        public PhotoService(IUnitOfWork unitOfWork, IPhotoStorage photoStorage)
        {
            _unitOfWork = unitOfWork;
            _photoStorage = photoStorage;
        }
        public async Task<Photo> UploadPhoto(Veichle veichle, IFormFile file, string uploadFolderPath)
        {
            var fileName = await _photoStorage.StorePhoto(uploadFolderPath, file);
            var photo = new Photo { FileName = fileName };
            veichle.Photos.Add(photo);
            await _unitOfWork.CompleteAsync();

            return photo;
        }
    }
}