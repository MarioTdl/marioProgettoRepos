using System;
using System.Collections.Generic;

using System.IO;

using System.Threading.Tasks;
using AutoMapper;
using marioProgetto.Core;
using marioProgettoRepos.Controllers.Resource;
using marioProgettoRepos.Core;
using marioProgettoRepos.Core.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace marioProgettoRepos.Controllers
{
    [Route("/api/vehicles/{veichlesId}/photos")]
    public class PhotosController : Controller
    {
        private readonly IHostingEnvironment _host;
        private readonly IVehicleRepository _repository;
        private readonly IMapper _mapper;
        private readonly PhotoSettings _options;
        private readonly IPhotoRepository _photoRepository;
        private readonly IPhotoService _photoService;
        public PhotosController(IHostingEnvironment host, IVehicleRepository repository,
         IUnitOfWork unitOfWork, IMapper mapper, IOptionsSnapshot<PhotoSettings> options,
          IPhotoRepository photoRepository, IPhotoService photoService)
        {
            _photoRepository = photoRepository;
            _options = options.Value;
            _host = host;
            _repository = repository;
            _mapper = mapper;
            _photoService = photoService;
        }
        [HttpPost]
        public async Task<IActionResult> Upload([FromRoute]int veichlesId, IFormFile file)
        {
            var veichle = await _repository.GetVeichle(veichlesId, includeResource: false);
            if (veichle == null)
                return NotFound();

            if (file == null)
                return BadRequest("Null FIle");

            if (file.Length == 0)
                return BadRequest("empty file");

            if (file.Length > _options.MaxBytes)
                return BadRequest("File troppo grande");

            if (!_options.isSupported(file.FileName))
                return BadRequest("Invalid extension file");

            var uploadsFolderPath = Path.Combine(_host.WebRootPath, "uploads");

            var photo = await _photoService.UploadPhoto(veichle, file, uploadsFolderPath);

            return Ok(_mapper.Map<Photo, PhotoResource>(photo));
        }
        public async Task<IEnumerable<PhotoResource>> GetPhotos(int veichlesId)
        {
            var photos = await _photoRepository.GetPhotos(veichlesId);

            List<PhotoResource> photoReturn = new List<PhotoResource>();
            foreach (var photo in photos)
            {
                var variable = _mapper.Map<Photo, PhotoResource>(photo);
                photoReturn.Add(variable);
            }

            return photoReturn;
        }
    }


}