using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using marioProgetto.Core;
using marioProgetto.Models;
using marioProgettoRepos.Core.Models;
using marioProgettoRepos.Extensions;
using Microsoft.EntityFrameworkCore;

namespace marioProgetto.Persistence
{

    public class VehicleRepository : IVehicleRepository
    {
        private readonly MarioProgettoDbContext _context;
        public VehicleRepository(MarioProgettoDbContext context)
        {
            _context = context;
        }
        public async Task<QueryResult<Veichle>> GetVeichles(VeichleQuery queryObj)
        {
            var result = new QueryResult<Veichle>();
            var query = _context.Veichles
            .Include(v => v.Model)
            .ThenInclude(v => v.Make)
            .Include(v => v.Features)
            .ThenInclude(v => v.Feature)
            .AsQueryable();

            var columsMap = new Dictionary<string, Expression<Func<Veichle, object>>>()
            {
                ["make"] = v => v.Model.Make.Name,
                ["model"] = v => v.Model.Name,
                ["contactName"] = v => v.ContactName,
            };

            query = query.ApplyOrding(queryObj, columsMap);

            result.TotalItems = await query.CountAsync();

            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();

            return result;

        }
        public async Task<Veichle> GetVeichle(int id, bool includeResource = true)
        {
            if (!includeResource)
                return await _context.Veichles.FindAsync(id);

            return await _context.Veichles
           .Include(v => v.Features)
           .ThenInclude(vf => vf.Feature)
           .Include(i => i.Model)
           .ThenInclude(m => m.Make)
           .SingleOrDefaultAsync(p => p.Id == id);
        }
        public void Add(Veichle veichle)
        {
            _context.Veichles.Add(veichle);
        }
        public void Remove(Veichle veichle)
        {
            _context.Veichles.Remove(veichle);
        }

    }
}