/// Author: Brian Haynes

using TransactionApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace TransactionApp.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<Transaction>().HasKey(t => t.Id);
        }
    }
}
