using InclinometryEditorBackend.Entities;
using Microsoft.EntityFrameworkCore;

namespace InclinometryEditorBackend
{
    public class InclinometryDBContext : DbContext
    {
        public InclinometryDBContext()
        {}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=InclinometryDB;Username=postgres;Password=1234");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
        public DbSet<WellEntity> Wells { get; set; }
        public DbSet<WellDataEntity> WellDatas { get; set; }
    }
}
