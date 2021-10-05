using HyperMap.Converters;
using HyperMap.IntTests.Sources;

namespace HyperMap.IntTests.Converters
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
