using System.Linq;
using HyperMap.Mapping;
using HyperMap.Tests.Support;
using Shouldly;
using Xunit;

namespace HyperMap.Tests
{
    public class MappingFactoryTests
    {
        [Fact]
        public void UnknownSourceAndTargetType_Create_ReturnsNull()
        {
            var factory = new MappingFactory(typeof(MapBase).Assembly);

            var mapper = factory.Create<User, UserView>();
            
            mapper.ShouldBeNull();
        }
        
        [Fact]
        public void KnownSourceAndTargetType_Create_ReturnsInstance()
        {
            var factory = new MappingFactory(typeof(UserToUserViewMap).Assembly);

            var mapper = factory.Create<User, UserView>();
            
            mapper.ShouldNotBeNull();
        }
        
        [Fact]
        public void KnownSourceAndTargetType_Create_ReturnsSameInstance()
        {
            var factory = new MappingFactory(typeof(UserToUserViewMap).Assembly);
            var mapper1 = factory.Create<User, UserView>();
            
            var mapper2 = factory.Create<User, UserView>();
            
            mapper1.ShouldBe(mapper2);
        }
        
        [Fact]
        public void ForAllMappings_GetAll_ReturnsList()
        {
            var factory = new MappingFactory(typeof(UserToUserViewMap).Assembly);

            var mappings = factory.GetAll();
            
            mappings.Count().ShouldBe(1);
        }
        
        [Fact]
        public void ForAllMappings_GetAll_ReturnsSameInstanceAsCreate()
        {
            var factory = new MappingFactory(typeof(UserToUserViewMap).Assembly);
            var mapping1 = factory.Create<User, UserView>();
            
            var mappings = factory.GetAll();
            
            var mapping2 = mappings.First();
            mapping2.ShouldBe(mapping1);
        }
    }
}
