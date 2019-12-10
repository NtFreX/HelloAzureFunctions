using System;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NtFreX.HelloAzureFunctions.Repositories;

[assembly: FunctionsStartup(typeof(NtFreX.HelloAzureFunctions.Startup))]

namespace NtFreX.HelloAzureFunctions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddLogging();

            builder.Services.AddTransient<UserRepository>();
            builder.Services.AddTransient<CosmosDbContext>();

            TrySetupDatabase(builder.Services.BuildServiceProvider()).GetAwaiter().GetResult();
        }

        private async Task TrySetupDatabase(ServiceProvider provider) {
            try {
                using(var context = provider.GetRequiredService<CosmosDbContext>()) {
                    await context.Database.EnsureCreatedAsync();
                }    
            } catch (Exception ex) {
                var logger = provider.GetService<ILogger>();
                logger?.LogError(ex, "Setting up the database failed");
            }
        }
    }
}