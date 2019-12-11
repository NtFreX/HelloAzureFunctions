using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Aliencube.AzureFunctions.Extensions.OpenApi;
using Aliencube.AzureFunctions.Extensions.OpenApi.Attributes;
using Aliencube.AzureFunctions.Extensions.OpenApi.Configurations;
using Aliencube.AzureFunctions.Extensions.OpenApi.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi;
using Newtonsoft.Json.Serialization;

namespace NtFreX.HelloAzureFunctions {
    public static class SwaggerFunctions {
        private const string ApiPrefix = "api";

        [FunctionName(nameof(RenderSwaggerDocument))]
        [OpenApiIgnore]
        public static async Task<IActionResult> RenderSwaggerDocument(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "swagger.json")] HttpRequest req,
            ILogger log)
        {
            var filter = new RouteConstraintFilter();
            var helper = new DocumentHelper(filter);
            var document = new Document(helper);
            
            var result = await document
                .InitialiseDocument()
                .AddServer(req, ApiPrefix)
                .Build(Assembly.GetExecutingAssembly(), new CamelCaseNamingStrategy())
                .RenderAsync(OpenApiSpecVersion.OpenApi2_0, OpenApiFormat.Json)
                .ConfigureAwait(false);

            var response = new ContentResult()
            {
                Content = result,
                ContentType = "application/json",
                StatusCode = (int)HttpStatusCode.OK
            };

            return response;
        }

        [FunctionName(nameof(RenderSwaggerUI))]
        [OpenApiIgnore]
        public static async Task<IActionResult> RenderSwaggerUI(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "swagger/ui")] HttpRequest req,
            ILogger log)
        {
            var ui = new SwaggerUI();
            var result = await ui
                .AddServer(req, ApiPrefix)
                .BuildAsync()
                .RenderAsync("swagger.json")
                .ConfigureAwait(false);
            
            var response = new ContentResult()
            {
                Content = result,
                ContentType = "text/html",
                StatusCode = (int)HttpStatusCode.OK
            };

            return response;
        }
    }
}