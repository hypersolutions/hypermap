using HyperMap.CodeGen;
using HyperMap.UnitTests.Support;
using Microsoft.CodeAnalysis;
using Shouldly;
using Xunit;

namespace HyperMap.UnitTests.CodeGen
{
    public class MapCodeGenTests
    {
        [Fact]
        public void ForMappingDetails_Create_ReturnsClassSignature()
        {
            var mapping = new UserToUserViewMap();

            var syntax = MapCodeGen.Create(mapping);

            syntax.NormalizeWhitespace().ToFullString().ShouldBe(
                "namespace HyperMap.Custom\r\n" +
                "{\r\n" +
                "    public class UserToUserViewMapper : IMapper<HyperMap.UnitTests.Support.User, HyperMap.UnitTests.Support.UserView>\r\n" +
                "    {\r\n" +
                "        public UserToUserViewMapper(HyperMap.IMappingFactory mappingFactory)\r\n" +
                "        {\r\n" +
                "            _converter1 = new HyperMap.Converters.DefaultTypeConverter<System.Int32>();\r\n" +
                "            _converter1.MappingFactory = mappingFactory;\r\n" +
                "            _converter2 = new HyperMap.Converters.DefaultTypeConverter<System.String>();\r\n" +
                "            _converter2.MappingFactory = mappingFactory;\r\n" +
                "        }\r\n\r\n" +
                "        private readonly HyperMap.Converters.DefaultTypeConverter<System.Int32> _converter1;\r\n" +
                "        private readonly HyperMap.Converters.DefaultTypeConverter<System.String> _converter2;\r\n" +
                "        public HyperMap.UnitTests.Support.UserView Map(HyperMap.UnitTests.Support.User source)\r\n" +
                "        {\r\n" +
                "            if (source == null)\r\n" + 
                "                return default(HyperMap.UnitTests.Support.UserView);\r\n" +
                "            var target = new HyperMap.UnitTests.Support.UserView();\r\n" +
                "            target.Id = _converter1.Convert(source.Id);\r\n" +
                "            target.Name = _converter2.Convert(source.Name);\r\n" +
                "            return target;\r\n" +
                "        }\r\n" +
                "    }\r\n" + 
                "}");
        }
    }
}
