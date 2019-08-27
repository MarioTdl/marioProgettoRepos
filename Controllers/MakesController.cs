using System.Collections.Generic;
using System.Threading.Tasks;
using marioProgetto.Models;
using marioProgetto.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace marioProgetto.Controllers
{
    public class MakesController : Controller
    {
        private readonly MarioProgettoDbContext _dbContext;
        public MakesController(MarioProgettoDbContext _db)
        {
            _dbContext = _db;
        }

        [HttpGet("/api/makes")]
        public async Task<IEnumerable<Make>> GetMakes()
        {
            return await _dbContext.Makes.Include(m => m.Models).ToListAsync();
        }

        
    }
}