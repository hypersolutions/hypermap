using HyperMap.Converters.Strings;
using Shouldly;
using Xunit;

namespace HyperMap.UnitTests.Converters.Strings
{
    public class StringToFloatTypeConverterTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("Hello")]
        public void WithInvalidString_Convert_ReturnsZero(string source)
        {
            var converter = new StringToFloatTypeConverter();

            var target = converter.Convert(source);
            
            target.ShouldBe(0);
        }
        
        [Theory]
        [InlineData("0.0")]
        [InlineData("1.0")]
        [InlineData("-1.0")]
        public void WithString_Convert_ReturnsFloat(string source)
        {
            var converter = new StringToFloatTypeConverter();

            var target = converter.Convert(source);
            
            target.ShouldBe(float.Parse(source));
        }
    }
}
