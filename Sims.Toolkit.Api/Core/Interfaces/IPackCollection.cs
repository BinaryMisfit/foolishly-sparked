using System.Collections.Generic;
using Sims.Toolkit.Api.Enums;

namespace Sims.Toolkit.Api.Core.Interfaces;

public interface IPackCollection : IList<IPack>
{
    IEnumerable<KeyValuePair<PackType, int>> Summary();
}
