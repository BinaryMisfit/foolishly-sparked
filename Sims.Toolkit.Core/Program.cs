using System;
using System.CommandLine;
using System.IO;
using System.Linq;
using Sims.Toolkit.Api.Core;
using Sims.Toolkit.Api.Helpers;
using Sims.Toolkit.Core.Tools.Xmods.DataLib;

namespace Sims.Toolkit.Core
{
    internal static class Program
    {
        private static int Main(string[] args)
        {
            var packageArgument = new Argument<FileInfo>(
                "PackageFile",
                "Full path to the .package file."
            ).ExistingOnly();
            var commandLine = new RootCommand
            {
                packageArgument
            };
            commandLine.Description = "Configures and updates Sims 4 .package files for compatibility.";
            commandLine.SetHandler(async (FileInfo packageFile) =>
            {
                Console.WriteLine($"Reading package file: {packageFile.Name}");
                try
                {
                    var progress = new Progress<ProgressReport>();
                    progress.ProgressChanged += (_, e) => { Console.WriteLine(e.Message); };
                    var pack = new Package(packageFile);
                    await pack.LoadPackageAsync(progress);
                    var package = (Tools.Packages.Package) Tools.Packages.Package.OpenPackage(0,
                        packageFile.FullName,
                        true);
                    var packageCasParts =
                        package.FindAll(part =>
                            part.ResourceType == (ulong) XmodsEnums.ResourceTypes.CASP);
                    if (!packageCasParts.Any())
                    {
                        throw new NotSupportedException("No CAS parts found.");
                    }

                    packageCasParts.ForEach(part =>
                    {
                        var casStream = package.GetResource(part);
                        casStream.Position = 0;
                        var casReader = new BinaryReader(casStream);
                        var casPart = new CASP(casReader);
                    });

                    Console.WriteLine($"{packageFile.Name} loaded successfully.");
                }
                catch (NotSupportedException)
                {
                    Console.WriteLine($"{packageFile.Name} does not contain any CAS parts.");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{e.GetType().Name}: {e.Message}");
                }
            }, packageArgument);
            return commandLine.InvokeAsync(args).Result;
        }
    }
}
