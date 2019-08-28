using System;
using System.Threading.Tasks;
using AutoMapper;
using marioProgetto.Controllers.Resource;
using marioProgetto.Models;
using marioProgetto.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace marioProgetto.Controllers
{
    [Route("/api/vehicles")]
    public class VeichleController : Controller
    {
        private readonly IMapper _mapper;
        private readonly MarioProgettoDbContext _context;
        private readonly IVehicleRepository _repository;
        public VeichleController(IMapper mapper, MarioProgettoDbContext db, IVehicleRepository repository)
        {
            _mapper = mapper;
            _context = db;
            _repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            var vehicle = await _repository.GetVeichle(id);
            if (vehicle == null)
                return NotFound();

            var veichleResource = _mapper.Map<Veichle, VeichleResource>(vehicle);

            return Ok(veichleResource);
        }

        [HttpPost]
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

            _context.Veichles.Add(veichle);
            await _context.SaveChangesAsync();

            veichle = await _repository.GetVeichle(veichle.Id);

            var resourceCreate = _mapper.Map<Veichle, VeichleResource>(veichle);

            return Ok(resourceCreate);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVeichle(int id, [FromBody] SaveVehicleResource veichleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var veichle = await _repository.GetVeichle(id);

            if (veichle == null)
                return NotFound();

            _mapper.Map<SaveVehicleResource, Veichle>(veichleResource, veichle);
            veichle.LastUpdate = DateTime.Now;

            await _context.SaveChangesAsync();

            var resourceCreate = _mapper.Map<Veichle, VeichleResource>(veichle);

            return Ok(resourceCreate);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle = await _context.Veichles.FindAsync(id);

            if (vehicle == null)
                return NotFound();

            _context.Remove(vehicle);
            await _context.SaveChangesAsync();

            return Ok(id);
        }
    }
}