using System;
using System.IO.Abstractions;
using System.Linq;
using Sims.Toolkit.Plugin.Properties;

namespace Sims.Toolkit.Api;

/// <summary>
///     Descriptor of a game pack for the Sims.
/// </summary>
public class PackDescriptor
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
        PackType = DetermineType();
        Collections = new PackageCollection();
    }

    /// <summary>
    ///     The pack identifier.
    /// </summary>
    public string PackId { get; }

    /// <summary>
    ///     The name of the pack.
    /// </summary>
    public string? PackName { get; }

    /// <summary>
    ///     The <see cref="PackType" />.
    /// </summary>
    public PackType PackType { get; }

    /// <summary>
    ///     The pack type identifier.
    /// </summary>
    public int PackTypeId { get; }

    /// <summary>
    ///     The <see cref="IDirectoryInfo" /> where the pack is installed.
    /// </summary>
    public IDirectoryInfo? Path { get; set; }

    /// <summary>
    ///     The <see cref="IPackageCollection" />.
    /// </summary>
    public IPackageCollection Collections { get; set; }

    private int DetermineTypeId()
    {
        var type = PackId[2..] ?? string.Empty;
        if (int.TryParse(type, out var id))
        {
            return id;
        }

        return -1;
    }

    private PackType DetermineType()
    {
        var type = PackId[..2]
            .ToUpperInvariant();
        if (string.IsNullOrEmpty(type))
        {
            return PackType.Error;
        }

        var key = Dictionaries.PackTypeFolders.FirstOrDefault(
                folder => folder.Value.ToUpperInvariant()
                    .StartsWith(type, StringComparison.CurrentCulture))
            .Key;
        return key;
    }
}
