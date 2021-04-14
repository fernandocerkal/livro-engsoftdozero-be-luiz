using Microsoft.EntityFrameworkCore;

namespace Botsuana.Covid19.Model
{
    public class EngSoftDoZeroDBContext : DbContext
    {
         public EngSoftDoZeroDBContext(DbContextOptions<EngSoftDoZeroDBContext> options) : base(options) { 
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<Dose> Dose { get; set; }
        public DbSet<Vacinado> Vacinado { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            modelBuilder.UseIdentityColumns();            
            modelBuilder.Entity<Dose>().HasKey(b => b.Identificador);
            modelBuilder.Entity<Vacinado>().HasKey(b => b.Identificador);
            modelBuilder.Entity<Vacinado>().HasIndex(b => b.CPF);

            base.OnModelCreating(modelBuilder); 
        }
    }
}