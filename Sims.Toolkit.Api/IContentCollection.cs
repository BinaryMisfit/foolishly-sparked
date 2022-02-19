using System.Collections.Generic;
using Sims.Toolkit.Api.Core;

namespace Sims.Toolkit.Api;

/// <inheritdoc />
public interface IContentCollection : IList<ContentDescriptor>
{
    /// <summary>
    ///     Returns a summary of the resources contained in the collection.
    /// </summary>
    /// <returns>A list of <see cref="KeyValuePair" />.</returns>
    IEnumerable<KeyValuePair<ResourceType, int>> Summary();
}
