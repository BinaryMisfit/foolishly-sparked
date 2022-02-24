using System.Globalization;
using System.IO.Abstractions;

namespace Foolishly.Sparked.Core;

internal static class PackCollectionExtensions
{
    internal static IPackCollection LoadPacks(
        this IPackCollection pack,
        PackTypes packTypes,
        IEnumerable<IDirectoryInfo> directories,
        IProgress<AsyncProgressReport>? progress)
    {
        if (!PackTypesMappings.PackTypeFolders.TryGetValue(packTypes, out var folderKey))
        {
            return pack;
        }

        if (string.IsNullOrEmpty(folderKey))
        {
            return pack;
        }

        var findPacks = directories.Where(directory => !GameFileMap.IgnoreGameFolders.Contains(directory.Parent.Name))
            .Where(directory => directory.Name.StartsWith(folderKey, StringComparison.InvariantCulture))
            .ToList();
        findPacks.OrderBy(directory => directory.Name)
            .ToList()
            .ForEach(
                directory =>
                {
                    var packFiles = directory.GetFiles(GameFileMap.FilesClientPackage, SearchOption.TopDirectoryOnly);
                    if (!packFiles.Any())
                    {
                        return;
                    }

                    var gamePack = new PackDescriptor(directory.Name) {Path = directory};
                    progress?.Report(
                        new AsyncProgressReport(
                            string.Format(
                                CultureInfo.CurrentCulture,
                                "Adding {0}: {1}",
                                PackTypesMappings.PackTypeName[gamePack.PackTypes],
                                gamePack.PackName)));
                    pack.Add(gamePack);
                });
        return pack;
    }
}
