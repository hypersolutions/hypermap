using System.Collections.Generic;

namespace HyperMap.IntTests.Sources
{
    public class Customer
    {
        public string Name { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}
