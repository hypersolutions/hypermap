using System.Collections.Generic;

namespace HyperMap
{
    public interface IMappingFactory
    {
        IMapper<TSource, TTarget> Create<TSource, TTarget>() where TSource : class where TTarget : class;
        IEnumerable<object> GetAll();
    }
}
