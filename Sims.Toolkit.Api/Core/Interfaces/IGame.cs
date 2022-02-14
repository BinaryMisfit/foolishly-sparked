namespace Sims.Toolkit.Api.Core.Interfaces;

public interface IGame
{
    string Path { get; }

    PackCollection Packs { get; }

    string Platform { get; }
}
