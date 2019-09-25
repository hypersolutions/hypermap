using HyperMap.Mapping;
using HyperMap.Tests.Support;
using Shouldly;
using Xunit;

namespace HyperMap.Tests
{
    public class MappingFactoryTests
    {
        [Fact]
        public void MappingFactory_UnknownSourceAndTargetType_Create_ReturnsNull()
        {
            var factory = new MappingFactory(typeof(MapBase).Assembly);

            var mapper = factory.Create<User, UserView>();
            
            mapper.ShouldBeNull();
        }
        
        [Fact]
        public void MappingFactory_KnownSourceAndTargetType_Create_ReturnsInstance()
        {
            var factory = new MappingFactory(typeof(UserToUserViewMap).Assembly);

            var mapper = factory.Create<User, UserView>();
            
            mapper.ShouldNotBeNull();
        }
        
        [Fact]
        public void MappingFactory_KnownSourceAndTargetType_Create_ReturnsSameInstance()
        {
            var factory = new MappingFactory(typeof(UserToUserViewMap).Assembly);
            var mapper1 = factory.Create<User, UserView>();
            
            var mapper2 = factory.Create<User, UserView>();
            
            mapper1.ShouldBe(mapper2);
        }
    }
}
