using System;
using System.Data;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Sims.Toolkit.Api.Helpers;

namespace Sims.Toolkit.Api.Core.Interfaces;

/// <summary>
///     Primary interface representing the full custom content package file.
/// </summary>
public interface IPackage
{
    /// <summary>
    ///     Gets the <see cref="PackageContentCollection" /> instances.
    /// </summary>
    PackageContentCollection Contents { get; }

    /// <summary>
    ///     Load and read the package file from the provided file path.
    /// </summary>
    /// <param name="filePathAndName">Full path and name of file.</param>
    /// <returns>A <see cref="IPackage" /> instance.</returns>
    IPackage LoadFromFile(string filePathAndName);

    /// <summary>
    ///     Read and load the package asynchronously.
    /// </summary>
    /// <returns>Returns a instance of <see cref="IPackage" /> with package information.</returns>
    /// <exception cref="EndOfStreamException">Thrown if the file header cannot be read.</exception>
    /// <exception cref="FileLoadException">Thrown when the file information is missing.</exception>
    /// <exception cref="FileNotFoundException">Thrown when the file cannot be found.</exception>
    /// <exception cref="InvalidCastException">Thrown if the magic bit check of the header fails.</exception>
    /// <exception cref="TaskCanceledException">Thrown to indicate the task was cancelled.</exception>
    /// <exception cref="VersionNotFoundException">Thrown if the version in the header is invalid.</exception>
    Task<IPackage> LoadPackageAsync();

    /// <summary>
    ///     Read and load the package asynchronously.
    /// </summary>
    /// <param name="token"><see cref="CancellationToken" /> for cancelling process.</param>
    /// <returns>Returns a instance of <see cref="IPackage" /> with package information.</returns>
    /// <exception cref="EndOfStreamException">Thrown if the file header cannot be read.</exception>
    /// <exception cref="FileLoadException">Thrown when the file information is missing.</exception>
    /// <exception cref="FileNotFoundException">Thrown when the file cannot be found.</exception>
    /// <exception cref="InvalidCastException">Thrown if the magic bit check of the header fails.</exception>
    /// <exception cref="TaskCanceledException">Thrown to indicate the task was cancelled.</exception>
    /// <exception cref="VersionNotFoundException">Thrown if the version in the header is invalid.</exception>
    Task<IPackage> LoadPackageAsync(CancellationToken token);

    /// <summary>
    ///     Read and load the package asynchronously.
    /// </summary>
    /// <param name="progress"><see cref="IProgress{T}" /> with <see cref="ProgressReport" /> for progress reporting.</param>
    /// <returns>Returns a instance of <see cref="IPackage" /> with package information.</returns>
    /// <exception cref="EndOfStreamException">Thrown if the file header cannot be read.</exception>
    /// <exception cref="FileLoadException">Thrown when the file information is missing.</exception>
    /// <exception cref="FileNotFoundException">Thrown when the file cannot be found.</exception>
    /// <exception cref="InvalidCastException">Thrown if the magic bit check of the header fails.</exception>
    /// <exception cref="TaskCanceledException">Thrown to indicate the task was cancelled.</exception>
    /// <exception cref="VersionNotFoundException">Thrown if the version in the header is invalid.</exception>
    Task<IPackage> LoadPackageAsync(IProgress<ProgressReport> progress);

    /// <summary>
    ///     Read and load the package asynchronously.
    /// </summary>
    /// <param name="progress"><see cref="IProgress{T}" /> with <see cref="ProgressReport" /> for progress reporting.</param>
    /// <param name="token"><see cref="CancellationToken" /> for cancelling process.</param>
    /// <returns>Returns a instance of <see cref="IPackage" /> with package information.</returns>
    /// <exception cref="EndOfStreamException">Thrown if the file header cannot be read.</exception>
    /// <exception cref="FileLoadException">Thrown when the file information is missing.</exception>
    /// <exception cref="FileNotFoundException">Thrown when the file cannot be found.</exception>
    /// <exception cref="InvalidCastException">Thrown if the magic bit check of the header fails.</exception>
    /// <exception cref="TaskCanceledException">Thrown to indicate the task was cancelled.</exception>
    /// <exception cref="VersionNotFoundException">Thrown if the version in the header is invalid.</exception>
    Task<IPackage> LoadPackageAsync(IProgress<ProgressReport> progress, CancellationToken token);

    /// <summary>
    ///     Read and load the package content asynchronously.
    /// </summary>
    /// <returns>Returns a instance of <see cref="IPackage" /> with package information.</returns>
    Task<IPackage> LoadPackageContentAsync();

    /// <summary>
    ///     Read and load the package content asynchronously.
    /// </summary>
    /// <param name="token"><see cref="CancellationToken" /> for cancelling process.</param>
    /// <returns>Returns a instance of <see cref="IPackage" /> with package information.</returns>
    Task<IPackage> LoadPackageContentAsync(CancellationToken token);

    /// <summary>
    ///     Read and load the package content asynchronously.
    /// </summary>
    /// <param name="progress"><see cref="IProgress{T}" /> with <see cref="ProgressReport" /> for progress reporting.</param>
    /// <returns>Returns a instance of <see cref="IPackage" /> with package information.</returns>
    Task<IPackage> LoadPackageContentAsync(IProgress<ProgressReport> progress);

    /// <summary>
    ///     Read and load the package content asynchronously.
    /// </summary>
    /// <param name="progress"><see cref="IProgress{T}" /> with <see cref="ProgressReport" /> for progress reporting.</param>
    /// <param name="token"><see cref="CancellationToken" /> for cancelling process.</param>
    /// <returns>Returns a instance of <see cref="IPackage" /> with package information.</returns>
    Task<IPackage> LoadPackageContentAsync(IProgress<ProgressReport> progress, CancellationToken token);
}
