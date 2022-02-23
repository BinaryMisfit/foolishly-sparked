using System.Collections;
using Sims.Core;

namespace Sims.Api.Catalog;

/// <summary>
///     Implementation of <see cref="ICatalogCollection" />.
/// </summary>
public class CatalogCollection : ICatalogCollection
{
    /// <summary>
    ///     Initializes an instance of CatalogCollection.
    /// </summary>
    public CatalogCollection()
    {
        Contents = new List<CatalogDescriptor>();
    }

    private IList<CatalogDescriptor> Contents { get; }

    /// <inheritdoc />
    public IEnumerator<CatalogDescriptor> GetEnumerator()
    {
        return Contents.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <inheritdoc />
    public void Add(CatalogDescriptor item)
    {
        Contents.Add(item);
    }

    /// <inheritdoc />
    public void Clear()
    {
        Contents.Clear();
    }

    /// <inheritdoc />
    public bool Contains(CatalogDescriptor item)
    {
        return Contents.Contains(item);
    }

    /// <inheritdoc />
    public void CopyTo(CatalogDescriptor[] array, int arrayIndex)
    {
        Contents.CopyTo(array, arrayIndex);
    }

    /// <inheritdoc />
    public bool Remove(CatalogDescriptor item)
    {
        return Contents.Remove(item);
    }

    /// <inheritdoc />
    public int Count => Contents.Count;

    /// <inheritdoc />
    public bool IsReadOnly => Contents.IsReadOnly;

    /// <inheritdoc />
    public int IndexOf(CatalogDescriptor item)
    {
        return Contents.IndexOf(item);
    }

    /// <inheritdoc />
    public void Insert(int index, CatalogDescriptor item)
    {
        Contents.Insert(index, item);
    }

    /// <inheritdoc />
    public void RemoveAt(int index)
    {
        Contents.RemoveAt(index);
    }

    /// <inheritdoc />
    public CatalogDescriptor this[int index]
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
