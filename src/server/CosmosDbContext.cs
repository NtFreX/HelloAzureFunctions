using Microsoft.EntityFrameworkCore;
using NtFreX.HelloAzureFunctions.Entities;

namespace NtFreX.HelloAzureFunctions.Repositories {
    public class CosmosDbContext : DbContext {
        public const string ContainerName = "Container";

        private readonly string _connectionKey;
        private readonly string _connectionEndpoint;

        public DbSet<UserEntity> Users;

        public CosmosDbContext(DbContextOptions<CosmosDbContext> options)
            :base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultContainer(ContainerName);
            modelBuilder.Entity<UserEntity>().ToContainer("Users");
        }
    }
}