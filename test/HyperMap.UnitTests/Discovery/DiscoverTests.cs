using System.Linq;
using HyperMap.Discovery;
using HyperMap.UnitTests.Support;
using Shouldly;
using Xunit;

namespace HyperMap.UnitTests.Discovery
{
    public class DiscoverTests
    {
        [Fact]
        public void FromTypeInAssembly_FindFrom_ReturnsMappings()
        {
            var discover = new Discover();

            var mappings = discover.FindFrom<DiscoverTests>();
            
            mappings.FirstOrDefault(m => m is UserToUserViewMap).ShouldNotBeNull();
        }
    }
}
