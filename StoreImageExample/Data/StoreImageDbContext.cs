using Microsoft.EntityFrameworkCore;
using StoreImageExample.Data.Configurations;
using StoreImageExample.Model;

namespace StoreImageExample.Data
{
    public class StoreImageDbContext : DbContext
    {
        public DbSet<Image> Images { get; set; }

        public StoreImageDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ImageConfiguration());
        }
    }
}
