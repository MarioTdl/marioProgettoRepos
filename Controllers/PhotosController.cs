using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using marioProgetto.Core;
using marioProgettoRepos.Controllers.Resource;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly PhotoSettings _options;
        public PhotosController(IHostingEnvironment host, IVehicleRepository repository,
         IUnitOfWork unitOfWork, IMapper mapper, IOptionsSnapshot<PhotoSettings> options)
        {
            _options = options.Value;
            _host = host;
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
            //se non e presente la directory la crea
            if (!Directory.Exists(uploadsFolderPath))
                Directory.CreateDirectory(uploadsFolderPath);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            var photo = new Photo { FileName = fileName };
            veichle.Photos.Add(photo);
            await _unitOfWork.CompleteAsync();

            return Ok(_mapper.Map<Photo, PhotoResource>(photo));
        }
    }
}