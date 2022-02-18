using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sims.Toolkit.Plugin.Manager;

public sealed class PluginProvider : IPluginProvider, IDisposable, IAsyncDisposable
{
    private bool _disposed;

    internal PluginProvider(ICollection<PluginDescriptor> pluginDescriptors)
    {
    }

    /// <inheritdoc />
    public ValueTask DisposeAsync()
    {
        DisposeProvider();
        return default;
    }

    /// <inheritdoc />
    public void Dispose()
    {
        DisposeProvider();
    }

    private void DisposeProvider()
    {
        _disposed = true;
    }
}
