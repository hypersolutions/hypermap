using HyperMap.Converters.Strings;
using HyperMap.IntTests.Sources;
using HyperMap.IntTests.Targets;
using HyperMap.Mapping;

namespace HyperMap.IntTests.Maps
{
    public sealed class AddressToAddressViewMap : MapBase<Address, AddressView>
    {
        public AddressToAddressViewMap()
        {
            For(p => p.HouseNumber).MapTo(p => p.HouseNumber).Using<AnyToStringTypeConverter<int>>();
        }
    }
}
