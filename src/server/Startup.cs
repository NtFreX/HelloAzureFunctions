using System;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using NtFreX.HelloAzureFunctions.Repositories;

[assembly: FunctionsStartup(typeof(NtFreX.HelloAzureFunctions.Startup))]

namespace NtFreX.HelloAzureFunctions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
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
                throw new Exception("Setting up the database failed", ex);
            }  
        }
    }
}