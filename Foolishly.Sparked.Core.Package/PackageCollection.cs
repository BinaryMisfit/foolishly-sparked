using System.Collections;
using System.Collections.Generic;

namespace Foolishly.Sparked.Core;

/// <inheritdoc />
public class PackageCollection : IPackageCollection
{
    private readonly IList<IPackageDescriptor> packages;

    public PackageCollection()
    {
        packages = new List<IPackageDescriptor>();
    }

    /// <inheritdoc />
    public IEnumerator<IPackageDescriptor> GetEnumerator()
    {
        return packages.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <inheritdoc />
    public void Add(IPackageDescriptor item)
    {
        packages.Add(item);
    }

    /// <inheritdoc />
    public void Clear()
    {
        packages.Clear();
    }

    /// <inheritdoc />
    public bool Contains(IPackageDescriptor item)
    {
        return packages.Contains(item);
    }

    /// <inheritdoc />
    public void CopyTo(IPackageDescriptor[] array, int arrayIndex)
    {
        packages.CopyTo(array, arrayIndex);
    }

    /// <inheritdoc />
    public bool Remove(IPackageDescriptor item)
    {
        return packages.Remove(item);
    }

    /// <inheritdoc />
    public int Count => packages.Count;

    /// <inheritdoc />
    public bool IsReadOnly => packages.IsReadOnly;

    /// <inheritdoc />
    public int IndexOf(IPackageDescriptor item)
    {
        return packages.IndexOf(item);
    }

    /// <inheritdoc />
    public void Insert(int index, IPackageDescriptor item)
    {
        packages.Insert(index, item);
    }

    /// <inheritdoc />
    public void RemoveAt(int index)
    {
        packages.RemoveAt(index);
    }

    /// <inheritdoc />
    public IPackageDescriptor this[int index]
    {
        get => packages[index];
        set => packages[index] = value;
    }
}
