using System.Data;
using System.Text;
using Sims.Toolkit.Api.Helpers;

namespace Sims.Toolkit.Api.Core;

/// <summary>
/// Allows interaction with a .package file used for Sims modifications and content.
/// </summary>
public class Package
{
    /// <summary>
    /// Gets the <see cref="FileInfo"/> instance of the package file.
    /// </summary>
    private FileInfo? sourceFile { get; }

    /// <summary>
    /// Gets the <see cref="DirectoryInfo"/> instance of the package file.
    /// </summary>
    public DirectoryInfo? PackagePath { get; private set; }

    /// <summary>
    /// Gets the file name of the package file.
    /// </summary>
    public string? PackageFileName { get; private set; }

    /// <summary>
    /// Gets the major version number of the package file.
    /// </summary>
    public int MajorVersion { get; private set; }

    /// <summary>
    /// Gets the minor version number of the package file.
    /// </summary>
    public int MinorVersion { get; private set; }

    /// <summary>
    /// Initializes an instance of the package.
    /// </summary>
    /// <param name="filePathAndName">The path and name of the file to load.</param>
    public Package(string filePathAndName) : this(new FileInfo(filePathAndName))
    {
        // Nothing to do but initialize the class
    }

    /// <summary>
    /// Initializes an instance of the package.
    /// </summary>
    /// <param name="fileInfo">Instance of <see cref="FileInfo"/>.</param>
    public Package(FileInfo fileInfo)
    {
        this.sourceFile = fileInfo;
    }

    /// <summary>
    /// Read and load the package content asynchronously.
    /// </summary>
    /// <returns>Returns a instance of <see cref="Package"/>.</returns>
    /// <exception cref="EndOfStreamException">Thrown if the file header cannot be read.</exception>
    /// <exception cref="FileLoadException">Thrown when the file information is missing.</exception>
    /// <exception cref="FileNotFoundException">Thrown when the file cannot be found.</exception>
    /// <exception cref="InvalidCastException">Thrown if the magic bit check of the header fails.</exception>
    /// <exception cref="NotImplementedException">Thrown to indicate no implementation currently.</exception>
    /// <exception cref="TaskCanceledException">Thrown to indicate the task was cancelled.</exception>
    /// <exception cref="VersionNotFoundException">Thrown if the version in the header is invalid.</exception>
    public Task<Package> LoadPackageAsync(IProgress<ProgressReport>? progress = null, CancellationToken token = default)
    {
        if (token.IsCancellationRequested)
        {
            token.ThrowIfCancellationRequested();
        }

        if (sourceFile == null)
        {
            throw new FileLoadException("File not specified.");
        }

        this.PackagePath = sourceFile.Directory ??
                           throw new FileNotFoundException($"{sourceFile.FullName} not found.");
        this.PackageFileName = sourceFile.Name;
        progress?.Report(new ProgressReport($"Found {PackageFileName} in {PackagePath}."));

        if (token.IsCancellationRequested)
        {
            token.ThrowIfCancellationRequested();
        }

        var stream = new FileStream(sourceFile.FullName, FileMode.Open, FileAccess.Read, FileShare.Read);
        stream.Position = 0;
        var header = new BinaryReader(stream).ReadBytes(Constants.headerId.Length);
        stream.Close();

        if (token.IsCancellationRequested)
        {
            token.ThrowIfCancellationRequested();
        }

        if (header.Length != 96)
        {
            throw new EndOfStreamException($"{PackageFileName} EOF reached prematurely.");
        }

        if (token.IsCancellationRequested)
        {
            token.ThrowIfCancellationRequested();
        }

        var magicBit = new byte[4];
        Array.Copy(header, 0, magicBit, 0, magicBit.Length);
        var magicCheck = Encoding.Default.GetString(magicBit);
        if (magicCheck != Constants.headerBit)
        {
            throw new InvalidCastException($"{PackageFileName} is NOT a valid modification file.");
        }

        if (token.IsCancellationRequested)
        {
            token.ThrowIfCancellationRequested();
        }

        MajorVersion = BitConverter.ToInt32(header, Constants.majorStart);
        MinorVersion = BitConverter.ToInt32(header, Constants.minorStart);
        if (MajorVersion != 2 && MinorVersion != 1)
        {
            throw new VersionNotFoundException($"{MajorVersion} ({MinorVersion}) does not match expected 2 (1).");
        }

        if (token.IsCancellationRequested)
        {
            token.ThrowIfCancellationRequested();
        }

        progress?.Report(new ProgressReport($"{PackageFileName} is a valid modification file."));
        throw new NotImplementedException();
    }
}
