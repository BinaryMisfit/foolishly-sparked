using Microsoft.Extensions.DependencyInjection;

namespace Sims.Core;

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
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        return services;
    }
}
