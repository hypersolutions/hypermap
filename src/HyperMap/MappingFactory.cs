using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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

            var instance = _instances.GetOrAdd(key, _ =>
            {
                var type = _mappingAssembly
                    .GetTypes()
                    .FirstOrDefault(t => typeof(IMapper<TSource, TTarget>).IsAssignableFrom(t));

                return type == null ? null : new Lazy<object>(() => Activator.CreateInstance(type, this));
            });

            return (IMapper<TSource, TTarget>) instance?.Value;
        }

        public IEnumerable<object> GetAll()
        {
            foreach (var type in _mappingAssembly.GetTypes())
            {
                var interfaceType = GetInterfaceType(type);

                if (interfaceType == null) continue;
                
                var sourceType = interfaceType.GenericTypeArguments[0];
                var targetType = interfaceType.GenericTypeArguments[1];
                var key = CreateKey(sourceType, targetType);
                var instance = _instances.GetOrAdd(key, _ =>
                {
                    return new Lazy<object>(() => Activator.CreateInstance(type, this));
                });
                yield return instance.Value;
            }
        }

        private static Type GetInterfaceType(Type type)
        {
            var interfaceType = type.GetInterfaces().FirstOrDefault();

            return interfaceType == null || 
                   !interfaceType.Name.Contains("IMapper") || 
                   interfaceType.GenericTypeArguments.Length != 2 
                ? null : interfaceType;
        }
        
        private static string CreateKey<TSource, TTarget>()
        {
            return CreateKey(typeof(TSource), typeof(TTarget));
        }
        
        private static string CreateKey(MemberInfo source, MemberInfo target)
        {
            return $"{source.Name}:{target.Name}";
        }
    }
}
