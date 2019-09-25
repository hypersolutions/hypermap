using System.Collections.Generic;
using System.Linq;

namespace HyperMap.Converters.Collections
{
    public abstract class EnumerableTypeConverterBase<TSource, TTarget> 
        where TSource : class where TTarget : class
    {
        protected IEnumerable<TTarget> ConvertInternal(IEnumerable<TSource> from, IMappingFactory mappingFactory)
        {
            var source = from?.ToList() ?? new List<TSource>(0);
        
            if (!source.Any()) return null;

            var mapper = mappingFactory.Create<TSource, TTarget>();

            return mapper == null ? null : source.Select(item => mapper.Map(item)).ToList();
        }
    }
}
