using marioProgetto.Models;
using marioProgettoRepos.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace marioProgetto.Persistence
{
    public class MarioProgettoDbContext : DbContext
    {
        public DbSet<Make> Makes { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Veichle> Veichles { get; set; }
         public DbSet<Photo> Photos { get; set; }
        public MarioProgettoDbContext (DbContextOptions<MarioProgettoDbContext> options)
         :base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VeichleFeature>().HasKey(vf=> new {vf.VeichleId,vf.FeatureId});
        }
    }
}