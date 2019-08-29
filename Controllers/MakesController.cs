using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using marioProgetto.Controllers.Resource;
using marioProgetto.Models;
using marioProgetto.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace marioProgetto.Controllers
{
    public class MakesController : Controller
    {
        private readonly MarioProgettoDbContext _dbContext;
        private readonly IMapper _mapper;
        public MakesController(MarioProgettoDbContext _db,IMapper map)
        {
            _dbContext = _db;
            _mapper=map;
        }

        [HttpGet("/api/makes")]
        public async Task<IEnumerable<KeyValuePairResource>> GetMakes()
        {
            var makes = await _dbContext.Makes.Include(m => m.Models).ToListAsync();
            
            return _mapper.Map<List<Make>,List<KeyValuePairResource>>(makes);
        }
  
    }
}