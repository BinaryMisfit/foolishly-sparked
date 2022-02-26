namespace Foolishly.Sparked.Core;

public interface IGame
{
    string InstalledPath { get; }

    string Platform { get; }

    string Version { get; }

    IPackageCollection? Base { get; }

    IPackCollection? Packs { get; }
}
