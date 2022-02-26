using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Globalization;
using System.IO;
using Foolishly.Sparked.Core;
using Foolishly.Sparked.Core.Properties;

Console.WriteLine(
    string.Format(
        CultureInfo.CurrentCulture,
        OutputMessages.TitleDisplay,
        OutputMessages.ApplicationTitle,
        OutputMessages.TerminalTitle));
var installPathOption = new Option<DirectoryInfo>("--installPath") {IsRequired = true}.ExistingOnly();
var contentPathOption = new Option<DirectoryInfo>("--contentPath") {IsRequired = true}.ExistingOnly();
var root = new RootCommand {installPathOption, contentPathOption};
root.SetHandler(
    (DirectoryInfo installPath, DirectoryInfo contentPath) =>
    {
        var game = GameBuilder.CreateDefaultGameBuilder()
            .WithOptions(
                new Dictionary<string, string>
                {
                    {"Game:InstallPath", installPath.FullName}, {"Game:ContentPath", contentPath.FullName}
                })
            .Configure()
            .Build();
        Console.WriteLine(game);
    },
    installPathOption,
    contentPathOption);
root.InvokeAsync(args);
