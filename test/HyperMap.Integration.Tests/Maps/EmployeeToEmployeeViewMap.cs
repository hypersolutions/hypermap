using HyperMap.Integration.Tests.Converters;
using HyperMap.Integration.Tests.Sources;
using HyperMap.Integration.Tests.Targets;
using HyperMap.Mapping;

namespace HyperMap.Integration.Tests.Maps
{
    public sealed class EmployeeToEmployeeViewMap : MapBase<Employee, EmployeeView>
    {
        public EmployeeToEmployeeViewMap()
        {
            For(p => p.HomeAddress).MapTo(p => p.HomeAddress).Using<SingleLineAddressConverter>();
        }
    }
}
