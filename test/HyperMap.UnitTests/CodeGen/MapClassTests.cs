using System.Linq;
using HyperMap.CodeGen;
using HyperMap.Converters;
using HyperMap.Mapping;
using HyperMap.UnitTests.Support;
using Microsoft.CodeAnalysis;
using Shouldly;
using Xunit;

namespace HyperMap.UnitTests.CodeGen
{
    public class MapClassTests
    {
        [Fact]
        public void ForMappingDetails_Create_ReturnsClassSignature()
        {
            var sourceType = typeof(User);
            var targetType = typeof(UserView);
            var converterTypes = new[] {typeof(DefaultTypeConverter<string>)};
            var mappings = new[]
            {
                new MapInfo
                {
                    Source = new MapItem {Name = "Name", Type = typeof(string)},
                    Target = new MapItem {Name = "Name", Type = typeof(string)}, 
                    Converter = converterTypes.First()
                }
            };

            var syntax = MapClass.Create(sourceType, targetType, mappings, converterTypes);

            syntax.NormalizeWhitespace().ToFullString().ShouldBe(
                "public class UserToUserViewMapper : IMapper<HyperMap.UnitTests.Support.User, HyperMap.UnitTests.Support.UserView>\r\n" +
                "{\r\n" +
                "    public UserToUserViewMapper(HyperMap.IMappingFactory mappingFactory)\r\n" +
                "    {\r\n" +
                "        _converter1 = new HyperMap.Converters.DefaultTypeConverter<System.String>();\r\n" +
                "        _converter1.MappingFactory = mappingFactory;\r\n" +
                "    }\r\n\r\n" +
                "    private readonly HyperMap.Converters.DefaultTypeConverter<System.String> _converter1;\r\n" +
                "    public HyperMap.UnitTests.Support.UserView Map(HyperMap.UnitTests.Support.User source)\r\n" +
                "    {\r\n" +
                "        if (source == null)\r\n" + 
                "            return default(HyperMap.UnitTests.Support.UserView);\r\n" +
                "        var target = new HyperMap.UnitTests.Support.UserView();\r\n" +
                "        target.Name = _converter1.Convert(source.Name);\r\n" +
                "        return target;\r\n" +
                "    }\r\n" +
                "}");
        }
    }
}
