using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Foolishly.Sparked.Core;

/// <inheritdoc />
public class PackCollection : IPackCollection
{
    private readonly IList<IPackDescriptor> packs;

    public PackCollection()
    {
        packs = new List<IPackDescriptor>();
    }

    /// <inheritdoc />
    public IPackDescriptor this[int index]
    {
        get => packs[index];
        set => packs[index] = value;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <inheritdoc />
    public void Clear()
    {
        packs.Clear();
    }

    /// <inheritdoc />
    public int Count => packs.Count;

    /// <inheritdoc />
    public bool IsReadOnly => packs.IsReadOnly;

    /// <inheritdoc />
    public void RemoveAt(int index)
    {
        packs.RemoveAt(index);
    }

    /// <inheritdoc />
    public IEnumerable<KeyValuePair<PackTypes, int>> Summary()
    {
        var sorted = packs.OrderBy(pack => pack.PackTypes)
            .ThenBy(pack => pack.PackTypeId);
        var summary = sorted.GroupBy(item => item.PackTypes)
            .AsEnumerable()
            .Select(item => new KeyValuePair<PackTypes, int>(item.Key, item.Count()));
        return summary;
    }

    /// <inheritdoc />
    public IEnumerator<IPackDescriptor> GetEnumerator()
    {
        return packs.GetEnumerator();
    }

    /// <inheritdoc />
    public void Add(IPackDescriptor item)
    {
        packs.Add(item);
    }

    /// <inheritdoc />
    public bool Contains(IPackDescriptor item)
    {
        return packs.Contains(item);
    }

    /// <inheritdoc />
    public void CopyTo(IPackDescriptor[] array, int arrayIndex)
    {
        packs.CopyTo(array, arrayIndex);
    }

    /// <inheritdoc />
    public bool Remove(IPackDescriptor item)
    {
        return packs.Remove(item);
    }

    /// <inheritdoc />
    public int IndexOf(IPackDescriptor item)
    {
        return packs.IndexOf(item);
    }

    /// <inheritdoc />
    public void Insert(int index, IPackDescriptor item)
    {
        packs.Insert(index, item);
    }
}
