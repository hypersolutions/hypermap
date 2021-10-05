using HyperMap.Converters;
using HyperMap.Mapping;
using HyperMap.UnitTests.Support;
using Shouldly;
using Xunit;

namespace HyperMap.UnitTests.Mapping
{
    public class ConverterMapTests
    {
        [Fact]
        public void ForPropertyInfo_Using_SetsConverter()
        {
            var mapInfo = new MapInfo();
            var converterBuilder = new ConverterMap<string, string>(mapInfo);
            
            converterBuilder.Using<DefaultTypeConverter<string>>();

            mapInfo.Converter.ShouldBe(typeof(DefaultTypeConverter<string>));
        }
        
        [Fact]
        public void ForPropertyInfo_UsingDefault_SetsConverter()
        {
            var mapInfo = new MapInfo();
            var converterBuilder = new ConverterMap<string, string>(mapInfo);
            
            converterBuilder.UsingDefault();

            mapInfo.Converter.ShouldBe(typeof(DefaultTypeConverter<string>));
        }
        
        [Fact]
        public void ForSourceAndTarget_UsingFactory_SetsConverter()
        {
            var mapInfo = new MapInfo();
            var converterBuilder = new ConverterMap<User, UserView>(mapInfo);
            
            converterBuilder.UsingFactory<User, UserView>();

            mapInfo.Converter.ShouldBe(typeof(MappingFactoryTypeConverter<User, UserView>));
        }
    }
}
