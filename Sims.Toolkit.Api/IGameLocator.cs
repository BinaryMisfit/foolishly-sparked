namespace Sims.Toolkit.Api;

internal interface IGameLocator : IGameInstance
{
    IGameInstance LocateGame();

    IGameInstance LocateGamePacks();
}
