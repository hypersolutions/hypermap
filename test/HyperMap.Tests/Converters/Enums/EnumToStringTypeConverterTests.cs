using System;
using System.IO;
using HyperMap.Converters.Enums;
using Shouldly;
using Xunit;

namespace HyperMap.Tests.Converters.Enums
{
    public class EnumToStringTypeConverterTests
    {
        [Fact]
        public void EnumToStringTypeConverter_InvalidEnum_Convert_ThrowsException()
        {
            var converter = new EnumToStringTypeConverter<int>();

            var error = Should.Throw<ArgumentException>(() => converter.Convert(10));

            error.Message.ShouldBe("TSource must be an enum type.");
        }

        [Fact]
        public void EnumToStringTypeConverter_ValidEnum_Convert_ReturnsString()
        {
            var converter = new EnumToStringTypeConverter<FileMode>();

            var target = converter.Convert(FileMode.Create);

            target.ShouldBe("Create");
        }
    }
}
