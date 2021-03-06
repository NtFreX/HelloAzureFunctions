
using System;
using System.IO;
using System.Threading.Tasks;
using Aliencube.AzureFunctions.Extensions.OpenApi.Attributes;
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
        [OpenApiOperation("add", "user")]
        [OpenApiRequestBody("application/json", typeof(UserEntity))]
        public async Task<IActionResult> Get(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "user")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation($"{nameof(AddUserFunction)} is running");

            try {
                var reader = new StreamReader(req.Body);
                var body = await reader.ReadToEndAsync();
                var user = JsonConvert.DeserializeObject<UserEntity>(body);

                await _userRepository.AddUserAsync(user);
                return new OkResult();
            } catch (Exception ex) {
                return ErrorHandler.Handle(ex, log);
            }
        }
    }
}
