using System.Threading.Tasks;

namespace Sims.Toolkit.Api.Plugin.Interfaces.Meta;

/// <summary>
///     Represents information regarding the operating system platform.
/// </summary>
public interface IPlatform
{
    /// <summary>
    ///     Gets or sets an indicator whether the platform is 64-Bit.
    /// </summary>
    bool Is64 { get; }

    /// <summary>
    ///     Gets or sets the platform as per the operating system.
    /// </summary>
    string Platform { get; }

    /// <summary>
    ///     Gets or sets the installation path for the game instance.
    /// </summary>
    string InstalledPath { get; }

    /// <summary>
    ///     Asynchronously locate the currently installed instance of the game.
    /// </summary>
    /// <returns>Populated instance of <see cref="IPlatform" />.</returns>
    Task<IPlatform> LocateGameAsync();
}
