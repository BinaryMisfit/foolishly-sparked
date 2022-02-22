using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Sims.Toolkit.Config;

/// <summary>
///     Extends <see cref="IServiceCollection" /> to inject DI interfaces.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    ///     Configures the DI container.
    /// </summary>
    /// <param name="services">Instance of <see cref="IServiceCollection" />.</param>
    /// <returns>A populated instance of <see cref="IServiceCollection" />.</returns>
    public static IServiceCollection ConfigureSimsToolkit(this IServiceCollection services)
    {
        var configuration = GetApiConfiguration();
        services.AddOptions<GameInstanceOptions>()
            .Bind(configuration.GetSection(GameInstanceOptions.ConfigurationSectionName))
            .ValidateDataAnnotations();
        return services;
    }

    private static IConfiguration GetApiConfiguration()
    {
        var builder = new ConfigurationBuilder().AddJsonFile("toolkit-settings.json", true);
        return builder.Build();
    }
}
