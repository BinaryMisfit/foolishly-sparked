using System;
using System.Threading;
using System.Threading.Tasks;

namespace Foolishly.Sparked.Core;

public interface IPackageDescriptor
{
    /// <summary>
    ///     Package is read-only. Applies to Base Game and Expansion Packs.
    /// </summary>
    bool IsReadOnly { get; set; }

    /// <summary>
    ///     The <see cref="ICatalogCollection" />.
    /// </summary>
    ICatalogCollection Catalog { get; }

    /// <summary>
    ///     Read package information from a .package file.
    /// </summary>
    /// <param name="filePathAndName">The file path.</param>
    /// <returns>The <see cref="IPackageDescriptor" />.</returns>
    IPackageDescriptor LoadFile(string filePathAndName);

    /// <summary>
    ///     Read package asynchronously.
    /// </summary>
    /// <returns>The <see cref="IPackageDescriptor" />.</returns>
    Task<IPackageDescriptor> ReadPackageAsync();

    /// <summary>
    ///     Read package asynchronously.
    /// </summary>
    /// <param name="token">The <see cref="CancellationToken" />.</param>
    /// <returns>The <see cref="IPackageDescriptor" />.</returns>
    Task<IPackageDescriptor> ReadPackageAsync(CancellationToken token);

    /// <summary>
    ///     Read package asynchronously.
    /// </summary>
    /// <param name="progress">The <see cref="AsyncProgressReport" /> used for progress reporting.</param>
    /// <returns>The <see cref="IPackageDescriptor" />.</returns>
    Task<IPackageDescriptor> ReadPackageAsync(IProgress<AsyncProgressReport>? progress);

    /// <summary>
    ///     Read package asynchronously.
    /// </summary>
    /// <param name="progress">The <see cref="AsyncProgressReport" /> used for progress reporting.</param>
    /// <param name="token">The <see cref="CancellationToken" />.</param>
    /// <returns>The <see cref="IPackageDescriptor" />.</returns>
    Task<IPackageDescriptor> ReadPackageAsync(IProgress<AsyncProgressReport>? progress, CancellationToken token);

    /// <summary>
    ///     Reads the package contents.
    /// </summary>
    /// <returns>The <see cref="IPackageDescriptor" />.</returns>
    Task<IPackageDescriptor> ReadPackageContentAsync();

    /// <summary>
    ///     Reads the package contents.
    /// </summary>
    /// <param name="token">The <see cref="CancellationToken" /></param>
    /// <returns>The <see cref="IPackageDescriptor" />.</returns>
    Task<IPackageDescriptor> ReadPackageContentAsync(CancellationToken token);

    /// <summary>
    ///     Reads the package contents.
    /// </summary>
    /// <param name="progress">The <see cref="AsyncProgressReport" /> used for progress reporting.</param>
    /// <returns>The <see cref="IPackageDescriptor" />.</returns>
    Task<IPackageDescriptor> ReadPackageContentAsync(IProgress<AsyncProgressReport>? progress);

    /// <summary>
    ///     Reads the package contents.
    /// </summary>
    /// <param name="progress">The <see cref="AsyncProgressReport" /> used for progress reporting.</param>
    /// <param name="token">The <see cref="CancellationToken" />.</param>
    /// <returns>The <see cref="IPackageDescriptor" />.</returns>
    Task<IPackageDescriptor> ReadPackageContentAsync(IProgress<AsyncProgressReport>? progress, CancellationToken token);
}
