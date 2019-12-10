using System;
using Microsoft.EntityFrameworkCore;
using NtFreX.HelloAzureFunctions.Entities;

namespace NtFreX.HelloAzureFunctions.Repositories {
    public class CosmosDbContext : DbContext {
        public const string DatabaseName = "HelloFunctions";
        public const string ContainerName = "Container";

        private readonly string _connectionKey;
        private readonly string _connectionEndpoint;

        public DbSet<UserEntity> Users;

        public CosmosDbContext(){
            _connectionKey =  Environment.GetEnvironmentVariable("HelloAzureFunctionsCosmosDbKey", EnvironmentVariableTarget.Process);
            _connectionEndpoint =  Environment.GetEnvironmentVariable("HelloAzureFunctionsCosmosDbEndpoint", EnvironmentVariableTarget.Process);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseCosmos(
                    _connectionEndpoint,
                    _connectionKey,
                    databaseName: DatabaseName);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultContainer(ContainerName);
            modelBuilder.Entity<UserEntity>().ToContainer("Users");
        }
    }
}