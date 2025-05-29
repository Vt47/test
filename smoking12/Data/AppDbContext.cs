using Microsoft.EntityFrameworkCore;
using smoking12.Models;

namespace smoking12.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<PackageMembership> PackageMemberships { get; set; }

        public DbSet<Ranking> Ranking { get; set; }
        public DbSet<Member> Member { get; set; }
        public DbSet<Account> Account { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Account ↔ User (1-1)
            modelBuilder.Entity<Account>()
                .HasOne(a => a.User)
                .WithOne(u => u.Account)
                .HasForeignKey<User>(u => u.Account_ID);

            // Account ↔ Member (1-1)
            modelBuilder.Entity<Account>()
                .HasOne(a => a.Member)
                .WithOne(m => m.Account)
                .HasForeignKey<Member>(m => m.AccountId);

            // Member ↔ Ranking (1-1)
            modelBuilder.Entity<Member>()
                .HasOne(m => m.Ranking)
                .WithOne(r => r.Member)
                .HasForeignKey<Ranking>(r => r.Member_ID);
        }
    }
}
