using System.Collections.Generic;

namespace Foolishly.Sparked.Core;

/// <inheritdoc />
public interface IPackCollection : IList<IPackDescriptor>
{
    /// <summary>
    ///     Returns a summary of the packs contained in the collection.
    /// </summary>
    /// <returns>A list of <see cref="KeyValuePair" />.</returns>
    IEnumerable<KeyValuePair<PackTypes, int>> Summary();
}
