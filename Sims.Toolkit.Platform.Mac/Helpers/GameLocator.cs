﻿using System.Composition;
using System.Runtime.InteropServices;
using JetBrains.Annotations;
using Sims.Toolkit.Api.Plugin.Interfaces;

namespace Sims.Toolkit.Platform.Mac.Helpers;

/// <summary>
///     Contains and stores game specific information.
/// </summary>
[Export(typeof(IPlatform))]
[PublicAPI]
public class GameLocator : IPlatform
{
    private const string GlobalPath = "/Applications/The Sims 4.app";

    private readonly string _userPath =
        Path.Join(Environment.GetEnvironmentVariable("HOME"), "/Applications/The Sims 4.app");

    public GameLocator()
    {
        Is64 = RuntimeInformation.OSArchitecture.HasFlag(Architecture.X64) ||
               RuntimeInformation.OSArchitecture.HasFlag(Architecture.Arm64);
        Platform = RuntimeInformation.RuntimeIdentifier;
        InstalledPath = GlobalPath;
    }

    public bool Is64 { get; }

    public string Platform { get; }

    public string InstalledPath { get; private set; }

    public Task<IPlatform> LocateGameAsync()
    {
        var gameFile = new FileInfo(GlobalPath);
        if (!gameFile.Exists)
        {
            gameFile = new FileInfo(_userPath);
        }

        if (gameFile.Directory == null)
        {
            throw new FileNotFoundException("Cannot locate an installed version of Sims 4");
        }

        if (!gameFile.Directory.Exists)
        {
            throw new FileNotFoundException("Cannot locate an installed version of Sims 4");
        }

        InstalledPath = gameFile.Directory.FullName;
        return Task.FromResult((IPlatform) this);
    }
}