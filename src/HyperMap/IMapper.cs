using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("HyperMap.Tests")]

namespace HyperMap
{
    public interface IMapper<in TSource, out TTarget> where TSource : class where TTarget : class
    {
        TTarget Map(TSource source);
    }
}
