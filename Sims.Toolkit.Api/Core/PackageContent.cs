using System;
using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using Sims.Toolkit.Api.Core.Interfaces;
using Sims.Toolkit.Api.Enums;

namespace Sims.Toolkit.Api.Core;

[PublicAPI]
public class PackageContent : IPackageContent
{
    /// <summary>
    ///     Initializes an instance of the package content.
    /// </summary>
    /// <param name="header">A <see cref="int" /> array containing the header.</param>
    /// <param name="entry">A <see cref="int" /> array containing the entry.</param>
    public PackageContent(IReadOnlyList<int> header, IReadOnlyList<int> entry)
    {
        Item = Write(header, entry);
    }

    /// <summary>
    ///     Gets the content of the package entry.
    /// </summary>
    public byte[] Item { get; }

    public ulong Instance { get; private set; }

    public ResourceType ResourceType { get; private set; }

    public uint ResourceGroup { get; private set; }

    private byte[] Write(IReadOnlyList<int> header, IReadOnlyList<int> entry)
    {
        var content = new byte[(header.Count + entry.Count) * 4];
        var stream = new MemoryStream(content);
        var writer = new BinaryWriter(stream);
        writer.Write(header[0]);
        var headerCount = 1;
        var entryCount = 0;
        var headerGroup = (uint) header[0];
        writer.Write((headerGroup & 0x01) != 0 ? header[headerCount++] : entry[entryCount++]);
        writer.Write((headerGroup & 0x02) != 0 ? header[headerCount++] : entry[entryCount++]);
        writer.Write((headerGroup & 0x04) != 0 ? header[headerCount++] : entry[entryCount++]);
        for (; headerCount < header.Count - 1; headerCount++)
        {
            writer.Write(header[headerCount]);
        }

        for (; entryCount < entry.Count; entryCount++)
        {
            writer.Write(entry[entryCount]);
        }

        Instance = ((ulong) BitConverter.ToUInt32(content, Constants.InstanceStart) << 32) |
                   BitConverter.ToUInt32(content, Constants.InstanceStartAlternate);
        ResourceType = (ResourceType) BitConverter.ToUInt32(content, Constants.ResourceTypeStart);
        ResourceGroup = BitConverter.ToUInt32(content, Constants.ResourceGroupStart);
        return content;
    }
}
