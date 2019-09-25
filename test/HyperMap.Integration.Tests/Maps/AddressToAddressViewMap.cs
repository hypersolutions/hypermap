using HyperMap.Converters.Strings;
using HyperMap.Integration.Tests.Sources;
using HyperMap.Integration.Tests.Targets;
using HyperMap.Mapping;

namespace HyperMap.Integration.Tests.Maps
{
    public sealed class AddressToAddressViewMap : MapBase<Address, AddressView>
    {
        public AddressToAddressViewMap()
        {
            For(p => p.HouseNumber).MapTo(p => p.HouseNumber).Using<AnyToStringTypeConverter<int>>();
        }
    }
}
