using Microgroove.CustomerApi.AvatarsAccess.Connectors;
using Microgroove.CustomerApi.AvatarsAccess.Connectors.Impl;
using Microgroove.CustomerApi.AvatarsAccess.Settings;
using Microgroove.CustomerApi.BusinessLogic.Services;
using Microgroove.CustomerApi.BusinessLogic.Services.Impl;
using Microgroove.CustomerApi.DataAccess.Repositories;
using Microgroove.CustomerApi.DataAccess.Repositories.Impl;
using Microgroove.CustomerApi.Function;
using Microgroove.CustomerApi.Function.Extensions;
using Microgroove.CustomerApi.Infrastructure;
using Microgroove.CustomerApi.Infrastructure.Impl;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]
namespace Microgroove.CustomerApi.Function
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services
                .AddOptions<AvatarApiSettings>()
                .Configure<IConfiguration>((settings, configuration) =>
                    {
                        configuration.GetSection("AvatarApiSettings").Bind(settings);
                    });

            builder.Services.AddScoped(typeof(ICustomerService), typeof(CustomerService));
            builder.Services.AddScoped(typeof(ICustomerRepository), typeof(CustomerRepository));
            builder.Services.AddScoped(typeof(IAvatarConnector), typeof(AvatarConnector));
            builder.Services.AddScoped(typeof(IHttpClientWrapper<,>), typeof(HttpClientWrapper<,>));
        }

        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            FunctionsHostBuilderContext context = builder.GetContext();

            builder.ConfigurationBuilder
                .AddAppsettingsFile(context)
                .AddAppsettingsFile(context, useEnvironment: true)
                .AddEnvironmentVariables();

            base.ConfigureAppConfiguration(builder);
        }
    }
}
