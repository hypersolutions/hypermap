using System.Linq;
using HyperMap.Tests.Support;
using Shouldly;
using Xunit;

namespace HyperMap.Tests.Mapping
{
    public class MapBaseTests
    {
        [Fact]
        public void MapBase_ForMapping_SourceType_IsUser()
        {
            var mapping = new UserToUserViewMap();
            
            mapping.SourceType.ShouldBe(typeof(User));
        }
        
        [Fact]
        public void MapBase_ForMapping_TargetType_IsUserView()
        {
            var mapping = new UserToUserViewMap();
            
            mapping.TargetType.ShouldBe(typeof(UserView));
        }
        
        [Fact]
        public void MapBase_ForMapping_Mappings_ContainsId()
        {
            var mapping = new UserToUserViewMap();
            
            mapping.Mappings.FirstOrDefault(m => m.Source.Name == "Id").ShouldNotBeNull();
        }
        
        [Fact]
        public void MapBase_ForMapping_Mappings_ContainsName()
        {
            var mapping = new UserToUserViewMap();
            
            mapping.Mappings.FirstOrDefault(m => m.Source.Name == "Name").ShouldNotBeNull();
        }
    }
}
