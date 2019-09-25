using HyperMap.Converters.Collections;
using HyperMap.Integration.Tests.Sources;
using HyperMap.Integration.Tests.Targets;
using HyperMap.Mapping;

namespace HyperMap.Integration.Tests.Maps
{
    public sealed class CustomerToCustomerEnumViewMap : MapBase<Customer, CustomerEnumView>
    {
        public CustomerToCustomerEnumViewMap()
        {
            For(p => p.Orders).MapTo(p => p.Orders).Using<EnumerableTypeConverter<Order, OrderView>>();
        }
    }
}
