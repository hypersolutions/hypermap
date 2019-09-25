using System.Collections.Generic;
using System.Linq;

namespace HyperMap.Converters.Collections
{
    public sealed class EnumerableToListTypeConverter<TSource, TTarget> 
        : EnumerableTypeConverterBase<TSource, TTarget>, ITypeConverter<IEnumerable<TSource>, List<TTarget>>
        where TSource : class where TTarget : class
    {
        public IMappingFactory MappingFactory { get; set; }
        
        public List<TTarget> Convert(IEnumerable<TSource> from)
        {
            return ConvertInternal(from, MappingFactory)?.ToList();
        }
    }
}
