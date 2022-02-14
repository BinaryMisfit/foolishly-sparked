using System.Collections;
using System.Collections.Generic;
using Sims.Toolkit.Api.Core.Interfaces;

namespace Sims.Toolkit.Api.Core;

public class PackContent : IPackContent
{
    private readonly IList<IPackage> packages = new List<IPackage>();

    public IEnumerator<IPackage> GetEnumerator()
    {
        return packages.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Add(IPackage item)
    {
        packages.Add(item);
    }

    public void Clear()
    {
        packages.Clear();
    }

    public bool Contains(IPackage item)
    {
        return packages.Contains(item);
    }

    public void CopyTo(IPackage[] array, int arrayIndex)
    {
        packages.CopyTo(array, arrayIndex);
    }

    public bool Remove(IPackage item)
    {
        return packages.Remove(item);
    }

    public int Count => packages.Count;

    public bool IsReadOnly => packages.IsReadOnly;

    public int IndexOf(IPackage item)
    {
        return packages.IndexOf(item);
    }

    public void Insert(int index, IPackage item)
    {
        packages.Insert(index, item);
    }

    public void RemoveAt(int index)
    {
        packages.RemoveAt(index);
    }

    public IPackage this[int index]
    {
        get => packages[index];
        set => packages[index] = value;
    }
}
