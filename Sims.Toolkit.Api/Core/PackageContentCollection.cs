using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sims.Toolkit.Api.Core.Interfaces;
using Sims.Toolkit.Api.Enums;

namespace Sims.Toolkit.Api.Core;

public class PackageContentCollection : IPackageContentCollection
{
    public PackageContentCollection()
    {
        Contents = new List<IPackageContent>();
    }

    private IList<IPackageContent> Contents { get; }

    public IEnumerator<IPackageContent> GetEnumerator()
    {
        return Contents.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Add(IPackageContent item)
    {
        Contents.Add(item);
    }

    public void Clear()
    {
        Contents.Clear();
    }

    public bool Contains(IPackageContent item)
    {
        return Contents.Contains(item);
    }

    public void CopyTo(IPackageContent[] array, int arrayIndex)
    {
        Contents.CopyTo(array, arrayIndex);
    }

    public bool Remove(IPackageContent item)
    {
        return Contents.Remove(item);
    }

    public int Count => Contents.Count;

    public bool IsReadOnly => Contents.IsReadOnly;

    public int IndexOf(IPackageContent item)
    {
        return Contents.IndexOf(item);
    }

    public void Insert(int index, IPackageContent item)
    {
        Contents.Insert(index, item);
    }

    public void RemoveAt(int index)
    {
        Contents.RemoveAt(index);
    }

    public IPackageContent this[int index]
    {
        get => Contents[index];
        set => Contents[index] = value;
    }

    public IEnumerable<KeyValuePair<ResourceType, int>> Summary()
    {
        var summary = Contents.GroupBy(item => item.ResourceType).AsEnumerable()
            .Select(item => new KeyValuePair<ResourceType, int>(item.Key, item.Count()));
        return summary;
    }
}
