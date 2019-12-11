using System;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NtFreX.HelloAzureFunctions.Repositories;

[assembly: FunctionsStartup(typeof(NtFreX.HelloAzureFunctions.Startup))]

namespace NtFreX.HelloAzureFunctions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddLogging();
            builder.Services.AddScoped<UserRepository>();

            ConfigureDatabase(builder.Services);
        }

        private void ConfigureDatabase(IServiceCollection services) {
            try {
                // TODO: use key value references
                var connectionKey =  Environment.GetEnvironmentVariable("CosmosDbKey", EnvironmentVariableTarget.Process);
                var connectionEndpoint =  Environment.GetEnvironmentVariable("CosmosDbEndpoint", EnvironmentVariableTarget.Process);
                var databaseName = "HelloFunctions";

                services.AddDbContext<CosmosDbContext>(options => options.UseCosmos(connectionEndpoint, connectionKey, databaseName));
            } catch (Exception ex) {
                throw new Exception("Configuring the database failed", ex);
            }
        }
    }
}