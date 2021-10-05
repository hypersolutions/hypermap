using HyperMap.IntTests.Converters;
using HyperMap.IntTests.Sources;
using HyperMap.IntTests.Targets;
using HyperMap.Mapping;

namespace HyperMap.IntTests.Maps
{
    public sealed class EmployeeToEmployeeViewMap : MapBase<Employee, EmployeeView>
    {
        public EmployeeToEmployeeViewMap()
        {
            For(p => p.HomeAddress).MapTo(p => p.HomeAddress).Using<SingleLineAddressConverter>();
        }
    }
}
