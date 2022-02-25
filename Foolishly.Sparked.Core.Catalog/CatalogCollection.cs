using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Foolishly.Sparked.Core;

/// <summary>
///     Implementation of <see cref="Foolishly.Sparked.Core.ICatalogCollection" />.
/// </summary>
public class CatalogCollection : ICatalogCollection
{
    /// <summary>
    ///     Initializes an instance of CatalogCollection.
    /// </summary>
    public CatalogCollection()
    {
        Contents = new List<ICatalogDescriptor>();
    }

    private IList<ICatalogDescriptor> Contents { get; }

    /// <inheritdoc />
    public int Count => Contents.Count;

    /// <inheritdoc />
    public bool IsReadOnly => Contents.IsReadOnly;

    /// <inheritdoc />
    public ICatalogDescriptor this[int index]
    {
        get => Contents[index];
        set => Contents[index] = value;
    }

    /// <inheritdoc />
    public IEnumerator<ICatalogDescriptor> GetEnumerator()
    {
        return Contents.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <inheritdoc />
    public void Clear()
    {
        Contents.Clear();
    }

    /// <inheritdoc />
    public bool Contains(ICatalogDescriptor item)
    {
        return Contents.Contains(item);
    }

    /// <inheritdoc />
    public void Insert(int index, ICatalogDescriptor item)
    {
        Contents.Insert(index, item);
    }

    /// <inheritdoc />
    public void RemoveAt(int index)
    {
        Contents.RemoveAt(index);
    }

    /// <inheritdoc />
    public IEnumerable<KeyValuePair<CatalogItemType, int>> Summary()
    {
        var sorted = Contents.OrderBy(item => item.CatalogItemType.ToString());
        var summary = sorted.GroupBy(item => item.CatalogItemType)
            .AsEnumerable()
            .Select(item => new KeyValuePair<CatalogItemType, int>(item.Key, item.Count()));
        return summary;
    }

    /// <inheritdoc />
    public void Add(ICatalogDescriptor item)
    {
        Contents.Add(item);
    }

    /// <inheritdoc />
    public void CopyTo(ICatalogDescriptor[] array, int arrayIndex)
    {
        Contents.CopyTo(array, arrayIndex);
    }

    /// <inheritdoc />
    public bool Remove(ICatalogDescriptor item)
    {
        return Contents.Remove(item);
    }

    /// <inheritdoc />
    public int IndexOf(ICatalogDescriptor item)
    {
        return Contents.IndexOf(item);
    }
}
