using System.Linq;
using HyperMap.CodeGen;
using HyperMap.Converters;
using Microsoft.CodeAnalysis;
using Shouldly;
using Xunit;

namespace HyperMap.UnitTests.CodeGen
{
    public class ConverterFieldInitTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void ForConverterIndex_Create_ReturnsInitializedConverter(int converterTypeIndex)
        {
            var typeArgs = new []{typeof(string)};
            var genericConverterType = typeof(DefaultTypeConverter<>);
            var stringConverterType = genericConverterType.MakeGenericType(typeArgs);

            var syntaxList = ConverterFieldInit.Create(stringConverterType, converterTypeIndex).ToList();

            syntaxList[0].NormalizeWhitespace().ToFullString().ShouldBe(
                $"_converter{converterTypeIndex + 1} = new HyperMap.Converters.DefaultTypeConverter<System.String>();");
            syntaxList[1].NormalizeWhitespace().ToFullString().ShouldBe(
                $"_converter{converterTypeIndex + 1}.MappingFactory = mappingFactory;");
        }
    }
}
