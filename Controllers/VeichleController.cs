using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using marioProgetto.Controllers.Resource;
using marioProgetto.Core;
using marioProgetto.Models;
using marioProgettoRepos.Controllers.Resource;
using marioProgettoRepos.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace marioProgetto.Controllers
{
    [Route("/api/vehicles")]
    public class VeichleController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IVehicleRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public VeichleController(IMapper mapper,
         IVehicleRepository repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<QueryResultResource<VeichleResource>> GetVeichles(VeichleQueryResource filterResource)
        {
            var filter = _mapper.Map<VeichleQueryResource, VeichleQuery>(filterResource);
            var queryResult = await _repository.GetVeichles(filter);

            return _mapper.Map<QueryResult<Veichle>, QueryResultResource<VeichleResource>>(queryResult);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetVehicle(int id)
        {
            var vehicle = await _repository.GetVeichle(id);
            if (vehicle == null)
                return NotFound();

            var veichleResource = _mapper.Map<Veichle, VeichleResource>(vehicle);

            return Ok(veichleResource);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateVeichle([FromBody] SaveVehicleResource veichleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //EXAMPLE MODEL STATE ERROR
            // var model = await _context.Models.FindAsync(veichleResource.Id);
            // if (model == null)
            // {
            //     ModelState.AddModelError("ModelId", "Invalid modelId");
            //     return BadRequest(ModelState);
            // }

            var veichle = _mapper.Map<SaveVehicleResource, Veichle>(veichleResource);
            veichle.LastUpdate = DateTime.Now;

            _repository.Add(veichle);
            await _unitOfWork.CompleteAsync();

            veichle = await _repository.GetVeichle(veichle.Id);

            var resourceCreate = _mapper.Map<Veichle, VeichleResource>(veichle);

            return Ok(resourceCreate);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateVeichle(int id, [FromBody] SaveVehicleResource veichleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var veichle = await _repository.GetVeichle(id);

            if (veichle == null)
                return NotFound();

            _mapper.Map<SaveVehicleResource, Veichle>(veichleResource, veichle);
            veichle.LastUpdate = DateTime.Now;

            await _unitOfWork.CompleteAsync();

            veichle = await _repository.GetVeichle(veichle.Id);
            var resourceCreate = _mapper.Map<Veichle, VeichleResource>(veichle);

            return Ok(resourceCreate);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle = await _repository.GetVeichle(id, includeResource: false);

            if (vehicle == null)
                return NotFound();

            _repository.Remove(vehicle);
            await _unitOfWork.CompleteAsync();

            return Ok(id);
        }
    }
}