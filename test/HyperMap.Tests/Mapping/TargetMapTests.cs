using System;
using HyperMap.Mapping;
using HyperMap.Tests.Support;
using Shouldly;
using Xunit;

namespace HyperMap.Tests.Mapping
{
    public class TargetMapTests
    {
        [Fact]
        public void TargetMap_ForSource_MapTo_SetsTarget()
        {
            var sourceProperty = typeof(User).GetProperty("Id");
            var mapInfo = new MapInfo
            {
                Source = new MapItem {Name = sourceProperty?.Name, Type = sourceProperty?.PropertyType}
            };
            var targetMapping = new TargetMap<UserView, string>(mapInfo);

            targetMapping.MapTo(p => p.Id);

            mapInfo.Target.Name.ShouldBe("Id");
        }
        
        [Fact]
        public void TargetMap_ForReadOnlyTarget_MapTo_ThrowsException()
        {
            var sourceProperty = typeof(User).GetProperty("Id");
            var mapInfo = new MapInfo
            {
                Source = new MapItem {Name = sourceProperty?.Name, Type = sourceProperty?.PropertyType}
            };
            var targetMapping = new TargetMap<UserView, string>(mapInfo);

            var exception = Should.Throw<InvalidOperationException>(() => targetMapping.MapTo(p => p.HasName));
            
            exception.Message.ShouldBe("Cannot map to readonly property 'HasName'.");
        }
    }
}
