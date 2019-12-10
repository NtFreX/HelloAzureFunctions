using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NtFreX.HelloAzureFunctions.Repositories;
using NtFreX.HelloAzureFunctions.Extensions;

namespace NtFreX.HelloAzureFunctions.Functions
{
    public class GetUsersFunction
    {
        private readonly UserRepository _userRepository;

        public GetUsersFunction(UserRepository userRepository) {
            _userRepository = userRepository;
        }

        [FunctionName("GetUsersFunction")]
        public async Task<IActionResult> Get(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "user")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation($"{nameof(GetUsersFunction)} is running");

            var users = await _userRepository.GetUsersAsync().ToListAsync();
            return new OkObjectResult(users);
        }
    }
}
