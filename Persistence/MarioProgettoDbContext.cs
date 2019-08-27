using marioProgetto.Models;
using Microsoft.EntityFrameworkCore;

namespace marioProgetto.Persistence
{
    public class MarioProgettoDbContext : DbContext
    {
        public MarioProgettoDbContext (DbContextOptions<MarioProgettoDbContext> options)
         :base(options)
        {

        }
        public DbSet<Make> Makes { get; set; }
        public DbSet<Model> Models { get; set; }
    }
}