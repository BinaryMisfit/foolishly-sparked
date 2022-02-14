using System.Collections.Generic;
using Sims.Toolkit.Api.Enums;

namespace Sims.Toolkit.Api.Core.Interfaces;

public interface IPackageContentCollection : IList<IPackageContent>
{
    IEnumerable<KeyValuePair<ResourceType, int>> Summary();
}
