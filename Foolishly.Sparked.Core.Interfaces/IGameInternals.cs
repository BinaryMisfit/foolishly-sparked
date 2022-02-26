namespace Foolishly.Sparked.Core;

public interface IGameInternals
{
    void AddBaseGame(IPackageCollection package);

    void AddGamePacks(IPackCollection pack);
}
