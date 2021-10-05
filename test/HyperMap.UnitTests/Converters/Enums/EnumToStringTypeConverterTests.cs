using System;
using System.IO;
using HyperMap.Converters.Enums;
using Shouldly;
using Xunit;

namespace HyperMap.UnitTests.Converters.Enums
{
    public class EnumToStringTypeConverterTests
    {
        [Fact]
        public void InvalidEnum_Convert_ThrowsException()
        {
            var converter = new EnumToStringTypeConverter<int>();

            var error = Should.Throw<ArgumentException>(() => converter.Convert(10));

            error.Message.ShouldBe("TSource must be an enum type.");
        }

        [Fact]
        public void ValidEnum_Convert_ReturnsString()
        {
            var converter = new EnumToStringTypeConverter<FileMode>();

            var target = converter.Convert(FileMode.Create);

            target.ShouldBe("Create");
        }
    }
}
