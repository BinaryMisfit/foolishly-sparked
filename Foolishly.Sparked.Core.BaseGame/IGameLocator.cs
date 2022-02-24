namespace Foolishly.Sparked.Core;

internal interface IGameLocator : IGameInstance
{
    IGameInstance LocateGame();

    IGameInstance LocateGamePacks();
}
