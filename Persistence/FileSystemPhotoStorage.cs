using System.Threading.Tasks;
using marioProgettoRepos.Core;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace marioProgettoRepos.Persistence
{
    public class FileSystemPhotoStorage : IPhotoStorage
    {
        public async Task<string> StorePhoto(string uploadFolderPath, IFormFile file)
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
            return fileName;
        }
    }
}