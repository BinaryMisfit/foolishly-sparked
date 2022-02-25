using System.Collections.Generic;

namespace Foolishly.Sparked.Core;

/// <inheritdoc />
public interface ICatalogCollection : IList<ICatalogDescriptor>
{
    /// <summary>
    ///     Returns a summary of the resources contained in the collection.
    /// </summary>
    /// <returns>A list of <see cref="KeyValuePair" />.</returns>
    IEnumerable<KeyValuePair<CatalogItemType, int>> Summary();
}
