using System.IO.Abstractions;
using JetBrains.Annotations;
using Sims.Toolkit.Api.Enums;

namespace Sims.Toolkit.Api.Core.Interfaces;

[PublicAPI]
public interface IPack
{
    string PackId { get; }

    string PackName { get; }

    PackType PackType { get; }

    int PackTypeId { get; }

    IDirectoryInfo? Path { get; set; }

    IPackContent Contents { get; set; }
}
