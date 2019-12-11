using Microsoft.EntityFrameworkCore;
using NtFreX.HelloAzureFunctions.Entities;

namespace NtFreX.HelloAzureFunctions.Repositories {
    public class CosmosDbContext : DbContext {
        public const string ContainerName = "Container";

        public DbSet<UserEntity> Users { get; set; }

        public CosmosDbContext(DbContextOptions<CosmosDbContext> options)
            :base(options) { 
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultContainer(ContainerName);
            modelBuilder.Entity<UserEntity>().ToContainer("Users");
        }
    }
}