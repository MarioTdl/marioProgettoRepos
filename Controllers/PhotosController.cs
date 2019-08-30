using System;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using marioProgetto.Core;
using marioProgettoRepos.Controllers.Resource;
using marioProgettoRepos.Core.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace marioProgettoRepos.Controllers
{
    [Route("/api/vehicles/{veichlesId}/photos")]
    public class PhotosController : Controller
    {
        private readonly IHostingEnvironment _host;
        private readonly IVehicleRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
         private readonly  IMapper _mapper;
        public PhotosController(IHostingEnvironment host, IVehicleRepository repository,IUnitOfWork unitOfWork,IMapper mapper)
        {
            _host = host;
            _repository = repository;
            _unitOfWork=unitOfWork;
            _mapper=mapper;
        }
        [HttpPost]
        public async Task<IActionResult> Upload(int vehicleId, IFormFile file ,IMapper mapper)
        {
            var veichle = await _repository.GetVeichle(vehicleId, includeResource: false);
            if (veichle == null)
                return NotFound();

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

            return Ok(_mapper.Map<Photo,PhotoResource>(photo)); 
        }
    }
}