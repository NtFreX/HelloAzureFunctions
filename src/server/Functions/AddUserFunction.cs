
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NtFreX.HelloAzureFunctions.Entities;
using NtFreX.HelloAzureFunctions.Repositories;

namespace NtFreX.HelloAzureFunctions.Functions
{
    public class AddUserFunction
    {
        private readonly UserRepository _userRepository;

        public AddUserFunction(UserRepository userRepository) {
            _userRepository = userRepository;
        }

        [FunctionName("AddUserFunction")]
        public async Task<IActionResult> Get(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation($"{nameof(AddUserFunction)} is running");

            var reader = new StreamReader(req.Body);
            var body = await reader.ReadToEndAsync();
            var user = JsonConvert.DeserializeObject<UserEntity>(body);

            await _userRepository.AddUserAsync(user);
            return new OkResult();
        }
    }
}
