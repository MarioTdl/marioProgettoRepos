using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using marioProgetto.Controllers.Resource;
using marioProgetto.Models;
using marioProgetto.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace marioProgetto.Controllers
{
    public class FeatureController: Controller
    {
        private readonly MarioProgettoDbContext _dbContext;
        private readonly IMapper _mapper;
        public FeatureController(MarioProgettoDbContext _db,IMapper map)
        {
            _dbContext = _db;
            _mapper=map;
        }

        [HttpGet("/api/features")]
        public async Task<IEnumerable<KeyValuePairResource>> GetFeature()
        {
            var features = await _dbContext.Features.ToListAsync();
            
            return _mapper.Map<List<Feature>,List<KeyValuePairResource>>(features);
        }
        
    }
}