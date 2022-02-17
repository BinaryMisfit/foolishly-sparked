using JetBrains.Annotations;

namespace Sims.Toolkit.Api.Enums;

/// <summary>
///     Identifies the different type of game packs.
/// </summary>
[PublicAPI]
public enum PackType
{
    /// <summary>
    ///     Base Game Indicator.
    /// </summary>
    BaseGame = 1,

    /// <summary>
    ///     Expansion Pack Indicator.
    /// </summary>
    Expansion = 2,

    /// <summary>
    ///     Unknown Pack Error Indicator.
    /// </summary>
    Error = -1,

    /// <summary>
    ///     Free Pack Indicator.
    /// </summary>
    Free = 6,

    /// <summary>
    ///     Game Pack Indicator.
    /// </summary>
    Game = 3,

    /// <summary>
    ///     Kit Pack Indicator.
    /// </summary>
    Kit = 5,

    /// <summary>
    ///     Stuff Pack Indicator.
    /// </summary>
    Stuff = 4,

    /// <summary>
    ///     New or Unknown Pack Indicator.
    /// </summary>
    Unknown = 0
}
