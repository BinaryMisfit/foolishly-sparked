﻿using Microsoft.Extensions.DependencyInjection;
using Sims.Api.Package;

namespace Sims.Api.Game;

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
    public static IServiceCollection AddApiGame(this IServiceCollection services)
    {
        services.AddApiPackage();
        return services;
    }
}
