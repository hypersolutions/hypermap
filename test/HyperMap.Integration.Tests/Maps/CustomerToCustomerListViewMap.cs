using HyperMap.Converters.Collections;
using HyperMap.Integration.Tests.Sources;
using HyperMap.Integration.Tests.Targets;
using HyperMap.Mapping;

namespace HyperMap.Integration.Tests.Maps
{
    public sealed class CustomerToCustomerListViewMap : MapBase<Customer, CustomerListView>
    {
        public CustomerToCustomerListViewMap()
        {
            For(p => p.Orders).MapTo(p => p.Orders).Using<EnumerableToListTypeConverter<Order, OrderView>>();
        }
    }
}
