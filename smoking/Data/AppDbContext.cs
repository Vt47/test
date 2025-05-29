using Microsoft.EntityFrameworkCore;
using smoking.Models;

namespace smoking.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Member> Member { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Account> Account { get; set; }
        public DbSet<Plan> Plan { get; set; }
        public DbSet<Phase> Phase { get; set; }
        public DbSet<Plan_detail> Plan_detail { get; set; }

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