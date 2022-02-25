using System.Collections.Generic;

namespace Foolishly.Sparked.Plugin;

/// <summary>
///     Specifies the contract for a collection of plugin descriptors.
/// </summary>
public interface IPluginCollection : IList<PluginDescriptor>
{
}
