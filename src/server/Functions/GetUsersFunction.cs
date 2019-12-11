using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NtFreX.HelloAzureFunctions.Repositories;
using System;
using Aliencube.AzureFunctions.Extensions.OpenApi.Attributes;
using System.Net;
using NtFreX.HelloAzureFunctions.Entities;

namespace NtFreX.HelloAzureFunctions.Functions
{
    public class GetUsersFunction
    {
        private readonly UserRepository _userRepository;

        public GetUsersFunction(UserRepository userRepository) {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        [FunctionName("GetUsersFunction")]
        [OpenApiOperation("get", "users")]
        [OpenApiResponseBody(HttpStatusCode.OK, "application/json", typeof(UserEntity[]))]
        public IActionResult Get(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "users")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation($"{nameof(GetUsersFunction)} is running");

            try {
                return new OkObjectResult(_userRepository.GetUsers());
            } catch (Exception ex) {
                return ErrorHandler.Handle(ex, log);
            }
        }
    }
}
