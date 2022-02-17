using System;
using System.ComponentModel.Composition;
using JetBrains.Annotations;
using Sims.Toolkit.Api.Plugin.Attributes.Interfaces;
using Sims.Toolkit.Api.Plugin.Interfaces.Shared;

namespace Sims.Toolkit.Api.Plugin.Attributes;

/// <summary>
///     Implementation of the <see cref="IExportPlatformAttribute" /> interface.
/// </summary>
[PublicAPI]
[MetadataAttribute]
[AttributeUsage(AttributeTargets.Class)]
public sealed class ExportPlatformAttribute : ExportAttribute, IExportPlatformAttribute
{
    /// <summary>
    ///     Initializes an instance of <see cref="ExportPlatformAttribute" />.
    /// </summary>
    /// <param name="platform">The <see cref="PlatformID" /> to be applied.</param>
    public ExportPlatformAttribute(PlatformID platform)
        : base(typeof(ICoreApiPlugin))
    {
        Platform = platform;
    }

    /// <inheritdoc />
    public PlatformID Platform { get; }
}
