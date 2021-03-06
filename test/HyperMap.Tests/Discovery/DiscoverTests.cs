using System.Linq;
using HyperMap.Discovery;
using HyperMap.Tests.Support;
using Shouldly;
using Xunit;

namespace HyperMap.Tests.Discovery
{
    public class DiscoverTests
    {
        [Fact]
        public void Discover_FromTypeInAssembly_FindFrom_ReturnsMappings()
        {
            var discover = new Discover();

            var mappings = discover.FindFrom<DiscoverTests>();
            
            mappings.FirstOrDefault(m => m is UserToUserViewMap).ShouldNotBeNull();
        }
    }
}
