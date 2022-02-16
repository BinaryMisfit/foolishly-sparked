using System;
using System.ComponentModel.Composition;
using JetBrains.Annotations;
using Sims.Toolkit.Api.Plugin.Attributes.Interfaces;
using Sims.Toolkit.Api.Plugin.Interfaces;

namespace Sims.Toolkit.Api.Plugin.Attributes;

[PublicAPI]
[MetadataAttribute]
[AttributeUsage(AttributeTargets.Class)]
public sealed class ExportPlatformAttribute : ExportAttribute, IExportPlatformAttribute
{
    public ExportPlatformAttribute(PlatformID platform) : base(typeof(ICoreApiPlugin))
    {
        Platform = platform;
    }

    public PlatformID Platform { get; }
}
