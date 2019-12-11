using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NtFreX.HelloAzureFunctions.Entities;

namespace NtFreX.HelloAzureFunctions.Repositories {
    public class UserRepository {
        private readonly CosmosDbContext _dbContext;

        public UserRepository(CosmosDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public UserEntity[] GetUsers(ILogger logger) {
            logger.LogInformation($"{nameof(UserRepository)}.{nameof(GetUsers)} has been called");
            logger.LogInformation($"Is DbContext.Users null = {_dbContext?.Users == null}");
            return _dbContext.Users.ToArray();
        }

        public async Task AddUserAsync(UserEntity user) { 
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}