namespace Foolishly.Sparked.Core;

/// <summary>
///     Static values to read the .package header.
/// </summary>
public static class HeaderByteMap
{
    /// <summary>
    ///     Retrieve the number of content items.
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
    ///     Header identifier.
    /// </summary>
    public static readonly string HeaderBit = "DBPF";

    /// <summary>
    ///     Represent the header id bit.
    /// </summary>
    public static readonly byte[] HeaderId = new byte[96];

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
}
