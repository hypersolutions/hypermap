using System.Collections.Generic;

namespace HyperMap.Integration.Tests.Sources
{
    public class Customer
    {
        public string Name { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}
