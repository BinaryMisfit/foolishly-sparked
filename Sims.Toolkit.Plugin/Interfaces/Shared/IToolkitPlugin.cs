using JetBrains.Annotations;

namespace Sims.Toolkit.Plugin.Interfaces.Shared;

/// <summary>
///     Represents a plugin for the Sims Toolkit containing extended functionality.
/// </summary>
[PublicAPI]
public interface IToolkitPlugin
{
    /// <summary>
    ///     Register functionality for the toolkit.
    /// </summary>
    void Register();
}
