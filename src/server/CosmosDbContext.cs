using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NtFreX.HelloAzureFunctions.Entities;

namespace NtFreX.HelloAzureFunctions.Repositories {
    public class CosmosDbContext : DbContext {
        public const string DatabaseName = "HelloFunctions";
        public const string ContainerName = "Container";

        private readonly string _connectionKey;
        private readonly string _connectionEndpoint;
        private readonly ILogger _logger;

        public DbSet<UserEntity> Users;

        public CosmosDbContext(ILogger logger) {
            _logger = logger;

            // TODO: use key value references
            _connectionKey =  Environment.GetEnvironmentVariable("CosmosDbKey", EnvironmentVariableTarget.Process);
            _connectionEndpoint =  Environment.GetEnvironmentVariable("CosmosDbEndpoint", EnvironmentVariableTarget.Process);

            _logger.LogInformation($"CosmosDbEndpoint = {_connectionEndpoint}");
            _logger.LogInformation($"CosmosDbKey = {(string.IsNullOrEmpty(_connectionKey) ? "" : "***")}");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseCosmos(
                    _connectionEndpoint,
                    _connectionKey,
                    databaseName: DatabaseName);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _logger.LogInformation($"{nameof(CosmosDbContext)}.{nameof(OnModelCreating)} has been called");

            modelBuilder.HasDefaultContainer(ContainerName);
            modelBuilder.Entity<UserEntity>().ToContainer("Users");
        }
    }
}