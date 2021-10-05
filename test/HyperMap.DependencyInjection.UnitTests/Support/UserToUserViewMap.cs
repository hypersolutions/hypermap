using HyperMap.Mapping;
// ReSharper disable UnusedType.Global

namespace HyperMap.DependencyInjection.UnitTests.Support
{
    public class UserToUserViewMap : MapBase<User, UserView>
    {
        public UserToUserViewMap()
        {
            For(p => p.Id).MapTo(p => p.Id).UsingDefault();
            For(p => p.Name).MapTo(p => p.Name).UsingDefault();
        }
    }
}
