using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Sims.Toolkit.Api;

/// <summary>
///     Implementation of <see cref="IContentCollection" />.
/// </summary>
public class ContentCollection : IContentCollection
{
    /// <summary>
    ///     Initializes an instance of ContentCollection.
    /// </summary>
    public ContentCollection()
    {
        Contents = new List<ContentDescriptor>();
    }

    private IList<ContentDescriptor> Contents { get; }

    /// <inheritdoc />
    public IEnumerator<ContentDescriptor> GetEnumerator()
    {
        return Contents.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <inheritdoc />
    public void Add(ContentDescriptor item)
    {
        Contents.Add(item);
    }

    /// <inheritdoc />
    public void Clear()
    {
        Contents.Clear();
    }

    /// <inheritdoc />
    public bool Contains(ContentDescriptor item)
    {
        return Contents.Contains(item);
    }

    /// <inheritdoc />
    public void CopyTo(ContentDescriptor[] array, int arrayIndex)
    {
        Contents.CopyTo(array, arrayIndex);
    }

    /// <inheritdoc />
    public bool Remove(ContentDescriptor item)
    {
        return Contents.Remove(item);
    }

    /// <inheritdoc />
    public int Count => Contents.Count;

    /// <inheritdoc />
    public bool IsReadOnly => Contents.IsReadOnly;

    /// <inheritdoc />
    public int IndexOf(ContentDescriptor item)
    {
        return Contents.IndexOf(item);
    }

    /// <inheritdoc />
    public void Insert(int index, ContentDescriptor item)
    {
        Contents.Insert(index, item);
    }

    /// <inheritdoc />
    public void RemoveAt(int index)
    {
        Contents.RemoveAt(index);
    }

    /// <inheritdoc />
    public ContentDescriptor this[int index]
    {
        get => Contents[index];
        set => Contents[index] = value;
    }

    /// <inheritdoc />
    public IEnumerable<KeyValuePair<ResourceType, int>> Summary()
    {
        var sorted = Contents.OrderBy(item => item.ResourceType.ToString());
        var summary = sorted.GroupBy(item => item.ResourceType)
            .AsEnumerable()
            .Select(item => new KeyValuePair<ResourceType, int>(item.Key, item.Count()));
        return summary;
    }
}
