using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using Sims.Toolkit.Api.Core;
using Sims.Toolkit.Api.Core.Interfaces;
using Sims.Toolkit.Api.Enums;
using Sims.Toolkit.Api.Helpers;

namespace Sims.Toolkit.Api.Extensions;

internal static class PackCollectionExtensions
{
    internal static IPackCollection LoadPacks(
        this IPackCollection pack,
        PackType packType,
        IEnumerable<IDirectoryInfo> directories,
        IProgress<ProgressReport>? progress)
    {
        if (!Dictionaries.PackTypeFolders.TryGetValue(packType, out var folderKey))
        {
            return pack;
        }

        if (string.IsNullOrEmpty(folderKey))
        {
            return pack;
        }

        var findPacks = directories.Where(directory => !Constants.IgnoreGameFolders.Contains(directory.Parent.Name))
            .Where(directory => directory.Name.StartsWith(folderKey, StringComparison.InvariantCulture))
            .ToList();
        findPacks.OrderBy(directory => directory.Name)
            .ToList()
            .ForEach(
                directory =>
                {
                    var packFiles = directory.GetFiles(Constants.ClientFiles, SearchOption.TopDirectoryOnly);
                    if (!packFiles.Any())
                    {
                        return;
                    }

                    var gamePack = new Pack(directory.Name) {Path = directory};
                    progress?.Report(
                        new ProgressReport(
                            string.Format(
                                CultureInfo.CurrentCulture,
                                "Adding {0}: {1}",
                                Dictionaries.PackTypeName[gamePack.PackType],
                                gamePack.PackName)));
                    pack.Add(gamePack);
                });
        return pack;
    }
}
