using System;
using JetBrains.Annotations;

namespace Sims.Toolkit.Api.Plugin.Attributes.Interfaces;

[PublicAPI]
public interface IExportPlatformAttribute
{
    public PlatformID Platform { get; }
}
