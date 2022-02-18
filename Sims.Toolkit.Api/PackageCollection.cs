using System.Collections;
using System.Collections.Generic;

namespace Sims.Toolkit.Api;

/// <inheritdoc />
public class PackageCollection : IPackageCollection
{
    private readonly IList<PackageDescriptor> packages = new List<PackageDescriptor>();

    /// <inheritdoc />
    public IEnumerator<PackageDescriptor> GetEnumerator()
    {
        return packages.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <inheritdoc />
    public void Add(PackageDescriptor item)
    {
        packages.Add(item);
    }

    /// <inheritdoc />
    public void Clear()
    {
        packages.Clear();
    }

    /// <inheritdoc />
    public bool Contains(PackageDescriptor item)
    {
        return packages.Contains(item);
    }

    /// <inheritdoc />
    public void CopyTo(PackageDescriptor[] array, int arrayIndex)
    {
        packages.CopyTo(array, arrayIndex);
    }

    /// <inheritdoc />
    public bool Remove(PackageDescriptor item)
    {
        return packages.Remove(item);
    }

    /// <inheritdoc />
    public int Count => packages.Count;

    /// <inheritdoc />
    public bool IsReadOnly => packages.IsReadOnly;

    /// <inheritdoc />
    public int IndexOf(PackageDescriptor item)
    {
        return packages.IndexOf(item);
    }

    /// <inheritdoc />
    public void Insert(int index, PackageDescriptor item)
    {
        packages.Insert(index, item);
    }

    /// <inheritdoc />
    public void RemoveAt(int index)
    {
        packages.RemoveAt(index);
    }

    /// <inheritdoc />
    public PackageDescriptor this[int index]
    {
        get => packages[index];
        set => packages[index] = value;
    }
}
