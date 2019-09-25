using HyperMap.Converters.Enums;
using HyperMap.Integration.Tests.Sources;
using HyperMap.Integration.Tests.Targets;
using HyperMap.Mapping;

namespace HyperMap.Integration.Tests.Maps
{
    public sealed class LoginToLoginViewMap : MapBase<Login, LoginView>
    {
        public LoginToLoginViewMap()
        {
            For(p => p.States).MapTo(p => p.State).Using<EnumToStringTypeConverter<LoginStates>>();
        }
    }
}
