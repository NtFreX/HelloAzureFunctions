using System;
using System.Linq;
using System.Threading.Tasks;
using NtFreX.HelloAzureFunctions.Entities;

namespace NtFreX.HelloAzureFunctions.Repositories {
    public class UserRepository {
        private readonly CosmosDbContext _dbContext;

        public UserRepository(CosmosDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public UserEntity[] GetUsers() {
            return _dbContext.Users.ToArray();
        }

        public async Task DeleteByIdAsync(Guid id) {
            var entity = _dbContext.Users.Find(id);
            _dbContext.Users.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddUserAsync(UserEntity user) { 
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}