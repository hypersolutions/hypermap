using System.Linq;
using HyperMap.UnitTests.Support;
using Shouldly;
using Xunit;

namespace HyperMap.UnitTests.Mapping
{
    public class MapBaseTests
    {
        [Fact]
        public void ForMapping_SourceType_IsUser()
        {
            var mapping = new UserToUserViewMap();
            
            mapping.SourceType.ShouldBe(typeof(User));
        }
        
        [Fact]
        public void ForMapping_TargetType_IsUserView()
        {
            var mapping = new UserToUserViewMap();
            
            mapping.TargetType.ShouldBe(typeof(UserView));
        }
        
        [Fact]
        public void ForMapping_Mappings_ContainsId()
        {
            var mapping = new UserToUserViewMap();
            
            mapping.Mappings.FirstOrDefault(m => m.Source.Name == "Id").ShouldNotBeNull();
        }
        
        [Fact]
        public void ForMapping_Mappings_ContainsName()
        {
            var mapping = new UserToUserViewMap();
            
            mapping.Mappings.FirstOrDefault(m => m.Source.Name == "Name").ShouldNotBeNull();
        }
    }
}
