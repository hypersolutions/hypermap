using HyperMap.CodeGen;
using HyperMap.Converters;
using Shouldly;
using Xunit;

namespace HyperMap.Tests.CodeGen
{
    public class ConverterTypeTests
    {
        [Fact]
        public void SingleTypeArg_Create_ReturnsTypeSyntax()
        {
            var typeArgs = new []{typeof(string)};
            var genericConverterType = typeof(DefaultTypeConverter<>);
            var stringConverterType = genericConverterType.MakeGenericType(typeArgs);

            var syntax = ConverterType.Create(stringConverterType);
            
            syntax.ToFullString().ShouldBe("HyperMap.Converters.DefaultTypeConverter<System.String>");
        }
        
        [Fact]
        public void MultipleTypeArgs_Create_ReturnsTypeSyntax()
        {
            var typeArgs = new []{typeof(string), typeof(int)};
            var genericConverterType = typeof(ITypeConverter<,>);
            var stringConverterType = genericConverterType.MakeGenericType(typeArgs);

            var syntax = ConverterType.Create(stringConverterType);
            
            syntax.ToFullString().ShouldBe("HyperMap.Converters.ITypeConverter<System.String,System.Int32>");
        }
    }
}
