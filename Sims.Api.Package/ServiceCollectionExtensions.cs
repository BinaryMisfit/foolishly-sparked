﻿using Foolishly.Sparked.Core.Catalog;
using Microsoft.Extensions.DependencyInjection;

namespace Sims.Api.Package;

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
    public static IServiceCollection AddApiPackage(this IServiceCollection services)
    {
        services.AddApiCatalog();
        return services;
    }
}
