using HyperMap.Converters.Strings;
using Shouldly;
using Xunit;

namespace HyperMap.Tests.Converters.Strings
{
    public class StringToIntegerTypeConverterTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("Hello")]
        public void StringToIntegerTypeConverter_WithInvalidString_Convert_ReturnsZero(string source)
        {
            var converter = new StringToIntegerTypeConverter();

            var target = converter.Convert(source);
            
            target.ShouldBe(0);
        }
        
        [Theory]
        [InlineData("0")]
        [InlineData("1")]
        [InlineData("-1")]
        public void StringToIntegerTypeConverter_WithString_Convert_ReturnsInteger(string source)
        {
            var converter = new StringToIntegerTypeConverter();

            var target = converter.Convert(source);
            
            target.ShouldBe(int.Parse(source));
        }
    }
}
