using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Foolishly.Sparked.Core;

public class GameBuilder : IGameBuilder
{
    private readonly IServiceCollection _services;

    public GameBuilder()
    {
        _services = new ServiceCollection();
        _services.ConfigureSims();
        _services.AddApiGame();
    }

    public IServiceProvider CreateProvider()
    {
        return _services.BuildServiceProvider();
    }

    public IGameBuilder UseConfiguration(IConfiguration configuration)
    {
        _services.AddOptions<GameOptions>()
            .Bind(configuration)
            .ValidateDataAnnotations();
        return this;
    }
}
