﻿using Microsoft.Extensions.DependencyInjection;

namespace Sims.Toolkit.Plugin.Manager;

/// <summary>
///     Extends <see cref="IServiceCollection" /> to inject DI interfaces.
/// </summary>
public static class DependencyInjectionExtension
{
    /// <summary>
    ///     Configures the DI container.
    /// </summary>
    /// <param name="services">Instance of <see cref="IServiceCollection" />.</param>
    /// <returns>A populated instance of <see cref="IServiceCollection" />.</returns>
    public static IServiceCollection AddPluginManager(this IServiceCollection services)
    {
        return services;
    }
}
