using HyperMap.CodeGen;
using HyperMap.Tests.Support;
using Microsoft.CodeAnalysis;
using Shouldly;
using Xunit;

namespace HyperMap.Tests.CodeGen
{
    public class MapNamespaceTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void MapNamespace_NoNamespaceProvided_Create_ReturnsDefaultNamespace(string namespaceName)
        {
            var mapping = new UserToUserViewMap {Namespace = namespaceName};
            
            var syntax = MapNamespace.Create(mapping); 
            
            syntax.NormalizeWhitespace().ToFullString().ShouldBe("namespace HyperMap.Custom\r\n{\r\n}");
        }
    }
}
