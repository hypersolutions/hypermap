using System.Collections.Generic;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace HyperMap.IntTests.Targets
{
    public class CustomerListView
    {
        public string Name { get; set; }
        public List<OrderView> Orders { get; set; }
    }
}
