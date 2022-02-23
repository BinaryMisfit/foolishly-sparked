using System.Collections;

namespace Sims.Plugin;

/// <summary>
///     Default implementation of <see cref="IPluginCollection" />
/// </summary>
public class PluginCollection : IPluginCollection
{
    private readonly List<PluginDescriptor> _descriptors = new();

    /// <inheritdoc />
    public IEnumerator<PluginDescriptor> GetEnumerator()
    {
        return _descriptors.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <inheritdoc />
    public void Add(PluginDescriptor item)
    {
        _descriptors.Add(item);
    }

    /// <inheritdoc />
    public void Clear()
    {
        _descriptors.Clear();
    }

    /// <inheritdoc />
    public bool Contains(PluginDescriptor item)
    {
        return _descriptors.Contains(item);
    }

    /// <inheritdoc />
    public void CopyTo(PluginDescriptor[] array, int arrayIndex)
    {
        _descriptors.CopyTo(array, arrayIndex);
    }

    /// <inheritdoc />
    public bool Remove(PluginDescriptor item)
    {
        return _descriptors.Remove(item);
    }

    /// <inheritdoc />
    public int Count => _descriptors.Count;

    /// <inheritdoc />
    public bool IsReadOnly => false;

    /// <inheritdoc />
    public int IndexOf(PluginDescriptor item)
    {
        return _descriptors.IndexOf(item);
    }

    /// <inheritdoc />
    public void Insert(int index, PluginDescriptor item)
    {
        _descriptors.Insert(index, item);
    }

    /// <inheritdoc />
    public void RemoveAt(int index)
    {
        _descriptors.RemoveAt(index);
    }

    /// <inheritdoc />
    public PluginDescriptor this[int index]
    {
        get => _descriptors[index];
        set => _descriptors[index] = value;
    }
}
