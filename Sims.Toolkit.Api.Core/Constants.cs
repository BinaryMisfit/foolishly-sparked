namespace Sims.Toolkit.Api.Core;

/// <summary>
///     Contains various static values required within the Api.
/// </summary>
public static class Constants
{
    /// <summary>
    ///     Specify the number of content items.
    /// </summary>
    public static readonly int ContentCount = 36;

    /// <summary>
    ///     Start position for content.
    /// </summary>
    public static readonly int ContentPosition = 64;

    /// <summary>
    ///     Alternate start position for content.
    /// </summary>
    public static readonly int ContentPositionAlternate = 40;

    /// <summary>
    ///     Number of fields containing content information.
    /// </summary>
    public static readonly int Fields = 9;

    /// <summary>
    ///     The client package file for a game pack.
    /// </summary>
    public static readonly string FilesClientPackage = "Client*Build0.package";

    /// <summary>
    ///     Header identifier.
    /// </summary>
    public static readonly string HeaderBit = "DBPF";

    /// <summary>
    ///     Represent the header id bit.
    /// </summary>
    public static readonly byte[] HeaderId = new byte[96];

    /// <summary>
    ///     List of folders to ignore during game import.
    /// </summary>
    public static readonly string[] IgnoreGameFolders = {"_Installer", "Delta", "Game", "Shared", "Soundtrack", "Support"};

    /// <summary>
    ///     Start position of instance identifier.
    /// </summary>
    public static readonly int InstanceStart = 12;

    /// <summary>
    ///     Alternate start position of the instance identifier.
    /// </summary>
    public static readonly int InstanceStartAlternate = 16;

    /// <summary>
    ///     Start position of major version.
    /// </summary>
    public static readonly int MajorStart = 4;

    /// <summary>
    ///     Start position of minor version.
    /// </summary>
    public static readonly int MinorStart = 8;

    /// <summary>
    ///     Max allowed package version.
    /// </summary>
    public static readonly int PackageMajor = 2;

    /// <summary>
    ///     Minor allowed package version.
    /// </summary>
    public static readonly int PackageMinor = 1;

    /// <summary>
    ///     Start position of the resource group.
    /// </summary>
    public static readonly int ResourceGroupStart = 8;

    /// <summary>
    ///     Start position of the resource type.
    /// </summary>
    public static readonly int ResourceTypeStart = 4;
}
