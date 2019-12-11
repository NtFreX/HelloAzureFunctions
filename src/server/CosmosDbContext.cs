using Microsoft.EntityFrameworkCore;
using NtFreX.HelloAzureFunctions.Entities;

namespace NtFreX.HelloAzureFunctions.Repositories {
    public class CosmosDbContext : DbContext {

        public DbSet<UserEntity> Users { get; set; }

        public CosmosDbContext(DbContextOptions<CosmosDbContext> options)
            :base(options) { 
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<UserEntity>()
                .ToContainer("Users");
        }
    }
}