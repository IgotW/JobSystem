using Microsoft.EntityFrameworkCore;

namespace JobSystem.Entities
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        { 

        }
        public DbSet<CandidateAccount> CandidateAccounts { get; set; }
        public DbSet<CompanyAccount> CompanyAccounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
