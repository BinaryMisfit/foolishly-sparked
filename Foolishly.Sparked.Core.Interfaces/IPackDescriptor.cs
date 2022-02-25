using System.IO.Abstractions;

namespace Foolishly.Sparked.Core;

public interface IPackDescriptor
{
    /// <summary>
    ///     The pack identifier.
    /// </summary>
    string PackId { get; }

    /// <summary>
    ///     The name of the pack.
    /// </summary>
    string? PackName { get; }

    /// <summary>
    ///     The <see cref="PackTypes" />.
    /// </summary>
    PackTypes PackTypes { get; }

    /// <summary>
    ///     The pack type identifier.
    /// </summary>
    int PackTypeId { get; }

    /// <summary>
    ///     The <see cref="System.IO.Abstractions.IDirectoryInfo" /> where the pack is installed.
    /// </summary>
    IDirectoryInfo? Path { get; set; }

    /// <summary>
    ///     The <see cref="Foolishly.Sparked.Core.IPackageCollection" />.
    /// </summary>
    IPackageCollection Collections { get; set; }
}
