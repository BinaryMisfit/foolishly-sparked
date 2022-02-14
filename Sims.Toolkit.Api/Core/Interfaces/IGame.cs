using JetBrains.Annotations;

namespace Sims.Toolkit.Api.Core.Interfaces;

[PublicAPI]
public interface IGame
{
    string Path { get; }

    PackCollection Packs { get; }

    string Platform { get; }
}
