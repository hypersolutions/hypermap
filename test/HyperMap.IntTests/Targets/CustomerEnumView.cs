using System.Collections.Generic;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace HyperMap.IntTests.Targets
{
    public class CustomerEnumView
    {
        public string Name { get; set; }
        public IEnumerable<OrderView> Orders { get; set; }
    }
}
