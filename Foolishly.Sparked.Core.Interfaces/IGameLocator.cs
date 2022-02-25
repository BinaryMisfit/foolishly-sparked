namespace Foolishly.Sparked.Core;

public interface IGameLocator : IGameInstance
{
    IGameInstance LocateGame();
}
