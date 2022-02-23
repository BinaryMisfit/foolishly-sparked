namespace Sims.Core;

/// <summary>
///     Static values to read the content inside a .package file.
/// </summary>
public static class ContentByteMap
{
    /// <summary>
    ///     Number of fields containing content information.
    /// </summary>
    public static readonly int Fields = 9;

    /// <summary>
    ///     Start position of instance identifier.
    /// </summary>
    public static readonly int InstanceStart = 12;

    /// <summary>
    ///     Alternate start position of the instance identifier.
    /// </summary>
    public static readonly int InstanceStartAlternate = 16;

    /// <summary>
    ///     Start position of the resource group.
    /// </summary>
    public static readonly int ResourceGroupStart = 8;

    /// <summary>
    ///     Start position of the resource type.
    /// </summary>
    public static readonly int ResourceTypeStart = 4;
}
