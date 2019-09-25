using HyperMap.Converters.Strings;
using Shouldly;
using Xunit;

namespace HyperMap.Tests.Converters.Strings
{
    public class StringToBooleanTypeConverterTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("Hello")]
        public void StringToBooleanTypeConverter_WithInvalidString_Convert_ReturnsFalse(string source)
        {
            var converter = new StringToBooleanTypeConverter();

            var target = converter.Convert(source);
            
            target.ShouldBeFalse();
        }
        
        [Theory]
        [InlineData("0")]
        [InlineData("False")]
        [InlineData("FALSE")]
        [InlineData("false")]
        public void StringToBooleanTypeConverter_WithString_Convert_ReturnsFalse(string source)
        {
            var converter = new StringToBooleanTypeConverter();

            var target = converter.Convert(source);
            
            target.ShouldBeFalse();
        }
        
        [Theory]
        [InlineData("True")]
        [InlineData("TRUE")]
        [InlineData("true")]
        public void StringToBooleanTypeConverter_WithString_Convert_ReturnsTrue(string source)
        {
            var converter = new StringToBooleanTypeConverter();

            var target = converter.Convert(source);
            
            target.ShouldBeTrue();
        }
    }
}
