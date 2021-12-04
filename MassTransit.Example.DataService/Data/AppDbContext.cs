using MassTransit.Example.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MassTransit.Example.DataService.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public virtual DbSet<UserEntity>? Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) :
            base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity.HasIndex(e => e.Username).IsUnique();
            });
        }
    }
}