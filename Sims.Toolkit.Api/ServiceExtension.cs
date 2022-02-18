using System.IO.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Sims.Toolkit.Api.Core;
using Sims.Toolkit.Api.Core.Interfaces;
using Sims.Toolkit.Api.Helpers;
using Sims.Toolkit.Api.Helpers.Interfaces;
using Sims.Toolkit.Plugin.Manager;

namespace Sims.Toolkit.Api;

/// <summary>
///     Extends <see cref="IServiceCollection" /> to inject DI interfaces.
/// </summary>
public static class ServiceExtension
{
    /// <summary>
    ///     Configures the DI container.
    /// </summary>
    /// <param name="services">Instance of <see cref="IServiceCollection" />.</param>
    /// <returns>A populated instance of <see cref="IServiceCollection" />.</returns>
    public static IServiceCollection AddSimsToolkitApi(this IServiceCollection services)
    {
        return services.AddScoped<IFileSystem, FileSystem>()
            .AddScoped<IGameLoader, GameLoader>()
            .AddScoped<IPackage, Package>()
            .AddPluginManager();
    }
}
