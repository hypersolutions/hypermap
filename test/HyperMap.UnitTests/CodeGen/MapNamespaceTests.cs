using HyperMap.CodeGen;
using HyperMap.UnitTests.Support;
using Microsoft.CodeAnalysis;
using Shouldly;
using Xunit;

namespace HyperMap.UnitTests.CodeGen
{
    public class MapNamespaceTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void NoNamespaceProvided_Create_ReturnsDefaultNamespace(string namespaceName)
        {
            var mapping = new UserToUserViewMap {Namespace = namespaceName};
            
            var syntax = MapNamespace.Create(mapping); 
            
            syntax.NormalizeWhitespace().ToFullString().ShouldBe("namespace HyperMap.Custom\r\n{\r\n}");
        }
    }
}
