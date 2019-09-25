using HyperMap.CodeGen;
using HyperMap.Mapping;
using HyperMap.Tests.Support;
using Microsoft.CodeAnalysis;
using Shouldly;
using Xunit;

namespace HyperMap.Tests.CodeGen
{
    public class TargetPropertyMapFromSourceTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void TargetPropertyMapFromSource_ForPropertyMap_Create_ReturnsConvertStatement(int converterTypeIndex)
        {
            var source = new MapItem {Name = "Id", Type = typeof(string)};
            var target = new MapItem {Name = "Id", Type = typeof(string)};

            var syntax = TargetPropertyMapFromSource.Create(source, target, converterTypeIndex);
            
            syntax.NormalizeWhitespace().ToFullString().ShouldBe(
                $"target.Id = _converter{converterTypeIndex + 1}.Convert(source.Id);");
        }
        
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void TargetPropertyMapFromSource_ForObjectMap_Create_ReturnsConvertStatement(int converterTypeIndex)
        {
            var source = new MapItem {Type = typeof(User)};
            var target = new MapItem {Name = "Id", Type = typeof(string)};

            var syntax = TargetPropertyMapFromSource.Create(source, target, converterTypeIndex);
            
            syntax.NormalizeWhitespace().ToFullString().ShouldBe(
                $"target.Id = _converter{converterTypeIndex + 1}.Convert(source);");
        }
    }
}
