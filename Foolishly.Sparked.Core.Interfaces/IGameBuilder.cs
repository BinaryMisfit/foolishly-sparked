using System;
using System.Collections.Generic;

namespace Foolishly.Sparked.Core;

public interface IGameBuilder
{
    IGameBuilder Configure();

    IServiceProvider CreateProvider();

    IGameBuilder WithOptions(Dictionary<string, string> options);
}
