using System;
using System.Collections.Generic;
using System.Linq;
using HyperMap.Mapping;

namespace HyperMap.Discovery
{
    internal class Discover
    {
        private static readonly Type _baseType = typeof(MapBase);

        internal IEnumerable<MapBase> FindFrom<TAssemblyType>()
        {
            return typeof(TAssemblyType).Assembly.GetTypes()
                .Where(t => _baseType.IsAssignableFrom(t))
                .Select(type => (MapBase)Activator.CreateInstance(type));
        }
    }
}
