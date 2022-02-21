using System.IO.Abstractions;
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
    /// <param name="configureOptions">Instance of <see cref="ApiOptions" /></param>
    /// <returns>A populated instance of <see cref="IServiceCollection" />.</returns>
    public static IServiceCollection AddSimsToolkitApi(this IServiceCollection services, ApiOptions configureOptions)
    {
        services.AddOptions<ApiOptions>()
            .Configure(options => { options.Game = configureOptions.Game; });
        services.AddScoped<IFileSystem, FileSystem>()
            .AddScoped<IGameInstance, GameInstance>()
            .AddPluginManager();
        return services;
    }
}
