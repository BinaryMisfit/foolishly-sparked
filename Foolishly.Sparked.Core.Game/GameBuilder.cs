using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;

namespace Foolishly.Sparked.Core;

public class GameBuilder : IGameBuilder
{
    private readonly IServiceCollection _services;
    private IDictionary<string, string>? _options;
    private IServiceProvider? _provider;

    public GameBuilder()
    {
        _services = new ServiceCollection();
    }

    public IGame Build()
    {
        if (_provider == null)
        {
            throw new ValidationException("Builder not configured.");
        }

        var instance = _provider?.GetService<IGameInstance>();
        if (instance == null)
        {
            throw new NotImplementedException(nameof(IGameInstance));
        }

        (instance as IGameLocator)?.LocateGame();
        return new Game(instance);
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
        _provider = _services.BuildServiceProvider();
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
