using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Foolishly.Sparked.Core;

/// <summary>
///     Extends <see cref="IServiceCollection" /> to inject DI interfaces.
/// </summary>
public static class ServiceCollectionExtensions
{
    private const string configFile = "foolishly-sparked-options.json";

    /// <summary>
    ///     Configures the DI container.
    /// </summary>
    /// <param name="services">Instance of <see cref="IServiceCollection" />.</param>
    /// <returns>A populated instance of <see cref="IServiceCollection" />.</returns>
    public static IServiceCollection ConfigureSparked(this IServiceCollection services)
    {
        var configuration = new ConfigurationBuilder().AddJsonFile(configFile, true)
            .Build();
        services.AddOptions<CoreOptions>()
            .Bind(configuration.GetSection(CoreOptions.ConfigurationSectionName))
            .ValidateDataAnnotations();
        return services;
    }

    public static IServiceCollection ConfigureSparked(this IServiceCollection services, IDictionary<string, string> options)
    {
        var configuration = new ConfigurationBuilder().AddJsonFile(configFile, true)
            .AddInMemoryCollection(options)
            .Build();
        services.AddOptions<CoreOptions>()
            .Bind(configuration)
            .ValidateDataAnnotations();
        return services;
    }
}
