using System.IO.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Sims.Toolkit.Api.Core;
using Sims.Toolkit.Api.Core.Interfaces;
using Sims.Toolkit.Api.Helpers;
using Sims.Toolkit.Api.Helpers.Interfaces;

namespace Sims.Toolkit.Api;

/// <summary>
///     Extends <see cref="IServiceCollection" /> to allow DI for the Api."/>
/// </summary>
public static class ServiceExtension
{
    /// <summary>
    ///     Configures the DI container for the Sims Api Toolkit.
    /// </summary>
    /// <param name="services">Instance of <see cref="IServiceCollection" />.</param>
    /// <returns>A populated instance of <see cref="IServiceCollection" />.</returns>
    public static IServiceCollection AddSimsToolkitApi(this IServiceCollection services)
    {
        services.AddScoped<IFileSystem, FileSystem>();
        services.AddScoped<IGameLoader, GameLoader>();
        services.AddScoped<IPackage, Package>();
        return services;
    }
}
