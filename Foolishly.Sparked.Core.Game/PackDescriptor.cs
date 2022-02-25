using System;
using System.IO.Abstractions;
using System.Linq;
using Foolishly.Sparked.Core.Properties;

namespace Foolishly.Sparked.Core;

/// <summary>
///     Descriptor of a game pack for the Sims.
/// </summary>
public class PackDescriptor : IPackDescriptor
{
    /// <summary>
    ///     Initializes an instance of a game pack.
    /// </summary>
    /// <param name="packId">The pack identifier.</param>
    public PackDescriptor(string packId)
    {
        PackId = packId;
        PackName = GamePacks.ResourceManager.GetString(packId) ?? GamePacks.EP00;
        PackTypeId = DetermineTypeId();
        PackTypes = DetermineType();
        Collections = new PackageCollection();
    }

    /// <summary>
    ///     The <see cref="PackTypes" />.
    /// </summary>
    public PackTypes PackTypes { get; }

    /// <summary>
    ///     The <see cref="System.IO.Abstractions.IDirectoryInfo" /> where the pack is installed.
    /// </summary>
    public IDirectoryInfo? Path { get; set; }

    /// <summary>
    ///     The <see cref="IPackageCollection" />.
    /// </summary>
    public IPackageCollection Collections { get; set; }

    /// <summary>
    ///     The pack identifier.
    /// </summary>
    public string PackId { get; }

    /// <summary>
    ///     The name of the pack.
    /// </summary>
    public string? PackName { get; }

    /// <summary>
    ///     The pack type identifier.
    /// </summary>
    public int PackTypeId { get; }

    private int DetermineTypeId()
    {
        var type = PackId[2..];
        if (int.TryParse(type, out var id))
        {
            return id;
        }

        return -1;
    }

    private PackTypes DetermineType()
    {
        var type = PackId[..2]
            .ToUpperInvariant();
        if (string.IsNullOrEmpty(type))
        {
            return PackTypes.Error;
        }

        var key = PackTypesMappings.PackTypeFolders.FirstOrDefault(
                folder => folder.Value.ToUpperInvariant()
                    .StartsWith(type, StringComparison.CurrentCulture))
            .Key;
        return key;
    }
}
