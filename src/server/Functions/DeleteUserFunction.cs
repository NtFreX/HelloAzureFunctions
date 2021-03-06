using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NtFreX.HelloAzureFunctions.Repositories;
using System;
using System.Threading.Tasks;
using Aliencube.AzureFunctions.Extensions.OpenApi.Attributes;

namespace NtFreX.HelloAzureFunctions.Functions
{
    public class DeleteUserFunction
    {
        private readonly UserRepository _userRepository;

        public DeleteUserFunction(UserRepository userRepository) {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        [FunctionName("DeleteUserFunction")]
        [OpenApiOperation("delete", "user")]
        [OpenApiParameter("id")]
        public async Task<IActionResult> Get(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "user/{id}")] HttpRequest req,
            Guid id,
            ILogger log)
        {
            log.LogInformation($"{nameof(DeleteUserFunction)} is running");

            try {
                await _userRepository.DeleteByIdAsync(id);
                return new OkResult();
            } catch (Exception ex) {
                return ErrorHandler.Handle(ex, log);
            }
        }
    }
}
