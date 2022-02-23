namespace Sims.Api.Game;

/// <summary>
///     Specifies the contract for a game provider.
/// </summary>
public interface IGameProvider
{
    /// <summary>
    ///     Print game installation details..
    /// </summary>
    string PrintGameInfo();
}
