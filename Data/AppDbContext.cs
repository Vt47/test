using Microsoft.EntityFrameworkCore;
using smoking.Models;

namespace smoking.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Member> Member { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Phase> Phases { get; set; }
        public DbSet<Plan_detail> Plan_details { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Account>().ToTable("Account");
            modelBuilder.Entity<Member>().ToTable("Member");
            modelBuilder.Entity<Plan>().ToTable("Plan");
            modelBuilder.Entity<Phase>().ToTable("Phase");
            modelBuilder.Entity<Plan_detail>().ToTable("Plan_detail");
            // Nếu có các entity khác, map tương tự
        }
    }
} 