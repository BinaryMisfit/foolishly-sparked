using System.Data;
using System.Text;
using Sims.Toolkit.Api.Helpers;

namespace Sims.Toolkit.Api.Core;

/// <summary>
/// Allows interaction with a .package file used for Sims custom content.
/// </summary>
public class Package
{
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
    /// Gets the <see cref="FileInfo"/> instance of the package file.
    /// </summary>
    private FileInfo? sourceFile { get; }

    /// <summary>
    /// Gets the major version number of the package file.
    /// </summary>
    private int MajorVersion { get; set; }

    /// <summary>
    ///     Gets the minor version number of the package file.
    /// </summary>
    private int MinorVersion { get; set; }

    /// <summary>
    /// Gets the package content entries as an <see cref="IList{T}"/>.
    /// </summary>
    public List<PackageContent>? Contents { get; private set; }

    /// <summary>
    /// Gets the position where content starts.
    /// </summary>
    private int ContentPosition { get; set; }

    /// <summary>
    /// Gets the number of content items.
    /// </summary>
    private int ContentCount { get; set; }

    /// <summary>
    /// Gets the <see cref="DirectoryInfo"/> instance of the package file.
    /// </summary>
    private DirectoryInfo? PackagePath { get; set; }

    /// <summary>
    ///     Gets the file name of the package file.
    /// </summary>
    public string? PackageFileName { get; private set; }

    /// <summary>
    /// Read and load the package asynchronously.
    /// </summary>
    /// <param name="progress"><see cref="IProgress{T}"/> with <see cref="ProgressReport"/> for progress reporting.</param>
    /// <param name="token"><see cref="CancellationToken"/> for cancelling process.</param>
    /// <returns>Returns a instance of <see cref="Package"/> with package information.</returns>
    /// <exception cref="EndOfStreamException">Thrown if the file header cannot be read.</exception>
    /// <exception cref="FileLoadException">Thrown when the file information is missing.</exception>
    /// <exception cref="FileNotFoundException">Thrown when the file cannot be found.</exception>
    /// <exception cref="InvalidCastException">Thrown if the magic bit check of the header fails.</exception>
    /// <exception cref="TaskCanceledException">Thrown to indicate the task was cancelled.</exception>
    /// <exception cref="VersionNotFoundException">Thrown if the version in the header is invalid.</exception>
    public Task<Package> LoadPackageAsync(IProgress<ProgressReport>? progress = null, CancellationToken token = default)
    {
        FileStream? stream = null;
        try
        {
            if (token.IsCancellationRequested) token.ThrowIfCancellationRequested();

            if (sourceFile == null) throw new FileLoadException("File not specified.");

            PackagePath = sourceFile.Directory ??
                          throw new FileNotFoundException($"{sourceFile.FullName} not found.");
            PackageFileName = sourceFile.Name;
            progress?.Report(new ProgressReport($"Found {PackageFileName} in {PackagePath}."));

            if (token.IsCancellationRequested) token.ThrowIfCancellationRequested();

            stream = new FileStream(sourceFile.FullName, FileMode.Open, FileAccess.Read, FileShare.Read);
            stream.Position = 0;
            var reader = new BinaryReader(stream).ReadBytes(Constants.headerId.Length);
            stream.Close();

            if (token.IsCancellationRequested) token.ThrowIfCancellationRequested();

            if (reader.Length != 96) throw new EndOfStreamException($"{PackageFileName} EOF reached prematurely.");

            if (token.IsCancellationRequested) token.ThrowIfCancellationRequested();

            var magicBit = new byte[4];
            Array.Copy(reader, 0, magicBit, 0, magicBit.Length);
            var magicCheck = Encoding.Default.GetString(magicBit);
            if (magicCheck != Constants.headerBit)
                throw new InvalidCastException($"{PackageFileName} is NOT a valid custom content file.");

            if (token.IsCancellationRequested) token.ThrowIfCancellationRequested();

            MajorVersion = BitConverter.ToInt32(reader, Constants.majorStart);
            MinorVersion = BitConverter.ToInt32(reader, Constants.minorStart);
            if (MajorVersion != 2 && MinorVersion != 1)
                throw new VersionNotFoundException($"{MajorVersion} ({MinorVersion}) does not match expected 2 (1).");

            if (token.IsCancellationRequested) token.ThrowIfCancellationRequested();

            ContentPosition = BitConverter.ToInt32(reader, Constants.contentPosition);
            if (ContentPosition == 0)
                ContentPosition = BitConverter.ToInt32(reader, Constants.contentPositionAlternate);

            if (ContentPosition == 0)
                throw new KeyNotFoundException($"{PackageFileName} does not contain any custom content.");

            if (token.IsCancellationRequested) token.ThrowIfCancellationRequested();

            ContentCount = BitConverter.ToInt32(reader, Constants.contentCount);
            if (ContentCount == 0) throw new KeyNotFoundException($"{PackageFileName} custom content cannot be read.");

            progress?.Report(new ProgressReport($"{PackageFileName} is a valid custom content file."));
            return Task.FromResult(this);
        }
        finally
        {
            stream?.Close();
        }
    }

    /// <summary>
    ///     Read and load the package content asynchronously.
    /// </summary>
    /// <param name="progress"><see cref="IProgress{T}" /> with <see cref="ProgressReport" /> for progress reporting.</param>
    /// <param name="token"><see cref="CancellationToken" /> for cancelling process.</param>
    /// <returns>Returns a instance of <see cref="Package" /> with package information.</returns>
    public Task<Package> LoadPackageContentAsync(IProgress<ProgressReport>? progress = null,
        CancellationToken token = default)
    {
        FileStream? stream = null;
        Contents = new List<PackageContent>();
        try
        {
            if (token.IsCancellationRequested) token.ThrowIfCancellationRequested();

            if (sourceFile == null) throw new FileLoadException("File not specified.");

            progress?.Report(new ProgressReport($"Loading content from {PackageFileName}."));
            stream = new FileStream(sourceFile.FullName, FileMode.Open, FileAccess.Read, FileShare.Read);
            stream.Position = 0;
            var reader = new BinaryReader(stream);
            stream.Position = ContentPosition;
            var type = reader.ReadUInt32();

            if (token.IsCancellationRequested) token.ThrowIfCancellationRequested();

            var headerSize = CalculateHeaderSize(type);
            var header = new int[headerSize];
            header[0] = (int) type;
            for (var i = 1; i < header.Length; i++) header[i] = reader.ReadInt32();

            if (token.IsCancellationRequested) token.ThrowIfCancellationRequested();

            var entry = new int[Constants.fields - headerSize];
            for (var i = 0; i < ContentCount; i++)
            {
                progress?.Report(new ProgressReport($"Adding item #{i + 1}."));
                for (var j = 0; j < entry.Length; j++)
                {
                    entry[j] = reader.ReadInt32();
                    Contents.Add(new PackageContent(header, entry));
                }
            }

            stream.Close();
            return Task.FromResult(this);
        }
        finally
        {
            stream?.Close();
        }
    }

    private static int CalculateHeaderSize(uint type)
    {
        var headerCount = 1;
        for (var i = 1; i < sizeof(uint); i++)
            if ((type & (1 << i)) != 0)
                headerCount++;

        return headerCount;
    }
}
