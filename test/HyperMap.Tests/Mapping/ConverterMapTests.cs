using HyperMap.Converters;
using HyperMap.Mapping;
using HyperMap.Tests.Support;
using Shouldly;
using Xunit;

namespace HyperMap.Tests.Mapping
{
    public class ConverterMapTests
    {
        [Fact]
        public void ConverterMap_ForPropertyInfo_Using_SetsConverter()
        {
            var mapInfo = new MapInfo();
            var converterBuilder = new ConverterMap<string, string>(mapInfo);
            
            converterBuilder.Using<DefaultTypeConverter<string>>();

            mapInfo.Converter.ShouldBe(typeof(DefaultTypeConverter<string>));
        }
        
        [Fact]
        public void ConverterMap_ForPropertyInfo_UsingDefault_SetsConverter()
        {
            var mapInfo = new MapInfo();
            var converterBuilder = new ConverterMap<string, string>(mapInfo);
            
            converterBuilder.UsingDefault();

            mapInfo.Converter.ShouldBe(typeof(DefaultTypeConverter<string>));
        }
        
        [Fact]
        public void ConverterMap_ForSourceAndTarget_UsingFactory_SetsConverter()
        {
            var mapInfo = new MapInfo();
            var converterBuilder = new ConverterMap<User, UserView>(mapInfo);
            
            converterBuilder.UsingFactory<User, UserView>();

            mapInfo.Converter.ShouldBe(typeof(MappingFactoryTypeConverter<User, UserView>));
        }
    }
}
