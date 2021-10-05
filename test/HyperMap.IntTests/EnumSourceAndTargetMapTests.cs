using HyperMap.IntTests.Sources;
using HyperMap.IntTests.Targets;
using Shouldly;
using Xunit;

namespace HyperMap.IntTests
{
    public class EnumSourceAndTargetMapTests
    {
        [Fact]
        public void WithSourceEnum()
        {
            var factory = MappingBuilder.DiscoverIn<Login>().BuildFactory();
            var mapper = factory.Create<Login, LoginView>();
            var source = new Login {Name = "homer.simpson", States = LoginStates.Locked};

            var target = mapper.Map(source);
            
            target.Name.ShouldBe(source.Name);
            target.State.ShouldBe(source.States.ToString());
        }
    }
}
