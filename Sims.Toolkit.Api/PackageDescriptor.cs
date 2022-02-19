﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Abstractions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Sims.Toolkit.Api.Core;

namespace Sims.Toolkit.Api;

/// <summary>
///     Descriptor of a Sims custom content package.
/// </summary>
public class PackageDescriptor
{
    private readonly IFileSystem _fileSystem;

    /// <summary>
    ///     Initializes an instance of the package.
    /// </summary>
    /// <param name="fileSystem">Instance of <see cref="IFileSystem" /> provided by DI.</param>
    public PackageDescriptor(IFileSystem fileSystem)
    {
        _fileSystem = fileSystem;
        Contents = new ContentCollection();
    }

    private IFileInfo? SourceFile { get; set; }

    private int MajorVersion { get; set; }

    private int MinorVersion { get; set; }

    private int ContentPosition { get; set; }

    private int ContentCount { get; set; }

    /// <summary>
    ///     Package is read-only. Applies to Base Game and Expansion Packs.
    /// </summary>
    public bool IsReadOnly { get; set; }

    /// <summary>
    ///     The <see cref="ContentCollection" />.
    /// </summary>
    public ContentCollection Contents { get; }

    /// <summary>
    ///     Read package information from a .package file.
    /// </summary>
    /// <param name="filePathAndName">The file path.</param>
    /// <returns>The <see cref="PackageDescriptor" />.</returns>
    public PackageDescriptor LoadFile(string filePathAndName)
    {
        SourceFile = _fileSystem.FileInfo.FromFileName(filePathAndName);
        return this;
    }

    /// <summary>
    ///     Read package asynchronously.
    /// </summary>
    /// <returns>The <see cref="PackageDescriptor" />.</returns>
    public Task<PackageDescriptor> ReadPackageAsync()
    {
        return ReadPackageAsync(null, default);
    }

    /// <summary>
    ///     Read package asynchronously.
    /// </summary>
    /// <param name="token">The <see cref="CancellationToken" />.</param>
    /// <returns>The <see cref="PackageDescriptor" />.</returns>
    public Task<PackageDescriptor> ReadPackageAsync(CancellationToken token)
    {
        return ReadPackageAsync(null, token);
    }

    /// <summary>
    ///     Read package asynchronously.
    /// </summary>
    /// <param name="progress">The <see cref="ProgressReport" /> used for progress reporting.</param>
    /// <returns>The <see cref="PackageDescriptor" />.</returns>
    public Task<PackageDescriptor> ReadPackageAsync(IProgress<ProgressReport>? progress)
    {
        return ReadPackageAsync(progress, default);
    }

    /// <summary>
    ///     Read package asynchronously.
    /// </summary>
    /// <param name="progress">The <see cref="ProgressReport" /> used for progress reporting.</param>
    /// <param name="token">The <see cref="CancellationToken" />.</param>
    /// <returns>The <see cref="PackageDescriptor" />.</returns>
    public Task<PackageDescriptor> ReadPackageAsync(IProgress<ProgressReport>? progress, CancellationToken token)
    {
        if (token.IsCancellationRequested)
        {
            token.ThrowIfCancellationRequested();
        }

        if (SourceFile == null)
        {
            throw new FileLoadException("File not specified.");
        }

        var stream = new MemoryStream(_fileSystem.File.ReadAllBytes(SourceFile.FullName));
        var header = ReadHeader(stream);
        VerifyHeader(header);
        PopulateVersionInfo(header);
        PopulateContentInfo(header);
        if (token.IsCancellationRequested)
        {
            token.ThrowIfCancellationRequested();
        }

        progress?.Report(new ProgressReport($"{SourceFile?.Name} is a valid custom content file."));
        return Task.FromResult(this);
    }

    /// <summary>
    ///     Reads the package contents.
    /// </summary>
    /// <returns>The <see cref="PackageDescriptor" />.</returns>
    public Task<PackageDescriptor> ReadPackageContentAsync()
    {
        return ReadPackageContentAsync(null, default);
    }

    /// <summary>
    ///     Reads the package contents.
    /// </summary>
    /// <param name="token">The <see cref="CancellationToken" /></param>
    /// <returns>The <see cref="PackageDescriptor" />.</returns>
    public Task<PackageDescriptor> ReadPackageContentAsync(CancellationToken token)
    {
        return ReadPackageContentAsync(null, token);
    }

    /// <summary>
    ///     Reads the package contents.
    /// </summary>
    /// <param name="progress">The <see cref="ProgressReport" /> used for progress reporting.</param>
    /// <returns>The <see cref="PackageDescriptor" />.</returns>
    public Task<PackageDescriptor> ReadPackageContentAsync(IProgress<ProgressReport>? progress)
    {
        return ReadPackageContentAsync(progress, default);
    }

    /// <summary>
    ///     Reads the package contents.
    /// </summary>
    /// <param name="progress">The <see cref="ProgressReport" /> used for progress reporting.</param>
    /// <param name="token">The <see cref="CancellationToken" />.</param>
    /// <returns>The <see cref="PackageDescriptor" />.</returns>
    public Task<PackageDescriptor> ReadPackageContentAsync(IProgress<ProgressReport>? progress, CancellationToken token)
    {
        if (token.IsCancellationRequested)
        {
            token.ThrowIfCancellationRequested();
        }

        if (SourceFile == null)
        {
            throw new FileLoadException("File not specified.");
        }

        var stream = new MemoryStream(_fileSystem.File.ReadAllBytes(SourceFile.FullName));
        try
        {
            progress?.Report(new ProgressReport($"Loading content from {SourceFile?.Name}."));
            stream.Position = 0;
            var reader = new BinaryReader(stream);
            stream.Position = ContentPosition;
            var type = reader.ReadUInt32();

            if (token.IsCancellationRequested)
            {
                token.ThrowIfCancellationRequested();
            }

            var headerSize = CalculateHeaderSize(type);
            var header = LoadHeader(reader, type, headerSize);
            if (token.IsCancellationRequested)
            {
                token.ThrowIfCancellationRequested();
            }

            LoadEntries(reader, header, headerSize);
            stream.Close();
            progress?.Report(new ProgressReport($"Loaded {this}"));
            return Task.FromResult(this);
        }
        finally
        {
            stream.Close();
        }
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{SourceFile?.Name} ({MajorVersion}/{MinorVersion}/{Contents.Count}) in {SourceFile?.DirectoryName}";
    }

    private static byte[] ReadHeader(Stream stream)
    {
        stream.Position = 0;
        var header = new BinaryReader(stream).ReadBytes(Constants.HeaderId.Length);
        stream.Close();
        return header;
    }

    private void VerifyHeader(byte[] header)
    {
        if (header.Length != Constants.HeaderId.Length)
        {
            throw new EndOfStreamException($"{SourceFile?.Name} EOF reached prematurely.");
        }

        VerifyMagicBit(header);
    }

    private void VerifyMagicBit(byte[] header)
    {
        var magicBit = new byte[4];
        Array.Copy(header, 0, magicBit, 0, magicBit.Length);
        var magicCheck = Encoding.Default.GetString(magicBit);
        if (magicCheck != Constants.HeaderBit)
        {
            throw new InvalidCastException($"{SourceFile?.Name} is NOT a valid custom content file.");
        }
    }

    private void PopulateVersionInfo(byte[] header)
    {
        MajorVersion = BitConverter.ToInt32(header, Constants.MajorStart);
        MinorVersion = BitConverter.ToInt32(header, Constants.MinorStart);
        if (MajorVersion != Constants.PackageMajor && MinorVersion != Constants.PackageMinor)
        {
            throw new VersionNotFoundException($"{MajorVersion} ({MinorVersion}) does not match expected 2 (1).");
        }
    }

    private void PopulateContentInfo(byte[] header)
    {
        ContentPosition = BitConverter.ToInt32(header, Constants.ContentPosition);
        if (ContentPosition == 0)
        {
            ContentPosition = BitConverter.ToInt32(header, Constants.ContentPositionAlternate);
        }

        if (ContentPosition == 0)
        {
            throw new KeyNotFoundException($"{SourceFile?.Name} does not contain any custom content.");
        }

        ContentCount = BitConverter.ToInt32(header, Constants.ContentCount);
        if (ContentCount == 0)
        {
            throw new KeyNotFoundException($"{SourceFile?.Name} custom content cannot be read.");
        }
    }

    private static int[] LoadHeader(BinaryReader reader, uint type, int headerSize)
    {
        var header = new int[headerSize];
        header[0] = (int) type;
        for (var i = 1; i < header.Length; i++)
        {
            header[i] = reader.ReadInt32();
        }

        return header;
    }

    private void LoadEntries(BinaryReader reader, IReadOnlyList<int> header, int headerSize)
    {
        var entry = new int[Constants.Fields - headerSize];
        for (var i = 0; i < ContentCount; i++)
        {
            for (var j = 0; j < entry.Length; j++)
            {
                entry[j] = reader.ReadInt32();
                var content = new ContentDescriptor(header, entry);
                Contents.Add(content);
            }
        }
    }

    private static int CalculateHeaderSize(uint type)
    {
        var headerCount = 1;
        for (var i = 1; i < sizeof(uint); i++)
        {
            if ((type & (1 << i)) != 0)
            {
                headerCount++;
            }
        }

        return headerCount;
    }
}
