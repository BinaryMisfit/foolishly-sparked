using System.Collections.Generic;

namespace Sims.Toolkit.Api;

/// <inheritdoc />
public interface IPackCollection : IList<PackDescriptor>
{
    /// <summary>
    ///     Returns a summary of the packs contained in the collection.
    /// </summary>
    /// <returns>A list of <see cref="KeyValuePair" />.</returns>
    IEnumerable<KeyValuePair<PackType, int>> Summary();
}
