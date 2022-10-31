using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Microgroove.CustomerApi.Function.Extensions
{
    public static class ConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddAppsettingsFile(this IConfigurationBuilder configurationBuilder,
            FunctionsHostBuilderContext context,
            bool useEnvironment = false)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            var environmentSection = string.Empty;

            if (useEnvironment)
            {
                environmentSection = $".{context.EnvironmentName}";
            }

            configurationBuilder.AddJsonFile(
                path: Path.Combine(context.ApplicationRootPath, $"appsettings{environmentSection}.json"),
                optional: true,
                reloadOnChange: false);

            return configurationBuilder;
        }
    }
}
