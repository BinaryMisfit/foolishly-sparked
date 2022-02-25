using System;
using Microsoft.Extensions.DependencyInjection;

namespace Foolishly.Sparked.Core;

public class Game : IGameInternals
{
    private IServiceProvider? _provider;

    public IPackCollection? Packs { get; set; }

    void IGameInternals.AddGameBuilder(IGameBuilder builder)
    {
        _provider = builder.CreateProvider();
    }

    public Game CreateFromLocalInstall()
    {
        var instance = _provider?.GetService<IGameInstance>();
        (instance as IGameLocator)?.LocateGame();
        return this;
    }
}
