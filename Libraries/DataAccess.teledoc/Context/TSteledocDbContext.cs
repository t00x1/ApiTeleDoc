using Microsoft.EntityFrameworkCore;
using Domain.Models;

namespace DataAccess.Context
{
    public class TSteledocDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Founder> Founders { get; set; }

      
        public TSteledocDbContext(DbContextOptions<TSteledocDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Client>()
                .HasMany(c => c.Founders)
                .WithOne(f => f.Client)
                .HasForeignKey(f => f.ClientINN)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}