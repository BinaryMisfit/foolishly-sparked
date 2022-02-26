using System;

namespace Foolishly.Sparked.Core;

public class Game : IGame, IGameInternals
{
    public Game(IGameInstance instance)
    {
        InstalledPath = instance.InstallPath;
        Version = instance.Version;
        Platform = instance.Platform switch
        {
            PlatformID.Win32NT => "Windows",
            PlatformID.MacOSX => "MacOSX",
            _ => "Not Supported"
        };
    }

    public string InstalledPath { get; }

    public string Platform { get; }

    public string Version { get; }

    public IPackageCollection? Base { get; private set; }

    public IPackCollection? Packs { get; private set; }

    void IGameInternals.AddBaseGame(IPackageCollection package)
    {
        Base = package;
    }

    void IGameInternals.AddGamePacks(IPackCollection packs)
    {
        Packs = packs;
    }
}
