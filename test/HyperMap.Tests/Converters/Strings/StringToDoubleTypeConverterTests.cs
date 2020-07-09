using HyperMap.Converters.Strings;
using Shouldly;
using Xunit;

namespace HyperMap.Tests.Converters.Strings
{
    public class StringToDoubleTypeConverterTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("Hello")]
        public void WithInvalidString_Convert_ReturnsZero(string source)
        {
            var converter = new StringToDoubleTypeConverter();

            var target = converter.Convert(source);
            
            target.ShouldBe(0);
        }
        
        [Theory]
        [InlineData("0.0")]
        [InlineData("1.0")]
        [InlineData("-1.0")]
        public void WithString_Convert_ReturnsDouble(string source)
        {
            var converter = new StringToDoubleTypeConverter();

            var target = converter.Convert(source);
            
            target.ShouldBe(double.Parse(source));
        }
    }
}
