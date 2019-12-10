using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NtFreX.HelloAzureFunctions.Repositories;
using NtFreX.HelloAzureFunctions.Extensions;
using System;

namespace NtFreX.HelloAzureFunctions.Functions
{
    public class GetUsersFunction
    {
        private readonly UserRepository _userRepository;

        public GetUsersFunction(UserRepository userRepository) {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        [FunctionName("GetUsersFunction")]
        public IActionResult Get(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "users")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation($"{nameof(GetUsersFunction)} is running");

            return new OkObjectResult(_userRepository.GetUsers());
        }
    }
}
