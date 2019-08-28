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
        public VeichleController(IMapper mapper, MarioProgettoDbContext db)
        {
            _mapper = mapper;
            _context = db;
        }

        [HttpPost]
        public async Task<IActionResult> CreateVeichle([FromBody] VehicleResource veichleResource)
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

            var veichle = _mapper.Map<VehicleResource, Veichle>(veichleResource);
            veichle.LastUpdate = DateTime.Now;

            _context.Veichles.Add(veichle);
            await _context.SaveChangesAsync();

            var resourceCreate = _mapper.Map<Veichle, VehicleResource>(veichle);

            return Ok(resourceCreate);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVeichle(int id, [FromBody] VehicleResource veichleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var veichle = await _context.Veichles.Include(v=> v.Features).SingleOrDefaultAsync(v=>v.Id==id);
            _mapper.Map<VehicleResource, Veichle>(veichleResource, veichle);
            veichle.LastUpdate = DateTime.Now;

            await _context.SaveChangesAsync();

            var resourceCreate = _mapper.Map<Veichle, VehicleResource>(veichle);

            return Ok(resourceCreate);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle=await _context.Veichles.FindAsync(id);
            _context.Remove(vehicle);
            await _context.SaveChangesAsync();

            return Ok(id);
        }
    }
}