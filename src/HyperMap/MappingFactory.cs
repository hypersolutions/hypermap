using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;

namespace HyperMap
{
    public sealed class MappingFactory : IMappingFactory
    {
        private readonly ConcurrentDictionary<string, Lazy<object>> _instances;
        private readonly Assembly _mappingAssembly;

        internal MappingFactory(Assembly mappingAssembly)
        {
            _mappingAssembly = mappingAssembly;
            _instances = new ConcurrentDictionary<string, Lazy<object>>();
        }
        
        public IMapper<TSource, TTarget> Create<TSource, TTarget>() where TSource : class where TTarget : class
        {
            var key = CreateKey<TSource, TTarget>();

            var instance = _instances.GetOrAdd(key, k =>
            {
                var type = _mappingAssembly
                    .GetTypes()
                    .FirstOrDefault(t => typeof(IMapper<TSource, TTarget>).IsAssignableFrom(t));

                return type == null ? null : new Lazy<object>(() => Activator.CreateInstance(type, this));
            });

            return (IMapper<TSource, TTarget>) instance?.Value;
        }

        private static string CreateKey<TSource, TTarget>()
        {
            return $"{typeof(TSource).Name}:{typeof(TTarget).Name}";
        }
    }
}
