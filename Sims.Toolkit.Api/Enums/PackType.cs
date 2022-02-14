using JetBrains.Annotations;

namespace Sims.Toolkit.Api.Enums;

[PublicAPI]
public enum PackType
{
    BaseGame = 1,
    Expansion = 2,
    Error = -1,
    Free = 6,
    Game = 3,
    Kit = 5,
    Stuff = 4,
    Unknown = 0
}
