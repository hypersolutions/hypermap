using HyperMap.Converters.Collections;
using HyperMap.IntTests.Sources;
using HyperMap.IntTests.Targets;
using HyperMap.Mapping;

namespace HyperMap.IntTests.Maps
{
    public sealed class CustomerToCustomerListViewMap : MapBase<Customer, CustomerListView>
    {
        public CustomerToCustomerListViewMap()
        {
            For(p => p.Orders).MapTo(p => p.Orders).Using<EnumerableToListTypeConverter<Order, OrderView>>();
        }
    }
}
