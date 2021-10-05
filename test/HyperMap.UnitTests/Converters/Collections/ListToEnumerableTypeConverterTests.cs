using System.Collections.Generic;
using System.Linq;
using HyperMap.Converters.Collections;
using HyperMap.UnitTests.Support;
using Moq;
using Shouldly;
using Xunit;

namespace HyperMap.UnitTests.Converters.Collections
{
    public class ListToEnumerableTypeConverterTests
    {
        [Fact]
        public void NullSource_Convert_ReturnsNull()
        {
            var converter = new ListToEnumerableTypeConverter<User, UserView>();

            var target = converter.Convert(null);
            
            target.ShouldBeNull();
        }
        
        [Fact]
        public void EmptySource_Convert_ReturnsNull()
        {
            var converter = new ListToEnumerableTypeConverter<User, UserView>();

            var target = converter.Convert(new List<User>(0));
            
            target.ShouldBeNull();
        }
        
        [Fact]
        public void UnknownTarget_Convert_ReturnsNull()
        {
            var mappingFactory = new Mock<IMappingFactory>();
            mappingFactory.Setup(f => f.Create<User, UserView>()).Returns((IMapper<User, UserView>) null);
            var converter = new ListToEnumerableTypeConverter<User, UserView> {MappingFactory = mappingFactory.Object};
            
            var target = converter.Convert(new[] {new User()}.ToList());
            
            target.ShouldBeNull();
        }
        
        [Fact]
        public void FromSource_Convert_ReturnsList()
        {
            var source = new[] {new User {Id = 1, Name = "Homer"}, new User {Id = 2, Name = "Marge"}};
            var mapper = new Mock<IMapper<User, UserView>>();
            mapper.Setup(m => m.Map(source[0])).Returns(new UserView {Id = 1, Name = "Homer"});
            mapper.Setup(m => m.Map(source[1])).Returns(new UserView {Id = 2, Name = "Marge"});
            var mappingFactory = new Mock<IMappingFactory>();
            mappingFactory.Setup(f => f.Create<User, UserView>()).Returns(mapper.Object);
            var converter = new ListToEnumerableTypeConverter<User, UserView> {MappingFactory = mappingFactory.Object};

            var target = converter.Convert(source.ToList()).ToList();
            
            target[0].Id.ShouldBe(1);
            target[0].Name.ShouldBe("Homer");
            target[1].Id.ShouldBe(2);
            target[1].Name.ShouldBe("Marge");
        }
    }
}
