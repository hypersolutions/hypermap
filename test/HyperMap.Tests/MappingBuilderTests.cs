using System;
using HyperMap.CodeGen.Compile;
using Shouldly;
using Xunit;

namespace HyperMap.Tests
{
    public class MappingBuilderTests
    {
        [Fact]
        public void SameAssemblyAddedTwice_AndDiscoverIn_ThrowsException()
        {
            var exception = Should.Throw<ArgumentException>(() =>
                MappingBuilder.DiscoverIn<MappingBuilderTests>().AndDiscoverIn<MappingFactoryTests>());
            
            exception.Message.ShouldStartWith("Already added mappings in assembly HyperMap.Tests");
        }
        
        [Fact]
        public void WithDefaultOptions_BuildFactory_ReturnsFactory()
        {
            var factory = MappingBuilder.DiscoverIn<MappingBuilderTests>().BuildFactory();
            
            factory.ShouldNotBeNull();
        }
        
        [Fact]
        public void WithCustomNamespace_BuildFactory_ReturnsFactory()
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
