using HyperMap.Converters.Enums;
using HyperMap.IntTests.Sources;
using HyperMap.IntTests.Targets;
using HyperMap.Mapping;

namespace HyperMap.IntTests.Maps
{
    public sealed class LoginToLoginViewMap : MapBase<Login, LoginView>
    {
        public LoginToLoginViewMap()
        {
            For(p => p.States).MapTo(p => p.State).Using<EnumToStringTypeConverter<LoginStates>>();
        }
    }
}
