using HyperMap.Converters.Strings;
using Shouldly;
using Xunit;

namespace HyperMap.Tests.Converters.Strings
{
    public class AnyToStringTypeConverterTests
    {
        [Fact]
        public void AnyToStringTypeConverter_WithNull_Convert_ReturnsNullString()
        {
            var converter = new AnyToStringTypeConverter<ObjectClass>();

            var target = converter.Convert(null);
            
            target.ShouldBeNull();
        }
        
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("Hello")]
        public void AnyToStringTypeConverter_WithString_Convert_ReturnsNullString(string source)
        {
            var converter = new AnyToStringTypeConverter<string>();

            var target = converter.Convert(source);
            
            target.ShouldBe(source);
        }
        
        [Fact]
        public void AnyToStringTypeConverter_WithObject_Convert_ReturnsStringOfObject()
        {
            var converter = new AnyToStringTypeConverter<ObjectClass>();

            var target = converter.Convert(new ObjectClass());
            
            target.ShouldBe("Hello");
        }

        private class ObjectClass
        {
            public override string ToString() => "Hello";
        }
    }
}
