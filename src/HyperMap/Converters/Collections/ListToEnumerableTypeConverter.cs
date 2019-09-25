using System.Collections.Generic;
using System.Linq;

namespace HyperMap.Converters.Collections
{
    public sealed class ListToEnumerableTypeConverter<TSource, TTarget> 
        : EnumerableTypeConverterBase<TSource, TTarget>, ITypeConverter<List<TSource>, IEnumerable<TTarget>>
        where TSource : class where TTarget : class
    {
        public IMappingFactory MappingFactory { get; set; }
        
        public IEnumerable<TTarget> Convert(List<TSource> from)
        {
            return ConvertInternal(from, MappingFactory)?.ToList();
        }
    }
}
