using HyperMap.Converters.Strings;
using Shouldly;
using Xunit;

namespace HyperMap.Tests.Converters.Strings
{
    public class StringToShortTypeConverterTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("Hello")]
        public void StringToShortTypeConverter_WithInvalidString_Convert_ReturnsZero(string source)
        {
            var converter = new StringToShortTypeConverter();

            var target = converter.Convert(source);
            
            target.ShouldBe((short)0);
        }
        
        [Theory]
        [InlineData("0")]
        [InlineData("1")]
        [InlineData("-1")]
        public void StringToShortTypeConverter_WithString_Convert_ReturnsShort(string source)
        {
            var converter = new StringToShortTypeConverter();

            var target = converter.Convert(source);
            
            target.ShouldBe(short.Parse(source));
        }
    }
}
