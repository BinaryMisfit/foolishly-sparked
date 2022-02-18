using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Sims.Toolkit.Api;

/// <inheritdoc />
public class PackCollection : IPackCollection
{
    private readonly IList<PackDescriptor> packs = new List<PackDescriptor>();

    /// <inheritdoc />
    public IEnumerator<PackDescriptor> GetEnumerator()
    {
        return packs.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <inheritdoc />
    public void Add(PackDescriptor item)
    {
        packs.Add(item);
    }

    /// <inheritdoc />
    public void Clear()
    {
        packs.Clear();
    }

    /// <inheritdoc />
    public bool Contains(PackDescriptor item)
    {
        return packs.Contains(item);
    }

    /// <inheritdoc />
    public void CopyTo(PackDescriptor[] array, int arrayIndex)
    {
        packs.CopyTo(array, arrayIndex);
    }

    /// <inheritdoc />
    public bool Remove(PackDescriptor item)
    {
        return packs.Remove(item);
    }

    /// <inheritdoc />
    public int Count => packs.Count;

    /// <inheritdoc />
    public bool IsReadOnly => packs.IsReadOnly;

    /// <inheritdoc />
    public int IndexOf(PackDescriptor item)
    {
        return packs.IndexOf(item);
    }

    /// <inheritdoc />
    public void Insert(int index, PackDescriptor item)
    {
        packs.Insert(index, item);
    }

    /// <inheritdoc />
    public void RemoveAt(int index)
    {
        packs.RemoveAt(index);
    }

    /// <inheritdoc />
    public PackDescriptor this[int index]
    {
        get => packs[index];
        set => packs[index] = value;
    }

    /// <inheritdoc />
    public IEnumerable<KeyValuePair<PackType, int>> Summary()
    {
        var sorted = packs.OrderBy(pack => pack.PackType)
            .ThenBy(pack => pack.PackTypeId);
        var summary = sorted.GroupBy(item => item.PackType)
            .AsEnumerable()
            .Select(item => new KeyValuePair<PackType, int>(item.Key, item.Count()));
        return summary;
    }
}
