using HyperMap.Converters;
using Shouldly;
using Xunit;

namespace HyperMap.Tests.Converters
{
    public class DefaultTypeConverterTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("Hello")]
        public void DefaultTypeConverter_WithString_Convert_ReturnsString(string source)
        {
            var converter = new DefaultTypeConverter<string>();

            var target = converter.Convert(source);
            
            target.ShouldBe(source);
        }
        
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(1)]
        public void DefaultTypeConverter_WithShort_Convert_ReturnsShort(short source)
        {
            var converter = new DefaultTypeConverter<short>();

            var target = converter.Convert(source);
            
            target.ShouldBe(source);
        }
        
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(1)]
        public void DefaultTypeConverter_WithInteger_Convert_ReturnsInteger(int source)
        {
            var converter = new DefaultTypeConverter<int>();

            var target = converter.Convert(source);
            
            target.ShouldBe(source);
        }
        
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(1)]
        public void DefaultTypeConverter_WithLong_Convert_ReturnsLong(long source)
        {
            var converter = new DefaultTypeConverter<long>();

            var target = converter.Convert(source);
            
            target.ShouldBe(source);
        }
        
        [Theory]
        [InlineData(0.0)]
        [InlineData(-1.0)]
        [InlineData(1.0)]
        public void DefaultTypeConverter_WithFloat_Convert_ReturnsFloat(float source)
        {
            var converter = new DefaultTypeConverter<float>();

            var target = converter.Convert(source);
            
            target.ShouldBe(source);
        }
        
        [Theory]
        [InlineData(0.0)]
        [InlineData(-1.0)]
        [InlineData(1.0)]
        public void DefaultTypeConverter_WithDouble_Convert_ReturnsDouble(double source)
        {
            var converter = new DefaultTypeConverter<double>();

            var target = converter.Convert(source);
            
            target.ShouldBe(source);
        }
        
        [Theory]
        [InlineData(0.0)]
        [InlineData(-1.0)]
        [InlineData(1.0)]
        public void DefaultTypeConverter_WithDecimal_Convert_ReturnsDecimal(decimal source)
        {
            var converter = new DefaultTypeConverter<decimal>();

            var target = converter.Convert(source);
            
            target.ShouldBe(source);
        }
        
        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void DefaultTypeConverter_WithBoolean_Convert_ReturnsBoolean(bool source)
        {
            var converter = new DefaultTypeConverter<bool>();

            var target = converter.Convert(source);
            
            target.ShouldBe(source);
        }
        
        [Theory]
        [InlineData(null)]
        [InlineData(false)]
        [InlineData(true)]
        public void DefaultTypeConverter_WithNullableBoolean_Convert_ReturnsBoolean(bool? source)
        {
            var converter = new DefaultTypeConverter<bool?>();

            var target = converter.Convert(source);
            
            target.ShouldBe(source);
        }
    }
}
