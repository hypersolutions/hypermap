using HyperMap.DependencyInjection.Tests.Support;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace HyperMap.DependencyInjection.Tests
{
    public class MappingFactoryExtensionsTests
    {
        [Fact]
        public void MappingFactoryExtensions_WithMappingFactory_RegisterWith_ReturnsMappingFactory()
        {
            var services = new ServiceCollection();
            
            var factory = MappingBuilder.DiscoverIn<User>().BuildFactory().RegisterWith(services);

            factory.ShouldNotBeNull();
        }
        
        [Fact]
        public void MappingFactoryExtensions_WithMappingFactory_RegisterWith_AddsMappersIntoCollection()
        {
            var services = new ServiceCollection();
            
            MappingBuilder.DiscoverIn<User>().BuildFactory().RegisterWith(services);

            var provider = services.BuildServiceProvider();
            var mapper2 = provider.GetService<IMapper<User, UserView>>();
            mapper2.ShouldNotBeNull();
        }
        
        [Fact]
        public void MappingFactoryExtensions_WithMappingFactory_RegisterWith_CollectionInstanceMatchesFactory()
        {
            var services = new ServiceCollection();
            
            var factory = MappingBuilder.DiscoverIn<User>().BuildFactory().RegisterWith(services);

            var mapper1 = factory.Create<User, UserView>();
            var provider = services.BuildServiceProvider();
            var mapper2 = provider.GetService<IMapper<User, UserView>>();
            mapper2.ShouldBe(mapper1);
        }
    }
}
