using HyperMap.Integration.Tests.Converters;
using HyperMap.Integration.Tests.Sources;
using HyperMap.Integration.Tests.Targets;
using HyperMap.Mapping;

namespace HyperMap.Integration.Tests.Maps
{
    public sealed class AddressToSingleLineAddressViewMap : MapBase<Address, SingleLineAddressView>
    {
        public AddressToSingleLineAddressViewMap()
        {
            For(p => p).MapTo(p => p.Display).Using<SingleLineAddressConverter>();
        }
    }
}
