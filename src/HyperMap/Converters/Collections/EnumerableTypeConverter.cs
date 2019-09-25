using System.Collections.Generic;

namespace HyperMap.Converters.Collections
{
    public sealed class EnumerableTypeConverter<TSource, TTarget> 
        : EnumerableTypeConverterBase<TSource, TTarget>, ITypeConverter<IEnumerable<TSource>, IEnumerable<TTarget>>
        where TSource : class where TTarget : class
    {
        public IMappingFactory MappingFactory { get; set; }
        
        public IEnumerable<TTarget> Convert(IEnumerable<TSource> from)
        {
            return ConvertInternal(from, MappingFactory);
        }
    }
}
