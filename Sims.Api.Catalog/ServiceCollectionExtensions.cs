using Microsoft.Extensions.DependencyInjection;
using Sims.Core;

namespace Sims.Api.Catalog;

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
    public static IServiceCollection AddApiCatalog(this IServiceCollection services)
    {
        services.AddCore();
        return services;
    }
}
