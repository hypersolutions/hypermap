using HyperMap.Integration.Tests.Sources;
using HyperMap.Integration.Tests.Targets;
using Shouldly;
using Xunit;

namespace HyperMap.Integration.Tests
{
    public class FlatSourceToTargetMapTests
    {
        [Fact]
        public void WithDefaultConverters()
        {
            var factory = MappingBuilder.DiscoverIn<Address>().BuildFactory();
            var mapper = factory.Create<Address, AddressView>();
            var source = new Address
            {
                HouseNumber = 742,
                Street = "Evergreen Terrace",
                Town = "Springfield"
            };

            var target = mapper.Map(source);
            
            target.HouseNumber.ShouldBe(source.HouseNumber.ToString());
            target.Street.ShouldBe(source.Street);
            target.Town.ShouldBe(source.Town);
        }
        
        [Fact]
        public void WithCustomConverter()
        {
            var factory = MappingBuilder.DiscoverIn<Address>().BuildFactory();
            var mapper = factory.Create<Address, SingleLineAddressView>();
            var source = new Address
            {
                HouseNumber = 742,
                Street = "Evergreen Terrace",
                Town = "Springfield"
            };

            var target = mapper.Map(source);
            
            target.Display.ShouldBe("742 Evergreen Terrace, Springfield");
        }
    }
}
