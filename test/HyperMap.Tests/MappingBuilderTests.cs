using HyperMap.CodeGen.Compile;
using Shouldly;
using Xunit;

namespace HyperMap.Tests
{
    public class MappingBuilderTests
    {
        [Fact]
        public void MappingBuilder_WithDefaultOptions_BuildFactory_ReturnsFactory()
        {
            var factory = MappingBuilder.DiscoverIn<MappingBuilderTests>().BuildFactory();
            
            factory.ShouldNotBeNull();
        }
        
        [Fact]
        public void MappingBuilder_WithCustomNamespace_BuildFactory_ReturnsFactory()
        {
            var options = new CompilerOptions
            {
                AssemblyName = "Test.Mappings"
            };
            
            var factory = MappingBuilder.DiscoverIn<MappingBuilderTests>().BuildFactory(options);
            
            factory.ShouldNotBeNull();
        }
    }
}
