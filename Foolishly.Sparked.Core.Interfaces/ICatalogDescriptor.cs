namespace Foolishly.Sparked.Core;

public interface ICatalogDescriptor
{
    /// <summary>
    ///     Gets the content of the package entry.
    /// </summary>
    byte[] Item { get; }

    /// <summary>
    ///     The instance identifier.
    /// </summary>
    ulong Instance { get; }

    /// <summary>
    ///     The <see cref="CatalogItemType" />.
    /// </summary>
    CatalogItemType CatalogItemType { get; }

    /// <summary>
    ///     The resource group identifier.
    /// </summary>
    uint ResourceGroup { get; }
}
