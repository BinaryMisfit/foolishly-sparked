using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace Foolishly.Sparked.Core;

public class GameBuilder : IGameBuilder
{
    private readonly IServiceCollection _services;
    private Dictionary<string, string>? _options;

    public GameBuilder()
    {
        _services = new ServiceCollection();
    }

    public IServiceProvider CreateProvider()
    {
        return _services.BuildServiceProvider();
    }

    public IGameBuilder Configure()
    {
        if (_options != null)
        {
            _services.ConfigureSparked(_options);
        }

        _services.AddGame();
        return this;
    }

    public IGameBuilder WithOptions(Dictionary<string, string> options)
    {
        _options = options;
        return this;
    }

    public static IGameBuilder CreateDefaultGameBuilder()
    {
        return new GameBuilder();
    }
}
