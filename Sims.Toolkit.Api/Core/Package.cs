using System.Data;
using System.Text;
using Sims.Toolkit.Api.Helpers;

namespace Sims.Toolkit.Api.Core;

/// <summary>
///     Allows interaction with a .package file used for Sims custom content.
/// </summary>
public class Package
{
    /// <summary>
    ///     Initializes an instance of the package.
    /// </summary>
    /// <param name="filePathAndName">The path and name of the file to load.</param>
    public Package(string filePathAndName) : this(new FileInfo(filePathAndName))
    {
        // Nothing to do but initialize the class
    }

    /// <summary>
    ///     Initializes an instance of the package.
    /// </summary>
    /// <param name="fileInfo">Instance of <see cref="FileInfo" />.</param>
    public Package(FileInfo fileInfo)
    {
        SourceFile = fileInfo;
    }

    /// <summary>
    ///     Gets the <see cref="FileInfo" /> instance of the package file.
    /// </summary>
    private FileInfo? SourceFile { get; }

    /// <summary>
    ///     Gets the major version number of the package file.
    /// </summary>
    private int MajorVersion { get; set; }

    /// <summary>
    ///     Gets the minor version number of the package file.
    /// </summary>
    private int MinorVersion { get; set; }

    /// <summary>
    ///     Gets the package content entries as an <see cref="IList{T}" />.
    /// </summary>
    public IList<PackageContent>? Contents { get; private set; }

    /// <summary>
    ///     Gets the position where content starts.
    /// </summary>
    private int ContentPosition { get; set; }

    /// <summary>
    ///     Gets the number of content items.
    /// </summary>
    private int ContentCount { get; set; }

    /// <summary>
    ///     Gets the <see cref="DirectoryInfo" /> instance of the package file.
    /// </summary>
    private DirectoryInfo? PackagePath { get; set; }

    /// <summary>
    ///     Gets the file name of the package file.
    /// </summary>
    private string? PackageFileName { get; set; }

    public override string ToString()
    {
        return $"{PackageFileName} ({MajorVersion}/{MinorVersion}/{Contents?.Count}) in {PackagePath?.FullName}";
    }

    /// <summary>
    ///     Read and load the package asynchronously.
    /// </summary>
    /// <returns>Returns a instance of <see cref="Package" /> with package information.</returns>
    /// <exception cref="EndOfStreamException">Thrown if the file header cannot be read.</exception>
    /// <exception cref="FileLoadException">Thrown when the file information is missing.</exception>
    /// <exception cref="FileNotFoundException">Thrown when the file cannot be found.</exception>
    /// <exception cref="InvalidCastException">Thrown if the magic bit check of the header fails.</exception>
    /// <exception cref="TaskCanceledException">Thrown to indicate the task was cancelled.</exception>
    /// <exception cref="VersionNotFoundException">Thrown if the version in the header is invalid.</exception>
    public Task<Package> LoadPackageAsync()
    {
        return LoadPackageAsync(null, default);
    }

    /// <summary>
    ///     Read and load the package asynchronously.
    /// </summary>
    /// <param name="progress"><see cref="IProgress{T}" /> with <see cref="ProgressReport" /> for progress reporting.</param>
    /// <returns>Returns a instance of <see cref="Package" /> with package information.</returns>
    /// <exception cref="EndOfStreamException">Thrown if the file header cannot be read.</exception>
    /// <exception cref="FileLoadException">Thrown when the file information is missing.</exception>
    /// <exception cref="FileNotFoundException">Thrown when the file cannot be found.</exception>
    /// <exception cref="InvalidCastException">Thrown if the magic bit check of the header fails.</exception>
    /// <exception cref="TaskCanceledException">Thrown to indicate the task was cancelled.</exception>
    /// <exception cref="VersionNotFoundException">Thrown if the version in the header is invalid.</exception>
    public Task<Package> LoadPackageAsync(IProgress<ProgressReport>? progress)
    {
        return LoadPackageAsync(progress, default);
    }

    /// <summary>
    ///     Read and load the package asynchronously.
    /// </summary>
    /// <param name="progress"><see cref="IProgress{T}" /> with <see cref="ProgressReport" /> for progress reporting.</param>
    /// <param name="token"><see cref="CancellationToken" /> for cancelling process.</param>
    /// <returns>Returns a instance of <see cref="Package" /> with package information.</returns>
    /// <exception cref="EndOfStreamException">Thrown if the file header cannot be read.</exception>
    /// <exception cref="FileLoadException">Thrown when the file information is missing.</exception>
    /// <exception cref="FileNotFoundException">Thrown when the file cannot be found.</exception>
    /// <exception cref="InvalidCastException">Thrown if the magic bit check of the header fails.</exception>
    /// <exception cref="TaskCanceledException">Thrown to indicate the task was cancelled.</exception>
    /// <exception cref="VersionNotFoundException">Thrown if the version in the header is invalid.</exception>
    private Task<Package> LoadPackageAsync(IProgress<ProgressReport>? progress, CancellationToken token)
    {
        if (token.IsCancellationRequested) token.ThrowIfCancellationRequested();

        if (SourceFile == null) throw new FileLoadException("File not specified.");

        var header = ReadHeader(SourceFile);
        VerifyHeader(header);
        PopulateVersionInfo(header);
        PopulateContentInfo(header);
        if (token.IsCancellationRequested) token.ThrowIfCancellationRequested();

        progress?.Report(new ProgressReport($"{PackageFileName} is a valid custom content file."));
        return Task.FromResult(this);
    }

    /// <summary>
    ///     Read and load the package content asynchronously.
    /// </summary>
    /// <returns>Returns a instance of <see cref="Package" /> with package information.</returns>
    public Task<Package> LoadPackageContentAsync()
    {
        return LoadPackageContentAsync(null, default);
    }

    /// <summary>
    ///     Read and load the package content asynchronously.
    /// </summary>
    /// <param name="progress"><see cref="IProgress{T}" /> with <see cref="ProgressReport" /> for progress reporting.</param>
    /// <returns>Returns a instance of <see cref="Package" /> with package information.</returns>
    public Task<Package> LoadPackageContentAsync(IProgress<ProgressReport>? progress)
    {
        return LoadPackageAsync(progress, default);
    }

    /// <summary>
    ///     Read and load the package content asynchronously.
    /// </summary>
    /// <param name="progress"><see cref="IProgress{T}" /> with <see cref="ProgressReport" /> for progress reporting.</param>
    /// <param name="token"><see cref="CancellationToken" /> for cancelling process.</param>
    /// <returns>Returns a instance of <see cref="Package" /> with package information.</returns>
    private Task<Package> LoadPackageContentAsync(IProgress<ProgressReport>? progress, CancellationToken token)
    {
        FileStream? stream = null;
        try
        {
            if (token.IsCancellationRequested) token.ThrowIfCancellationRequested();

            if (SourceFile == null) throw new FileLoadException("File not specified.");

            progress?.Report(new ProgressReport($"Loading content from {PackageFileName}."));
            stream = new FileStream(SourceFile.FullName, FileMode.Open, FileAccess.Read, FileShare.Read);
            stream.Position = 0;
            var reader = new BinaryReader(stream);
            stream.Position = ContentPosition;
            var type = reader.ReadUInt32();

            if (token.IsCancellationRequested) token.ThrowIfCancellationRequested();

            var headerSize = CalculateHeaderSize(type);
            var header = LoadHeader(reader, type, headerSize);
            if (token.IsCancellationRequested) token.ThrowIfCancellationRequested();

            LoadEntries(reader, header, headerSize);
            stream.Close();
            return Task.FromResult(this);
        }
        finally
        {
            stream?.Close();
        }
    }

    private byte[] ReadHeader(FileInfo sourceFile)
    {
        PackagePath = sourceFile.Directory ??
                      throw new FileNotFoundException($"{sourceFile.FullName} not found.");
        PackageFileName = sourceFile.Name;
        var stream = new FileStream(sourceFile.FullName, FileMode.Open, FileAccess.Read, FileShare.Read);
        stream.Position = 0;
        var header = new BinaryReader(stream).ReadBytes(Constants.HeaderId.Length);
        stream.Close();
        return header;
    }

    private void VerifyHeader(byte[] header)
    {
        if (header.Length != Constants.HeaderId.Length)
            throw new EndOfStreamException($"{PackageFileName} EOF reached prematurely.");

        VerifyMagicBit(header);
    }

    private void VerifyMagicBit(byte[] header)
    {
        var magicBit = new byte[4];
        Array.Copy(header, 0, magicBit, 0, magicBit.Length);
        var magicCheck = Encoding.Default.GetString(magicBit);
        if (magicCheck != Constants.HeaderBit)
            throw new InvalidCastException($"{PackageFileName} is NOT a valid custom content file.");
    }

    private void PopulateVersionInfo(byte[] header)
    {
        MajorVersion = BitConverter.ToInt32(header, Constants.MajorStart);
        MinorVersion = BitConverter.ToInt32(header, Constants.MinorStart);
        if (MajorVersion != Constants.PackageMajor && MinorVersion != Constants.PackageMinor)
            throw new VersionNotFoundException($"{MajorVersion} ({MinorVersion}) does not match expected 2 (1).");
    }

    private void PopulateContentInfo(byte[] header)
    {
        ContentPosition = BitConverter.ToInt32(header, Constants.ContentPosition);
        if (ContentPosition == 0) ContentPosition = BitConverter.ToInt32(header, Constants.ContentPositionAlternate);

        if (ContentPosition == 0)
            throw new KeyNotFoundException($"{PackageFileName} does not contain any custom content.");

        ContentCount = BitConverter.ToInt32(header, Constants.ContentCount);
        if (ContentCount == 0) throw new KeyNotFoundException($"{PackageFileName} custom content cannot be read.");
    }

    private static int[] LoadHeader(BinaryReader reader, uint type, int headerSize)
    {
        var header = new int[headerSize];
        header[0] = (int) type;
        for (var i = 1; i < header.Length; i++) header[i] = reader.ReadInt32();

        return header;
    }

    private void LoadEntries(BinaryReader reader, IReadOnlyList<int> header, int headerSize)
    {
        Contents = new List<PackageContent>();
        var entry = new int[Constants.Fields - headerSize];
        for (var i = 0; i < ContentCount; i++)
        for (var j = 0; j < entry.Length; j++)
        {
            entry[j] = reader.ReadInt32();
            Contents.Add(new PackageContent(header, entry));
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