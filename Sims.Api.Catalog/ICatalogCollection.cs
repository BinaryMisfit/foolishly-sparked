using Sims.Core;

namespace Sims.Api.Catalog;

/// <inheritdoc />
public interface ICatalogCollection : IList<CatalogDescriptor>
{
    /// <summary>
    ///     Returns a summary of the resources contained in the collection.
    /// </summary>
    /// <returns>A list of <see cref="KeyValuePair" />.</returns>
    IEnumerable<KeyValuePair<CatalogItemType, int>> Summary();
}
