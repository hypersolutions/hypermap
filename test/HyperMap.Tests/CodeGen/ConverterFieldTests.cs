using HyperMap.CodeGen;
using HyperMap.Converters;
using Microsoft.CodeAnalysis;
using Shouldly;
using Xunit;

namespace HyperMap.Tests.CodeGen
{
    public class ConverterFieldTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void ConverterField_ZeroIndex_Create_ReturnsConverter1(int converterTypeIndex)
        {
            var typeArgs = new []{typeof(string)};
            var genericConverterType = typeof(DefaultTypeConverter<>);
            var stringConverterType = genericConverterType.MakeGenericType(typeArgs);

            var syntax = ConverterField.Create(stringConverterType, converterTypeIndex);
            
            syntax.NormalizeWhitespace().ToFullString().ShouldBe(
                "private readonly HyperMap.Converters.DefaultTypeConverter" +
                $"<System.String> _converter{converterTypeIndex + 1};");
        }
    }
}
