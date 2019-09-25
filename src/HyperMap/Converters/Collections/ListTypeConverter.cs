using System.Collections.Generic;
using System.Linq;

namespace HyperMap.Converters.Collections
{
    public sealed class ListTypeConverter<TSource, TTarget> 
        : EnumerableTypeConverterBase<TSource, TTarget>, ITypeConverter<List<TSource>, List<TTarget>>
        where TSource : class where TTarget : class
    {
        public IMappingFactory MappingFactory { get; set; }
        
        public List<TTarget> Convert(List<TSource> from)
        {
            return ConvertInternal(from, MappingFactory)?.ToList();
        }
    }
}
