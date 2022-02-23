namespace Sims.Api.Game;

internal interface IGameLocator : IGameInstance
{
    IGameInstance LocateGame();

    IGameInstance LocateGamePacks();
}
