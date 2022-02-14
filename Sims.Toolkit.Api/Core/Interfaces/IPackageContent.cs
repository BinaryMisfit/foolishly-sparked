using JetBrains.Annotations;
using Sims.Toolkit.Api.Enums;

namespace Sims.Toolkit.Api.Core.Interfaces;

/// <summary>
///     Interface to represent an item contained within a <see cref="IPackage" /> instance.
/// </summary>
[PublicAPI]
public interface IPackageContent
{
    byte[] Item { get; }

    ulong Instance { get; }

    ResourceType ResourceType { get; }

    uint ResourceGroup { get; }
}
