// ReSharper disable UnusedParameter.Local
namespace HyperMap.Tests.Support
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
