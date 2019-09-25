using HyperMap.Converters;
using HyperMap.Integration.Tests.Sources;

namespace HyperMap.Integration.Tests.Converters
{
    public class SingleLineAddressConverter : ITypeConverter<Address, string>
    {
        public IMappingFactory MappingFactory { get; set; }

        public string Convert(Address from)
        {
            return $"{from.HouseNumber} {from.Street}, {from.Town}";
        }
    }
}
