namespace Sims.Toolkit.Api.Interfaces;

/// <summary>
///     Interface to represent an item contained within a <see cref="IPackage" /> instance.
/// </summary>
public interface IPackageContent
{
    byte[] Item { get; }
}
