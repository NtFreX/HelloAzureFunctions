using System;
using System.Threading.Tasks;
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
            SetupDatabase(builder.Services.BuildServiceProvider()).GetAwaiter().GetResult();
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

        private async Task SetupDatabase(ServiceProvider provider) {
            try {
                using(var context = provider.GetRequiredService<CosmosDbContext>()) {
                    await context.Database.EnsureCreatedAsync();
                }
            } catch (Exception ex) {
                throw new Exception("Setting up the database failed", ex);
            }
        }
    }
}