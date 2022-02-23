namespace Sims.Api.Core;

internal interface IGameLocator : IGameInstance
{
    IGameInstance LocateGame();

    IGameInstance LocateGamePacks();
}
