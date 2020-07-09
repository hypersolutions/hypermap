using HyperMap.Converters;
using HyperMap.Tests.Support;
using Moq;
using Shouldly;
using Xunit;

namespace HyperMap.Tests.Converters
{
    public class MappingFactoryTypeConverterTests
    {
        private readonly MappingFactoryTypeConverter<User, UserView> _subject;
        private readonly Mock<IMappingFactory> _mappingFactory;
        
        public MappingFactoryTypeConverterTests()
        {
            _mappingFactory = new Mock<IMappingFactory>();
            _subject = new MappingFactoryTypeConverter<User, UserView> {MappingFactory = _mappingFactory.Object};
        }
        
        [Fact]
        public void UnknownMapper_Convert_ReturnsNull()
        {
            var source = new User {Id = 1, Name = "Homer"};
            _mappingFactory.Setup(f => f.Create<User, UserView>()).Returns((IMapper<User, UserView>)null);
            
            var target = _subject.Convert(source);
            
            target.ShouldBeNull();
        }
        
        [Fact]
        public void KnownMapper_Convert_ReturnsMappedTarget()
        {
            var source = new User {Id = 1, Name = "Homer"};
            var mapper = new Mock<IMapper<User, UserView>>();
            mapper.Setup(m => m.Map(source)).Returns(new UserView {Id = 1, Name = "Homer"});
            _mappingFactory.Setup(f => f.Create<User, UserView>()).Returns(mapper.Object);
            
            var target = _subject.Convert(source);
            
            target.Id.ShouldBe(source.Id);
            target.Name.ShouldBe(source.Name);
        }
    }
}
