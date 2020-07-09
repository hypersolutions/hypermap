using HyperMap.Converters.Strings;
using Shouldly;
using Xunit;

namespace HyperMap.Tests.Converters.Strings
{
    public class StringToLongTypeConverterTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("Hello")]
        public void WithInvalidString_Convert_ReturnsZero(string source)
        {
            var converter = new StringToLongTypeConverter();

            var target = converter.Convert(source);
            
            target.ShouldBe(0);
        }
        
        [Theory]
        [InlineData("0")]
        [InlineData("1")]
        [InlineData("-1")]
        public void WithString_Convert_ReturnsLong(string source)
        {
            var converter = new StringToLongTypeConverter();

            var target = converter.Convert(source);
            
            target.ShouldBe(long.Parse(source));
        }
    }
}
