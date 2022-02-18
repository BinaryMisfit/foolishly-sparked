using System.Collections.Generic;

namespace Sims.Toolkit.Plugin.Manager;

/// <summary>
///     Specifies the contract for a collection of plugin descriptors.
/// </summary>
public interface IPluginCollection : IList<PluginDescriptor>
{
}
