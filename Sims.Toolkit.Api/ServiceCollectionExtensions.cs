using System.IO.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sims.Toolkit.Config;
using Sims.Toolkit.Plugin.Manager;

namespace Sims.Toolkit.Api;

/// <summary>
///     Extends <see cref="IServiceCollection" /> to inject DI interfaces.
/// </summary>
public static class DependencyExtensions
{
    /// <summary>
    ///     Configures the DI container.
    /// </summary>
    /// <param name="services">Instance of <see cref="IServiceCollection" />.</param>
    /// <returns>A populated instance of <see cref="IServiceCollection" />.</returns>
    public static IServiceCollection AddSimsToolkitApi(this IServiceCollection services)
    {
        var configuration = GetApiConfiguration();
        services.Configure<GameInstanceOptions>(_ => configuration.GetSection("GameInstanceOptions"));
        services.AddScoped<IFileSystem, FileSystem>()
            .AddScoped<IGameInstance, GameInstance>()
            .AddPluginManager();
        return services;
    }

    private static IConfiguration GetApiConfiguration()
    {
        var builder = new ConfigurationBuilder().AddJsonFile("toolkit-settings.json", true);
        return builder.Build();
    }
}
