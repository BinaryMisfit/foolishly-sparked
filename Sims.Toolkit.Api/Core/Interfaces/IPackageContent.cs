using Sims.Toolkit.Api.Enums;

namespace Sims.Toolkit.Api.Core.Interfaces;

/// <summary>
///     Interface to represent an item contained within a <see cref="IPackage" /> instance.
/// </summary>
public interface IPackageContent
{
    byte[] Item { get; }

    ResourceType ResourceType { get; }
}
