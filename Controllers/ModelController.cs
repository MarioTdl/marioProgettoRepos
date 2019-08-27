using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using marioProgetto.Controllers.Resource;
using marioProgetto.Models;
using marioProgetto.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace marioProgetto.Controllers
{
    public class ModelController: Controller
    {
        private readonly MarioProgettoDbContext _dbContext;
        private readonly IMapper _mapper;
        public ModelController(MarioProgettoDbContext _db,IMapper map)
        {
            _dbContext = _db;
            _mapper=map;
        }

        [HttpGet("/api/model/{id}")]
        public async Task<IEnumerable<ModelResource>> GetMakes([FromRoute]int id)
        {
            Make makeFakeId = _dbContext.Makes.Where(x=>x.Id==id).FirstOrDefault();
            int makeId= makeFakeId.Id;
            var model= await _dbContext.Models.Where(x=>x.MakeId==makeId).ToListAsync();
                    
            return _mapper.Map<List<Model>,List<ModelResource>>(model);
        }
    
        
    }
}