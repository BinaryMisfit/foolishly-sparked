using Sims.Toolkit.Api.Core.Interfaces;

namespace Sims.Toolkit.Api.Core;

public class Game : IGame
{
    public Game(string path, string platform, PackCollection packs)
    {
        Packs = packs;
        Path = path;
        Platform = platform;
    }

    public string Path { get; }

    public PackCollection Packs { get; }

    public string Platform { get; }
}
