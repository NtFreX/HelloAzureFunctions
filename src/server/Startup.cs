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

            /*var provider = builder.Services.BuildServiceProvider();
            using(var context = provider.GetRequiredService<CosmosDbContext>()) {
                context.Database.EnsureCreatedAsync().GetAwaiter().GetResult();
            }*/
        }
    }
}