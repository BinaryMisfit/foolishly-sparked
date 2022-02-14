using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sims.Toolkit.Api.Core.Interfaces;
using Sims.Toolkit.Api.Enums;

namespace Sims.Toolkit.Api.Core;

public class PackCollection : IPackCollection
{
    private readonly IList<IPack> packs = new List<IPack>();

    public IEnumerator<IPack> GetEnumerator()
    {
        return packs.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Add(IPack item)
    {
        packs.Add(item);
    }

    public void Clear()
    {
        packs.Clear();
    }

    public bool Contains(IPack item)
    {
        return packs.Contains(item);
    }

    public void CopyTo(IPack[] array, int arrayIndex)
    {
        packs.CopyTo(array, arrayIndex);
    }

    public bool Remove(IPack item)
    {
        return packs.Remove(item);
    }

    public int Count => packs.Count;

    public bool IsReadOnly => packs.IsReadOnly;

    public int IndexOf(IPack item)
    {
        return packs.IndexOf(item);
    }

    public void Insert(int index, IPack item)
    {
        packs.Insert(index, item);
    }

    public void RemoveAt(int index)
    {
        packs.RemoveAt(index);
    }

    public IPack this[int index]
    {
        get => packs[index];
        set => packs[index] = value;
    }

    public IEnumerable<KeyValuePair<PackType, int>> Summary()
    {
        var sorted = packs.OrderBy(pack => pack.PackType).ThenBy(pack => pack.PackTypeId);
        var summary = sorted.GroupBy(item => item.PackType).AsEnumerable()
            .Select(item => new KeyValuePair<PackType, int>(item.Key, item.Count()));
        return summary;
    }
}
