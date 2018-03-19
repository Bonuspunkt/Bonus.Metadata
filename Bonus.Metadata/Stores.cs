
using System;
using System.Collections.Generic;

namespace Bonus
{
    internal class MetadataStore : Dictionary<Type, MetaEntityStore>
    { }

    internal class MetaEntityStore : Dictionary<string, object>
    { }
}