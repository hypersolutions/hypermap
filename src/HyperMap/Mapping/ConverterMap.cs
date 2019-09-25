using HyperMap.Converters;

namespace HyperMap.Mapping
{
    public class ConverterMap<TSourceMember, TTargetMember>
    {
        private readonly MapInfo _map;

        internal ConverterMap(MapInfo map)
        {
            _map = map;
        }
        
        public void Using<TConverter>() 
            where TConverter : ITypeConverter<TSourceMember, TTargetMember>
        {
            _map.Converter = typeof(TConverter);
        }
        
        public void UsingDefault()
        {
            _map.Converter = typeof(DefaultTypeConverter<TSourceMember>);
        }
        
        public void UsingFactory<TSource, TTarget>() where TSource : class where TTarget : class
        {
            _map.Converter = typeof(MappingFactoryTypeConverter<TSource, TTarget>);
        }
    }
}
