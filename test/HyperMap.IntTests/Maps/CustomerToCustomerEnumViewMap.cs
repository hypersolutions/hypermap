using HyperMap.Converters.Collections;
using HyperMap.IntTests.Sources;
using HyperMap.IntTests.Targets;
using HyperMap.Mapping;

namespace HyperMap.IntTests.Maps
{
    public sealed class CustomerToCustomerEnumViewMap : MapBase<Customer, CustomerEnumView>
    {
        public CustomerToCustomerEnumViewMap()
        {
            For(p => p.Orders).MapTo(p => p.Orders).Using<EnumerableTypeConverter<Order, OrderView>>();
        }
    }
}
