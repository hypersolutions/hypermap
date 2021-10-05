using HyperMap.CodeGen.Compile;
using Shouldly;
using Xunit;

namespace HyperMap.UnitTests.CodeGen.Compile
{
    public class CompilerOptionsTests
    {
        [Fact]
        public void NoAssemblyNameSet_AssemblyName_ReturnsDefault()
        {
            var options = new CompilerOptions();
            
            options.AssemblyName.ShouldBe("HyperMap.Custom.Mappings");
        }
        
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void InvalidAssemblyName_AssemblyName_ReturnsDefault(string assemblyName)
        {
            var options = new CompilerOptions {AssemblyName = assemblyName};
            
            options.AssemblyName.ShouldBe("HyperMap.Custom.Mappings");
        }
        
        [Theory]
        [InlineData("MyMappings")]
        [InlineData("HyperMap.Mappings")]
        public void ValidAssemblyName_AssemblyName_ReturnsSelected(string assemblyName)
        {
            var options = new CompilerOptions {AssemblyName = assemblyName};
            
            options.AssemblyName.ShouldBe(assemblyName);
        }
    }
}