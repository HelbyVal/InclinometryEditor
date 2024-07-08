using InclinometryEditorBackend.Entities;
using Microsoft.EntityFrameworkCore;

namespace InclinometryEditorBackend
{
    public class InclinometryDBContext : DbContext
    {
        public InclinometryDBContext(DbContextOptions<InclinometryDBContext> op) : base(op)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=InclinometryDB;Username=postgre;Password=1234");
        }

        public DbSet<WellEntity> Wells { get; set; }
        public DbSet<WellDataEntity> WellDatas { get; set; }
    }
}
