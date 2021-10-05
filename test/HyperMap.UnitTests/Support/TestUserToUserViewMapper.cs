// ReSharper disable UnusedParameter.Local
// ReSharper disable UnusedType.Global
namespace HyperMap.UnitTests.Support
{
    public class TestUserToUserViewMapper : IMapper<User, UserView>
    {
        public TestUserToUserViewMapper(IMappingFactory mappingFactory)
        {
            
        }
        
        public UserView Map(User source)
        {
            return null;
        }
    }
}
