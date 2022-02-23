using Microsoft.Extensions.Configuration;

namespace Sims.Api.Core;

public interface IGameBuilder
{
    IServiceProvider CreateProvider();

    IGameBuilder UseConfiguration(IConfiguration configuration);
}
