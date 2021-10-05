using HyperMap.IntTests.Converters;
using HyperMap.IntTests.Sources;
using HyperMap.IntTests.Targets;
using HyperMap.Mapping;

namespace HyperMap.IntTests.Maps
{
    public sealed class AddressToSingleLineAddressViewMap : MapBase<Address, SingleLineAddressView>
    {
        public AddressToSingleLineAddressViewMap()
        {
            For(p => p).MapTo(p => p.Display).Using<SingleLineAddressConverter>();
        }
    }
}
