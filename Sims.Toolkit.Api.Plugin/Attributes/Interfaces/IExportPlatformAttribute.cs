using System;
using JetBrains.Annotations;

namespace Sims.Toolkit.Api.Plugin.Attributes.Interfaces;

/// <summary>
///     Represent an attribute used to specify the target platform for a plugin.
/// </summary>
[PublicAPI]
public interface IExportPlatformAttribute
{
    /// <summary>
    ///     Gets or sets the target platform as a <see cref="PlatformID" />.
    /// </summary>
    public PlatformID Platform { get; }
}
